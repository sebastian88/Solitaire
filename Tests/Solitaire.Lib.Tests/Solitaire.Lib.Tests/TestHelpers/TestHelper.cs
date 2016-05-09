using Solitaire.Lib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Tests.TestHelpers
{
  public static class TestHelper
  {
    public static Card QUEEN_CLUBS = new Card(Enums.Values.Queen, Enums.Suits.Clubs);
    public static Card KING_DIAMONDS = new Card(Enums.Values.King, Enums.Suits.Diamonds);
    public static Card TWO_DIAMONDS = new Card(Enums.Values.Two, Enums.Suits.Diamonds);
    public static Card SIX_HEARTS = new Card(Enums.Values.Six, Enums.Suits.Hearts);

    public static List<Card> GenerateTestDeck()
    {
      List<Card> cards = new List<Card>();
      for (int i = 1; i <= 4; i++)
      {
        for (int j = 1; j <= 13; j++)
        {
          cards.Add(new Card((Enums.Values)j, (Enums.Suits)i, false));
        }
      }

      return cards;
    }
  }
}
