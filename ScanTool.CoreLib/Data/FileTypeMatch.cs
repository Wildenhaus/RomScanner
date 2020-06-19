namespace ScanTool.CoreLib.Data
{

  public readonly struct FileTypeMatch
  {

    #region Data Members

    public readonly double Confidence;
    public readonly FileType FileType;

    #endregion

    #region Constructor

    public FileTypeMatch( FileType fileType, double confidence )
    {
      Confidence = confidence;
      FileType = fileType;
    }

    #endregion

  }

}
