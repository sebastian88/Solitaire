using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.FunctionalModels.HandModels;
using Solitaire.Lib.FunctionalModels.StackModels.Impl;

namespace Solitaire.Lib.FunctionalModels.TableModels
{
  public class Table
  {
    private readonly int NUMBER_OF_TABLEAU_STACKS = 7;

    public List<TableauStack> Tableau;
    public List<List<Card>> FoundationCards;
    public Hand Hand;

    public Table()
    {
      InstanciateTableau();
      instanciateHand();
    }

    private void InstanciateTableau()
    {
      Tableau = new List<TableauStack>();
      for (int i = 0; i < NUMBER_OF_TABLEAU_STACKS; i++)
        Tableau.Add(new TableauStack());
    }

    private void InstanciateFoundationCards()
    {

    }

    private void instanciateHand()
    {
      Hand = new Hand();
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

    private int UpTurnEndTableauCards()
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
