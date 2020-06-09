using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScanTool.CoreLib.Extensions;

namespace ScanTool.Core.VFS
{

  public abstract class VfsEntry
  {

    #region Data Members

    protected readonly string _name;
    protected readonly string _path;

    protected readonly VfsDevice _device;
    protected readonly VfsEntry _parent;
    protected readonly Dictionary<string, VfsEntry> _children;

    #endregion

    #region Properties

    
    public string Name => _name;
    public string FullPath => _path;
    public abstract VfsEntryType EntryType { get; }

    public VfsDevice Device => _device;
    public VfsEntry Parent => _parent;
    public IEnumerable<VfsEntry> Children => _children.Values;

    public FileAttributes Attributes { get; protected set; }
    public DateTime? CreationTime { get; protected set; }
    public DateTime? LastAccessTime { get; protected set; }
    public DateTime? LastModifiedTime { get; protected set; }

    #endregion

    #region Constructor

    protected VfsEntry( string name, VfsDevice device, VfsEntry parent = null )
    {
      _device = device;
      _parent = parent;
      _children = new Dictionary<string, VfsEntry>();

      _name = name;

      if( parent != null )
      {
        _path = System.IO.Path.Combine( parent._path, _name );
        parent.AddChild( this );
      }
      else
        _path = _name;
    }

    #endregion

    #region Public Methods

    public IEnumerable<VfsEntry> EnumerateChildren( bool recursive = false )
    {
      var results = Children;

      if( recursive )
        results = results.Concat( Children.SelectManyRecursive( x => x.Children ) );

      return results;
    }

    public IEnumerable<VfsDirectory> EnumerateDirectories( bool recursive = false )
      => EnumerateChildren( recursive ).Where( x => x is VfsDirectory ).Cast<VfsDirectory>();

    public IEnumerable<VfsFile> EnumerateFiles( bool recursive = false )
      => EnumerateChildren( recursive ).Where( x => x is VfsFile ).Cast<VfsFile>();

    public bool TryGetChild( string name, out VfsEntry child )
      => _children.TryGetValue( name, out child );

    #endregion

    #region Internal Methods

    internal void AddChild( VfsEntry childEntry )
      => _children.TryAdd( childEntry.Name, childEntry );

    #endregion

    #region Overrides

    public override bool Equals( object obj )
      => obj is VfsEntry other && FullPath == other.FullPath;

    public override int GetHashCode()
      => FullPath.GetHashCode();

    #endregion

  }

}
