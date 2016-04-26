using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.FunctionalModels.Games;
using Solitaire.Lib.Services;
using Solitaire.Lib.Models;
using System.Collections.Generic;
using Solitaire.Lib.FunctionalModels.Tables;
using Solitaire.Lib.Context;
using Solitaire.Lib.IoC;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class GameTests
  {
    UnitOfWork _unitOfWork;
    private GameTests()
    {
      _unitOfWork = IoCContainer.GetUnitOfWork();
    }

    private Game GenerateGame()
    {
      CardService cardService = new CardService();
      List<Card> deckOfCards = cardService.GenerateShuffledCards();
      Table table = new Table(_unitOfWork, deckOfCards);
      return new Game(table);
    }

    [TestMethod]
    public void GameTests_RunGame_ReachesEnd_NoPossibleMoves()
    {
      Game game = GenerateGame();

      game.Run();

      Assert.IsTrue(game.NoRemainingMoves());
    }
  }
}
