using System;
using System.Collections.Generic;
using System.IO;

namespace ScanTool.Core.VFS
{

  public abstract class FsDevice : IDisposable
  {

    #region Properties

    public bool IsDisposed { get; private set; }

    public bool IsInitialized { get; private set; }

    #endregion

    #region Constructor

    ~FsDevice()
    {
      Dispose(false);
    }

    #endregion

    #region Public Methods

    public void Initialize()
    {
      if( IsInitialized )
        return;

      OnInitializing();
      IsInitialized = true;
    }

    public abstract FsDirectory GetRootDirectory();

    public abstract IEnumerable<FsFile> GetFiles();
    public abstract IEnumerable<FsFile> GetFiles( string pattern );
    public abstract IEnumerable<FsFile> GetFiles( string pattern, SearchOption opts );

    #endregion

    #region Virtual Methods

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

    protected virtual void OnDisposing( bool disposing )
    {
    }

    #endregion

  }

}
