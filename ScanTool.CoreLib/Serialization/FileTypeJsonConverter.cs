using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ScanTool.CoreLib.Data;

namespace ScanTool.CoreLib.Serialization
{

  public class FileTypeJsonConverter : JsonConverter<FileType>
  {

    public override FileType Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
      string typeName = null;
      FileCategory? typeCategory = null;
      Signature[] typeSignatures = null;
      string[] typeExtensions = null;

      while( reader.Read() )
      {
        if( reader.TokenType == JsonTokenType.EndObject )
          break;

        if( reader.TokenType != JsonTokenType.PropertyName )
          throw new JsonException();

        var propertyName = reader.GetString();
        switch( propertyName )
        {
          case nameof( FileType.Name ):
            reader.Read();
            typeName = reader.GetString();
            break;

          case nameof( FileType.Category ):
            reader.Read();
            if( Enum.TryParse<FileCategory>( reader.GetString(), out var category ) )
              typeCategory = category;
            break;

          case nameof( FileType.Signatures ):
            reader.Read();
            typeSignatures = JsonSerializer.Deserialize<Signature[]>( ref reader );
            break;

          case nameof( FileType.Extensions ):
            reader.Read();
            typeExtensions = JsonSerializer.Deserialize<string[]>( ref reader );
            break;

          default:
            reader.Read();
            break;
        }
      }

      if( string.IsNullOrWhiteSpace( typeName ) )
        throw new JsonException( "Encountered a FileType without a specified Name." );
      if( !typeCategory.HasValue )
        throw new JsonException( "Encountered a FileType without a specified Category." );

      return FileType.Create( typeName, typeCategory.Value, typeSignatures, typeExtensions );
    }

    public override void Write( Utf8JsonWriter writer, FileType value, JsonSerializerOptions options )
    {
      throw new NotImplementedException();
    }

  }

}
