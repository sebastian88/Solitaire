using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Utils;
using System.Collections.Generic;

namespace Solitaire.Lib.Tests.UtilsTests
{
  [TestClass]
  public class CardUtilsTests
  {
    [TestMethod]
    public void CardServiceTests_GenerateShuffledCards_CountCards_Is52()
    {
      List<Card> cards = CardUtils.GenerateShuffledCards();

      Assert.IsTrue(cards.Count == 52);
    }
  }
}
