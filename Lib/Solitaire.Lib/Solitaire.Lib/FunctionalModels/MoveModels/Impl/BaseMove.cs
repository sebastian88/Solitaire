using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.MoveModels.Impl
{
  public abstract class BaseMove
  {

    protected Card _topCard;
    protected Card _bottomCard;

    public BaseMove(Card topcard, Card bottomCard)
    {
      _topCard = topcard;
      _bottomCard = bottomCard;
    }

    protected bool IsInSequence()
    {
      return Math.Abs(_topCard.ValueInt - _bottomCard.ValueInt) == 1;
    }
  }
}
