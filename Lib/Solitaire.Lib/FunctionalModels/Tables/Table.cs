using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.FunctionalModels.Hands;
using Solitaire.Lib.FunctionalModels.Stacks.Impl;
using Solitaire.Lib.FunctionalModels.Moves;

namespace Solitaire.Lib.FunctionalModels.Tables
{
  public class Table
  {
    private readonly int NUMBER_OF_TABLEAU_STACKS = 7;
    private readonly int NUMBER_OF_FOUNDATION_STACKS = 4;


    private BaseTable _initialTable;
    private List<Move> _movesTaken;
    private BaseTable _currentTable;

    public Table()
    {
      InstanciateTables();
      InstanciateTableau();
      InstanciateFoundation();
      instanciateHand();
    }
    public Table(List<Card> cards)
      :this()
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
      return null;
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
      {
        if (!tableauStack.ViewTopCard().IsFaceUp)
        {
          tableauStack.ViewTopCard().IsFaceUp = true;
          countFlippedCards++;
        }
      }
      return countFlippedCards;
    }

    private void DealHand(List<Card> cards)
    {
      foreach (Card card in cards)
        card.IsFaceUp = true;
      _currentTable.Hand.SetCards(cards);
    }
  }
}
