using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ScanTool.CoreLib.Data;

namespace ScanTool.CoreLib.Tools
{

  public static class SignatureScanner
  {

    #region Constants

    public const int NOT_FOUND = -1;
    private const int BUFFER_SIZE = 1024 * 1024;

    #endregion

    #region Public Methods

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static int Match( Signature signature, Span<byte> buffer )
      => Match( signature, buffer, 0 );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static int Match( Signature signature, Span<byte> buffer, int offset )
    {
      var pattern = signature.Pattern.AsSpan();
      var patternSize = pattern.Length;

      for( int i = offset, pos = 0; i < buffer.Length; i++ )
      {
        if( MatchByte( ref buffer[ i ], ref pattern[ pos ] ) )
        {
          pos++;
          if( pos == patternSize )
          {
            // We have a full match
            return i - patternSize + 1;
          }
        }
        else
        {
          i -= pos;
          pos = 0;
        }
      }

      return NOT_FOUND;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static SignatureMatch[] MatchAll( Signature signature, Span<byte> buffer )
    {
      var matches = new List<SignatureMatch>();

      var pos = 0;
      var size = buffer.Length;
      var signatureLength = signature.Length;

      while( size > pos )
      {
        var matchOffset = Match( signature, buffer, pos );
        if( matchOffset != NOT_FOUND )
        {
          pos = matchOffset + signatureLength;
          matches.Add( new SignatureMatch( signature, matchOffset ) );
        }
        else
          break;
      }

      return matches.ToArray();
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static SignatureMatch[] Scan( IEnumerable<Signature> signatures, Span<byte> buffer )
    {
      var matches = new List<SignatureMatch>();

      var sigs = signatures.ToArray();
      foreach( var sig in sigs )
      {
        var matchOffset = Match( sig, buffer );
        if( matchOffset != NOT_FOUND )
          matches.Add( new SignatureMatch( sig, matchOffset ) );
      }

      return matches.ToArray();
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static IEnumerable<SignatureMatch> Scan( IEnumerable<Signature> signatures, Stream stream )
    {
      var matches = new List<SignatureMatch>();

      var sigs = signatures.ToArray();
      var buffer = ArrayPool<byte>.Shared.Rent( BUFFER_SIZE );

      while( stream.Position < stream.Length )
      {
        var bytesRead = stream.Read( buffer );
        if( bytesRead <= 0 )
          break;

        var span = buffer.AsSpan( 0, bytesRead );
        foreach( var sig in sigs )
        {
          var matchOffset = Match( sig, span );
          if( matchOffset != NOT_FOUND )
            matches.Add( new SignatureMatch( sig, matchOffset ) );
        }
      }

      ArrayPool<byte>.Shared.Return( buffer, true );
      return matches.ToArray();
    }

    #endregion

    #region Private Methods

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    private static bool MatchByte( ref byte bufferByte, ref Signature.Byte signatureByte )
    {
      var nibbleA = signatureByte.A;
      if( !nibbleA.IsMasked )
      {
        var a = bufferByte >> 4;
        if( a != nibbleA.Value )
          return false;
      }

      var nibbleB = signatureByte.B;
      if( !nibbleB.IsMasked )
      {
        var b = bufferByte & 0xF;
        if( b != nibbleB.Value )
          return false;
      }

      return true;
    }

    #endregion

  }

}
