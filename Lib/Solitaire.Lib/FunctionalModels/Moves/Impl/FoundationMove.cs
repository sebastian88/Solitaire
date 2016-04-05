using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.Moves.Impl
{
  public class FoundationMove : BaseMove, Move
  {

    public FoundationMove(Card topcard, Card bottomCard)
      : base(topcard, bottomCard)
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
