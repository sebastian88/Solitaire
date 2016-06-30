using Solitaire.Lib.Config;
using Solitaire.Lib.Context;
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
  public abstract class FoundationMove : BaseMove
  {

    public FoundationMove(IUnitOfWork unitOfWork, IStackable topcard, IStackable bottomCard)
      : base(unitOfWork, topcard, bottomCard)
    { }

    public override bool IsValid()
    {
      bool isValid = false;
      if (base.IsValid())
      {
        isValid = IsOfSameSuit() && IsInSequence()
          && IsTopCardOfHigherValueThanBottomCard();
      }
      return isValid;
    }

    private bool IsOfSameSuit()
    {
      if (_bottomCard.CanBeStackedOnByAnySuit())
        return true;
      else
        return _topCard.SuitInt() == _bottomCard.SuitInt();
    }

    private bool IsTopCardOfHigherValueThanBottomCard()
    {
      return _topCard.ValueInt() > _bottomCard.ValueInt();
    }
  }
}
