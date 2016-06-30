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
using Solitaire.Lib.Enums;

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

      Assert.AreEqual(1, table.GetTableauStack(0).CountCards());
    }

    [TestMethod]
    public void TableTests_Deal_CheckSecondStackCount_ReturnsTwo()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(2, table.GetTableauStack(1).CountCards());
    }

    [TestMethod]
    public void TableTests_Deal_CheckSeventhStackCount_ReturnsSeven()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.AreEqual(7, table.GetTableauStack(6).CountCards());
    }

    [TestMethod]
    public void TableTests_Deal_CheckFirstStackFirstCard_IsFaceUp()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());

      Assert.IsTrue(table.GetTableauStack(0).ViewTopCard().IsFaceUp());
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
      table.SetTableauStack(1, new List<ICard>());

      Assert.AreEqual(5, table.GetAvailableMoves().Count);
    }
    
    [TestMethod]
    public void TableTests_GetAvailableMoves_EmptyTableauCountMoves_IsZero()
    {
      Table table = new Table(_unitOfWork);

      table.Deal(TestHelper.GenerateTestDeck());
      table.SetTableauStack(0, new List<ICard>());
      table.SetTableauStack(1, new List<ICard>());
      table.SetTableauStack(2, new List<ICard>());
      table.SetTableauStack(3, new List<ICard>());
      table.SetTableauStack(4, new List<ICard>());
      table.SetTableauStack(5, new List<ICard>());
      table.SetTableauStack(6, new List<ICard>());

      Assert.AreEqual(0, table.GetAvailableMoves().Count);
    }

    [TestMethod]
    public void TableTests_GetAvailableMoves_EmptyHandCountMoves_IsTwo()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.SetHand(new List<ICard>());

      Assert.AreEqual(2, table.GetAvailableMoves().Count);
    }

    [TestMethod]
    public void TableTests_GetCard_GetCardNotOnTable_ReturnNull()
    {
      Table table = new Table(_unitOfWork);

      ICard card = table.GetCard(TestHelper.QUEEN_HEARTS);

      Assert.IsNull(card);
    }

    [TestMethod]
    public void TableTests_GetCard_GetCardInHand_CardIsReturned()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      ICard card = table.GetCard(TestHelper.QUEEN_HEARTS);

      Assert.IsTrue(card.Equals(TestHelper.QUEEN_HEARTS));
    }

    [TestMethod]
    public void TableTests_GetCard_GetCardInTableau_CardIsReturned()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      ICard card = table.GetCard(TestHelper.EIGHT_DIAMONDS);

      Assert.IsTrue(card.Equals(TestHelper.EIGHT_DIAMONDS));
    }

    [TestMethod]
    public void TableTests_GetCard_GetCardInTableau_NextCardIsNotNull()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      ICard card = table.GetCard(TestHelper.EIGHT_DIAMONDS);

      Assert.IsNotNull(card.GetNext());
    }

    [TestMethod]
    public void TableTests_GetCard_GetCardInFoundation_CardIsReturned()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      IStackable cardToPutOnFoundation = table.GetTableauStack(0).PopTopCard();
      table.GetFoundationStack(0).PushTopCard(cardToPutOnFoundation as ICard);

      ICard card = table.GetCard(TestHelper.KING_SPADES);

      Assert.IsTrue(card.Equals(TestHelper.KING_SPADES));
    }

    [TestMethod]
    public void TableTests_GetAvailableMoves_TableauIsStackedWithFaceUpCards_MovesTheCardAtBottomOfStack()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      ICard queenHearts = table.GetCard(TestHelper.QUEEN_HEARTS);
      ICard kingSpades = table.GetCard(TestHelper.KING_SPADES);
      ICard jackHearts = table.GetCard(TestHelper.JACK_HEARTS);
      ICard queenClubs = table.GetCard(TestHelper.QUEEN_CLUBS);

      //TODO these cards need to be pulled from the table not just created. 
      table.MakeMove(new TableauToTableauMove(_unitOfWork, queenHearts, kingSpades));
      table.MakeMove(new TableauToTableauMove(_unitOfWork, jackHearts, queenClubs));

      IMove move = table.GetAvailableMoves().FirstOrDefault(x => TestHelper.QUEEN_CLUBS.Equals(x.GetTopCard())
      & TestHelper.KING_DIAMONDS.Equals(x.GetBottomCard()));

      Assert.IsNotNull(move);
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveOneCard_OnLessAvailableMove()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      ICard queenClubs = table.GetCard(TestHelper.QUEEN_CLUBS);
      ICard kingDiamonds = table.GetCard(TestHelper.KING_DIAMONDS);

      table.MakeMove(new TableauToTableauMove(_unitOfWork, queenClubs, kingDiamonds));

      Assert.AreEqual(2, table.GetAvailableMoves().Count);
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveOneCard_CardIsMoved()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());
      
      ICard queenClubs = table.GetCard(TestHelper.QUEEN_CLUBS);
      ICard kingDiamonds = table.GetCard(TestHelper.KING_DIAMONDS);
      table.MakeMove(new TableauToTableauMove(_unitOfWork, queenClubs, kingDiamonds));

      Assert.AreEqual(TestHelper.QUEEN_CLUBS.SuitInt(), table.GetTableauStack(2).ViewTopCard().SuitInt());
      Assert.AreEqual(TestHelper.QUEEN_CLUBS.ValueInt(), table.GetTableauStack(2).ViewTopCard().ValueInt());
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveCardOntoFoundation_CardIsMovedToFoundation()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToFoundationMove(_unitOfWork, 
        table.GetCard(TestHelper.ACE_DIAMONDS), 
        table.GetFoundationStack(0).ViewTopCard()));

      Assert.AreEqual(TestHelper.ACE_DIAMONDS.SuitInt(), table.GetFoundationStack(0).ViewTopCard().SuitInt());
      Assert.AreEqual(TestHelper.ACE_DIAMONDS.ValueInt(), table.GetFoundationStack(0).ViewTopCard().ValueInt());
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveTwoCards_SecondCardIsMoved()
    {
      Table table = new Table(_unitOfWork);
      table.Deal(TestHelper.GenerateTestDeck());

      table.MakeMove(new TableauToTableauMove(_unitOfWork,
        table.GetCard(TestHelper.QUEEN_HEARTS),
        table.GetCard(TestHelper.KING_SPADES)));
      table.MakeMove(new TableauToTableauMove(_unitOfWork,
        table.GetCard(TestHelper.JACK_CLUBS),
        table.GetCard(TestHelper.QUEEN_HEARTS)));

      Assert.AreEqual(TestHelper.JACK_CLUBS.SuitInt(), table.GetTableauStack(0).ViewTopCard().SuitInt());
      Assert.AreEqual(TestHelper.JACK_CLUBS.ValueInt(), table.GetTableauStack(0).ViewTopCard().ValueInt());
    }

    [TestMethod]
    public void TableTests_MakeMove_MoveAceToFoundation_AceIsMovedToFoundation()
    {
      Table table = new Table(_unitOfWork);
      table.SetTableauStack(0, new List<ICard>() { TestHelper.ACE_DIAMONDS });

      table.MakeMove(new TableauToFoundationMove(_unitOfWork,
        table.GetCard(TestHelper.ACE_DIAMONDS),
        table.GetFoundationStack(0).ViewTopCard()));

      Assert.AreEqual(TestHelper.ACE_DIAMONDS.SuitInt(), table.GetFoundationStack(0).ViewTopCard().SuitInt());
      Assert.AreEqual(TestHelper.ACE_DIAMONDS.ValueInt(), table.GetFoundationStack(0).ViewTopCard().ValueInt());
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

      Assert.AreEqual(0, table.GetAvailableMoves_TESTING_ONLY().Count);
    }

    [TestMethod]
    public void TableTests_Clone_ChangeCardPositionOnClonedTable_OriginalTableRemainsTheSame()
    {
      Table original = new Table(_unitOfWork);
      original.Deal(TestHelper.GenerateTestDeck());
      Table clone = (Table)original.Clone();


      clone.MakeMove(new TableauToTableauMove(_unitOfWork,
        clone.GetCard(TestHelper.QUEEN_HEARTS),
        clone.GetCard(TestHelper.KING_SPADES)));

      Assert.IsNull(original.GetCard(TestHelper.QUEEN_HEARTS).GetNext());
    }

    [TestMethod]
    public void TableTests_Clone_ChangeCardPositionOnClonedTable_CloneTableIsUpdated()
    {
      Table original = new Table(_unitOfWork);
      original.Deal(TestHelper.GenerateTestDeck());
      Table clone = (Table)original.Clone();
      
      clone.MakeMove(new TableauToTableauMove(_unitOfWork,
        clone.GetCard(TestHelper.QUEEN_HEARTS),
        clone.GetCard(TestHelper.KING_SPADES)));

      ICard topCard = clone.GetTableauStack(0).ViewTopCard() as ICard;
      Assert.AreEqual(topCard.Suit(), Suits.Hearts);
    }
  }
}
