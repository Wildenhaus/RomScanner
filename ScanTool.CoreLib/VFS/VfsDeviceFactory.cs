using System.IO;
using ScanTool.CoreLib.Data;
using ScanTool.CoreLib.VFS.Devices;

namespace ScanTool.CoreLib.VFS
{

  public static class VfsDeviceFactory
  {

    public static bool TryGetDevice( FileType fileType, Stream stream, out VfsDevice device )
    {
      device = null;

      if( fileType == FileType.GZip )
        device = new GZipArchiveDevice( stream );
      else if ( fileType == FileType.Iso9660 )
        device = new Iso9960Device( stream );
      else if (fileType == FileType.Rar )
        device = new RarArchiveDevice( stream );
      else if (fileType == FileType.SevenZip )
        device = new SevenZipArchiveDevice( stream );
      else if ( fileType == FileType.Tar )
        device = new TarArchiveDevice( stream );
      else if ( fileType == FileType.Zip )
        device = new ZipArchiveDevice( stream );

      if( device == null )
        return false;

      device.Initialize();
      return true;
    }

  }

}
