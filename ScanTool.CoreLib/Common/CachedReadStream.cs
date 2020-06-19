using System;
using System.Buffers;
using System.IO;

namespace ScanTool.CoreLib
{

  public class CachedReadStream : Stream
  {

    #region Constants

    const int BUFFER_SIZE = 1024 * 1024;

    #endregion

    #region Data Members

    private readonly Stream _sourceStream;
    private readonly Stream _cacheStream;
    private readonly byte[] _buffer;

    private long _sourcePosition;
    private readonly long _length;

    #endregion

    #region Properties

    public override bool CanRead => true;
    public override bool CanSeek => true;
    public override bool CanWrite => false;

    public override long Length => _length;

    public override long Position
    {
      get => _cacheStream.Position;
      set => Seek( value, SeekOrigin.Begin );
    }

    #endregion

    #region Constructor

    public CachedReadStream( Stream sourceStream, long length = -1 )
      : this( sourceStream, new MemoryStream(), length )
    {
    }

    public CachedReadStream( Stream sourceStream, Stream cacheStream, long length = -1 )
    {
      _sourceStream = sourceStream;
      _cacheStream = cacheStream;
      _buffer = new byte[ BUFFER_SIZE ];

      if( length > -1 )
        _length = length;
      else
        _length = _sourceStream.Length;
    }

    #endregion

    #region Public Methods

    public override void Flush()
    {
    }

    public override int Read( byte[] buffer, int offset, int count )
    {
      EnsureCachedUpTo( _sourcePosition + offset + count );
      return _cacheStream.Read( buffer, offset, count );
    }

    public override long Seek( long offset, SeekOrigin origin )
    {
      switch( origin )
      {
        case SeekOrigin.Begin:
          EnsureCachedUpTo( offset );
          return _cacheStream.Seek( offset, SeekOrigin.Begin );
        case SeekOrigin.Current:
          EnsureCachedUpTo( Position + offset );
          return _cacheStream.Seek( offset, origin );
        case SeekOrigin.End:
          EnsureCachedUpTo( Length + offset );
          return _cacheStream.Seek( offset, origin );
        default:
          return 0;
      }
    }

    public override void SetLength( long value )
    {
      throw new NotSupportedException();
    }

    public override void Write( byte[] buffer, int offset, int count )
    {
      throw new NotSupportedException();
    }

    #endregion

    #region Private Methods

    private void EnsureCachedUpTo( long offset )
    {
      offset = Math.Min( offset, _length );
      if( _sourcePosition >= offset )
        return;

      var originalPosition = _cacheStream.Position;
      _cacheStream.Seek( _sourcePosition, SeekOrigin.Begin );

      while( _sourcePosition < offset )
      {
        var bytesToRead = ( int ) Math.Min( BUFFER_SIZE, offset - _sourcePosition );
        var bytesRead = _sourceStream.Read( _buffer, 0, bytesToRead );
        if( bytesRead == 0 )
          break;

        _cacheStream.Write( _buffer, 0, bytesRead );
      }

      _sourcePosition = offset;
      _cacheStream.Seek( originalPosition, SeekOrigin.Begin );
    }

    #endregion

  }

}
