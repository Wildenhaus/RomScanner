using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PatternFinder;

namespace ScanTool.CoreLib.Scanning.Signatures
{

  public static class FileSignatureScanner
  {

    const int BUFFER_SIZE = 1024 * 1024;

    #region Data Members

    private static readonly Signature[] _signatures;

    #endregion

    #region Constructor

    static FileSignatureScanner()
    {
      _signatures = GetFileSignatureDefinitions();
    }

    #endregion

    public static IEnumerable<SignatureScanResult> FindSignatures( Stream stream )
    {
      stream.Seek( 0, SeekOrigin.Begin );
      var matches = new HashSet<SignatureScanResult>();

      var signatures = _signatures;
      var maxLen = _signatures.Max( x => x.Pattern.Length );

      long offset = 0;
      int bytesRead = 0;

      var buffer = ArrayPool<byte>.Shared.Rent( BUFFER_SIZE );
      while( stream.Position < stream.Length )
      {
        bytesRead = stream.Read( buffer, 0, BUFFER_SIZE );
        if( bytesRead <= 0 )
          break;


        var results = Pattern.Scan( buffer, signatures );
        foreach( var result in results )
          matches.Add( new SignatureScanResult( result.Name, offset + result.FoundOffset ) );

        if( stream.Position >= stream.Length )
          break;

        // Try to back up a bit in case a signature was cut off
        var seekOffset = Math.Max( stream.Position - maxLen, 0 );
        if( seekOffset == 0 )
          continue;

        stream.Seek( 0 - maxLen, SeekOrigin.Current );
        offset = stream.Position;
      }
      ArrayPool<byte>.Shared.Return( buffer );

      return matches;
    }

    #region Private Methods

    private static Signature[] GetFileSignatureDefinitions()
    {
      return typeof( SignatureDefinitions )
        .GetFields( BindingFlags.Public | BindingFlags.Static )
        .Where( x => x.FieldType == typeof( Signature ) )
        .Select( x => ( Signature ) x.GetValue( null ) )
        .ToArray();
    }

    #endregion

  }

}
