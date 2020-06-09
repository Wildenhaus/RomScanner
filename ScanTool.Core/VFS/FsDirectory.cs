using System.Collections.Generic;
using System.IO;

namespace ScanTool.Core.VFS
{

  public abstract class FsDirectory : FsEntry
  {

    #region Public Methods

    public abstract IEnumerable<FsDirectory> GetDirectories( string pattern = "*.*", SearchOption opts = SearchOption.TopDirectoryOnly );
    public abstract IEnumerable<FsFile> GetFiles( string pattern = "*.*", SearchOption opts = SearchOption.TopDirectoryOnly );

    #endregion

  }

}
