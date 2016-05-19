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
      _unitOfWork = IoCContainer.GetTestUnitOfWork();
    }

    private Game GenerateTestGame()
    {
      List<Card> deckOfCards = TestHelper.GenerateTestDeck();
      return new Game(_unitOfWork, deckOfCards);
    }

    private Game GenerateTestGameWith3Moves()
    {
      Table table = new Table(_unitOfWork);
      table.SetTableauStack(0, new List<Card>() { TestHelper.TWO_DIAMONDS });
      table.SetTableauStack(1, new List<Card>() { TestHelper.THREE_DIAMONDS });
      table.SetTableauStack(2, new List<Card>() { TestHelper.FOUR_DIAMONDS });
      table.SetTableauStack(3, new List<Card>() { TestHelper.FIVE_DIAMONDS });
      table.SetTableauStack(4, new List<Card>() { TestHelper.SIX_DIAMONDS });
      table.SetTableauStack(5, new List<Card>() { TestHelper.SEVEN_DIAMONDS });
      table.SetTableauStack(6, new List<Card>() { TestHelper.EIGHT_DIAMONDS });

      Hand hand = new Hand(new List<Card>()
      {
        TestHelper.QUEEN_DIAMONDS,
        TestHelper.KING_DIAMONDS,
        TestHelper.SIX_CLUBS,
        TestHelper.NINE_DIAMONDS,
        TestHelper.TEN_DIAMONDS,
        TestHelper.JACK_DIAMONDS,
      });

      table.SetHand(hand);

      return new Game(_unitOfWork, table);
    }

    [TestMethod]
    public void TableTests_GetBestMoveBranch_CountMoves_IsSeven()
    {
      Game game = GenerateTestGame();

      Assert.AreEqual(7, game.GetBestMoveBranch().Count);
    }

    [TestMethod]
    public void TableTests_GetBestMoveBranch_GameOnlyHas3Moves_BranchIs2MovesLong()
    {
      Game game = GenerateTestGameWith3Moves();

      Assert.AreEqual(3, game.GetBestMoveBranch().Count);
    }

    [TestMethod]
    public void TableTests_GetBestMoveBranch_GameOnlyHas1Branch_BranchCountIs1()
    {
      Game game = GenerateTestGameWith3Moves();

      Assert.AreEqual(1, game.GetAllBranches().Count);
    }
  }
}
