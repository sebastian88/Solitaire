using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Utils;

namespace Solitaire.Lib.Objects
{
  public class TableauStack : BaseStack, IPushableStack, IPopableStack
  {

    public TableauStack() 
      : base()
    {
    }

    public TableauStack(List<Card> cards)
      : base(cards)
    {
    }

    public int Count()
    {
      return _cards.Count;
    }

    public void TurnoverTopCardIfNecessary()
    {
      if (!IsEmpty())
        ViewTopCard().IsFaceUp = true;
    }

    public Card ViewFirstFaceUpCard()
    {
      Card firstFaceUpCard = null;
      foreach(Card card in _cards.Reverse<Card>())
      {
        if (card.IsFaceUp)
          firstFaceUpCard = card;
        else
          break;
      }
      return firstFaceUpCard;
    }

    public List<Card> FindCardAndCardsOnTop(Card cardToFind)
    {
      List<Card> foundCardStack = new List<Card>();
      int indexOfCardToFind = _cards.IndexOf(cardToFind);

      if (indexOfCardToFind != -1)
        foundCardStack = RemoveCardsFromEndOfStack(indexOfCardToFind);

      return foundCardStack;
    }

    private List<Card> RemoveCardsFromEndOfStack(int indexToStartFrom)
    {
      int numberOfCardsToRemove = _cards.Count - indexToStartFrom;

      List<Card> removedCards = _cards.GetRange(indexToStartFrom, numberOfCardsToRemove);
      _cards.RemoveRange(indexToStartFrom, numberOfCardsToRemove);

      return removedCards;
    }
  }
}