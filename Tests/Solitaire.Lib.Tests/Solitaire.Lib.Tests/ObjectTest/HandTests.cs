using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using System.Collections.Generic;
using Solitaire.Lib.Tests.TestHelpers;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class HandTests
  {
    private Hand _hand;

    private void Setup()
    {
      _hand = new Hand(new List<Card>()
      {
        new Card(Enums.Values.Ace, Enums.Suits.Diamonds),
        new Card(Enums.Values.Five, Enums.Suits.Hearts),
        new Card(Enums.Values.Jack, Enums.Suits.Hearts),
        new Card(Enums.Values.King, Enums.Suits.Clubs),
        new Card(Enums.Values.Six, Enums.Suits.Clubs),
        new Card(Enums.Values.Two, Enums.Suits.Spades),
        new Card(Enums.Values.Ten, Enums.Suits.Diamonds),
        new Card(Enums.Values.Six, Enums.Suits.Spades),
        new Card(Enums.Values.Six, Enums.Suits.Hearts),
        new Card(Enums.Values.Ace, Enums.Suits.Clubs),
        new Card(Enums.Values.Eight, Enums.Suits.Hearts),
      });
    }

    [TestMethod]
    public void HandTests_Deal_GetThirdCard_IsJackHearts()
    {
      Setup();
      
      Card card = _hand.ViewTopCard();

      Assert.AreEqual(Enums.Values.Jack, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_Deal_GetSixthCard_IsTwoSpades()
    {
      Setup();
      
      _hand.Deal();
      Card card = _hand.ViewTopCard();

      Assert.AreEqual(Enums.Values.Two, card.Value);
      Assert.AreEqual(Enums.Suits.Spades, card.Suit);
    }

    [TestMethod]
    public void HandTests_Deal_GetNinthCard_IsSixHearts()
    {
      Setup();
      
      _hand.Deal();
      _hand.Deal();
      Card card = _hand.ViewTopCard();

      Assert.AreEqual(Enums.Values.Six, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_Deal_GetTwelthCard_IsEightHearth()
    {
      Setup();
      
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      Card card = _hand.ViewTopCard();

      Assert.AreEqual(Enums.Values.Eight, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeThirdCard_IsJackHearts()
    {
      Setup();

      Card card = _hand.PopTopCard();

      Assert.AreEqual(Enums.Values.Jack, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeThirdAndSixthCard_IsTwoSpades()
    {
      Setup();

      _hand.PopTopCard();
      _hand.Deal();
      Card card = _hand.PopTopCard();

      Assert.AreEqual(Enums.Values.Two, card.Value);
      Assert.AreEqual(Enums.Suits.Spades, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeFirstCardDealFourTime_IsKingClubs()
    {
      Setup();

      _hand.PopTopCard();
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      Card card = _hand.PopTopCard();

      Assert.AreEqual(Enums.Values.King, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeThreeCards_IsTwoSpades()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      Card card = _hand.ViewTopCard();


      Assert.AreEqual(Enums.Values.Two, card.Value);
      Assert.AreEqual(Enums.Suits.Spades, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeAllBut2Cards_IsEightHearts()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      Card card = _hand.ViewTopCard();


      Assert.AreEqual(Enums.Values.Eight, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeAllBut1Cards_IsAceClubs()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      Card card = _hand.ViewTopCard();


      Assert.AreEqual(Enums.Values.Ace, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_DealWithTwoCards_IsEightHearts()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.Deal();
      Card card = _hand.ViewTopCard();


      Assert.AreEqual(Enums.Values.Eight, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeCardFromFirstDealThenDealRound_IsKingClubs()
    {
      Setup();
      
      _hand.PopTopCard();
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      Card card = _hand.ViewTopCard();


      Assert.AreEqual(Enums.Values.King, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_ReduceToFourCardsThenReDeal_IsAceClubs()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.Deal();
      _hand.Deal();
      Card card = _hand.ViewTopCard();

      Assert.AreEqual(Enums.Values.Ace, card.Value);
      Assert.AreEqual(Enums.Suits.Clubs, card.Suit);
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_ReduceToFourCardsThenReDealToEnd_IsEightHearts()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      Card card = _hand.ViewTopCard();

      Assert.AreEqual(Enums.Values.Eight, card.Value);
      Assert.AreEqual(Enums.Suits.Hearts, card.Suit);
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardWhichIsNotInHand_ReturnsNull()
    {
      Setup();
      
      Card card = _hand.RemoveCard(TestHelper.TWO_DIAMONDS);

      Assert.IsNull(card);
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardWhichIsNotInHand_HandIsUnchanged()
    {
      Setup();

      Card card = _hand.RemoveCard(TestHelper.TWO_DIAMONDS);

      Assert.AreEqual(11, _hand.Count());
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardFromPack_HandHasOneLessCard()
    {
      Setup();

      Card card = _hand.RemoveCard(TestHelper.SIX_HEARTS);

      Assert.AreEqual(10, _hand.Count());
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardFromPack_HandDoesNotContainThatCard()
    {
      Setup();

      Card card = _hand.RemoveCard(TestHelper.SIX_HEARTS);

      Assert.IsNull(_hand.RemoveCard(TestHelper.SIX_HEARTS));
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardFromPack_CardReturnedIsThatCard()
    {
      Setup();

      Card card = _hand.RemoveCard(TestHelper.SIX_HEARTS);

      Assert.AreEqual(TestHelper.SIX_HEARTS.SuitInt, card.SuitInt);
      Assert.AreEqual(TestHelper.SIX_HEARTS.ValueInt, card.ValueInt);
    }

    [TestMethod]
    public void HandTests_RemoveCard_ViewTopCardInHand_IsCardBehindRemovedCard()
    {
      Setup();

      Card card = _hand.RemoveCard(TestHelper.TWO_SPADES);

      Assert.AreEqual(TestHelper.SIX_CLUBS.SuitInt, _hand.ViewTopCard().SuitInt);
      Assert.AreEqual(TestHelper.SIX_CLUBS.ValueInt, _hand.ViewTopCard().ValueInt);
    }
  }
}
