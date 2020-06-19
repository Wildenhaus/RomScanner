using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace ScanTool.CoreLib.Data
{

  public static class FileTypeDatabase
  {

    #region Constants

    const string FILENAME_FILE_TYPE_DEFINITIONS = "FileTypeDefinitions.json";

    #endregion

    #region Data Members

    private static readonly HashSet<FileType> _fileTypes;
    private static readonly Dictionary<string, HashSet<FileType>> _fileTypesByExt;
    private static readonly Dictionary<string, FileType> _fileTypesByName;

    #endregion

    #region Properties

    public static IEnumerable<FileType> Definitions
    {
      get => _fileTypesByName.Values;
    }

    #endregion

    #region Constructor

    static FileTypeDatabase()
    {
      _fileTypes = new HashSet<FileType>();
      _fileTypesByExt = new Dictionary<string, HashSet<FileType>>();
      _fileTypesByName = new Dictionary<string, FileType>();
      LoadFileTypeDefinitions();
    }

    #endregion

    #region Public Methods

    public static IEnumerable<FileType> GetFileTypesByExtension( string extension )
    {
      extension = extension.ToLower();
      if( _fileTypesByExt.TryGetValue( extension, out var typeList ) )
        return typeList;

      return Enumerable.Empty<FileType>();
    }

    #endregion

    #region Private Methods

    private static IEnumerable<FileType> GetBuiltInFileTypeDefinitions()
    {
      return typeof( FileType )
        .GetFields( BindingFlags.Public | BindingFlags.Static )
        .Where( x => x.FieldType == typeof( FileType ) )
        .Select( x => ( FileType ) x.GetValue( null ) );
    }

    private static void LoadFileTypeDefinitions()
    {
      if( !File.Exists( FILENAME_FILE_TYPE_DEFINITIONS ) )
        throw new FileNotFoundException( $"Could not find {FILENAME_FILE_TYPE_DEFINITIONS}." );

      var jsonData = File.ReadAllText( FILENAME_FILE_TYPE_DEFINITIONS );
      var definitions = JsonSerializer.Deserialize<FileType[]>( jsonData, new JsonSerializerOptions
      {
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip
      } );

      foreach( var definition in definitions )
        RegisterFileTypeDefinition( definition );

      foreach( var definition in GetBuiltInFileTypeDefinitions() )
        RegisterFileTypeDefinition( definition );
    }

    private static void RegisterFileTypeDefinition( FileType definition )
    {
      if( !_fileTypes.Add( definition ) )
        throw new System.Exception( $"Duplicate FileType definition: {definition.Name}." );

      _fileTypesByName.Add( definition.Name, definition );
      foreach( var extension in definition.Extensions )
      {
        if( !_fileTypesByExt.TryGetValue( extension, out var typeList ) )
        {
          typeList = new HashSet<FileType>();
          _fileTypesByExt.Add( extension, typeList );
        }

        typeList.Add( definition );
      }
    }

    #endregion

  }

}
