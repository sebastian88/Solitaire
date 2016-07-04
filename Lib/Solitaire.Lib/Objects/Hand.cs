using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Ententions;
using Solitaire.Lib.Comparers;

namespace Solitaire.Lib.Objects
{
  public class Hand : IPopableStack, ISearchableStack, ICloneable
  {
    private readonly int DEAL_NUMBER = 3;

    private List<ICard> _faceUpCards;
    private List<ICard> _faceDownCards;

    public Hand()
    {
      _faceUpCards = new List<ICard>();
      _faceDownCards = new List<ICard>();
    }

    public Hand(List<ICard> cards)
    {
      SetCards(cards);
    }

    public void SetCards(List<ICard> cards)
    {
      _faceUpCards = cards;
      RecycleHand();
      Deal();
    }

    public void SetFaceUpCards(List<ICard> faceUpCards)
    {
      _faceUpCards = faceUpCards;
    }

    public void SetFaceDownCards(List<ICard> faceDownCards)
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

      List<ICard> deltCards = PopNextCards();

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
      _faceUpCards = new List<ICard>();
    }

    private List<ICard> PopNextCards()
    {
      int numberOfCardsToPop = Math.Min(DEAL_NUMBER, _faceDownCards.Count);
      return PopCards(numberOfCardsToPop);
    }

    private List<ICard> PopCards(int numberOfCardsToPop)
    {
      List<ICard> poppedCards = _faceDownCards.Take(numberOfCardsToPop).ToList();
      _faceDownCards.RemoveRange(0, numberOfCardsToPop);
      return poppedCards;
    }

    public IStackable ViewTopCard()
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

    public IStackable PopTopCard()
    {
      IStackable card = ViewTopCard();
      _faceUpCards.RemoveAt(_faceUpCards.Count - 1);
      return card;
    }

    private bool IsFaceUpCardsEmpty()
    {
      return _faceUpCards.Count == 0;
    }

    public ICard RemoveCard(ICard cardToRemove)
    {
      ICard removedCard = null;
      if (ContainsCard(cardToRemove))
      {
        MoveAllCardsFromFaceDownToFaceUp();
        removedCard = RemoveCardFromFaceUpCardsAndMoveCardsAfterToFaceDown(cardToRemove);
      }
      return removedCard;
    }

    private bool ContainsCard(ICard cardToFind)
    {
      bool isCardFound = false;
      foreach (ICard card in _faceDownCards.Concat(_faceUpCards))
        if (card.Equals(cardToFind))
          isCardFound = true;

      return isCardFound;
    }

    private void MoveAllCardsFromFaceDownToFaceUp()
    {
      _faceUpCards.AddRange(PopCards(_faceDownCards.Count()));
    }

    private ICard RemoveCardFromFaceUpCardsAndMoveCardsAfterToFaceDown(ICard cardToRemove)
    {
      ICard removedCard = null;
      int indexOfCardToRemove = _faceUpCards.FindIndexOf<ICard>(cardToRemove, new CardComparer());
      if (indexOfCardToRemove != -1)
      {
        removedCard = RemoveCardfromFaceUpCards(indexOfCardToRemove);
        _faceDownCards = RemoveRangeFromFaceUpCards(indexOfCardToRemove);
      }
      return removedCard;
    }

    private ICard RemoveCardfromFaceUpCards(int indexOfCardToRemove)
    {
      ICard removedCard = _faceUpCards[indexOfCardToRemove];
      _faceUpCards.RemoveAt(indexOfCardToRemove);
      return removedCard;
    }

    private List<ICard> RemoveRangeFromFaceUpCards(int index)
    {
      int cardsToRemove = _faceUpCards.Count - index;
      List<ICard> cardsAfterRemovedCard = _faceUpCards.GetRange(index, cardsToRemove);
      _faceUpCards.RemoveRange(index, cardsToRemove);
      return cardsAfterRemovedCard;
    }

    public IStackable GetStackable(IStackable card)
    {
      ICard foundCard = null;
      List<ICard> allCards = _faceUpCards.Concat(_faceDownCards).ToList();
      foreach (ICard cardToCheck in allCards)
      {
        if (cardToCheck.Equals(card))
        {
          foundCard = cardToCheck;
          break;
        }
      }
      return foundCard;
    }

    public Object Clone()
    {
      Hand hand = new Hand();
      hand.SetFaceDownCards(_faceDownCards.Clone<ICard>());
      hand.SetFaceUpCards(_faceUpCards.Clone<ICard>());

      return hand;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      sb.AppendLine("FaceUpCards: ");
      foreach (ICard card in _faceUpCards)
        sb.Append(card.ToString());

      sb.AppendLine("FaceDownCards: ");
      foreach (ICard card in _faceDownCards)
        sb.Append(card.ToString());

      return sb.ToString();
    }
  }
}
