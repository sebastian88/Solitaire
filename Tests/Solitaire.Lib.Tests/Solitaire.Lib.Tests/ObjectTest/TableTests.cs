using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;
using Solitaire.Lib.Utils;
using Solitaire.Lib.Tests.TestHelpers;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class TableTests
  {
    IUnitOfWork _unitOfWork;
    public TableTests()
    {
      _unitOfWork = IoCContainer.GetTestUnitOfWork();
    }

    [TestMethod]
    public void TableTests_Deal_CheckFirstStackCount_ReturnsOne()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(1, table.GetTableauStack(0).Count());
    }

    [TestMethod]
    public void TableTests_Deal_CheckSecondStackCount_ReturnsTwo()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(2, table.GetTableauStack(1).Count());
    }

    [TestMethod]
    public void TableTests_Deal_CheckSeventhStackCount_ReturnsSeven()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(7, table.GetTableauStack(6).Count());
    }

    [TestMethod]
    public void TableTests_Deal_CheckFirstStackFirstCard_IsFaceUp()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.IsTrue(table.GetTableauStack(0).ViewTopCard().IsFaceUp);
    }

    [TestMethod]
    public void TableTests_Deal_CheckHandCount_ReturnsTwentyFour()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(24, table.GetHand().Count());
    }

    [TestMethod]
    public void TableTests_Deal_CheckFirstFoundationStack_IsNotNull()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.IsNotNull(table.GetFoundationStack(0));
    }

    [TestMethod]
    public void TableTests_AvailableMoves_CountMoves_IsNotNull()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.IsNotNull(table.GetAvailableMoves());
    }

    [TestMethod]
    public void TableTests_GetAvailableMoves_CountMoves_IsThree()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(3, table.GetAvailableMoves().Count);
    }

    [TestMethod]
    public void TableTests_GetAvailableMoves_CallMultipleTimes_IsConsistantlyThree()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());
      int first = table.GetAvailableMoves().Count;
      int second = table.GetAvailableMoves().Count;
      int third = table.GetAvailableMoves().Count;
      int fourth = table.GetAvailableMoves().Count;
      int fifth = table.GetAvailableMoves().Count;
      int sixth = table.GetAvailableMoves().Count;
      int seventh = table.GetAvailableMoves().Count;
      int eighth = table.GetAvailableMoves().Count;


      //Assert.AreEqual(3,
      Assert.AreEqual(3, first);
      Assert.AreEqual(3, second);
      Assert.AreEqual(3, third);
      Assert.AreEqual(3, fourth);
      Assert.AreEqual(3, fifth);
      Assert.AreEqual(3, sixth);
      Assert.AreEqual(3, seventh);
      Assert.AreEqual(3, eighth);
    }

    [TestMethod]
    public void TableTests_GetAvailableMoves_CountMoves_IsFive()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());
      table.SetTableauStack(1, new List<Card>());

      Assert.AreEqual(5, table.GetAvailableMoves().Count);
    }
    
    [TestMethod]
    public void TableTests_GetAvailableMoves_EmptyTableauCountMoves_IsZero()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());
      table.SetTableauStack(0, new List<Card>());
      table.SetTableauStack(1, new List<Card>());
      table.SetTableauStack(2, new List<Card>());
      table.SetTableauStack(3, new List<Card>());
      table.SetTableauStack(4, new List<Card>());
      table.SetTableauStack(5, new List<Card>());
      table.SetTableauStack(6, new List<Card>());

      Assert.AreEqual(0, table.GetAvailableMoves().Count);
    }

    [TestMethod]
    public void TableTests_GetAvailableMoves_EmptyHandCountMoves_IsTwo()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.SetHand(new List<Card>());

      Assert.AreEqual(2, table.GetAvailableMoves().Count);
    }
    
    [TestMethod]
    public void TableTests_GetAvailableMoves_TableauIsStackedWithFaceUpCards_MovesTheCardAtBottomOfStack()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.QUEEN_HEARTS, TestHelper.KING_SPADES));
      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.JACK_HEARTS, TestHelper.QUEEN_CLUBS));

      IMove move = table.GetAvailableMoves().FirstOrDefault(x => TestHelper.QUEEN_CLUBS.Equals(x.GetTopCard())
      & TestHelper.KING_DIAMONDS.Equals(x.GetBottomCard()));

      Assert.IsNotNull(move);
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveOneCard_OnLessAvailableMove()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.QUEEN_CLUBS, TestHelper.KING_DIAMONDS));

      Assert.AreEqual(2, table.GetAvailableMoves().Count);
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveOneCard_CardIsMoved()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.QUEEN_CLUBS, TestHelper.KING_DIAMONDS));

      Assert.AreEqual(TestHelper.QUEEN_CLUBS.SuitInt, table.GetTableauStack(2).ViewTopCard().SuitInt);
      Assert.AreEqual(TestHelper.QUEEN_CLUBS.ValueInt, table.GetTableauStack(2).ViewTopCard().ValueInt);
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveCardOntoFoundation_CardIsMovedToFoundation()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.ACE_DIAMONDS, null));

      Assert.AreEqual(TestHelper.ACE_DIAMONDS.SuitInt, table.GetFoundationStack(0).ViewTopCard().SuitInt);
      Assert.AreEqual(TestHelper.ACE_DIAMONDS.ValueInt, table.GetFoundationStack(0).ViewTopCard().ValueInt);
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveTwoCards_SecondCardIsMoved()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.QUEEN_HEARTS, TestHelper.KING_SPADES));
      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.JACK_CLUBS, TestHelper.QUEEN_HEARTS));

      Assert.AreEqual(TestHelper.JACK_CLUBS.SuitInt, table.GetTableauStack(0).ViewTopCard().SuitInt);
      Assert.AreEqual(TestHelper.JACK_CLUBS.ValueInt, table.GetTableauStack(0).ViewTopCard().ValueInt);
    }
    
    [TestMethod]
    public void TableTests_RemoveMovesAlreadyTaken_MoveHasAlreadyBeenTaken_MoveIsRemoved()
    {
      Table table = new Table(_unitOfWork);
      List<IMove> pastMoves = new List<IMove>()
      {
        new TableauToTableauMove(_unitOfWork, TestHelper.QUEEN_HEARTS, TestHelper.KING_SPADES),
        new TableauToTableauMove(_unitOfWork, TestHelper.JACK_CLUBS, TestHelper.QUEEN_HEARTS)
      };
      table.SetPastMoves(pastMoves);

      table.SetAvailableMoves(new List<IMove>()
      {
        new TableauToTableauMove(_unitOfWork, TestHelper.JACK_CLUBS, TestHelper.QUEEN_HEARTS)
      });

      table.RemoveMovesAlreadyTaken();

      Assert.AreEqual(0, table.GetAvailableMovesForTestingOnly().Count);
    }
  }
}
