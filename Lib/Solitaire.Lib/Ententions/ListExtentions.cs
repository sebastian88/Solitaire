using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Ententions
{
  public static class ListExtentions
  {
    public static List<T> Clone<T>(this List<T> listToClone)
         where T : ICloneable
    {
      var list = (List<T>)Activator.CreateInstance(listToClone.GetType());
      foreach (var item in listToClone)
      {
        list.Add((T)item.Clone());
      }
      return list;
    }
  }
}
