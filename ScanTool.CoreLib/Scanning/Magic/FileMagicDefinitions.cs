namespace ScanTool.CoreLib.Scanning
{

  public readonly partial struct FileMagic
  {

    //=========================================================================
    // Archive Files
    //=========================================================================
    public static readonly FileMagic SevenZip     = new FileMagic( FileType.Archive    , "7-zip"                   , "\x37\x7A\xBC\xAF\x27\x1C"                         );
    public static readonly FileMagic Amiga        = new FileMagic( FileType.Archive    , "AmiBack"                 , "\x42\x41\x43\x4B\x4D\x49\x4B\x45"                 );
    public static readonly FileMagic Bzip2        = new FileMagic( FileType.Archive    , "Bzip2"                   , "\x42\x5A\x68"                                     );
    public static readonly FileMagic Cab          = new FileMagic( FileType.Archive    , "Cab"                     , "\x4D\x53\x43\x46"                                 );
    public static readonly FileMagic Debian       = new FileMagic( FileType.Archive    , "Debian"                  , "\x21\x3C\x61\x72\x63\x68\x3E"                     );
    public static readonly FileMagic Gzip         = new FileMagic( FileType.Archive    , "Gzip"                    , "\x1F\x8B"                                         );
    public static readonly FileMagic IsoImage     = new FileMagic( FileType.Archive    , "ISO Image"               , "\x43\x44\x30\x30\x31"                             );
    public static readonly FileMagic Lz4          = new FileMagic( FileType.Archive    , "LZ4"                     , "\x04\x22\x4D\x18"                                 );
    public static readonly FileMagic Lzfse        = new FileMagic( FileType.Archive    , "LZFSE"                   , "\x62\x76\x78\x32"                                 );
    public static readonly FileMagic Lzh          = new FileMagic( FileType.Archive    , "LZH"                     , "\x1F\xA0"                                         );
    public static readonly FileMagic Lzip         = new FileMagic( FileType.Archive    , "LZip"                    , "\x4C\x5A\x49\x50"                                 );
    public static readonly FileMagic Lzw          = new FileMagic( FileType.Archive    , "LZW"                     , "\x1F\x9D"                                         );
    public static readonly FileMagic MsQuantum    = new FileMagic( FileType.Archive    , "MS Quantum"              , "\x53\x5A\x44\x44\x88\xF0\x27\x33"                 );
    public static readonly FileMagic MsTape       = new FileMagic( FileType.Archive    , "MS Tape"                 , "\x54\x41\x50\x45"                                 );
    public static readonly FileMagic Oar          = new FileMagic( FileType.Archive    , "OAR"                     , "\x4F\x41\x52"                                     );
    public static readonly FileMagic RarV1_5      = new FileMagic( FileType.Archive    , "Rar 1.5+"                , "\x52\x61\x72\x21\x1A\x07\x00"                     );
    public static readonly FileMagic RarV5_0      = new FileMagic( FileType.Archive    , "Rar 5.0+"                , "\x52\x61\x72\x21\x1A\x07\x01\x00"                 );
    public static readonly FileMagic Rnc1         = new FileMagic( FileType.Archive    , "RNC1"                    , "\x52\x4E\x43\x01"                                 );
    public static readonly FileMagic Rnc2         = new FileMagic( FileType.Archive    , "RNC2"                    , "\x52\x4E\x43\x02"                                 );
    public static readonly FileMagic Tar          = new FileMagic( FileType.Archive    , "TAR"                     , "\x75\x73\x74\x61\x72\x00\x30\x30"                 );
    public static readonly FileMagic TarAlt       = new FileMagic( FileType.Archive    , "TAR Alt"                 , "\x75\x73\x74\x61\x72\x20\x20\x00"                 );
    public static readonly FileMagic Vmdk         = new FileMagic( FileType.Archive    , "VM Disk"                 , "\x4B\x44\x4D"                                     );
    public static readonly FileMagic XAR          = new FileMagic( FileType.Archive    , "XAR"                     , "\x78\x61\x72\x21"                                 );
    public static readonly FileMagic XzLzma2      = new FileMagic( FileType.Archive    , "XZ LZMA2"                , "\xFD\x37\x7A\x58\x5A\x00"                         );
    public static readonly FileMagic Zip          = new FileMagic( FileType.Archive    , "Zip"                     , "\x50\x4B\x03\x04"                                 );
    public static readonly FileMagic ZipEmpty     = new FileMagic( FileType.Archive    , "Zip, Empty"              , "\x50\x4B\x05\x06"                                 );
    public static readonly FileMagic ZipSpanned   = new FileMagic( FileType.Archive    , "Zip, Spanned"            , "\x50\x4B\x07\x08"                                 );
    public static readonly FileMagic Zlib_00      = new FileMagic( FileType.Archive    , "Zlib 00"                 , "\x78\x01"                                         );
    public static readonly FileMagic Zlib_01      = new FileMagic( FileType.Archive    , "Zlib 01"                 , "\x78\x5E"                                         );
    public static readonly FileMagic Zlib_02      = new FileMagic( FileType.Archive    , "Zlib 02"                 , "\x78\x9C"                                         );
    public static readonly FileMagic Zlib_03      = new FileMagic( FileType.Archive    , "Zlib 03"                 , "\x78\xDA"                                         );
    public static readonly FileMagic Zlib_04      = new FileMagic( FileType.Archive    , "Zlib 04"                 , "\x78\x20"                                         );
    public static readonly FileMagic Zlib_05      = new FileMagic( FileType.Archive    , "Zlib 05"                 , "\x78\x7D"                                         );
    public static readonly FileMagic Zlib_06      = new FileMagic( FileType.Archive    , "Zlib 06"                 , "\x78\xBB"                                         );
    public static readonly FileMagic Zlib_07      = new FileMagic( FileType.Archive    , "Zlib 07"                 , "\x78\xF9"                                         );
    public static readonly FileMagic Zst          = new FileMagic( FileType.Archive    , "Zst"                     , "\x28\xB5\x2F\xFD"                                 );
    
    //=========================================================================
    // Development Files
    //=========================================================================
    public static readonly FileMagic JavaClass    = new FileMagic( FileType.Development, "Java Class"              , "\xCA\xFE\xBA\xBE"                                 );
    public static readonly FileMagic JavaKeyStore = new FileMagic( FileType.Development, "Java KeyStore"           , "\xFE\xED\xFE\xED"                                 );
    public static readonly FileMagic Pdb          = new FileMagic( FileType.Development, "PDB Symbol File"         , "\x4D\x69\x63\x72\x6F\x73\x6F\x66\x74\x20\x43"     );
    public static readonly FileMagic Pgp          = new FileMagic( FileType.Development, "PGP File"                , "\x85\x01\x0C\x03"                                 );
    public static readonly FileMagic PostScript   = new FileMagic( FileType.Development, "PostScript"              , "\x25\x21\x50\x53"                                 );
    public static readonly FileMagic X509CertDer  = new FileMagic( FileType.Development, "X.509 Cert (DER)"        , "\x30\x82"                                         );

    //=========================================================================
    // Document Files
    //=========================================================================
    public static readonly FileMagic Chm          = new FileMagic( FileType.Document   , "CHM HelpFile"            , "\x49\x54\x53\x46\x03\x00\x00\x00\x60\x00\x00\x00" );
    public static readonly FileMagic MsOffice     = new FileMagic( FileType.Document   , "MS Office"               , "\xD0\xCF\x11\xE0\xA1\xB1\x1A\xE1"                 );
    public static readonly FileMagic Pdf          = new FileMagic( FileType.Document   , "PDF"                     , "\x25\x50\x44\x46\x2D"                             );

    //=========================================================================
    // Executable Files
    //=========================================================================
    public static readonly FileMagic Dalvik       = new FileMagic( FileType.Executable , "Dalvik"                  , "\x64\x65\x78\x0A\x30\x33\x35\x00"                 );
    public static readonly FileMagic DosMZ        = new FileMagic( FileType.Executable , "DOS MZ"                  , "\x4D\x5A"                                         );
    public static readonly FileMagic DosZM        = new FileMagic( FileType.Executable , "DOS ZM"                  , "\x5A\x4D"                                         );
    public static readonly FileMagic ELF          = new FileMagic( FileType.Executable , "ELF"                     , "\x7F\x45\x4C\x46"                                 );
    public static readonly FileMagic NesRom       = new FileMagic( FileType.Executable , "NES Rom"                 , "\x4E\x45\x53\x1A"                                 );
    public static readonly FileMagic Xex1         = new FileMagic( FileType.Executable , "Xbox 360 Executable (v1)", "\x58\x45\x58\x31"                                 );
    public static readonly FileMagic Xex2         = new FileMagic( FileType.Executable , "Xbox 360 Executable (v2)", "\x58\x45\x58\x32"                                 );
    
    //=========================================================================
    // Media Files
    //=========================================================================
    public static readonly FileMagic CanonRaw     = new FileMagic( FileType.Media      , "Canon Raw"               , "\x49\x49\x2A\x00\x10\x00\x00\x00\x43\x52"         );
    public static readonly FileMagic Cineon       = new FileMagic( FileType.Media      , "Cineon"                  , "\x80\x2A\x5F\xD7"                                 );
    public static readonly FileMagic DpxBE        = new FileMagic( FileType.Media      , "DPX-BE"                  , "\x53\x44\x50\x58"                                 );
    public static readonly FileMagic DpxLE        = new FileMagic( FileType.Media      , "DPX-LE"                  , "\x58\x50\x44\x53"                                 );
    public static readonly FileMagic Gif87a       = new FileMagic( FileType.Media      , "GIF-87a"                 , "\x47\x49\x46\x38\x37\x61"                         );
    public static readonly FileMagic Gif89a       = new FileMagic( FileType.Media      , "GIF-89a"                 , "\x47\x49\x46\x38\x39\x61"                         );
    public static readonly FileMagic Photoshop    = new FileMagic( FileType.Media      , "Photoshop"               , "\x38\x42\x50\x53"                                 );
    public static readonly FileMagic TiffBE       = new FileMagic( FileType.Media      , "Tiff-BE"                 , "\x4D\x4D\x00\x2A"                                 );
    public static readonly FileMagic TiffLE       = new FileMagic( FileType.Media      , "Tiff-LE"                 , "\x49\x49\x2A\x00"                                 );

  }

}
