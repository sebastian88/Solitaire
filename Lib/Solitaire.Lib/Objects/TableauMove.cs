using Solitaire.Lib.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public abstract class TableauMove : BaseMove
  {
    public TableauMove(IUnitOfWork unitOfWork, Card topCard, Card bottomCard)
      : base (unitOfWork, topCard, bottomCard)
    {
    }

    public bool IsValid()
    {
      if (IsFirstCardOnStack())
        return IsTopCardKing();
      else
        return IsTopCardOneValueLowerThanBottomCard()
          && IsDifferentColourSuit();
    }

    private bool IsTopCardKing()
    {
      return _topCard.Value == Enums.Values.King;
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
