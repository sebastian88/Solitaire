using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Comparers
{
  public class CardComparer : IEqualityComparer<ICard>
  {
    public bool Equals(ICard x, ICard y)
    {
      return x.Equals(y);
    }

    public int GetHashCode(ICard obj)
    {
      int hash = 17;
      if (obj != null)
      {

        hash = hash * 23 + obj.SuitInt().GetHashCode();
        hash = hash * 23 + obj.ValueInt().GetHashCode();
      }
      return hash;
    }
  }
}
