using Solitaire.Lib.Config;
using Solitaire.Lib.Context;
using Solitaire.Lib.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public abstract class FoundationMove : BaseMove
  {

    public FoundationMove(IUnitOfWork unitOfWork, Card topcard, Card bottomCard)
      : base(unitOfWork, topcard, bottomCard)
    { }

    public bool IsValid()
    {
      if (IsFirstCardOnStack())
        return TopCardIsAce();
      else
        return IsOfSameSuit() && IsInSequence()
          && IsTopCardOfHigherValueThanBottomCard();
    }

    private bool TopCardIsAce()
    {
      return _topCard.Value == Enums.Values.Ace;
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
