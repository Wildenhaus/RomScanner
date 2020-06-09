using System;
using System.Runtime.CompilerServices;

namespace ScanTool.CoreLib.Hashing
{

  public readonly struct Hash : IEquatable<Hash>, IEquatable<string>
  {

    #region Data Members

    public readonly HashType Type;
    public readonly string Value;

    #endregion

    #region Constructor

    public Hash( HashType type, string value )
    {
      Type = type;
      Value = value;
    }

    #endregion

    #region Operators

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static implicit operator string( Hash hash ) => hash.Value;

    #endregion

    #region IEquatable Methods

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public bool Equals( Hash other )
      => Type == other.Type && Value.Equals( other.Value, StringComparison.InvariantCultureIgnoreCase );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public bool Equals( string other )
      => Value.Equals( other, StringComparison.InvariantCultureIgnoreCase );

    #endregion

    #region Overrides

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public override int GetHashCode()
      => Value.GetHashCode();

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public override bool Equals( object obj )
      => ( obj is string other && Equals( other ) )
      || ( obj is Hash otherHash && Equals( otherHash ) );

    #endregion

  }

}
