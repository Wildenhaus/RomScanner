namespace ScanTool.Core.VFS
{

  public abstract class VfsDirectory : VfsEntry
  {

    #region Properties

    public override VfsEntryType EntryType => VfsEntryType.Directory;

    #endregion

    #region Constructor

    protected VfsDirectory( string path, VfsDevice device, VfsEntry parent = null )
      : base( path, device, parent )
    {
    }

    #endregion

  }

}
