using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Ententions;

namespace Solitaire.Lib.Objects
{
  public class Hand : IPopableStack, ICloneable
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

    public void SetFaceUpCards(List<Card> faceUpCards)
    {
      _faceUpCards = faceUpCards;
    }

    public void SetFaceDownCards(List<Card> faceDownCards)
    {
      _faceDownCards = faceDownCards;
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
      MoveAllCardsFromFaceDownToFaceUp();

      Card removedCard = RemoveCardFromFaceUpCardsAndMoveCardsAfterToFaceDown(cardToRemove);

      return removedCard;
    }

    private void MoveAllCardsFromFaceDownToFaceUp()
    {
      _faceUpCards.AddRange(PopCards(_faceDownCards.Count()));
    }

    private Card RemoveCardFromFaceUpCardsAndMoveCardsAfterToFaceDown(Card cardToRemove)
    {
      Card removedCard = null;
      int indexOfCardToRemove = _faceUpCards.IndexOf(cardToRemove);
      if (indexOfCardToRemove != -1)
      {
        removedCard = RemoveCardfromFaceUpCards(indexOfCardToRemove);
        _faceDownCards = RemoveRangeFromFaceUpCards(indexOfCardToRemove);
      }
      return removedCard;
    }

    private Card RemoveCardfromFaceUpCards(int indexOfCardToRemove)
    {
      Card removedCard = _faceUpCards[indexOfCardToRemove];
      _faceUpCards.RemoveAt(indexOfCardToRemove);
      return removedCard;
    }

    private List<Card> RemoveRangeFromFaceUpCards(int index)
    {
      int cardsToRemove = _faceUpCards.Count - index;
      List<Card> cardsAfterRemovedCard = _faceUpCards.GetRange(index, cardsToRemove);
      _faceUpCards.RemoveRange(index, cardsToRemove);
      return cardsAfterRemovedCard;
    }

    public Object Clone()
    {
      Hand hand = (Hand)this.MemberwiseClone();
      hand.SetFaceDownCards(_faceDownCards.Clone<Card>());
      hand.SetFaceUpCards(_faceUpCards.Clone<Card>());

      return hand;
    }
  }
}
