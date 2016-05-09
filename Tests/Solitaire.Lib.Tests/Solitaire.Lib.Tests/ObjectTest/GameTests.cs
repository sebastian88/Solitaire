using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using System.Collections.Generic;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;
using Solitaire.Lib.Utils;
using Solitaire.Lib.Tests.TestHelpers;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class GameTests
  {
    IUnitOfWork _unitOfWork;
    public GameTests()
    {
      _unitOfWork = IoCContainer.GetUnitOfWork();
    }

    private Game GenerateTestGame()
    {
      List<Card> deckOfCards = TestHelper.GenerateTestDeck();
      Table table = new Table(_unitOfWork, deckOfCards);
      return new Game(table);
    }

    [TestMethod]
    public void GameTests_RunGame_ReachesEnd_NoPossibleMoves()
    {
      Game game = GenerateTestGame();

      game.Run();

      Assert.IsTrue(game.NoRemainingMoves());
    }

    [TestMethod]
    public void TableTests_GetBestMoveBranch_CountMoves_IsFour()
    {
      Game game = GenerateTestGame();

      Assert.AreEqual(4, game.GetBestMoveBranch());
    }
  }
}
