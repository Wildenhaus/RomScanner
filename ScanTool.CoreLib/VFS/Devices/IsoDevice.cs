using System;
using System.IO;
using System.Linq;
using DiscUtils;
using DiscUtils.Iso9660;
using ScanTool.Core.VFS;

namespace ScanTool.CoreLib.VFS.Devices
{

  public class IsoDevice : VfsDevice
  {

    #region Data Members

    private readonly Stream _isoFileStream;
    private readonly CDReader _isoReader;

    #endregion

    #region Constructor

    public IsoDevice( string isoFilePath )
    {
      _isoFileStream = File.OpenRead( isoFilePath );
      _isoReader = new CDReader( _isoFileStream, true );
    }

    public IsoDevice( Stream isoFileStream )
    {
      _isoFileStream = isoFileStream;
      _isoReader = new CDReader( _isoFileStream, true );
    }

    #endregion

    #region Overrides

    protected override VfsDirectory BuildFileHierarchy()
    {
      var root = new IsoDirectory( this );

      var files = _isoReader.Root.GetFiles( "*.*", SearchOption.AllDirectories );
      foreach( var file in files )
      {
        var parent = ResolveParent( file, root );
        new IsoFile( file, this, parent );
      }

      return root;
    }

    protected override void OnDisposing( bool disposing )
    {
      _isoReader.Dispose();
      _isoFileStream.Dispose();
    }

    #endregion

    #region Private Methods

    private VfsEntry ResolveParent( DiscFileInfo file, VfsEntry root )
    {
      var targetEntryName = file.Name;
      var pathParts = file.FullName.Split( Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries );

      VfsEntry currentEntry = root;
      foreach( var part in pathParts )
      {
        if( part == targetEntryName )
          break;

        if( !currentEntry.TryGetChild( part, out var targetEntry ) )
        {
          var targetPath = Path.Combine( currentEntry.FullPath, part );
          var targetDirectory = _isoReader.GetDirectoryInfo( targetPath );

          targetEntry = new IsoDirectory( targetDirectory, this, currentEntry );
        }

        currentEntry = targetEntry;
      }  

      return currentEntry;
    }

    #endregion

    #region Child Classes

    internal class IsoDirectory : VfsDirectory
    {

      #region Data Members

      private readonly DiscDirectoryInfo _directory;

      #endregion

      #region Constructor

      internal IsoDirectory( VfsDevice device )
        : base( $"{Path.DirectorySeparatorChar}", device, null )
      {
      }

      internal IsoDirectory( DiscDirectoryInfo directory, VfsDevice device, VfsEntry parent = null )
        : base( directory.Name, device, parent )
      {
        _directory = directory;
      }

      #endregion

    }

    internal class IsoFile : VfsFile
    {

      #region Data Members

      private readonly DiscFileInfo _file;

      #endregion

      #region Constructor

      internal IsoFile( DiscFileInfo file, VfsDevice device, VfsEntry parent = null )
        : base( SanitizeFileName( file.Name ), device, parent )
      {
        _file = file;
      }

      #endregion

      #region Overrides

      public override Stream GetStream()
        => _file.OpenRead();

      #endregion

      #region Private Methods

      private static string SanitizeFileName( string fileName )
      {
        // DiscUtils is appending ';1' to file names for some reason
        var smcIndex = fileName.IndexOf( ';' );
        if( smcIndex != -1 )
          fileName = fileName.Substring(0, smcIndex );

        return fileName;
      }

      #endregion

    }

    #endregion

  }

}
