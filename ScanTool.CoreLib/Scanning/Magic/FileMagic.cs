using System;

namespace ScanTool.CoreLib.Scanning
{

  public readonly partial struct FileMagic : IEquatable<FileMagic>
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

    #region Operators

    public static bool operator==( FileMagic left, FileMagic right )
      => left.Equals( right );

    public static bool operator!=( FileMagic left, FileMagic right )
      => !left.Equals( right );

    #endregion

    #region IEquatable Methods

    public bool Equals( FileMagic other )
      => Type == other.Type 
      && Description == other.Description;

    #endregion

  }

}
