using System.IO;

namespace ScanTool.CoreLib.Scanning.Symbols
{

  public static class ElfDebugSymbolScanner
  {

    public static bool HasDebugSymbols( Stream stream )
    {
      stream.Seek( 0, SeekOrigin.Begin );

      using( var reader = new BigEndianBinaryReader( stream ) )
      {
        // Read e_shoff
        stream.Seek( 0x20, SeekOrigin.Begin );
        var shoff = reader.ReadUInt16();

        // Read e_shentsize and e_shnum
        stream.Seek( 0x2E, SeekOrigin.Begin );
        var shentsize = reader.ReadUInt16();
        var shnum = reader.ReadUInt16();

        for( var i = 0; i < shnum; i++ )
        {
          // Calculate section header i offset
          var sectionOffset = (shoff + (i * shentsize));

          // Seek to section header i, plus the offset of sh_type (4)
          stream.Seek( sectionOffset + 4, SeekOrigin.Begin );

          // Read sh_type
          var shtype = reader.ReadUInt32(); // SecHdr + 0x4

          if( shtype == 2 ) // SHT_SYMTAB
            return true;
        }

      }

      // ELF does not have debug symbols
      return false;
    }

  }

}
