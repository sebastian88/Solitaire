using Solitaire.Lib.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Enums;
using Solitaire.Lib.Objects.Interfaces;

namespace Solitaire.Lib.Objects
{
  public abstract class TableauMove : BaseMove
  {
    public TableauMove(IUnitOfWork unitOfWork, IStackable topCard, IStackable bottomCard)
      : base (unitOfWork, topCard, bottomCard)
    {
    }

    public override bool IsValid()
    {
      bool isValid = false;
      if (base.IsValid())
      {
        isValid = IsTopCardOneValueLowerThanBottomCard()
          && IsDifferentColourSuit()
          && TopCardIsValidCard();
      }
      return isValid;
    }

    private bool TopCardIsValidCard()
    {
      return !_topCard.Equals(new Card(Values.NotACard, Suits.NotACard));
    }

    private bool IsTopCardOneValueLowerThanBottomCard()
    {
      return IsTopCardOfLowerValueThanBottomCard()
        && IsInSequence();
    }

    private bool IsTopCardOfLowerValueThanBottomCard()
    {
      return _topCard.ValueInt() - _bottomCard.ValueInt() < 0;
    }

    private bool IsDifferentColourSuit()
    {
      if (_bottomCard.CanBeStackedOnByAnySuit())
        return true;
      else
        return IsOdd(_topCard.SuitInt() + _bottomCard.SuitInt());
    }

    private bool IsOdd(int value)
    {
      return value % 2 != 0;
    }
  }
}
