using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScanTool.CoreLib.Data;
using ScanTool.CoreLib.VFS;

namespace ScanTool.CoreLib.Tools
{

  public static class FileScanner
  {

    #region Constants

    private const uint POINTS_EXTENSION = 1;
    private const uint POINTS_SIG_CONTENT = 3;
    private const uint POINTS_SIG_MAGIC = 5;

    #endregion

    #region Data Members

    private static readonly int _longestMagicSignatureLength;
    private static readonly int _longestContentSignatureLength;

    private static readonly HashSet<Signature> _fileMagicSignatures;
    private static readonly HashSet<Signature> _fileContentSignatures;
    private static readonly Dictionary<Signature, HashSet<FileType>> _signatureTypeLookup;

    #endregion

    #region Constructor

    static FileScanner()
    {
      _fileMagicSignatures = BuildSignatureCache( SignatureType.Magic );
      _fileContentSignatures = BuildSignatureCache( SignatureType.Content );
      _signatureTypeLookup = BuildSignatureTypeLookup();

      _longestMagicSignatureLength = _fileMagicSignatures.Max( x => x.Pattern.Length );
      _longestContentSignatureLength = _fileContentSignatures.Max( x => x.Pattern.Length );
    }

    #endregion

    #region Public Methods

    public static FileTypeMatch[] DetermineFileType( Stream fileStream )
    {
      var evaluator = new Evaluator();

      EvaluateMagicSignatures( fileStream, evaluator );
      EvaluateContentSignatures( fileStream, evaluator );

      return evaluator.FinalizeMatches();
    }

    public static FileTypeMatch[] DetermineFileType( VfsFile file )
    {
      var evaluator = new Evaluator();
      var fileStream = new CachedReadStream( file.GetStream() );

      EvaluateContentSignatures( fileStream, evaluator );
      EvaluateMagicSignatures( fileStream, evaluator );
      EvaluateFileExtension( file, evaluator );

      return evaluator.FinalizeMatches();
    }

    #endregion

    #region Private Methods

    private static HashSet<Signature> BuildSignatureCache( SignatureType signatureType )
    {
      return FileTypeDatabase.Definitions
        .SelectMany( x => x.Signatures )
        .Where( x => x.Type == signatureType )
        .ToHashSet();
    }

    private static Dictionary<Signature, HashSet<FileType>> BuildSignatureTypeLookup()
    {
      var cache = new Dictionary<Signature, HashSet<FileType>>();
      foreach( var definition in FileTypeDatabase.Definitions )
      {
        foreach( var signature in definition.Signatures )
        {
          if( !cache.TryGetValue( signature, out var associatedTypes ) )
          {
            associatedTypes = new HashSet<FileType>();
            cache.Add( signature, associatedTypes );
          }

          associatedTypes.Add( definition );
        }
      }

      return cache;
    }

    private static void EvaluateFileExtension( VfsFile file, Evaluator evaluator )
    {
      var extension = Path.GetExtension( file.FullPath );
      foreach( var type in FileTypeDatabase.GetFileTypesByExtension( extension ) )
        evaluator.Add( type, POINTS_EXTENSION );
    }

    private static unsafe void EvaluateContentSignatures( Stream fileStream, Evaluator evaluator )
    {
      fileStream.Seek( 0, SeekOrigin.Begin );

      var matches = SignatureScanner.Scan( _fileContentSignatures, fileStream );
      foreach( var match in matches )
      {
        foreach( var type in _signatureTypeLookup[ match.Signature ] )
          evaluator.Add( type, POINTS_SIG_CONTENT );
      }
    }

    private static unsafe void EvaluateMagicSignatures( Stream fileStream, Evaluator evaluator )
    {
      fileStream.Seek( 0, SeekOrigin.Begin );

      var alloc = new byte[ _longestMagicSignatureLength ];
      var bytesRead = fileStream.Read( alloc );
      var buffer = new Memory<byte>( alloc, 0, Math.Min( bytesRead, _longestMagicSignatureLength ) );

      var matches = SignatureScanner.Scan( _fileMagicSignatures, buffer.Slice( 0, bytesRead ) );
      foreach( var match in matches )
      {
        if( match.Offset != 0 )
          continue;

        foreach( var type in _signatureTypeLookup[ match.Signature ] )
          evaluator.Add( type, POINTS_SIG_MAGIC );
      }
    }

    #endregion

    #region Child Classes

    internal class Evaluator
    {

      #region Data Members

      private double _totalPoints;
      private readonly Dictionary<FileType, uint> _points;

      #endregion

      #region Constructor

      internal Evaluator()
      {
        _totalPoints = 0;
        _points = new Dictionary<FileType, uint>();
      }

      #endregion

      #region Public Methods

      public void Add( FileType fileType, uint points = 1 )
      {
        if( _points.ContainsKey( fileType ) )
          _points[ fileType ] += points;
        else
          _points[ fileType ] = points;

        _totalPoints += points;
      }

      public FileTypeMatch[] FinalizeMatches()
      {
        return _points
          .Select( x => new FileTypeMatch( x.Key, x.Value / _totalPoints ) )
          .OrderByDescending( x => x.Confidence )
          .ToArray();
      }

      #endregion

    }

    #endregion

  }

}
