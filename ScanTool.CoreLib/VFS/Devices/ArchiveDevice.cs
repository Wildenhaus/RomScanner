using System;
using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.GZip;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Tar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Common.GZip;
using SharpCompress.Common.Rar;
using SharpCompress.Common.SevenZip;
using SharpCompress.Common.Tar;
using SharpCompress.Common.Zip;

namespace ScanTool.CoreLib.VFS.Devices
{

  public abstract class ArchiveDevice<TEntry, TVolume> : VfsDevice
    where TEntry : IArchiveEntry
    where TVolume : IVolume
  {

    #region Data Members

    private readonly Stream _archiveStream;
    private readonly AbstractArchive<TEntry,TVolume> _archive;

    #endregion

    #region Constructor

    protected ArchiveDevice( Stream archiveStream )
    {
      _archiveStream = archiveStream;
      _archive = OpenArchive( archiveStream );
    }

    #endregion

    #region Overrides

    protected override VfsDirectory BuildFileHierarchy()
    {
      _archiveStream.Seek( 0, SeekOrigin.Begin );
      var root = new ArchiveDirectory( this );

      foreach( var entry in _archive.Entries )
      {
        if( entry.IsDirectory )
          continue;

        if( entry == null || entry.Key == null )
          continue;

        var parent = ResolveParent( entry, root );
        new ArchiveFile( entry, this, parent );
      }

      return root;
    }

    #endregion

    #region Private Methods

    protected abstract AbstractArchive<TEntry, TVolume> OpenArchive( Stream stream );

    private VfsEntry ResolveParent( TEntry entry, VfsEntry root )
    {
      var path = SanitizePath( entry.Key );
      var targetEntryName = GetEntryName( path );
      var pathParts = path.Split( Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries );

      VfsEntry currentEntry = root;
      foreach( var part in pathParts )
      {
        if( part == targetEntryName )
          break;

        if( !currentEntry.TryGetChild( part, out var targetEntry ) )
          targetEntry = new ArchiveDirectory( part, this, currentEntry );

        currentEntry = targetEntry;
      }

      return currentEntry;
    }

    private static string GetEntryName( string path )
      => SanitizePath( path ).Split( Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries ).Last();

    private static string SanitizePath( string path )
      => path.Replace( Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar );

    #endregion

    #region Child Classes

    internal class ArchiveDirectory : VfsDirectory
    {

      #region Constructor

      internal ArchiveDirectory( VfsDevice device )
        : base( $"{Path.DirectorySeparatorChar}", device, null )
      {
      }

      internal ArchiveDirectory( string path, VfsDevice device, VfsEntry parent = null )
        : base( GetEntryName( path ), device, parent )
      {
      }

      #endregion

    }

    internal class ArchiveFile : VfsFile
    {

      #region Data Members

      private readonly TEntry _entry;

      #endregion

      #region Constructor

      public ArchiveFile( TEntry entry, VfsDevice device, VfsEntry parent = null )
        : base( GetEntryName( entry.Key ), device, parent )
      {
        _entry = entry;
        Size = entry.Size;
      }

      #endregion

      #region Overrides

      public override Stream GetStream()
        => new CachedReadStream( _entry.OpenEntryStream(), _entry.Size );

      #endregion

    }

    #endregion

  }

  public sealed class GZipArchiveDevice : ArchiveDevice<GZipArchiveEntry, GZipVolume>
  {

    public GZipArchiveDevice( Stream archiveStream )
      : base( archiveStream )
    {
    }

    protected override AbstractArchive<GZipArchiveEntry, GZipVolume> OpenArchive( Stream stream )
      => GZipArchive.Open( stream );

  }

  public sealed class TarArchiveDevice : ArchiveDevice<TarArchiveEntry, TarVolume>
  {

    public TarArchiveDevice( Stream archiveStream )
      : base( archiveStream )
    {
    }

    protected override AbstractArchive<TarArchiveEntry, TarVolume> OpenArchive( Stream stream )
      => TarArchive.Open( stream );

  }

  public sealed class RarArchiveDevice : ArchiveDevice<RarArchiveEntry, RarVolume>
  {

    public RarArchiveDevice( Stream archiveStream )
      : base( archiveStream )
    {
    }

    protected override AbstractArchive<RarArchiveEntry, RarVolume> OpenArchive( Stream stream )
      => RarArchive.Open( stream );

  }

  public sealed class SevenZipArchiveDevice : ArchiveDevice<SevenZipArchiveEntry, SevenZipVolume>
  {

    public SevenZipArchiveDevice( Stream archiveStream )
      : base( archiveStream )
    {
    }

    protected override AbstractArchive<SevenZipArchiveEntry, SevenZipVolume> OpenArchive( Stream stream )
      => SevenZipArchive.Open( stream );

  }

  public sealed class ZipArchiveDevice : ArchiveDevice<ZipArchiveEntry, ZipVolume>
  {

    public ZipArchiveDevice( Stream archiveStream )
      : base( archiveStream )
    {
    }

    protected override AbstractArchive<ZipArchiveEntry, ZipVolume> OpenArchive( Stream stream )
      => ZipArchive.Open( stream );

  }

}
