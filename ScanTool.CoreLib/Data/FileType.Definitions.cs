namespace ScanTool.CoreLib.Data
{

  public partial struct FileType
  {

    #region Archive Definitions

    public static readonly FileType AppleDMG = Create
    (
      name: "Apple Disk Image (DMG)",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "78 01 73 0D 62 62 60" )
      },
      extensions: new [] { ".dmg" }
    );

    public static readonly FileType Bzip2 = Create
    (
      name: "Bzip2 Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "42 5A 68" )
      },
      extensions: new [] { ".bz2" }
    );

    public static readonly FileType GZip = Create
    (
      name: "Gzip Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "1F 8B" )
      },
      extensions: new [] { ".gz", ".tar.gz" }
    );

    public static readonly FileType Iso9660 = Create
    (
      name: "ISO9660 CD/DVD Image",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "43 44 30 30 31" )
      },
      extensions: new [] { ".iso" }
    );

    public static readonly FileType LZ4 = Create
    (
      name: "LZ4 Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "04 22 4D 18" )
      },
      extensions: new [] { ".lz4" }
    );

    public static readonly FileType LZH = Create
    (
      name: "LZH Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "1F A0" )
      },
      extensions: new [] { ".lzh", ".z", ".tar.z" }
    );

    public static readonly FileType Rar = Create
    (
      name: "WinRAR Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "52 61 72 21 1A 07 00", "WinRAR 1.50+" ),
        Signature.Magic( "52 61 72 21 1A 07 01 00", "WinRAR 5.0+" )
      },
      extensions: new [] { ".rar" }
    );

    public static readonly FileType SevenZip = Create
    (
      name: "7-zip Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "37 7A BC AF 27 1C" )
      },
      extensions: new [] { ".7z" }
    );

    public static readonly FileType Tar = Create
    (
      name: "Tarball Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "75 73 74 61 72 00 30 30" ),
        Signature.Magic( "75 73 74 61 72 20 20 00" )
      },
      extensions: new [] { ".tar" }
    );

    public static readonly FileType Zip = Create
    (
      name: "Zip Compressed Archive",
      category: FileCategory.Archive,
      signatures: new[]
      {
        Signature.Magic( "50 4B 03 04" ),
        Signature.Magic( "50 4B 05 06", "Empty Archive" ),
        Signature.Magic( "50 4B 07 08", "Spanned Archive" ),
      },
      extensions: new [] { ".zip" }
    );

    #endregion

    #region Executable Definitions

    public static readonly FileType ELF = Create
    (
      name: "ELF Executable",
      category: FileCategory.Executable,
      signatures: new[]
      {
        Signature.Magic( "7F 45 4C 46" )
      },
      extensions: new [] { ".elf" }
    );

    #endregion

  }

}
