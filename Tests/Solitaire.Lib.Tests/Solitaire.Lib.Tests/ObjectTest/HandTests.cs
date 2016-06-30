using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using System.Collections.Generic;
using Solitaire.Lib.Tests.TestHelpers;
using Solitaire.Lib.Enums;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class HandTests
  {
    private Hand _hand;

    private void Setup()
    {
      _hand = new Hand(new List<ICard>()
      {
        new Card(Values.Ace, Suits.Diamonds),
        new Card(Values.Five, Suits.Hearts),
        new Card(Values.Jack, Suits.Hearts),
        new Card(Values.King, Suits.Clubs),
        new Card(Values.Six, Suits.Clubs),
        new Card(Values.Two, Suits.Spades),
        new Card(Values.Ten, Suits.Diamonds),
        new Card(Values.Six, Suits.Spades),
        new Card(Values.Six, Suits.Hearts),
        new Card(Values.Ace, Suits.Clubs),
        new Card(Values.Eight, Suits.Hearts),
      });
    }

    [TestMethod]
    public void HandTests_Deal_GetThirdCard_IsJackHearts()
    {
      Setup();

      ICard card = _hand.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Jack, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void HandTests_Deal_GetSixthCard_IsTwoSpades()
    {
      Setup();
      
      _hand.Deal();
      ICard card = _hand.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Two, card.Value());
      Assert.AreEqual(Suits.Spades, card.Suit());
    }

    [TestMethod]
    public void HandTests_Deal_GetNinthCard_IsSixHearts()
    {
      Setup();
      
      _hand.Deal();
      _hand.Deal();
      ICard card = _hand.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Six, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void HandTests_Deal_GetTwelthCard_IsEightHearth()
    {
      Setup();
      
      _hand.Deal();
      _hand.Deal();
      _hand.Deal();
      ICard card = _hand.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Eight, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeThirdCard_IsJackHearts()
    {
      Setup();

      ICard card = _hand.PopTopCard() as ICard;

      Assert.AreEqual(Values.Jack, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeThirdAndSixthCard_IsTwoSpades()
    {
      Setup();

      _hand.PopTopCard();
      _hand.Deal();
      ICard card = _hand.PopTopCard() as ICard;

      Assert.AreEqual(Values.Two, card.Value());
      Assert.AreEqual(Suits.Spades, card.Suit());
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
      ICard card = _hand.PopTopCard() as ICard;

      Assert.AreEqual(Values.King, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
    }

    [TestMethod]
    public void HandTests_TakeCurrentCard_TakeThreeCards_IsTwoSpades()
    {
      Setup();

      _hand.PopTopCard();
      _hand.PopTopCard();
      _hand.PopTopCard();
      ICard card = _hand.ViewTopCard() as ICard;


      Assert.AreEqual(Values.Two, card.Value());
      Assert.AreEqual(Suits.Spades, card.Suit());
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
      ICard card = _hand.ViewTopCard() as ICard;


      Assert.AreEqual(Values.Eight, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
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
      ICard card = _hand.ViewTopCard() as ICard;


      Assert.AreEqual(Values.Ace, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
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
      ICard card = _hand.ViewTopCard() as ICard;


      Assert.AreEqual(Values.Eight, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
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
      ICard card = _hand.ViewTopCard() as ICard;


      Assert.AreEqual(Values.King, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
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
      ICard card = _hand.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Ace, card.Value());
      Assert.AreEqual(Suits.Clubs, card.Suit());
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
      ICard card = _hand.ViewTopCard() as ICard;

      Assert.AreEqual(Values.Eight, card.Value());
      Assert.AreEqual(Suits.Hearts, card.Suit());
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardWhichIsNotInHand_ReturnsNull()
    {
      Setup();

      ICard card = _hand.RemoveCard(TestHelper.TWO_DIAMONDS);

      Assert.IsNull(card);
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardWhichIsNotInHand_HandIsUnchanged()
    {
      Setup();

      ICard card = _hand.RemoveCard(TestHelper.TWO_DIAMONDS);

      Assert.AreEqual(11, _hand.Count());
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardFromPack_HandHasOneLessCard()
    {
      Setup();

      ICard card = _hand.RemoveCard(TestHelper.SIX_HEARTS);

      Assert.AreEqual(10, _hand.Count());
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardFromPack_HandDoesNotContainThatCard()
    {
      Setup();

      ICard card = _hand.RemoveCard(TestHelper.SIX_HEARTS);

      Assert.IsNull(_hand.RemoveCard(TestHelper.SIX_HEARTS));
    }

    [TestMethod]
    public void HandTests_RemoveCard_RemoveCardFromPack_CardReturnedIsThatCard()
    {
      Setup();

      ICard card = _hand.RemoveCard(TestHelper.SIX_HEARTS);

      Assert.AreEqual(TestHelper.SIX_HEARTS.SuitInt(), card.SuitInt());
      Assert.AreEqual(TestHelper.SIX_HEARTS.ValueInt(), card.ValueInt());
    }

    [TestMethod]
    public void HandTests_RemoveCard_ViewTopCardInHand_IsCardBehindRemovedCard()
    {
      Setup();

      ICard card = _hand.RemoveCard(TestHelper.TWO_SPADES);

      Assert.AreEqual(TestHelper.SIX_CLUBS.SuitInt(), _hand.ViewTopCard().SuitInt());
      Assert.AreEqual(TestHelper.SIX_CLUBS.ValueInt(), _hand.ViewTopCard().ValueInt());
    }
  }
}
