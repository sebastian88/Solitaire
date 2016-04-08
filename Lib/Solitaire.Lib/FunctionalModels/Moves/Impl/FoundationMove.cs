using Solitaire.Lib.Config;
using Solitaire.Lib.Context;
using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.Moves.Impl
{
  public abstract class FoundationMove : BaseMove
  {

    public FoundationMove(UnitOfWork unitOfWork, Card topcard, Card bottomCard)
      : base(unitOfWork, topcard, bottomCard)
    { }

    public bool IsValid()
    {
      return IsOfSameSuit() && IsInSequence()
        && IsTopCardOfHigherValueThanBottomCard();
    }

    private bool IsOfSameSuit()
    {
      return _topCard.Suit == _bottomCard.Suit;
    }

    private bool IsTopCardOfHigherValueThanBottomCard()
    {
      return _topCard.ValueInt > _bottomCard.ValueInt;
    }
  }
}
