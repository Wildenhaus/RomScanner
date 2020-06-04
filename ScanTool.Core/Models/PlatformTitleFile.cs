using System.Collections;
using System.Collections.Generic;

namespace ScanTool.Core.Models
{

  public class PlatformTitleFile
  {

    public string Path { get; }
    public PlatformTitleFile Parent { get; }
    public IList<PlatformTitleFile> Children { get; }

    public Hash[] Hashes { get; }

  }

}
