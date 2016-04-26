using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.FunctionalModels.Stacks.Impl;
using System.Collections.Generic;
using Solitaire.Lib.Models;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class TableauTests
  {

    public TableauTests()
    {

    }

    private TableauStack Setup()
    {
      List<Card> cards = new List<Card>();
      cards.Add(new Card(Enums.Values.Eight, Enums.Suits.Hearts, false));
      cards.Add(new Card(Enums.Values.Six, Enums.Suits.Hearts, false));
      cards.Add(new Card(Enums.Values.Two, Enums.Suits.Diamonds, false));
      cards.Add(new Card(Enums.Values.Queen, Enums.Suits.Spades));
      cards.Add(new Card(Enums.Values.Five, Enums.Suits.Clubs));
      return new TableauStack(cards);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_EmptyStack_IsNull()
    {
      TableauStack tableau = new TableauStack(new List<Models.Card>());

      Card card = tableau.PopTopCard();

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeTopCard_IsFiveClubs()
    {
      TableauStack tableau = Setup();

      Card card = tableau.PopTopCard();
      
      Assert.AreEqual(Enums.Values.Five, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeTopCard_IsFaceUpCard()
    {
      TableauStack tableau = Setup();

      Card card = tableau.PopTopCard();
      
      Assert.IsTrue(card.IsFaceUp);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSecondCard_IsQueenSpades()
    {
      TableauStack tableau = Setup();

      tableau.PopTopCard();
      Card card = tableau.PopTopCard();
      
      Assert.AreEqual(Enums.Values.Queen, card.Value);
      Assert.AreEqual(Enums.Suits.Spades, card.Suit);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSecondCard_IsFaceUp()
    {
      TableauStack tableau = Setup();

      tableau.PopTopCard();
      Card card = tableau.PopTopCard();

      Assert.IsTrue(card.IsFaceUp);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeFourthCard_IsSixHearts()
    {
      TableauStack tableau = Setup();

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
      TableauStack tableau = Setup();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.PopTopCard();

      Assert.IsFalse(card.IsFaceUp);
    }

    [TestMethod]
    public void TableauTests_PopTopCard_TakeSixthCard_IsNull()
    {
      TableauStack tableau = Setup();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.PopTopCard();

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_EmptyStack_IsNull()
    {
      TableauStack tableau = new TableauStack(new List<Models.Card>());

      Card card = tableau.ViewTopCard();

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFirstCard_IsFiveClubs()
    {
      TableauStack tableau = Setup();

      Card card = tableau.ViewTopCard();

      Assert.AreEqual(Enums.Values.Five, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewFifthCard_IsEightHearts()
    {
      TableauStack tableau = Setup();

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
      TableauStack tableau = Setup();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.ViewTopCard();

      Assert.IsFalse(card.IsFaceUp);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewSixthCard_IsNull()
    {
      TableauStack tableau = Setup();

      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      tableau.PopTopCard();
      Card card = tableau.ViewTopCard();

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableauTests_ViewTopCard_ViewTopCardTwice_IsFiveClubs()
    {
      TableauStack tableau = Setup();

      tableau.ViewTopCard();
      Card card = tableau.ViewTopCard();

      Assert.AreEqual(Enums.Values.Five, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void TableauTests_IsEmpty_EmptyStack_IsTrue()
    {
      TableauStack tableau = new TableauStack(new List<Models.Card>());

      bool isEmpty = tableau.IsEmpty();

      Assert.IsTrue(isEmpty);
    }

    [TestMethod]
    public void TableauTests_IsEmpty_FullStack_IsFalse()
    {
      TableauStack tableau = Setup();

      bool isEmpty = tableau.IsEmpty();

      Assert.IsFalse(isEmpty);
    }
  }
} 