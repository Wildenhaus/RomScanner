using System;
using System.Runtime.CompilerServices;

namespace ScanTool.CoreLib.Tests
{

  /// <summary>
  ///   A static, thread-safe wrapper for <see cref="System.Random" />.
  /// </summary>
  public static class StaticRandom
  {

    #region Properties

    [ThreadStatic]
    private static Random _random;

    private static Random Random
    {
      [MethodImpl( MethodImplOptions.AggressiveInlining )]
      get
      {
        if( _random == null )
          _random = new Random( Guid.NewGuid().GetHashCode() );

        return _random;
      }
    }

    #endregion

    #region Public Methods

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static int Next( int min = int.MinValue, int max = int.MaxValue )
      => Random.Next( min, max );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static double NextDouble()
      => Random.NextDouble();

    #endregion

  }

}
