using System;
using System.IO;
using System.Linq;
using ScanTool.CoreLib.Data;
using ScanTool.CoreLib.Scanning.Symbols;
using ScanTool.CoreLib.Tools;
using ScanTool.CoreLib.VFS;

namespace ScanTool
{

  public class Scanner
  {

    #region Data Members

    private readonly VfsDevice _device;

    #endregion

    #region Constructor

    public Scanner( VfsDevice device )
    {
      _device = device;
    }

    #endregion

    #region Public Methods

    public void Scan()
    {
      if( !_device.IsInitialized )
        _device.Initialize();

      foreach( var file in _device.EnumerateFiles( true ) )
        Scan( file );
    }

    #endregion

    #region Private Methods

    private void Scan( VfsFile file, int level = 1 )
    {
      SetStatus( file.FullPath );
      var typeMatches = FileScanner.DetermineFileType( file.GetStream() );
      if( typeMatches.Length == 0 )
        return;

      var fileType = typeMatches[0].FileType;
      Console.WriteLine( "{0}- {1}: {2}", new string( ' ', level * 2 ), file.FullPath, fileType.Name );

      if( fileType.Category == FileCategory.Archive
        && VfsDeviceFactory.TryGetDevice( fileType, file.GetStream(), out var device ) )
      {
        try
        {
          device.Initialize();
          foreach( var childFile in device.EnumerateFiles( true ) )
            Scan( childFile, level + 1 );
        }
        catch( Exception ex )
        {
          Console.WriteLine( "{0}- Failed to read from the archive: {1}", new string( ' ', ( level + 1 ) * 2 ), ex.Message );
        }
      }
      else if( fileType == FileType.ELF )
      {
        var symbols = ElfDebugSymbolScanner.GetDebugSymbolCount( file.GetStream() );
        if( symbols > 2 )
          Console.WriteLine( "{0}- {1} Debug Symbols Found", new string( ' ', ( level + 1 ) * 2 ), symbols );
      }
    }

    private void SetStatus( string fileName )
    {
      Console.Title = $"ScanTool | {fileName}";
    }

    #endregion

  }

}
