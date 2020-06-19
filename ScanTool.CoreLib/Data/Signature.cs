using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using ScanTool.CoreLib.Serialization;

namespace ScanTool.CoreLib.Data
{

  [JsonConverter( typeof( SignatureJsonConverter ) )]
  public readonly struct Signature
  {

    #region Data Members

    public readonly SignatureType Type;
    public readonly Signature.Byte[] Pattern;
    public readonly string Description;

    #endregion

    #region Properties

    public int Length
    {
      [MethodImpl( MethodImplOptions.AggressiveInlining )]
      get => Pattern.Length;
    }

    #endregion

    #region Constructor

    private Signature( SignatureType type, Signature.Byte[] pattern, string description )
    {
      Type = type;
      Pattern = pattern;
      Description = description;
    }

    public static Signature Define( SignatureType type, string pattern, string description = null )
      => new Signature( type, ParsePattern( pattern ), description ?? string.Empty );

    public static Signature Content( string pattern, string description = null )
      => new Signature( SignatureType.Content, ParsePattern( pattern ), description ?? string.Empty );

    public static Signature Magic( string pattern, string description = null )
      => new Signature( SignatureType.Magic, ParsePattern( pattern ), description ?? string.Empty );

    #endregion

    #region Implicit Casts



    #endregion

    #region Private Methods

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    private static int HexCharToInt( char c )
    {
      if( char.IsDigit( c ) )
        return c - '0';
      else if( c >= 'A' && c <= 'F' )
        return c - 'A' + 10;

      ThrowInvalidPatternTokenException( c );
      return -1;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    private static Signature.Byte[] ParsePattern( string pattern )
    {
      // Sanitize
      pattern = SanitizePattern( pattern );
      var span = pattern.AsSpan();

      var len = pattern.Length;
      var bytes = new List<Signature.Byte>( len / 2 );

      for( var i = 0; i < len; i += 2 )
      {
        var nibbleA = ParseNibble( span[ i + 0 ] );
        var nibbleB = ParseNibble( span[ i + 1 ] );

        bytes.Add( new Byte( nibbleA, nibbleB ) );
      }

      return bytes.ToArray();
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    private static Signature.Nibble ParseNibble( char c )
    {
      if( c == '?' )
        return Nibble.Masked;
      else
        return new Nibble( ( byte ) ( HexCharToInt( c ) & 0xF ) );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    private static string SanitizePattern( string pattern )
    {
      var builder = new StringBuilder( pattern.Length );
      foreach( var c in pattern )
      {
        if( char.IsDigit( c ) || ( c >= 'A' && c <= 'F' ) )
          builder.Append( c );
        else if( c >= 'a' && c <= 'f' )
          builder.Append( char.ToUpper( c ) );
        else if( c == '?' )
          builder.Append( '?' );
        else if( c == ' ' )
          continue;
        else
          ThrowInvalidPatternTokenException( c );
      }

      // If we end on an incomplete byte, add a wildcard
      if( builder.Length % 2 != 0 )
        builder.Append( '?' );

      return builder.ToString();
    }

    private static void ThrowInvalidPatternTokenException( char token )
      => throw new Exception( $"Invalid token encountered when parsing signature: '{token}'" );

    #endregion

    #region Child Structures

    [StructLayout( LayoutKind.Explicit, Size = 4 )]
    public readonly struct Byte
    {

      #region Data Members

      [FieldOffset( 0x0 )] public readonly Nibble A;
      [FieldOffset( 0x2 )] public readonly Nibble B;

      #endregion

      #region Constructor

      internal Byte( Nibble a, Nibble b )
      {
        A = a;
        B = b;
      }

      #endregion

    }

    [StructLayout( LayoutKind.Explicit, Size = 2 )]
    public readonly struct Nibble
    {

      #region Constants

      public static readonly Nibble Masked = new Nibble( 0, true );

      #endregion

      #region Data Members

      [FieldOffset( 0x0 )] public readonly bool IsMasked;
      [FieldOffset( 0x1 )] public readonly byte Value;

      #endregion

      #region Constructor

      internal Nibble( byte value, bool isMasked = false )
      {
        Value = value;
        IsMasked = isMasked;
      }

      #endregion

    }

    #endregion

  }

}
