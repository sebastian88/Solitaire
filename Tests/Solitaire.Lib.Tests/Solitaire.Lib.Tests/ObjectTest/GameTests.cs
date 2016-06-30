using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using System.Collections.Generic;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;
using Solitaire.Lib.Utils;
using Solitaire.Lib.Tests.TestHelpers;
using Solitaire.Lib.Enums;

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
      List<ICard> deckOfCards = TestHelper.GenerateTestDeck();
      return new Game(_unitOfWork, deckOfCards);
    }

    private Game GenerateTestGameWith3Moves()
    {
      Table table = new Table(_unitOfWork);
      table.SetTableauStack(0, new List<ICard>() { TestHelper.TWO_DIAMONDS });
      table.SetTableauStack(1, new List<ICard>() { TestHelper.THREE_DIAMONDS });
      table.SetTableauStack(2, new List<ICard>() { TestHelper.FOUR_DIAMONDS });
      table.SetTableauStack(3, new List<ICard>() { TestHelper.FIVE_DIAMONDS });
      table.SetTableauStack(4, new List<ICard>() { TestHelper.SIX_DIAMONDS });
      table.SetTableauStack(5, new List<ICard>() { TestHelper.SEVEN_DIAMONDS });
      table.SetTableauStack(6, new List<ICard>() { TestHelper.EIGHT_DIAMONDS });

      Hand hand = new Hand(new List<ICard>()
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

    private Game GenerateTestGameWith3MovesAndOneCardToTurnOver()
    {
      Card JackHeartsFaceDown = new Card(Values.Jack, Suits.Hearts, false);
      Table table = new Table(_unitOfWork);
      table.SetTableauStack(0, new List<ICard>() { TestHelper.TWO_DIAMONDS });
      table.SetTableauStack(1, new List<ICard>() { TestHelper.THREE_DIAMONDS });
      table.SetTableauStack(2, new List<ICard>() { TestHelper.FOUR_DIAMONDS });
      table.SetTableauStack(3, new List<ICard>() { JackHeartsFaceDown, TestHelper.FIVE_DIAMONDS });
      table.SetTableauStack(4, new List<ICard>() { TestHelper.SIX_DIAMONDS });
      table.SetTableauStack(5, new List<ICard>() { TestHelper.SEVEN_DIAMONDS });
      table.SetTableauStack(6, new List<ICard>() { TestHelper.EIGHT_DIAMONDS });

      Hand hand = new Hand(new List<ICard>()
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

    private Game GenerateTestGameWhichIsPlayableToEndApartFromSixClubs()
    {
      Card aceDiamondsFaceDown = new Card(Values.Ace, Suits.Diamonds, false);
      Card sevenHeartsFaceDown = new Card(Values.Seven, Suits.Hearts, false);
      Table table = new Table(_unitOfWork);
      table.SetTableauStack(0, new List<ICard>() { TestHelper.TWO_DIAMONDS });
      table.SetTableauStack(1, new List<ICard>() { TestHelper.THREE_DIAMONDS });
      table.SetTableauStack(2, new List<ICard>() { TestHelper.FOUR_DIAMONDS });
      table.SetTableauStack(3, new List<ICard>() { aceDiamondsFaceDown, TestHelper.FIVE_DIAMONDS });
      table.SetTableauStack(4, new List<ICard>() { TestHelper.SIX_DIAMONDS });
      table.SetTableauStack(5, new List<ICard>() { TestHelper.SEVEN_DIAMONDS });
      table.SetTableauStack(6, new List<ICard>() { sevenHeartsFaceDown, TestHelper.EIGHT_DIAMONDS });

      Hand hand = new Hand(new List<ICard>()
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
    public void GameTests_GetBestMoveBranch_CountMoves_IsSeven()
    {
      Game game = GenerateTestGame();

      Assert.AreEqual(7, game.GetBestMoveBranch().Count);
    }

    [TestMethod]
    public void GameTests_GetBestMoveBranch_GameOnlyHas3Moves_BranchIs2MovesLong()
    {
      Game game = GenerateTestGameWith3Moves();

      Assert.AreEqual(3, game.GetBestMoveBranch().Count);
    }

    [TestMethod]
    public void GameTests_GetBestMoveBranch_GameOnlyHas1Branch_BranchCountIs1()
    {
      Game game = GenerateTestGameWith3Moves();

      Assert.AreEqual(1, game.GetAllBranches().Count);
    }

    [TestMethod]
    public void GameTests_GetBestMoveBranch_GameHas3Moves_LastCardIsMoved()
    {
      Game game = GenerateTestGameWith3Moves();

      Assert.AreEqual(1, game.GetAllBranches().Count);
    }

    [TestMethod]
    public void GameTests_Run_Make3MovesAndTurnCardOver_TurnsOverCard()
    {
      Game game = GenerateTestGameWith3MovesAndOneCardToTurnOver();

      game.Run();

      ICard shouldBeJackHearts = game.GetTable().GetTableauStack(3).ViewTopCard() as ICard;
      Assert.IsTrue(TestHelper.JACK_HEARTS.Equals(shouldBeJackHearts));
    }

    //[TestMethod]
    public void GameTests_Run_GameIsPlayableTillEnd_GameIfFinishedApartFromOneCard()
    {
      Game game = GenerateTestGameWhichIsPlayableToEndApartFromSixClubs();


      game.Run();

      ICard shouldBeSixClubs = game.GetTable().GetTableauStack(6).ViewTopCard() as ICard;
      Assert.IsTrue(TestHelper.JACK_HEARTS.Equals(shouldBeSixClubs));
    }
  }
}