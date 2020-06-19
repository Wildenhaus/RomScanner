using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ScanTool.CoreLib.Data;

namespace ScanTool.CoreLib.Serialization
{

  public class SignatureJsonConverter : JsonConverter<Signature>
  {

    public override Signature Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
      string signaturePattern = null;
      string signatureDescription = null;
      SignatureType? signatureType = null;

      while( reader.Read() )
      {
        if( reader.TokenType == JsonTokenType.EndObject )
          break;

        if( reader.TokenType != JsonTokenType.PropertyName )
          throw new JsonException();

        var propertyName = reader.GetString();
        switch( propertyName )
        {
          case nameof( Signature.Type ):
            reader.Read();
            if( Enum.TryParse<SignatureType>( reader.GetString(), out var type ) )
              signatureType = type;
            break;

          case nameof( Signature.Pattern ):
            reader.Read();
            signaturePattern = reader.GetString();
            break;

          case nameof( Signature.Description ):
            reader.Read();
            signatureDescription = reader.GetString();
            break;

          default:
            reader.Read();
            break;
        }
      }

      if( !signatureType.HasValue )
        throw new JsonException( "Encountered a Signature without a specified Type." );

      if( string.IsNullOrWhiteSpace( signaturePattern ) )
        throw new JsonException( "Encountered a Signature without a specified Pattern." );

      return Signature.Define( signatureType.Value, signaturePattern, signatureDescription );
    }

    public override void Write( Utf8JsonWriter writer, Signature value, JsonSerializerOptions options )
    {
      throw new NotImplementedException();
    }

  }

}
