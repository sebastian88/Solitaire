using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
      _unitOfWork = IoCContainer.GetUnitOfWork();
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
    public void TableTests_MakeMove_MoveOneCard_CardIsMoved()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork, TestHelper.KING_DIAMONDS, TestHelper.QUEEN_CLUBS));

      Assert.AreEqual(2, table.GetAvailableMoves().Count);
    }
  }
}
