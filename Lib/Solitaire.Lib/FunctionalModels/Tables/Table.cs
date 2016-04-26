using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.FunctionalModels.Hands;
using Solitaire.Lib.FunctionalModels.Stacks.Impl;
using Solitaire.Lib.FunctionalModels.Moves;
using Solitaire.Lib.FunctionalModels.Moves.Impl;
using Solitaire.Lib.Context;
using Solitaire.Lib.FunctionalModels.Stacks;

namespace Solitaire.Lib.FunctionalModels.Tables
{
  public class Table
  {
    private readonly int NUMBER_OF_TABLEAU_STACKS = 7;
    private readonly int NUMBER_OF_FOUNDATION_STACKS = 4;

    private UnitOfWork _unitOfWork;
    private BaseTable _initialTable;
    List<Move> _availableMoves;
    private List<Move> _movesTaken;
    private BaseTable _currentTable;

    public Table(UnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      InstanciateTables();
      InstanciateTableau();
      InstanciateFoundation();
      instanciateHand();
    }
    public Table(UnitOfWork unitOfWork, List<Card> cards)
      :this(unitOfWork)
    {
      Deal(cards);
    }

    private void InstanciateTables()
    {
      _initialTable = new BaseTable();
      _currentTable = new BaseTable();
    }

    private void InstanciateTableau()
    {
      _currentTable.Tableau = new List<TableauStack>();
      for (int i = 0; i < NUMBER_OF_TABLEAU_STACKS; i++)
        _currentTable.Tableau.Add(new TableauStack());
    }

    private void InstanciateFoundation()
    {
      _currentTable.Foundation = new List<FoundationStack>();
      for (int i = 0; i < NUMBER_OF_FOUNDATION_STACKS; i++)
        _currentTable.Foundation.Add(new FoundationStack());
    }

    private void instanciateHand()
    {
      _currentTable.Hand = new Hand();
    }

    public void Deal(List<Card> cards)
    {
      DealTableau(cards);
      DealHand(cards);
    }

    private void DealTableau(List<Card> cards)
    {
      for (int currentRow = 0; currentRow < NUMBER_OF_TABLEAU_STACKS; currentRow++)
        cards = DealRow(currentRow, cards);

      UpTurnEndTableauCards();
    }

    private List<Card> DealRow(int rowToDeal, List<Card> cards)
    {
      for (int currentTableau = rowToDeal; currentTableau < NUMBER_OF_TABLEAU_STACKS; currentTableau++)
      {
        _currentTable.Tableau[currentTableau].PushTopCard(cards.Last());
        cards.RemoveAt(cards.Count - 1);
      }
      return cards;
    }

    public TableauStack GetTableauStack(int tableauStack)
    {
      return _currentTable.Tableau[tableauStack];
    }

    public FoundationStack GetFoundationStack(int foundationStack)
    {
      return _currentTable.Foundation[foundationStack];
    }

    public Hand GetHand()
    {
      return _currentTable.Hand;
    }

    public List<Move> GetAvailableMoves()
    {
      _availableMoves = new List<Move>();

      PopulateAvailableMovesForTableau();

      PopulateAvailableMovesfromHand();

      return _availableMoves;
    }

    private void PopulateAvailableMovesForTableau()
    {
      foreach (TableauStack tableauStack in _currentTable.Tableau)
        AvailableMovesFromTableauCard(tableauStack.ViewTopCard());
    }

    private void PopulateAvailableMovesfromHand()
    {
      Card firstHandCard = _currentTable.Hand.ViewTopCard();
      CheckCardInHandAndDealHand();
      while (firstHandCard != _currentTable.Hand.ViewTopCard())
        CheckCardInHandAndDealHand();
    }

    private void CheckCardInHandAndDealHand()
    {
      AvailableMovesFromHandCard(_currentTable.Hand.ViewTopCard());
      _currentTable.Hand.Deal();
    }

    private void AvailableMovesFromTableauCard(Card card)
    {
      AvailableMovesForCardOntoFoundation<TableauToFoundationMove>(card);
      AvailableMovesForCardOntoTableau<TableauToTableauMove>(card);
    }

    private void AvailableMovesFromHandCard(Card card)
    {
      AvailableMovesForCardOntoFoundation<HandToFoundationMove>(card);
      AvailableMovesForCardOntoTableau<HandToTableauMove>(card);
    }

    private void AvailableMovesForCardOntoFoundation<T>(Card card) where T : FoundationMove
    {
      foreach (FoundationStack foundationStack in _currentTable.Foundation)
      {
        Move move = (Move)Activator.CreateInstance(typeof(T), _unitOfWork, card, foundationStack.ViewTopCard());
        if (move.IsValid())
          AddToAvailableMoves(move);
      }
    }

    private void AvailableMovesForCardOntoTableau<T>(Card card) where T : TableauMove
    {
      foreach (TableauStack foundationStack in _currentTable.Tableau)
      {
        Move move = (Move)Activator.CreateInstance(typeof(T), _unitOfWork, card, foundationStack.ViewTopCard());
        if (move.IsValid())
          AddToAvailableMoves(move);
      }
    }

    private void AddToAvailableMoves(Move move)
    {
      if (_availableMoves == null)
        _availableMoves = new List<Move>();
      _availableMoves.Add(move);
    }


    public void MakeMove(Move move)
    {
      // find top card

      // find bottom card

      // move top card onto bottom card

      // Add move to list of moves taken
    }

    public int UpTurnEndTableauCards()
    {
      int countFlippedCards = 0;
      foreach (TableauStack tableauStack in _currentTable.Tableau)
        countFlippedCards = TurnCardFaceUpIfFaceDown(tableauStack) ? countFlippedCards + 1 : countFlippedCards;
      return countFlippedCards;
    }

    private bool TurnCardFaceUpIfFaceDown(TableauStack tableauStack)
    {
      bool isCardTurned = false;
      if (!tableauStack.ViewTopCard().IsFaceUp)
        tableauStack.ViewTopCard().IsFaceUp = isCardTurned = true;
      return isCardTurned;
    }

    private void DealHand(List<Card> cards)
    {
      foreach (Card card in cards)
        card.IsFaceUp = true;
      _currentTable.Hand.SetCards(cards);
    }
  }
}
