using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ScanTool.Core.VFS;

namespace ScanTool.CoreLib.VFS.Devices
{

  public class ZipDevice : VfsDevice
  {

    #region Data Members

    private readonly Stream _zipFileStream;
    private readonly ZipArchive _zipArchive;

    #endregion

    #region Constructor

    public ZipDevice( string zipFilePath )
      : this( File.OpenRead( zipFilePath ) )
    {
    }

    public ZipDevice( Stream zipFileStream )
    {
      _zipFileStream = zipFileStream;
      _zipArchive = new ZipArchive( zipFileStream, ZipArchiveMode.Read );
    }

    #endregion

    #region Overrides

    protected override void OnDisposing( bool disposing )
    {
      _zipArchive?.Dispose();
      _zipFileStream?.Dispose();
    }

    protected override VfsDirectory BuildFileHierarchy()
    {
      var root = new ZipDirectory( this );

      foreach( var entry in _zipArchive.Entries )
      {
        var parent = ResolveParent( entry, root );
        switch( entry.GetEntryType() )
        {
          case VfsEntryType.Directory:
            new ZipDirectory( entry, this, parent );
            break;
          case VfsEntryType.File:
            new ZipFile( entry, this, parent );
            break;
        }
      }

      return root;
    }

    #endregion

    #region Private Methods

    private VfsEntry ResolveParent( ZipArchiveEntry entry, VfsEntry root )
    {
      // TODO: This is icky
      var entryPath = entry.GetFixedPath();
      var targetEntryName = entry.GetEntryName();
      var pathParts = entryPath.Split( Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries );


      VfsEntry currentEntry = root;
      foreach( var part in pathParts )
      {
        if( part == targetEntryName )
          break;

        if( !currentEntry.TryGetChild( part, out var targetEntry ) )
          ThrowUnresolvedParentException( entry.FullName );

        currentEntry = targetEntry;
      }

      return currentEntry;
    }

    

    #endregion

    #region Child Classes

    internal class ZipDirectory : VfsDirectory
    {

      #region Constructor

      internal ZipDirectory( ZipDevice device )
        : base( $"{Path.DirectorySeparatorChar}", device )
      {
      }

      internal ZipDirectory( ZipArchiveEntry entry, ZipDevice device, VfsEntry parent = null )
        : base( entry.GetEntryName(), device, parent )
      {
        Attributes = FileAttributes.Directory;
      }

      #endregion

    }

    internal class ZipFile : VfsFile
    {

      #region Data Members

      private readonly ZipArchiveEntry _entry;

      #endregion

      #region Constructor

      internal ZipFile( ZipArchiveEntry entry, ZipDevice device, VfsEntry parent = null )
        : base( entry.GetEntryName(), device, parent )
      {
        _entry = entry;
        Size = entry.Length;
      }

      #endregion

      #region Overrides

      public override Stream GetStream()
        => _entry.Open();

      #endregion

    }

    #endregion

  }

  public static class ZipArchiveExtensions
  {

    public static string GetEntryName( this ZipArchiveEntry entry )
    {
      return entry.GetFixedPath()
        .Split( Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries )
        .Last();
    }

    public static string GetFixedPath( this ZipArchiveEntry entry )
    {
      // ZipArchiveEntry::Name property is FUCKING BROKEN >:|
      return entry.FullName.Replace( Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar );
    }

    public static VfsEntryType GetEntryType( this ZipArchiveEntry entry )
    {
      const int FLAG_DIRECTORY = 0x0000010;
      return (entry.ExternalAttributes &= FLAG_DIRECTORY) != 0
        ? VfsEntryType.Directory
        : VfsEntryType.File;
    }

    public static bool IsDirectory( this ZipArchiveEntry entry )
      => entry.GetEntryType() == VfsEntryType.Directory;

    public static bool IsFile( this ZipArchiveEntry entry )
      => entry.GetEntryType() == VfsEntryType.File;

  }

}
