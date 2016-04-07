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
  public abstract class TableauMove : BaseMove
  {
    public TableauMove(UnitOfWork unitOfWork, Card topCard, Card bottomCard)
      : base (unitOfWork, topCard, bottomCard)
    {
    }

    public bool IsValid()
    {
      return IsTopCardOneValueLowerThanBottomCard()
        && IsDifferentColourSuit();
    }

    private bool IsTopCardOneValueLowerThanBottomCard()
    {
      return IsTopCardOfLowerValueThanBottomCard()
        && IsInSequence();
    }

    private bool IsTopCardOfLowerValueThanBottomCard()
    {
      return _topCard.ValueInt - _bottomCard.ValueInt < 0;
    }

    private bool IsDifferentColourSuit()
    {
      return IsOdd(_topCard.SuitInt + _bottomCard.SuitInt);
    }

    private bool IsOdd(int value)
    {
      return value % 2 != 0;
    }
  }
}
