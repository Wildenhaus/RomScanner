using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ScanTool.Core.Models;

namespace ScanTool.Core.Tools
{

  public static class FileSignatureResolver
  {

    #region Data Members

    /// <summary>
    ///   The length (in bytes) of the longest file signature.
    /// </summary>
    private static readonly int _maxSignatureLength;

    /// <summary>
    ///   A dictionary for looking up file signatures.
    /// </summary>
    private static readonly Dictionary<string, FileSignature> _signatureLookup;

    #endregion

    #region Constructor

    /// <summary>
    ///   Initializes the signature dictionary and max signature length.
    /// </summary>
    static FileSignatureResolver()
    {
      _signatureLookup = GetDefinedFileSignatures().ToDictionary( x => x.Signature, x => x );
      _maxSignatureLength = _signatureLookup.Keys.Max( x => x.Length );
    }

    #endregion

    #region Public Methods

    /// <summary>
    ///   Attempts to resolve a matching file signature given a sequence of bytes.
    /// </summary>
    /// <param name="data">
    ///   A sequence of bytes to resolve a file signature from.
    /// </param>
    /// <param name="signature">
    ///   The output matching signature, if one is found.
    /// </param>
    /// <returns>
    ///   True if a match has been found.
    /// </returns>
    public static bool TryResolveFileSignature( Span<byte> data, out FileSignature signature )
    {
      // Attempt to find the longest matching file signature
      var startLength = Math.Min( data.Length, _maxSignatureLength );
      for( var i = startLength; i >= 0; i-- )
      {
        var chunk = data.Slice( 0, i );
        var chunkStr = ConvertBytesToLookupString( chunk );

        if( _signatureLookup.TryGetValue( chunkStr, out signature ) )
          return true;
      }

      signature = default;
      return false;
    }

    #endregion

    #region Private Methods

    /// <summary>
    ///   Converts a sequence of bytes to a string for lookup.
    /// </summary>
    /// <param name="data">
    ///   A sequence of bytes to convert into a lookup string.
    /// </param>
    /// <returns>
    ///   A lookup string for use with the signature dictionary..
    /// </returns>
    private static string ConvertBytesToLookupString( Span<byte> data )
    {
      var builder = new StringBuilder();
      foreach( var b in data )
        builder.Append( ( char ) b );

      return builder.ToString();
    }

    /// <summary>
    ///   Gets all of the FileSignature definitions from <see cref="FileSignatures" />.
    /// </summary>
    private static IEnumerable<FileSignature> GetDefinedFileSignatures()
    {
      return typeof( FileSignatures )
        .GetFields( BindingFlags.Public | BindingFlags.Static )
        .Where( x => x.FieldType == typeof( FileSignature ) )
        .Select( x => ( FileSignature ) x.GetValue( null ) );
    }

    #endregion

  }

}
