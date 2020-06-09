namespace ScanTool.CoreLib.Scanning
{

  public readonly struct FileSignature
  {

    #region Data Members

    public readonly FileType Type;
    public readonly string Description;
    public readonly string Signature;

    #endregion

    #region Constructor

    public FileSignature( FileType type, string description, string signature )
    {
      Type = type;
      Description = description;
      Signature = signature;
    }

    #endregion

  }

}
