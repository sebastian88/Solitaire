using Solitaire.Lib.Config;
using Solitaire.Lib.Context;
using Solitaire.Lib.IoC;
using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.Moves.Impl
{
  public abstract class BaseMove
  {
    protected UnitOfWork _unitOfWork;
    protected Card _topCard;
    protected Card _bottomCard;

    public BaseMove(UnitOfWork unitOfWork, Card topcard, Card bottomCard)
    {
      _unitOfWork = unitOfWork;
      _topCard = topcard;
      _bottomCard = bottomCard;
    }

    protected bool IsInSequence()
    {
      return Math.Abs(_topCard.ValueInt - _bottomCard.ValueInt) == 1;
    }
  }
}
