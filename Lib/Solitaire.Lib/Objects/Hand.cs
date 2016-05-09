using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public class Hand : IPopableStack
  {
    private readonly int DEAL_NUMBER = 3;

    private List<Card> _faceUpCards;
    private List<Card> _faceDownCards;

    public Hand()
    {
      _faceUpCards = new List<Card>();
      _faceDownCards = new List<Card>();
    }

    public Hand(List<Card> cards)
    {
      SetCards(cards);
    }

    public void SetCards(List<Card> cards)
    {
      _faceUpCards = cards;
      RecycleHand();
      Deal();
    }

    public bool IsEmpty()
    {
      return _faceUpCards.Count == 0
        && _faceDownCards.Count == 0;
    }

    public int Count()
    {
      return _faceDownCards.Count + _faceUpCards.Count;
    }

    public void Deal()
    {
      RecycleFaceDownCardsIfEmpty();

      List<Card> deltCards = PopNextCards();

      _faceUpCards.AddRange(deltCards);
    }

    private void RecycleFaceDownCardsIfEmpty()
    {
      if (_faceDownCards.Count == 0)
        RecycleHand();
    }

    private void RecycleHand()
    {
      _faceDownCards = _faceUpCards;
      _faceUpCards = new List<Card>();
    }

    private List<Card> PopNextCards()
    {
      int numberOfCardsToPop = Math.Min(DEAL_NUMBER, _faceDownCards.Count);
      return PopCards(numberOfCardsToPop);
    }

    private List<Card> PopCards(int numberOfCardsToPop)
    {
      List<Card> poppedCards = _faceDownCards.Take(numberOfCardsToPop).ToList();
      _faceDownCards.RemoveRange(0, numberOfCardsToPop);
      return poppedCards;
    }

    public Card ViewTopCard()
    {
      if (IsFaceUpCardsEmpty())
        Deal();

      if (!IsHandEmpty())
        return _faceUpCards.Last();
      else
        return null;

    }

    private bool IsHandEmpty()
    {
      return _faceUpCards.Count + _faceDownCards.Count == 0;
    }

    public Card PopTopCard()
    {
      Card card = ViewTopCard();
      _faceUpCards.RemoveAt(_faceUpCards.Count - 1);
      return card;
    }

    private bool IsFaceUpCardsEmpty()
    {
      return _faceUpCards.Count == 0;
    }

    public Card RemoveCard(Card cardToRemove)
    { 
      Card removedCard = RemoveCardFromFaceDownCards(cardToRemove);
      if (removedCard == null)
        removedCard = RemoveCardFromFaceUpCards(cardToRemove);

      // TODO set cards to be the starting from the card removed. 
      return removedCard;
    }

    private Card RemoveCardFromFaceDownCards(Card cardToRemove)
    {
      return RemoveCardFromStack(_faceDownCards, cardToRemove);
    }

    private Card RemoveCardFromFaceUpCards(Card cardToRemove)
    {
      return RemoveCardFromStack(_faceUpCards, cardToRemove);
    }

    private Card RemoveCardFromStack(List<Card> stack, Card cardToRemove)
    {
      Card removedCard = null;

      int indexInFaceDown = stack.IndexOf(cardToRemove);
      if (indexInFaceDown != -1)
      {
        removedCard = stack[indexInFaceDown];
        stack.RemoveAt(indexInFaceDown);
      }
      return removedCard;
    }
  }
}
