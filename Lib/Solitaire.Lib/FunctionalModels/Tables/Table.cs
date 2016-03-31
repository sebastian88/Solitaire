using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.FunctionalModels.Hands;
using Solitaire.Lib.FunctionalModels.Stacks.Impl;

namespace Solitaire.Lib.FunctionalModels.Tables
{
  public class Table
  {
    private readonly int NUMBER_OF_TABLEAU_STACKS = 7;
    private readonly int NUMBER_OF_FOUNDATION_STACKS = 4;

    private List<TableauStack> _tableau;
    private List<FoundationStack> _foundation;
    private Hand _hand;

    public Table()
    {
      InstanciateTableau();
      InstanciateFoundation();
      instanciateHand();
    }
    public Table(List<Card> cards)
      :this()
    {
      Deal(cards);
    }

    private void InstanciateTableau()
    {
      _tableau = new List<TableauStack>();
      for (int i = 0; i < NUMBER_OF_TABLEAU_STACKS; i++)
        _tableau.Add(new TableauStack());
    }

    private void InstanciateFoundation()
    {
      _foundation = new List<FoundationStack>();
      for (int i = 0; i < NUMBER_OF_FOUNDATION_STACKS; i++)
        _foundation.Add(new FoundationStack());
    }

    private void instanciateHand()
    {
      _hand = new Hand();
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
        _tableau[currentTableau].PushTopCard(cards.Last());
        cards.RemoveAt(cards.Count - 1);
      }
      return cards;
    }

    public TableauStack GetTableauStack(int tableauStack)
    {
      return _tableau[tableauStack];
    }

    public FoundationStack GetFoundationStack(int foundationStack)
    {
      return _foundation[foundationStack];
    }

    public Hand GetHand()
    {
      return _hand;
    }

    public int UpTurnEndTableauCards()
    {
      int countFlippedCards = 0;
      foreach (TableauStack tableauStack in _tableau)
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
      _hand.SetCards(cards);
    }
  }
}
