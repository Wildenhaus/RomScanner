using System.Diagnostics;

namespace ScanTool.Core.Models
{

  [DebuggerDisplay("[Platform: {Name}]")]
  public abstract class Platform : UniqueObject<Platform>
  {

    #region Properties

    public abstract string Name { get; }
    public abstract string ShortName { get; }

    #endregion

    #region Overrides

    protected override int OnCalculateHash()
      => Name.GetHashCode();

    protected override bool OnEquals( Platform other )
      => Name.Equals( other.Name );

    #endregion

  }

}
