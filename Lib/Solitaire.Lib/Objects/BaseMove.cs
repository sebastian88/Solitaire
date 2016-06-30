using Solitaire.Lib.Config.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Enums;
using Solitaire.Lib.Objects.Interfaces;

namespace Solitaire.Lib.Objects
{
  public abstract class BaseMove
  {
    protected IUnitOfWork _unitOfWork;
    protected IStackable _topCard;
    protected IStackable _bottomCard;

    public BaseMove(IUnitOfWork unitOfWork, IStackable topcard, IStackable bottomCard)
    {
      _unitOfWork = unitOfWork;
      _topCard = topcard;
      _bottomCard = bottomCard;
    }

    public IStackable GetBottomCard()
    {
      if (_bottomCard == null)
        return new Card(Values.NotACard, Suits.NotACard);
      else
        return _bottomCard;
    }

    public IStackable GetTopCard()
    {
      return _topCard;
    }

    public virtual bool IsValid()
    {
      return AreBothCardsFaceUp();
    }

    protected bool AreBothCardsFaceUp()
    {
      return _topCard.IsFaceUp() && _bottomCard.IsFaceUp();
    }

    protected bool IsInSequence()
    {
      return Math.Abs(_topCard.ValueInt() - _bottomCard.ValueInt()) == 1;
    }
    
    public object Clone()
    {
      var clone = this.MemberwiseClone();
      return clone;
    }

    public override string ToString()
    {
      return _topCard.ToString() + " on to " + _bottomCard.ToString();
    }
  }
}
