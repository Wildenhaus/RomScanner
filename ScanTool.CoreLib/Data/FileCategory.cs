namespace ScanTool.CoreLib.Data
{

  public enum FileCategory : byte
  {

    /// <summary>
    ///   Unknown category.
    /// </summary>
    Unknown = 0x00,

    /// <summary>
    ///   A category for compressed archive files.
    /// </summary>
    Archive,

    /// <summary>
    ///   A category for developer files, such as code, debug symbols,
    ///   linker maps, etc.
    /// </summary>
    Developer,

    /// <summary>
    ///   A category for documents such as PDFs, MS Office documents,
    ///   spreadsheets, etc.
    /// </summary>
    Document,

    /// <summary>
    ///   A category for executable files, such as ELF, Windows exe, etc.
    /// </summary>
    Executable,

    /// <summary>
    ///   A category for media files, such as audio and video.
    /// </summary>
    Media,

  }

}
