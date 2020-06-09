using System.Collections.Generic;
using System.IO;
using DiscUtils;
using DiscUtils.Iso9660;

namespace ScanTool.Core.VFS.Devices
{

  public class IsoDevice : FsDevice
  {

    #region Data Members

    private readonly string _isoFilePath;
    private readonly Stream _isoFileStream;
    private readonly CDReader _isoReader;

    #endregion

    #region Constructor

    public IsoDevice( string isoFilePath )
    {
      _isoFilePath = isoFilePath;
      _isoFileStream = File.OpenRead( isoFilePath );
      _isoReader = new CDReader( _isoFileStream, true );
    }

    public IsoDevice( Stream isoFileStream )
    {
      _isoFileStream = isoFileStream;
      _isoReader = new CDReader( _isoFileStream, true );
    }

    #endregion

    #region Public Methods

    public override FsDirectory GetRootDirectory()
    {
      return new IsoDirectory( this, _isoReader.Root );
    }

    public override IEnumerable<FsFile> GetFiles( string pattern, SearchOption opts )
    {
      foreach( var file in _isoReader.Root.GetFiles( pattern, opts ) )
        yield return new IsoFile( this, file );
    }

    #endregion

    #region Overrides

    protected override void OnDisposing( bool disposing )
    {
      _isoReader.Dispose();
      _isoFileStream.Dispose();
    }

    #endregion

    #region IsoDirectory Class

    internal class IsoDirectory : FsDirectory
    {

      #region Data Members

      private readonly IsoDevice _device;
      private readonly DiscDirectoryInfo _directory;

      #endregion

      #region Properties

      public override FsDevice Device { get => _device; }
      public override string Path { get => _directory.FullName; }
      public override string Name { get => _directory.Name; }

      #endregion

      #region Constructor

      internal IsoDirectory( IsoDevice device, DiscDirectoryInfo directory )
      {
        _device = device;
        _directory = directory;
      }

      #endregion

      #region Public Methods

      public override IEnumerable<FsDirectory> GetDirectories( string pattern, SearchOption opts )
      {
        foreach( var childDirectory in _directory.GetDirectories( pattern, opts ) )
          yield return new IsoDirectory( _device, childDirectory );
      }

      public override IEnumerable<FsFile> GetFiles( string pattern, SearchOption opts )
      {
        foreach( var file in _directory.GetFiles( pattern, opts ) )
          yield return new IsoFile( _device, file );
      }

      #endregion

    }

    #endregion

    #region IsoFile Class

    internal class IsoFile : FsFile
    {

      #region Data Members

      private readonly IsoDevice _device;
      private readonly DiscFileInfo _file;

      #endregion

      #region Properties

      public override FsDevice Device => _device;
      public override string Name => _file.Name;
      public override string Path => _file.FullName;
      public override long SizeInBytes => _file.Length;

      #endregion

      #region Constructor

      internal IsoFile( IsoDevice device, DiscFileInfo file )
      {
        _device = device;
        _file = file;
      }

      #endregion

      #region Public Methods

      public override Stream Open()
      {
        throw new System.NotImplementedException();
      }

      #endregion

    }

    #endregion

  }

}
