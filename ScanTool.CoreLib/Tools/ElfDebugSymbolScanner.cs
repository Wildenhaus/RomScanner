using System.IO;

namespace ScanTool.CoreLib.Scanning.Symbols
{

  public static class ElfDebugSymbolScanner
  {

    public static uint GetDebugSymbolCount( Stream stream )
    {
      stream.Seek( 0, SeekOrigin.Begin );

      using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
      {
        // Read register size
        stream.Seek( 0x04, SeekOrigin.Begin );
        var is64Bit = reader.ReadByte() == 2;

        // Read file endian-ness
        stream.Seek( 0x05, SeekOrigin.Begin );
        var endianness = reader.ReadByte();
        if( endianness == 2 )
          reader.Endianness = Endianness.BigEndian;

        // Read e_shoff
        stream.Seek( is64Bit ? 0x28 : 0x20, SeekOrigin.Begin );
        var shoff = is64Bit ? reader.ReadUInt64() : reader.ReadUInt32();

        // Read e_shentsize and e_shnum
        stream.Seek( is64Bit ? 0x3A : 0x2E, SeekOrigin.Begin );
        var shentsize = reader.ReadUInt16();
        var shnum = reader.ReadUInt16();

        uint symbolCount = 0;
        for( var i = 0U; i < shnum; i++ )
        {
          // Calculate section header i offset
          var sectionOffset = (shoff + (i * shentsize));

          // Seek to section header i, plus the offset of sh_type (4)
          stream.Seek( ( long ) ( sectionOffset + 4U ), SeekOrigin.Begin );

          // Read sh_type
          var shtype = reader.ReadUInt32(); // SecHdr + 0x4

          if( shtype == 2 ) // SHT_SYMTAB
          {
            // Seek to section header i, plus the offset of sh_size (0x18)
            stream.Seek( ( long ) ( sectionOffset + ( is64Bit ? 0x20U : 0x14U ) ), SeekOrigin.Begin );

            // Read sh_size
            var shsize = is64Bit ? reader.ReadUInt64() : reader.ReadUInt32();

            // .symtab size divided by sizeof(Elf32_Sym)
            symbolCount += ( uint ) ( shsize / ( is64Bit ? 0x18U : 0x10U ) );
          }
        }

        return symbolCount;
      }

    }

  }

}
