using Solitaire.Lib.Config.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public class BaseStack : IPushableStack, IPopableStack, ICloneable
  {
    protected List<Card> _cards;

    public BaseStack()
    {
      _cards = new List<Card>();
    }

    public BaseStack(List<Card> cards)
    {
      _cards = cards;
    }

    public void SetCards(List<Card> cards)
    {
      _cards = cards;
    }

    public void PushTopCard(Card card)
    {
      _cards.Add(card);
    }

    public bool IsEmpty()
    {
      return _cards.Count == 0;
    }

    public Card ViewTopCard()
    {
      Card lastCard = new Card(Enums.Values.NotACard, Enums.Suits.NotACard);
      if (!IsEmpty())
        lastCard = _cards.Last();
      return lastCard;
    }

    public Card PopTopCard()
    {
      Card card = ViewTopCard();
      if (!IsEmpty())
        _cards.RemoveAt(_cards.Count - 1);
      return card;
    }

    public object Clone()
    {
      BaseStack clone = (BaseStack)this.MemberwiseClone();
      clone.SetCards(_cards.Select(x => (Card)x.Clone()).ToList());
      return clone;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      foreach (Card card in _cards)
        sb.Append(card.ToString() + ", ");

      return sb.ToString();
    }
  }
}
