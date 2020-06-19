namespace ScanTool.CoreLib.Data
{

  public readonly struct SignatureMatch
  {

    #region Data Members

    public readonly Signature Signature;
    public readonly long Offset;

    #endregion

    #region Constructor

    public SignatureMatch( Signature signature, long offset )
    {
      Signature = signature;
      Offset = offset;
    }

    #endregion

  }

}
