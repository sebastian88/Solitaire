using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Models;
using Solitaire.Lib.Services;
using System.Collections.Generic;

namespace Solitaire.Lib.Tests.ServicesTests
{
  [TestClass]
  public class CardServiceTests
  {
    private CardService _cardService;
    public CardServiceTests()
    {
      _cardService = new CardService();
    }

    [TestMethod]
    public void GenerateShuffledCards_CountCards_Is52()
    {
      List<Card> cards = _cardService.GenerateShuffledCards();

      Assert.IsTrue(cards.Count == 52);
    }
  }
}
