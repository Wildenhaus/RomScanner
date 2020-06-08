using System.Collections.Generic;
using System.IO;

namespace ScanTool.Core.VFS
{

  public abstract class FsDirectory : FsEntry
  {

    #region Public Methods

    public abstract IEnumerable<FsDirectory> GetDirectories();
    public abstract IEnumerable<FsDirectory> GetDirectories( string pattern );
    public abstract IEnumerable<FsDirectory> GetDirectories( string pattern, SearchOption opts );

    public abstract IEnumerable<FsFile> GetFiles();
    public abstract IEnumerable<FsFile> GetFiles( string pattern );
    public abstract IEnumerable<FsFile> GetFiles( string pattern, SearchOption opts );

    #endregion

  }

}
