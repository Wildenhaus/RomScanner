using System;
using System.Threading.Tasks;
using ScanTool.Core.Enumerations;
using ScanTool.Core.Tools;

namespace ScanTool
{
  class Program
  {
    static async Task Main( string[] args )
    {
      var hashes = await HashTool.HashAsync(@"D:\WiiTools\nNASOS.exe", HashType.MD5, HashType.SHA1 );
      foreach( var hash in hashes )
        Console.WriteLine( "{0,-5}: {1}", hash.Type, hash.Value );
    }
  }
}
