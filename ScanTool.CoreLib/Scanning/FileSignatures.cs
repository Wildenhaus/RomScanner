namespace ScanTool.CoreLib.Scanning
{

  public static class FileSignatures
  {

    //=========================================================================
    // Archive Files
    //=========================================================================
    public static readonly FileSignature SevenZip     = new FileSignature( FileType.Archive    , "7-zip"                   , "\x37\x7A\xBC\xAF\x27\x1C"                         );
    public static readonly FileSignature Amiga        = new FileSignature( FileType.Archive    , "AmiBack"                 , "\x42\x41\x43\x4B\x4D\x49\x4B\x45"                 );
    public static readonly FileSignature Bzip2        = new FileSignature( FileType.Archive    , "Bzip2"                   , "\x42\x5A\x68"                                     );
    public static readonly FileSignature Cab          = new FileSignature( FileType.Archive    , "Cab"                     , "\x4D\x53\x43\x46"                                 );
    public static readonly FileSignature Debian       = new FileSignature( FileType.Archive    , "Debian"                  , "\x21\x3C\x61\x72\x63\x68\x3E"                     );
    public static readonly FileSignature Gzip         = new FileSignature( FileType.Archive    , "Gzip"                    , "\x1F\x8B"                                         );
    public static readonly FileSignature IsoImage     = new FileSignature( FileType.Archive    , "ISO Image"               , "\x43\x44\x30\x30\x31"                             );
    public static readonly FileSignature Lz4          = new FileSignature( FileType.Archive    , "LZ4"                     , "\x04\x22\x4D\x18"                                 );
    public static readonly FileSignature Lzfse        = new FileSignature( FileType.Archive    , "LZFSE"                   , "\x62\x76\x78\x32"                                 );
    public static readonly FileSignature Lzh          = new FileSignature( FileType.Archive    , "LZH"                     , "\x1F\xA0"                                         );
    public static readonly FileSignature Lzip         = new FileSignature( FileType.Archive    , "LZip"                    , "\x4C\x5A\x49\x50"                                 );
    public static readonly FileSignature Lzw          = new FileSignature( FileType.Archive    , "LZW"                     , "\x1F\x9D"                                         );
    public static readonly FileSignature MsQuantum    = new FileSignature( FileType.Archive    , "MS Quantum"              , "\x53\x5A\x44\x44\x88\xF0\x27\x33"                 );
    public static readonly FileSignature MsTape       = new FileSignature( FileType.Archive    , "MS Tape"                 , "\x54\x41\x50\x45"                                 );
    public static readonly FileSignature Oar          = new FileSignature( FileType.Archive    , "OAR"                     , "\x4F\x41\x52"                                     );
    public static readonly FileSignature RarV1_5      = new FileSignature( FileType.Archive    , "Rar 1.5+"                , "\x52\x61\x72\x21\x1A\x07\x00"                     );
    public static readonly FileSignature RarV5_0      = new FileSignature( FileType.Archive    , "Rar 5.0+"                , "\x52\x61\x72\x21\x1A\x07\x01\x00"                 );
    public static readonly FileSignature Rnc1         = new FileSignature( FileType.Archive    , "RNC1"                    , "\x52\x4E\x43\x01"                                 );
    public static readonly FileSignature Rnc2         = new FileSignature( FileType.Archive    , "RNC2"                    , "\x52\x4E\x43\x02"                                 );
    public static readonly FileSignature Tar          = new FileSignature( FileType.Archive    , "TAR"                     , "\x75\x73\x74\x61\x72\x00\x30\x30"                 );
    public static readonly FileSignature TarAlt       = new FileSignature( FileType.Archive    , "TAR Alt"                 , "\x75\x73\x74\x61\x72\x20\x20\x00"                 );
    public static readonly FileSignature Vmdk         = new FileSignature( FileType.Archive    , "VM Disk"                 , "\x4B\x44\x4D"                                     );
    public static readonly FileSignature XAR          = new FileSignature( FileType.Archive    , "XAR"                     , "\x78\x61\x72\x21"                                 );
    public static readonly FileSignature XzLzma2      = new FileSignature( FileType.Archive    , "XZ LZMA2"                , "\xFD\x37\x7A\x58\x5A\x00"                         );
    public static readonly FileSignature Zip          = new FileSignature( FileType.Archive    , "Zip"                     , "\x50\x4B\x03\x04"                                 );
    public static readonly FileSignature ZipEmpty     = new FileSignature( FileType.Archive    , "Zip, Empty"              , "\x50\x4B\x05\x06"                                 );
    public static readonly FileSignature ZipSpanned   = new FileSignature( FileType.Archive    , "Zip, Spanned"            , "\x50\x4B\x07\x08"                                 );
    public static readonly FileSignature Zlib_00      = new FileSignature( FileType.Archive    , "Zlib 00"                 , "\x78\x01"                                         );
    public static readonly FileSignature Zlib_01      = new FileSignature( FileType.Archive    , "Zlib 01"                 , "\x78\x5E"                                         );
    public static readonly FileSignature Zlib_02      = new FileSignature( FileType.Archive    , "Zlib 02"                 , "\x78\x9C"                                         );
    public static readonly FileSignature Zlib_03      = new FileSignature( FileType.Archive    , "Zlib 03"                 , "\x78\xDA"                                         );
    public static readonly FileSignature Zlib_04      = new FileSignature( FileType.Archive    , "Zlib 04"                 , "\x78\x20"                                         );
    public static readonly FileSignature Zlib_05      = new FileSignature( FileType.Archive    , "Zlib 05"                 , "\x78\x7D"                                         );
    public static readonly FileSignature Zlib_06      = new FileSignature( FileType.Archive    , "Zlib 06"                 , "\x78\xBB"                                         );
    public static readonly FileSignature Zlib_07      = new FileSignature( FileType.Archive    , "Zlib 07"                 , "\x78\xF9"                                         );
    public static readonly FileSignature Zst          = new FileSignature( FileType.Archive    , "Zst"                     , "\x28\xB5\x2F\xFD"                                 );
    
    //=========================================================================
    // Development Files
    //=========================================================================
    public static readonly FileSignature JavaClass    = new FileSignature( FileType.Development, "Java Class"              , "\xCA\xFE\xBA\xBE"                                 );
    public static readonly FileSignature JavaKeyStore = new FileSignature( FileType.Development, "Java KeyStore"           , "\xFE\xED\xFE\xED"                                 );
    public static readonly FileSignature Pdb          = new FileSignature( FileType.Development, "PDB Symbol File"         , "\x4D\x69\x63\x72\x6F\x73\x6F\x66\x74\x20\x43"     );
    public static readonly FileSignature Pgp          = new FileSignature( FileType.Development, "PGP File"                , "\x85\x01\x0C\x03"                                 );
    public static readonly FileSignature PostScript   = new FileSignature( FileType.Development, "PostScript"              , "\x25\x21\x50\x53"                                 );
    public static readonly FileSignature X509CertDer  = new FileSignature( FileType.Development, "X.509 Cert (DER)"        , "\x30\x82"                                         );

    //=========================================================================
    // Document Files
    //=========================================================================
    public static readonly FileSignature Chm          = new FileSignature( FileType.Document   , "CHM HelpFile"            , "\x49\x54\x53\x46\x03\x00\x00\x00\x60\x00\x00\x00" );
    public static readonly FileSignature MsOffice     = new FileSignature( FileType.Document   , "MS Office"               , "\xD0\xCF\x11\xE0\xA1\xB1\x1A\xE1"                 );
    public static readonly FileSignature Pdf          = new FileSignature( FileType.Document   , "PDF"                     , "\x25\x50\x44\x46\x2D"                             );

    //=========================================================================
    // Executable Files
    //=========================================================================
    public static readonly FileSignature Dalvik       = new FileSignature( FileType.Executable , "Dalvik"                  , "\x64\x65\x78\x0A\x30\x33\x35\x00"                 );
    public static readonly FileSignature DosMZ        = new FileSignature( FileType.Executable , "DOS MZ"                  , "\x4D\x5A"                                         );
    public static readonly FileSignature DosZM        = new FileSignature( FileType.Executable , "DOS ZM"                  , "\x5A\x4D"                                         );
    public static readonly FileSignature ELF          = new FileSignature( FileType.Executable , "ELF"                     , "\x7F\x45\x4C\x46"                                 );
    public static readonly FileSignature NesRom       = new FileSignature( FileType.Executable , "NES Rom"                 , "\x4E\x45\x53\x1A"                                 );
    public static readonly FileSignature Xex1         = new FileSignature( FileType.Executable , "Xbox 360 Executable (v1)", "\x58\x45\x58\x31"                                 );
    public static readonly FileSignature Xex2         = new FileSignature( FileType.Executable , "Xbox 360 Executable (v2)", "\x58\x45\x58\x32"                                 );
    
    //=========================================================================
    // Media Files
    //=========================================================================
    public static readonly FileSignature CanonRaw     = new FileSignature( FileType.Media      , "Canon Raw"               , "\x49\x49\x2A\x00\x10\x00\x00\x00\x43\x52"         );
    public static readonly FileSignature Cineon       = new FileSignature( FileType.Media      , "Cineon"                  , "\x80\x2A\x5F\xD7"                                 );
    public static readonly FileSignature DpxBE        = new FileSignature( FileType.Media      , "DPX-BE"                  , "\x53\x44\x50\x58"                                 );
    public static readonly FileSignature DpxLE        = new FileSignature( FileType.Media      , "DPX-LE"                  , "\x58\x50\x44\x53"                                 );
    public static readonly FileSignature Gif87a       = new FileSignature( FileType.Media      , "GIF-87a"                 , "\x47\x49\x46\x38\x37\x61"                         );
    public static readonly FileSignature Gif89a       = new FileSignature( FileType.Media      , "GIF-89a"                 , "\x47\x49\x46\x38\x39\x61"                         );
    public static readonly FileSignature Photoshop    = new FileSignature( FileType.Media      , "Photoshop"               , "\x38\x42\x50\x53"                                 );
    public static readonly FileSignature TiffBE       = new FileSignature( FileType.Media      , "Tiff-BE"                 , "\x4D\x4D\x00\x2A"                                 );
    public static readonly FileSignature TiffLE       = new FileSignature( FileType.Media      , "Tiff-LE"                 , "\x49\x49\x2A\x00"                                 );
   
    //=========================================================================
    // Other Files
    //=========================================================================
    //public static readonly FileSignature _            = new FileSignature( FileType.Other      , ""                        , ""                                                 );

  }

}
