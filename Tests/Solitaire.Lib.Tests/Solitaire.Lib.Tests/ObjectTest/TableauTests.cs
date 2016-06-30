using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using System.Collections.Generic;
using Solitaire.Lib.Tests.TestHelpers;
using Solitaire.Lib.Enums;
using Solitaire.Lib.Utils;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class TableauTests
  {

    public TableauTests()
    {

    }

    private TableauStack Setup5Cards2FaceUp()
    {
      List<ICard> cards = new List<ICard>();
      cards.Add(new Card(Values.Eight, Suits.Hearts, false));
      cards.Add(new Card(Values.Six, Suits.Hearts, false));
      cards.Add(new Card(Values.Two, Suits.Diamonds, false));
      cards.Add(new Card(Values.Queen, Suits.Spades));
      cards.Add(new Card(Values.Five, Suits.Clubs));
      return new TableauStack(cards, 0);
    }

    private TableauStack Setup5Cards3FaceUp()
    {
      List<ICard> cards = new List<ICard>();
      cards.Add(new Card(Values.Eight, Suits.Hearts, false));
      cards.Add(new Card(Values.Six, Suits.Hearts, false));
      cards.Add(new Card(Values.Two, Suits.Diamonds));
      cards.Add(new Card(Values.Queen, Suits.Spades));
      cards.Add(new Card(Values.Five, Suits.Clubs));
      return new TableauStack(cards, 0);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_EmptyStack_IsNotACard()
    {
      TableauStack tableau = new TableauStack(new List<ICard>(), 0);

      ICard card = tableau.PopTopCard() as ICard;

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeTopCard_IsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      ICard card = tableau.PopTopCard() as ICard;
      
      Assert.AreEqual(Values.Five, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeTopCard_IsFaceUpCard()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      ICard card = tableau.PopTopCard() as ICard;
      
      Assert.IsTrue(card.IsFaceUp());
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSecondCard_IsQueenSpades()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      ICard card = tableau.PopTopCard() as ICard;
      
      Assert.AreEqual(Values.Queen, card.Value());
      Assert.AreEqual(Suits.Spades, card.Suit());
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSecondCard_IsFaceUp()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      ICard card = tableau.PopTopCard() as ICard;

      Assert.IsTrue(card.IsFaceUp());
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeFourthCard_IsSixHearts()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      ICard card = tableau.PopTopCard() as ICard;

      Assert.AreEqual(Values.Six, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeFourthCard_IsFaceUp()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      ICard card = tableau.PopTopCard() as ICard;

      Assert.IsFalse(card.IsFaceUp());
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSixthCard_IsNotACard()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      ICard card = tableau.PopTopCard() as ICard;

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_EmptyStack_IsNotACard()
    {
      TableauStack tableau = new TableauStack(new List<ICard>(), 0);

      ICard card = tableau.ViewTopCard() as ICard;

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFirstCard_IsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      ICard card = tableau.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Five, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFifthCard_IsEightHearts()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      ICard card = tableau.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Eight, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFifthCard_IsFaceDown()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      ICard card = tableau.ViewTopCard() as ICard;

      Assert.IsFalse(card.IsFaceUp());
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewSixthCard_IsNotACard()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      ICard card = tableau.ViewTopCard() as ICard;

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewTopCardTwice_IsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.ViewTopCard();
      ICard card = tableau.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Five, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
    }

    [TestMethod]
    public void TableauTests_IsEmpty_EmptyStack_IsTrue()
    {
      TableauStack tableau = new TableauStack(new List<ICard>(), 0);

      bool isEmpty = tableau.IsEmpty();

      Assert.IsTrue(isEmpty);
    }

    [TestMethod]
    public void TableauTests_IsEmpty_FullStack_IsFalse()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      bool isEmpty = tableau.IsEmpty();

      Assert.IsFalse(isEmpty);
    }

    [TestMethod]
    public void TableauTests_ViewMovableCard_EmptyStack_IsNull()
    {
      TableauStack tableau = new TableauStack(new List<ICard>(), 0);

      ICard card = tableau.ViewFirstFaceUpCard() as ICard;

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewMovableCard_2CardsFaceUp_IsQueenSpades()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      ICard card = tableau.ViewFirstFaceUpCard() as ICard;
      
      Assert.AreEqual(Values.Queen, card.Value());
      Assert.AreEqual(Suits.Spades, card.Suit());
    }

    [TestMethod]
    public void TableauTests_ViewMovableCard_3CardsFaceUp_IsTwoDiamonds()
    {
      TableauStack tableau = Setup5Cards3FaceUp();

      ICard card = tableau.ViewFirstFaceUpCard() as ICard;

      Assert.AreEqual(Values.Two, card.Value());
      Assert.AreEqual(Suits.Diamonds, card.Suit());
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_NotInStack_ReturnsEmptyList()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      ICard foundCard = tableau.RemoveCardAndCardsOnTop(new Card(Values.Jack, Suits.Diamonds));

      Assert.AreEqual(foundCard, null);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_FirstCardInStack_CountIs1()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Values.Five, Suits.Clubs);

      ICard foundCard = tableau.RemoveCardAndCardsOnTop(cardToFind);

      Assert.AreEqual(foundCard.GetNext(), null);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_FirstCardInStack_FirstCardIsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Values.Five, Suits.Clubs);

      ICard foundCard = tableau.RemoveCardAndCardsOnTop(cardToFind);
      
      Assert.AreEqual(Values.Five, foundCard.Value());
      Assert.AreEqual(Suits.Clubs, foundCard.Suit());
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_CountIs2()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Values.Queen, Suits.Spades);

      ICard foundCards = tableau.RemoveCardAndCardsOnTop(cardToFind);

      Assert.AreEqual(CardUtils.CountCards(foundCards), 2);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_SecondCardIsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Values.Queen, Suits.Spades);

      ICard foundCard = tableau.RemoveCardAndCardsOnTop(cardToFind).GetNext() as ICard;

      Assert.AreEqual(Values.Five, foundCard.Value());
      Assert.AreEqual(Suits.Clubs, foundCard.Suit());
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_FirstCardIsQueenSpades()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Values.Queen, Suits.Spades);

      ICard foundCards = tableau.RemoveCardAndCardsOnTop(cardToFind);
      
      Assert.AreEqual(Values.Queen, foundCards.Value());
      Assert.AreEqual(Suits.Spades, foundCards.Suit());
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_TableauCoundIs3()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Values.Queen, Suits.Spades);

      ICard foundCards = tableau.RemoveCardAndCardsOnTop(cardToFind);

      Assert.AreEqual(tableau.CountCards(), 3);
    }
  }
} 