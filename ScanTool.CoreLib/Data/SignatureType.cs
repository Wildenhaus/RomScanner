namespace ScanTool.CoreLib.Data
{

  public enum SignatureType : byte
  {

    /// <summary>
    ///   Signature is found at the beginning of the file as magic number.
    /// </summary>
    Magic = 0x00,

    /// <summary>
    ///   Signature is found within the file's body content.
    /// </summary>
    Content  = 0xFF

  }

}
