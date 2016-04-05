using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.FunctionalModels.Games;
using Solitaire.Lib.Services;
using Solitaire.Lib.Models;
using System.Collections.Generic;
using Solitaire.Lib.FunctionalModels.Tables;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class GameTests
  {
    private Game GenerateGame()
    {
      CardService cardService = new CardService();
      List<Card> deckOfCards = cardService.GenerateShuffledCards();
      Table table = new Table(deckOfCards);
      return new Game(table);
    }

    [TestMethod]
    public void TestMethod1()
    {
    }
  }
}
