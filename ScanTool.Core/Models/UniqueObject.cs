using System;
using System.Runtime.CompilerServices;

namespace ScanTool.Core.Models
{

  public abstract class UniqueObject<T> : IEquatable<T>
    where T : UniqueObject<T>
  {

    #region Data Members

    private readonly int _hash;

    #endregion

    #region Constructor

    protected UniqueObject()
    {
      _hash = OnCalculateHash();
    }

    #endregion

    #region Private Methods

    protected abstract int OnCalculateHash();
    protected abstract bool OnEquals( T other );

    #endregion

    #region IEquatable Methods

    public bool Equals( T other ) => OnEquals( other );

    #endregion

    #region Overrides

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public override bool Equals( object obj )
      => obj is T other && Equals( other );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public override int GetHashCode() => _hash;

    #endregion

  }

}
