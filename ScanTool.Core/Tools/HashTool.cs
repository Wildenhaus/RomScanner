using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ScanTool.Core.Enumerations;
using ScanTool.Core.Models;

namespace ScanTool.Core.Tools
{

  public static class HashTool
  {

    #region Constants

    const int STREAM_EOF = 0;
    const int HASH_BUFFER_SIZE = 0x1000;

    #endregion

    #region Public Methods

    public static async Task<Hash[]> HashAsync( string filePath, params HashType[] hashTypes )
    {
      if( !File.Exists( filePath ) )
        throw new FileNotFoundException( "Invalid path.", filePath );

      using( var fileStream = File.OpenRead( filePath ) )
        return await HashAsync( fileStream, hashTypes );
    }

    public static async Task<Hash[]> HashAsync( Stream stream, params HashType[] hashTypes )
    {
      if( hashTypes.Length == 0 )
        throw new Exception( "At least one hash type must be specified." );

      if( !stream.CanRead )
        throw new Exception( "Cannot read from stream." );

      var builders = InitializeHashBuilders( hashTypes );
      await HashFromStream( stream, builders );

      return FinalizeHashes( builders );
    }

    #endregion

    #region Private Methods

    private static HashAlgorithm[] InitializeHashBuilders( params HashType[] hashTypes )
    {
      var builders = new HashAlgorithm[ hashTypes.Length ];

      for( var i = 0; i < builders.Length; i++ )
      {
        var hashType = hashTypes[ i ];
        switch( hashType )
        {
          case HashType.MD5:
            builders[ i ] = MD5.Create();
            break;
          case HashType.SHA1:
            builders[ i ] = SHA1.Create();
            break;
          default:
            throw new Exception( $"Unsupported HashType: {hashType}" );
        }

        builders[ i ].Initialize();
      }

      return builders;
    }

    private static async Task HashFromStream( Stream stream, HashAlgorithm[] builders )
    {
      stream.Seek( 0, SeekOrigin.Begin );

      int bytesRead = 0, offset = 0;
      var buffer = new byte[ HASH_BUFFER_SIZE ];

      while( stream.Position < stream.Length )
      {
        bytesRead = await stream.ReadAsync( buffer, 0, HASH_BUFFER_SIZE );
        if( bytesRead == STREAM_EOF )
          break;

        foreach( var builder in builders )
        {
          if( stream.Position == stream.Length )
            builder.TransformFinalBlock( buffer, 0, bytesRead );
          else
            builder.TransformBlock( buffer, 0, bytesRead, buffer, 0 );
        }

        offset += bytesRead;
      }
    }

    private static Hash[] FinalizeHashes( HashAlgorithm[] builders )
    {
      var hashes = new Hash[ builders.Length ];

      for( var i = 0; i < builders.Length; i++ )
      {
        var builder = builders[ i ];
        var hashString = HashBufferToString( builder.Hash );
        switch( builder )
        {
          case MD5 _:
            hashes[ i ] = new Hash( HashType.MD5, hashString );
            break;
          case SHA1 _:
            hashes[ i ] = new Hash( HashType.SHA1, hashString );
            break;
          default:
            throw new Exception( $"Unsupported HashType: `{builder.GetType().Name}`" );
        }
      }

      return hashes;
    }

    private static string HashBufferToString( byte[] hashBuffer )
    {
      var stringBuilder = new StringBuilder( 2 * hashBuffer.Length );
      foreach( byte b in hashBuffer )
        stringBuilder.AppendFormat( "{0:X2}", b );

      return stringBuilder.ToString();
    }

    #endregion

  }

}
