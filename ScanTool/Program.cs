using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanTool.CoreLib.Scanning;
using ScanTool.CoreLib.Scanning.Signatures;
using ScanTool.CoreLib.Scanning.Symbols;
using ScanTool.CoreLib.VFS.Devices;

namespace ScanTool
{
  class Program
  {
    static async Task Main( string[] args )
    {
      foreach( var iso in Directory.GetFiles( "T:\\PS2\\", "*.iso", SearchOption.AllDirectories ) )
        Scan( iso );
    }


    static void Scan( string path )
    {
      using( var iso = new IsoDevice( path ) )
      {
        iso.Initialize();

        bool show = false;
        var sb = new StringBuilder();
        sb.AppendFormat( "ROM: {0}\n", path );
        foreach( var file in iso.EnumerateFiles( true ) )
        {
          var foundMagic = FileMagicScanner.TryResolveFileMagic( file.GetStream(), out var magic );

          if( !foundMagic )
            continue;

          if( magic != FileMagicDefinitions.ELF || !ElfDebugSymbolScanner.HasDebugSymbols( file.GetStream() ) )
            continue;

          sb.AppendFormat( " - {0} has debug symbols!\n", file.FullPath );
          show = true;
        }

        if( !show )
          return;

        Console.WriteLine( sb.ToString() );
      }


    }

  }
}
