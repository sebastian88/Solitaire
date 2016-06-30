using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Comparers
{
  public class MoveComparer : IEqualityComparer<IMove>
  {
    public bool Equals(IMove x, IMove y)
    {
      return x.GetTopCard().Equals(y.GetTopCard())
        && x.GetBottomCard().Equals(y.GetBottomCard());
    }

    public int GetHashCode(IMove obj)
    {
      int hash = 17;
      if (obj != null)
      {

        hash = hash * 23 + obj.GetTopCard().SuitInt().GetHashCode();
        hash = hash * 23 + obj.GetTopCard().ValueInt().GetHashCode();
        hash = hash * 23 + obj.GetBottomCard().SuitInt().GetHashCode();
        hash = hash * 23 + obj.GetBottomCard().ValueInt().GetHashCode();
      }
      return hash;
    }
  }
}
