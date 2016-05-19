using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using System.Collections.Generic;
using Solitaire.Lib.Tests.TestHelpers;

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
      List<Card> cards = new List<Card>();
      cards.Add(new Card(Enums.Values.Eight, Enums.Suits.Hearts, false));
      cards.Add(new Card(Enums.Values.Six, Enums.Suits.Hearts, false));
      cards.Add(new Card(Enums.Values.Two, Enums.Suits.Diamonds, false));
      cards.Add(new Card(Enums.Values.Queen, Enums.Suits.Spades));
      cards.Add(new Card(Enums.Values.Five, Enums.Suits.Clubs));
      return new TableauStack(cards);
    }

    private TableauStack Setup5Cards3FaceUp()
    {
      List<Card> cards = new List<Card>();
      cards.Add(new Card(Enums.Values.Eight, Enums.Suits.Hearts, false));
      cards.Add(new Card(Enums.Values.Six, Enums.Suits.Hearts, false));
      cards.Add(new Card(Enums.Values.Two, Enums.Suits.Diamonds));
      cards.Add(new Card(Enums.Values.Queen, Enums.Suits.Spades));
      cards.Add(new Card(Enums.Values.Five, Enums.Suits.Clubs));
      return new TableauStack(cards);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_EmptyStack_IsNotACard()
    {
      TableauStack tableau = new TableauStack(new List<Card>());

      Card card = tableau.PopTopCard();

      Assert.IsTrue(TestHelper.NOT_A_CARD.Equals(card));
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeTopCard_IsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      Card card = tableau.PopTopCard();
      
      Assert.AreEqual(Enums.Values.Five, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeTopCard_IsFaceUpCard()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      Card card = tableau.PopTopCard();
      
      Assert.IsTrue(card.IsFaceUp);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSecondCard_IsQueenSpades()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      Card card = tableau.PopTopCard();
      
      Assert.AreEqual(Enums.Values.Queen, card.Value);
      Assert.AreEqual(Enums.Suits.Spades, card.Suit);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSecondCard_IsFaceUp()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      Card card = tableau.PopTopCard();

      Assert.IsTrue(card.IsFaceUp);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeFourthCard_IsSixHearts()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.PopTopCard();

      Assert.AreEqual(Enums.Values.Six, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeFourthCard_IsFaceUp()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.PopTopCard();

      Assert.IsFalse(card.IsFaceUp);
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
      Card card = tableau.PopTopCard();

      Assert.IsTrue(TestHelper.NOT_A_CARD.Equals(card));
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_EmptyStack_IsNotACard()
    {
      TableauStack tableau = new TableauStack(new List<Card>());

      Card card = tableau.ViewTopCard();

      Assert.IsTrue(TestHelper.NOT_A_CARD.Equals(card));
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFirstCard_IsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      Card card = tableau.ViewTopCard();

      Assert.AreEqual(Enums.Values.Five, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFifthCard_IsEightHearts()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.ViewTopCard();

      Assert.AreEqual(Enums.Values.Eight, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFifthCard_IsFaceDown()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.ViewTopCard();

      Assert.IsFalse(card.IsFaceUp);
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
      Card card = tableau.ViewTopCard();

      Assert.IsTrue(TestHelper.NOT_A_CARD.Equals(card));
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewTopCardTwice_IsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      tableau.ViewTopCard();
      Card card = tableau.ViewTopCard();

      Assert.AreEqual(Enums.Values.Five, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void TableauTests_IsEmpty_EmptyStack_IsTrue()
    {
      TableauStack tableau = new TableauStack(new List<Card>());

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
      TableauStack tableau = new TableauStack(new List<Card>()); 

      Card card = tableau.ViewFirstFaceUpCard();

      Assert.IsTrue(TestHelper.NOT_A_CARD.Equals(card));
    }

    [TestMethod]
    public void TableauTests_ViewMovableCard_2CardsFaceUp_IsQueenSpades()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      Card card = tableau.ViewFirstFaceUpCard();
      
      Assert.AreEqual(Enums.Values.Queen, card.Value);
      Assert.AreEqual(Enums.Suits.Spades, card.Suit);
    }

    [TestMethod]
    public void TableauTests_ViewMovableCard_3CardsFaceUp_IsTwoDiamonds()
    {
      TableauStack tableau = Setup5Cards3FaceUp();

      Card card = tableau.ViewFirstFaceUpCard();

      Assert.AreEqual(Enums.Values.Two, card.Value);
      Assert.AreEqual(Enums.Suits.Diamonds, card.Suit);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_NotInStack_ReturnsEmptyList()
    {
      TableauStack tableau = Setup5Cards2FaceUp();

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(new Card(Enums.Values.Jack, Enums.Suits.Diamonds));

      Assert.AreEqual(foundCards.Count, 0);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_FirstCardInStack_CountIs1()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Enums.Values.Five, Enums.Suits.Clubs);

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(cardToFind);

      Assert.AreEqual(foundCards.Count, 1);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_FirstCardInStack_FirstCardIsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Enums.Values.Five, Enums.Suits.Clubs);

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(cardToFind);
      
      Assert.AreEqual(Enums.Values.Five, foundCards[0].Value);
      Assert.AreEqual(Enums.Suits.Clubs, foundCards[0].Suit);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_CountIs2()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Enums.Values.Queen, Enums.Suits.Spades);

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(cardToFind);

      Assert.AreEqual(foundCards.Count, 2);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_SecondCardIsFiveClubs()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Enums.Values.Queen, Enums.Suits.Spades);

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(cardToFind);
      
      Assert.AreEqual(Enums.Values.Five, foundCards[1].Value);
      Assert.AreEqual(Enums.Suits.Clubs, foundCards[1].Suit);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_FirstCardIsQueenSpades()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Enums.Values.Queen, Enums.Suits.Spades);

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(cardToFind);
      
      Assert.AreEqual(Enums.Values.Queen, foundCards[0].Value);
      Assert.AreEqual(Enums.Suits.Spades, foundCards[0].Suit);
    }

    [TestMethod]
    public void TableauTests_FindCardAndCardsOnTop_SecondCardInStack_TableauCoundIs3()
    {
      TableauStack tableau = Setup5Cards2FaceUp();
      Card cardToFind = new Card(Enums.Values.Queen, Enums.Suits.Spades);

      List<Card> foundCards = tableau.FindCardAndCardsOnTop(cardToFind);

      Assert.AreEqual(tableau.Count(), 3);
    }
  }
} 