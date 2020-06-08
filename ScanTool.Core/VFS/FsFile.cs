using System.IO;

namespace ScanTool.Core.VFS
{

  public abstract class FsFile : FsEntry
  {

    #region Properties

    public abstract long SizeInBytes { get; }

    #endregion

    #region Public Methods

    public abstract Stream Open();

    #endregion

  }

}
