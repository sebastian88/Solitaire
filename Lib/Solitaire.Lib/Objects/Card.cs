using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib;
using Solitaire.Lib.Utils;
using Solitaire.Lib.Enums;
using Solitaire.Lib.Objects.Interfaces;

namespace Solitaire.Lib.Objects
{
  public class Card : ICard
  {
    private bool _isFaceUp;
    private Values _value;
    private Suits _suit;
    private IStackable _next;

    public Card(Values value, Suits suit)
    {
      _value = value;
      _suit = suit;
      _isFaceUp = true;
    }
    public Card(Values value, Suits suit, bool isFaceUp)
      : this(value, suit)
    {
      _isFaceUp = isFaceUp;
    }

    public bool IsFaceUp()
    {
      return _isFaceUp;
    }

    public Values Value()
    {
      return _value;
    }

    public int ValueInt()
    {
      return (int)_value;
    }

    public Suits Suit()
    {
      return _suit;
    }

    public int SuitInt()
    {
      return (int)_suit;
    } 

    public void TurnFaceUp()
    {
      _isFaceUp = true;
    }


    public bool Equals(IStackable other)
    {
      ICard card = other as ICard;
      if (card == null)
        return false;
      else
        return IsSameValue(card) && IsSameSuit(card);
    }

    public bool IsSameSuit(ICard other)
    {
      return SuitInt() == other.SuitInt();
    }

    public bool IsSameValue(ICard other)
    {
      return ValueInt() == other.ValueInt();
    }

    public object Clone()
    {
      Card clone = new Card(_value, _suit, _isFaceUp);
      if (_next != null && !_next.IsHead())
        clone.SetNext((IStackable)_next.Clone());
      return clone;
    }

    public override string ToString()
    {
      return _value.ToString() + " " + _suit.ToString() + "(" + (_isFaceUp ? "faceup" : "facedown") + ")";
    }

    public IStackable GetNext()
    {
      return _next;
    }

    public void SetNext(IStackable stackableItem)
    {
      _next = stackableItem;
    }

    public bool IsHead()
    {
      return false;
    }

    public bool CanBeStackedOnByAnySuit()
    {
      return false;
    }

    public IHeadOfStack GetHeadOfStack()
    {
      IStackable card = GetNext();
      while (!card.IsHead())
        card = card.GetNext();
      return card as IHeadOfStack;
    }
  }
}
