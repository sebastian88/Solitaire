using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Models;

namespace Solitaire.Lib.FunctionalModels.StackModels.Impl
{
  public class TableauStack : PushableStack, PopableStack
  {
    private List<Card> _cards;

    public TableauStack()
    {
      _cards = new List<Card>();
    }

    public TableauStack(List<Card> cards)
    {
      _cards = cards;
    }

    public bool IsEmpty()
    {
      return _cards.Count == 0;
    }

    public int Count()
    {
      return _cards.Count;
    }

    public Card PopTopCard()
    {
      Card card = ViewTopCard();
      if(!IsEmpty())
        _cards.RemoveAt(_cards.Count - 1);
      return card;
    }

    public void TurnoverTopCardIfNecessary()
    {
      if (!IsEmpty())
        ViewTopCard().IsFaceUp = true;
    }

    public void PushTopCard(Card card)
    {
      _cards.Add(card);
    }

    public Card ViewTopCard()
    {
      Card lastCard = null;
      if(!IsEmpty())
        lastCard = _cards.Last();
      return lastCard;
    }
  }
}