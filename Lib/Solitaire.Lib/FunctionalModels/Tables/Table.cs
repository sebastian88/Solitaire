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

    public List<TableauStack> Tableau;
    public List<FoundationStack> Foundation;
    public Hands.Hand Hand;

    public Table()
    {
      InstanciateTableau();
      InstanciateFoundation();
      instanciateHand();
    }

    private void InstanciateTableau()
    {
      Tableau = new List<TableauStack>();
      for (int i = 0; i < NUMBER_OF_TABLEAU_STACKS; i++)
        Tableau.Add(new TableauStack());
    }

    private void InstanciateFoundation()
    {
      Foundation = new List<FoundationStack>();
      for (int i = 0; i < NUMBER_OF_FOUNDATION_STACKS; i++)
        Foundation.Add(new FoundationStack());
    }

    private void instanciateHand()
    {
      Hand = new Hands.Hand();
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
        Tableau[currentTableau].PushTopCard(cards.Last());
        cards.RemoveAt(cards.Count - 1);
      }
      return cards;
    }

    public int UpTurnEndTableauCards()
    {
      int countFlippedCards = 0;
      foreach (TableauStack tableauStack in Tableau)
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
      Hand.SetCards(cards);
    }
  }
}
