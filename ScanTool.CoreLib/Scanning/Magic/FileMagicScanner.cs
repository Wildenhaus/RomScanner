using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScanTool.CoreLib.Scanning
{

  public static class FileMagicScanner
  {

    #region Data Members

    /// <summary>
    ///   The length (in bytes) of the longest magic number signature.
    /// </summary>
    private static readonly int _maxSignatureLength;

    /// <summary>
    ///   A dictionary for looking up magic numbers.
    /// </summary>
    private static readonly Dictionary<string, FileMagic> _signatureLookup;

    #endregion

    #region Constructor

    /// <summary>
    ///   Initializes the magic number dictionary and max signature length.
    /// </summary>
    static FileMagicScanner()
    {
      _signatureLookup = GetFileMagicDefinitions().ToDictionary( x => x.Magic, x => x );
      _maxSignatureLength = _signatureLookup.Keys.Max( x => x.Length );
    }

    #endregion

    #region Public Methods

    /// <summary>
    ///   Attempts to resolve a matching magic number given a sequence of bytes.
    /// </summary>
    /// <param name="stream">
    ///   The file stream.
    /// </param>
    /// <param name="magic">
    ///   The output matching magic number, if one is found.
    /// </param>
    /// <returns>
    ///   True if a match has been found.
    /// </returns>
    public static bool TryResolveFileMagic( Stream stream, out FileMagic magic )
    {
      magic = default;

      if( !stream.CanRead )
        return false;

      var buffer = new byte[ _maxSignatureLength ];
      var bytesRead = stream.Read( buffer );

      return TryResolveFileMagic( buffer.AsSpan( 0, bytesRead ), out magic );
    }

    /// <summary>
    ///   Attempts to resolve a matching magic number given a sequence of bytes.
    /// </summary>
    /// <param name="data">
    ///   A sequence of bytes to resolve a file magic number from.
    /// </param>
    /// <param name="magic">
    ///   The output matching magic, if one is found.
    /// </param>
    /// <returns>
    ///   True if a match has been found.
    /// </returns>
    public static bool TryResolveFileMagic( Span<byte> data, out FileMagic magic )
    {
      // Attempt to find the longest matching file signature
      var startLength = Math.Min( data.Length, _maxSignatureLength );
      for( var i = startLength; i >= 0; i-- )
      {
        var chunk = data.Slice( 0, i );
        var chunkStr = ConvertBytesToLookupString( chunk );

        if( _signatureLookup.TryGetValue( chunkStr, out magic ) )
          return true;
      }

      magic = default;
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
    ///   A lookup string for use with the signature dictionary.
    /// </returns>
    private static string ConvertBytesToLookupString( Span<byte> data )
    {
      var builder = new StringBuilder();
      foreach( var b in data )
        builder.Append( ( char ) b );

      return builder.ToString();
    }

    /// <summary>
    ///   Gets all of the FileSignature definitions from <see cref="FileMagicDefinitions" />.
    /// </summary>
    private static IEnumerable<FileMagic> GetFileMagicDefinitions()
    {
      return typeof( FileMagic )
        .GetFields( BindingFlags.Public | BindingFlags.Static )
        .Where( x => x.FieldType == typeof( FileMagic ) )
        .Select( x => ( FileMagic ) x.GetValue( null ) );
    }

    #endregion

  }

}
