using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using ScanTool.CoreLib.Serialization;

namespace ScanTool.CoreLib.Data
{

  [JsonConverter( typeof( FileTypeJsonConverter ) )]
  public readonly partial struct FileType : IEquatable<FileType>
  {

    #region Data Members

    /// <summary>
    ///   A unique identifier that is generated on runtime and
    ///   is used to equate two <see cref="FileType" />s.
    /// </summary>
    private readonly Guid _id;

    public readonly string Name;
    public readonly FileCategory Category;
    public readonly Signature[] Signatures;
    public readonly string[] Extensions;

    #endregion

    #region Constructor

    private FileType( string name, FileCategory category,
      Signature[] signatures, string[] extensions )
    {
      _id = Guid.NewGuid();

      Name = name;
      Category = category;
      Signatures = signatures;
      Extensions = extensions;
    }

    public static FileType Create(
      string name,
      FileCategory category = FileCategory.Unknown,
      IEnumerable<Signature> signatures = null,
      IEnumerable<string> extensions = null )
    {
      Trace.Assert( !string.IsNullOrWhiteSpace( name ), "FileType must have a name." );
      signatures = signatures ?? Enumerable.Empty<Signature>();
      extensions = extensions ?? Enumerable.Empty<string>();

      // Sanitize extensions to be lowercase and start with a '.'.
      extensions = extensions.Select( x =>
      {
        if( !x.StartsWith( '.' ) )
          x = '.' + x;

        return x.ToLower();
      } );

      return new FileType( name, category, signatures.ToArray(), extensions.ToArray() );
    }

    #endregion

    #region Operators

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool operator ==( FileType left, FileType right )
      => left.Equals( right );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool operator !=( FileType left, FileType right )
      => !left.Equals( right );

    #endregion

    #region Overrides

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public bool Equals( FileType other )
      => _id == other._id;

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public override bool Equals( object obj )
      => obj is FileType other && Equals( other );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public override int GetHashCode()
      => _id.GetHashCode();

    #endregion

  }

}
