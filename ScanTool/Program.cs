using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ScanTool.CoreLib.VFS.Devices;

namespace ScanTool
{

  class Program
  {

    static async Task Main( string[] args )
    {
      Console.Title = "ScanTool";
      if( args.Length < 1 )
      {
        Console.WriteLine( "Usage: ScanTool [ISO Path]" );
        return;
      }

      var path = args[0];
      if( !File.Exists( path ) )
      {
        Console.WriteLine( "File not found." );
        return;
      }

      Scan( path );
    }

    static void Scan( string path )
    {
      Console.WriteLine( path );

      var isoDevice = new Iso9960Device( path );

      try
      {
        isoDevice.Initialize();
      }
      catch( Exception ex )
      {
        Console.WriteLine( "Failed to read ISO: {0}", ex.Message );
        return;
      }

      var stopwatch = Stopwatch.StartNew();
      new Scanner( isoDevice ).Scan();
      stopwatch.Stop();

      Console.WriteLine( "Finished in {0}.", stopwatch.Elapsed );
    }


  }

}
