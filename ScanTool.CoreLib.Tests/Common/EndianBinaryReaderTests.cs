using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xunit;

namespace ScanTool.CoreLib.Tests.Common
{

  public static class EndianBinaryReaderTests
  {

    #region Constants

    private const int TEST_ITERATIONS = (int)1e5;

    #endregion

    #region Tests

    [Fact]
    public static void Reads_Int16_BE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( Int16 ) StaticRandom.Next( Int16.MinValue, Int16.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.BigEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.BigEndian ) )
          Assert.Equal( inputValue, reader.ReadInt16() );
      } );
    }

    [Fact]
    public static void Reads_Int16_LE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( Int16 ) StaticRandom.Next( Int16.MinValue, Int16.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.LittleEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
          Assert.Equal( inputValue, reader.ReadInt16() );
      } );
    }

    [Fact]
    public static void Reads_UInt16_BE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( UInt16 ) StaticRandom.Next( UInt16.MinValue, UInt16.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.BigEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.BigEndian ) )
          Assert.Equal( inputValue, reader.ReadUInt16() );
      } );
    }

    [Fact]
    public static void Reads_UInt16_LE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( UInt16 ) StaticRandom.Next( UInt16.MinValue, UInt16.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.LittleEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
          Assert.Equal( inputValue, reader.ReadUInt16() );
      } );
    }

    [Fact]
    public static void Reads_Int32_BE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( Int32 ) StaticRandom.Next( Int32.MinValue, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.BigEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.BigEndian ) )
          Assert.Equal( inputValue, reader.ReadInt32() );
      } );
    }

    [Fact]
    public static void Reads_Int32_LE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( Int32 ) StaticRandom.Next( Int32.MinValue, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.LittleEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
          Assert.Equal( inputValue, reader.ReadInt32() );
      } );
    }

    [Fact]
    public static void Reads_UInt32_BE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( UInt32 ) StaticRandom.Next( 0, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.BigEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.BigEndian ) )
          Assert.Equal( inputValue, reader.ReadUInt32() );
      } );
    }

    [Fact]
    public static void Reads_UInt32_LE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( UInt32 ) StaticRandom.Next( 0, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.LittleEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
          Assert.Equal( inputValue, reader.ReadUInt32() );
      } );
    }

    [Fact]
    public static void Reads_Int64_BE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( Int64 ) StaticRandom.Next( 0, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.BigEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.BigEndian ) )
          Assert.Equal( inputValue, reader.ReadInt64() );
      } );
    }

    [Fact]
    public static void Reads_Int64_LE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( Int64 ) StaticRandom.Next( 0, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.LittleEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
          Assert.Equal( inputValue, reader.ReadInt64() );
      } );
    }

    [Fact]
    public static void Reads_UInt64_BE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( UInt64 ) StaticRandom.Next( 0, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.BigEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.BigEndian ) )
          Assert.Equal( inputValue, reader.ReadUInt64() );
      } );
    }

    [Fact]
    public static void Reads_UInt64_LE()
    {
      Parallel.For( 0, TEST_ITERATIONS, i =>
      {
        var inputValue = ( UInt64 ) StaticRandom.Next( 0, Int32.MaxValue );
        using( var stream = CreateStream( inputValue, Endianness.LittleEndian ) )
        using( var reader = new EndianBinaryReader( stream, Endianness.LittleEndian ) )
          Assert.Equal( inputValue, reader.ReadUInt64() );
      } );
    }

    #endregion

    #region Helper Methods

    private unsafe static MemoryStream CreateStream<T>( T value, Endianness endianness = Endianness.LittleEndian )
      where T : unmanaged
    {
      var alloc = new byte[ sizeof( T ) ];
      fixed( void* pAlloc = &alloc[ 0 ] )
        Marshal.StructureToPtr( value, ( IntPtr ) pAlloc, false );

      if( NeedsOrderSwap( endianness ) )
        alloc.AsSpan().Reverse();

      return new MemoryStream( alloc, 0, sizeof( T ), false );
    }

    private static bool NeedsOrderSwap( Endianness endianness )
      => BitConverter.IsLittleEndian ^ endianness == Endianness.LittleEndian;

    #endregion

  }

}
