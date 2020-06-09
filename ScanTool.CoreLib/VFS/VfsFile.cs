using System.IO;

namespace ScanTool.Core.VFS
{

  public abstract class VfsFile : VfsEntry
  {

    #region Properties

    public override VfsEntryType EntryType => VfsEntryType.File;

    public long Size { get; protected set; }

    #endregion

    #region Constructor

    protected VfsFile( string name, VfsDevice device, VfsEntry parent = null )
      : base( name, device, parent )
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    ///   Gets the file's stream for reading.
    /// </summary>
    /// <returns></returns>
    public abstract Stream GetStream();

    #endregion

  }

}
