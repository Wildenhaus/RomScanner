using System;
using System.IO;

namespace ScanTool.CoreLib
{

  public sealed unsafe class BigEndianBinaryReader : BinaryReader
  {

    // TODO: This is just stubbed in for now.

    #region Constructor

    public BigEndianBinaryReader( Stream input )
      : base( input )
    {
    }

    #endregion

    public override ushort ReadUInt16()
    {
      var buffer = stackalloc byte[2];
      var span = new Span<byte>( buffer, 2 );

      Read( span );
      span.Reverse();

      return BitConverter.ToUInt16( span );
    }

    public override uint ReadUInt32()
    {
      var buffer = stackalloc byte[4];
      var span = new Span<byte>( buffer, 4 );

      Read( span );
      span.Reverse();

      return BitConverter.ToUInt32( span );
    }

  }

}
