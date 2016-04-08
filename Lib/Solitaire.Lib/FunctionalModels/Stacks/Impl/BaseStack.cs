using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.Stacks.Impl
{
  public class BaseStack : PushableStack, PopableStack
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
      Card lastCard = null;
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
  }
}
