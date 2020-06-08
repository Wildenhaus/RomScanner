namespace ScanTool.Core.VFS
{

  public abstract class FsEntry
  {

    public abstract FsDevice Device { get; }
    public abstract string Path { get; }
    public abstract string Name { get; }

  }

}
