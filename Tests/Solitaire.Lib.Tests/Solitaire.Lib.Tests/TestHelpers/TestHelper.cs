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
    public static Card NOT_A_CARD = new Card(Enums.Values.NotACard, Enums.Suits.NotACard);
    public static Card SIX_CLUBS = new Card(Enums.Values.Six, Enums.Suits.Clubs);
    public static Card JACK_CLUBS = new Card(Enums.Values.Jack, Enums.Suits.Clubs);
    public static Card QUEEN_CLUBS = new Card(Enums.Values.Queen, Enums.Suits.Clubs);
    public static Card ACE_DIAMONDS = new Card(Enums.Values.Ace, Enums.Suits.Diamonds);
    public static Card TWO_DIAMONDS = new Card(Enums.Values.Two, Enums.Suits.Diamonds);
    public static Card THREE_DIAMONDS = new Card(Enums.Values.Three, Enums.Suits.Diamonds);
    public static Card FOUR_DIAMONDS = new Card(Enums.Values.Four, Enums.Suits.Diamonds);
    public static Card FIVE_DIAMONDS = new Card(Enums.Values.Five, Enums.Suits.Diamonds);
    public static Card SIX_DIAMONDS = new Card(Enums.Values.Six, Enums.Suits.Diamonds);
    public static Card SEVEN_DIAMONDS = new Card(Enums.Values.Seven, Enums.Suits.Diamonds);
    public static Card EIGHT_DIAMONDS = new Card(Enums.Values.Eight, Enums.Suits.Diamonds);
    public static Card NINE_DIAMONDS = new Card(Enums.Values.Nine, Enums.Suits.Diamonds);
    public static Card TEN_DIAMONDS = new Card(Enums.Values.Ten, Enums.Suits.Diamonds);
    public static Card JACK_DIAMONDS = new Card(Enums.Values.Jack, Enums.Suits.Diamonds);
    public static Card QUEEN_DIAMONDS = new Card(Enums.Values.Queen, Enums.Suits.Diamonds);
    public static Card KING_DIAMONDS = new Card(Enums.Values.King, Enums.Suits.Diamonds);
    public static Card SIX_HEARTS = new Card(Enums.Values.Six, Enums.Suits.Hearts);
    public static Card JACK_HEARTS = new Card(Enums.Values.Jack, Enums.Suits.Hearts);
    public static Card QUEEN_HEARTS = new Card(Enums.Values.Queen, Enums.Suits.Hearts);
    public static Card KING_HEARTS = new Card(Enums.Values.King, Enums.Suits.Hearts);
    public static Card TWO_SPADES = new Card(Enums.Values.Two, Enums.Suits.Spades);
    public static Card KING_SPADES = new Card(Enums.Values.King, Enums.Suits.Spades);

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
