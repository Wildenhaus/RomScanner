using System;
using System.Collections.Generic;
using System.Linq;

namespace ScanTool.CoreLib.Extensions
{

  public static class LinqExtensions
  {

    public static IEnumerable<T> SelectManyRecursive<T>( this IEnumerable<T> source, Func<T, IEnumerable<T>> selector )
    {
      var result = source.SelectMany( selector );
      if( !result.Any() )
      {
        return result;
      }
      return result.Concat( result.SelectManyRecursive( selector ) );
    }

  }

}
