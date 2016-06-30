using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Enums;

namespace Solitaire.Lib.Tests.ObjectTest
{
  [TestClass]
  public class CardTests
  {
    [TestMethod]
    public void CardUtilsTests_IsSameSuit_QueenHeartsAndQueenHearts_ReturnsTrue()
    {
      Card topCard = new Card(Values.Queen, Suits.Hearts);
      Card bottomCard = new Card(Values.Queen, Suits.Hearts);

      Assert.IsTrue(topCard.IsSameSuit(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameSuit_QueenHeartsAndQueenClubs_ReturnsFalse()
    {
      Card topCard = new Card(Values.Queen, Suits.Hearts);
      Card bottomCard = new Card(Values.Queen, Suits.Clubs);

      Assert.IsFalse(topCard.IsSameSuit(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameValue_QueenHeartsAndQueenClubs_ReturnsTrue()
    {
      Card topCard = new Card(Values.Queen, Suits.Hearts);
      Card bottomCard = new Card(Values.Queen, Suits.Clubs);

      Assert.IsTrue(topCard.IsSameValue(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameValue_QueenHeartsAndTwoClubs_ReturnsFalse()
    {
      Card topCard = new Card(Values.Queen, Suits.Hearts);
      Card bottomCard = new Card(Values.Two, Suits.Clubs);

      Assert.IsFalse(topCard.IsSameValue(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameCard_FourHeartsAndFourHearts_ReturnsTrue()
    {
      Card topCard = new Card(Values.Four, Suits.Hearts);
      Card bottomCard = new Card(Values.Four, Suits.Hearts);

      Assert.IsTrue(topCard.Equals(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameCard_FourHeartsAndFiveHearts_ReturnsFalse()
    {
      Card topCard = new Card(Values.Four, Suits.Hearts);
      Card bottomCard = new Card(Values.Five, Suits.Hearts);

      Assert.IsFalse(topCard.Equals(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameCard_FiveHeartsAndFiveSpades_ReturnsFalse()
    {
      Card topCard = new Card(Values.Five, Suits.Hearts);
      Card bottomCard = new Card(Values.Five, Suits.Spades);

      Assert.IsFalse(topCard.Equals(bottomCard));
    }

    [TestMethod]
    public void CardUtilsTests_IsSameCard_FiveHeartsAndTenSpades_ReturnsFalse()
    {
      Card topCard = new Card(Values.Five, Suits.Hearts);
      Card bottomCard = new Card(Values.Ten, Suits.Spades);

      Assert.IsFalse(topCard.Equals(bottomCard));
    }
  }
}
