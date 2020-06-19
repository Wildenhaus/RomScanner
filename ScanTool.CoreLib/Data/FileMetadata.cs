using System;
using System.IO;

namespace ScanTool.CoreLib.Data
{

  public class FileMetadata
  {

    public string FullPath { get; protected set; }
    public string Name { get => Path.GetFileName( FullPath ); }
    public ulong SizeInBytes { get; protected set; }

    public DateTime? DateAccessed { get; protected set; }
    public DateTime? DateCreated { get; protected set; }
    public DateTime? DateModified { get; protected set; }

  }

}
