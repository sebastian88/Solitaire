using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Services
{
  public class CardService
  {
    public CardService()
    {
      // test
    }

    public bool IsValidTableauMove(Card topCard, Card bottomCard)
    {
      return IsTopCardOneValueLowerThanBottomCard(topCard, bottomCard)
        && IsOppositeSuit(topCard, bottomCard);
    }

    private bool IsTopCardOneValueLowerThanBottomCard(Card topCard, Card bottomCard)
    {
      return IsTopCardOfLowerValueThanBottomCard(topCard, bottomCard)
        && IsInSequence(topCard, bottomCard);
    }

    private bool IsTopCardOfLowerValueThanBottomCard(Card topCard, Card bottomCard)
    {
      return topCard.ValueInt - bottomCard.ValueInt < 0;
    }

    private bool IsInSequence(Card cardOne, Card cardTwo)
    {
      return Math.Abs(cardOne.ValueInt - cardTwo.ValueInt) == 1;
    }

    private bool IsOppositeSuit(Card cardOne, Card cardTwo)
    {
      return IsOdd(cardOne.SuitInt + cardTwo.SuitInt);
    }
    
    private bool IsOdd(int value)
    {
      return value % 2 != 0;
    }

    public bool IsValidFoundationMove(Card topCard, Card bottomCard)
    {
      return true;
    }
  }
}
