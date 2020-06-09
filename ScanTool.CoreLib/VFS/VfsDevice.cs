using System;
using System.Collections.Generic;

namespace ScanTool.CoreLib.VFS
{

  public abstract class VfsDevice : IDisposable
  {

    #region Data Members

    private VfsDirectory _root;

    #endregion

    #region Properties

    public bool IsDisposed { get; private set; }
    public bool IsInitialized { get; private set; }

    #endregion

    #region Public Methods

    public void Initialize()
    {
      if( IsInitialized )
        return;

      OnInitializing();
      _root = BuildFileHierarchy();

      IsInitialized = true;
    }

     public IEnumerable<VfsEntry> EnumerateChildren( bool recursive = false )
      => _root.EnumerateChildren( recursive );

    public IEnumerable<VfsDirectory> EnumerateDirectories( bool recursive = false )
      => _root.EnumerateDirectories( recursive );

    public IEnumerable<VfsFile> EnumerateFiles( bool recursive = false )
      => _root.EnumerateFiles( recursive );

    #endregion

    #region Private Methods

    protected abstract VfsDirectory BuildFileHierarchy();

    protected static void ThrowUnresolvedParentException( string entryName )
    {
      throw new Exception( $"Could not resolve parent for entry: '{entryName}'." );
    }

    #endregion

    #region Virtual Methods

    protected virtual void OnDisposing( bool disposing )
    {
    }

    protected virtual void OnInitializing()
    {
    }

    #endregion

    #region IDisposable Methods

    public void Dispose()
    {
      Dispose( true );
      GC.SuppressFinalize( this );
    }

    private void Dispose( bool disposing )
    {
      if( IsDisposed )
        return;

      OnDisposing( disposing );
      IsDisposed = true;
    }

    #endregion

  }

}
