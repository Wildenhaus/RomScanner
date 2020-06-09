namespace ScanTool.CoreLib.Scanning
{

  public readonly struct FileMagic
  {

    #region Data Members

    public readonly FileType Type;
    public readonly string Description;
    public readonly string Magic;

    #endregion

    #region Constructor

    public FileMagic( FileType type, string description, string signature )
    {
      Type = type;
      Description = description;
      Magic = signature;
    }

    #endregion

  }

}
