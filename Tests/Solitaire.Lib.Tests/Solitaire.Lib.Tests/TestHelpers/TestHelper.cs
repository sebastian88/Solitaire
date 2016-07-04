using Solitaire.Lib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Enums;
using Solitaire.Lib.Objects.Interfaces;

namespace Solitaire.Lib.Tests.TestHelpers
{
  public static class TestHelper
  {
    public static Card NOT_A_CARD = new Card(Values.NotACard, Suits.NotACard);
    public static Card SIX_CLUBS = new Card(Values.Six, Suits.Clubs);
    public static Card JACK_CLUBS = new Card(Values.Jack, Suits.Clubs);
    public static Card QUEEN_CLUBS = new Card(Values.Queen, Suits.Clubs);
    public static Card ACE_DIAMONDS = new Card(Values.Ace, Suits.Diamonds);
    public static Card TWO_DIAMONDS = new Card(Values.Two, Suits.Diamonds);
    public static Card THREE_DIAMONDS = new Card(Values.Three, Suits.Diamonds);
    public static Card FOUR_DIAMONDS = new Card(Values.Four, Suits.Diamonds);
    public static Card FIVE_DIAMONDS = new Card(Values.Five, Suits.Diamonds);
    public static Card SIX_DIAMONDS = new Card(Values.Six, Suits.Diamonds);
    public static Card SEVEN_DIAMONDS = new Card(Values.Seven, Suits.Diamonds);
    public static Card EIGHT_DIAMONDS = new Card(Values.Eight, Suits.Diamonds);
    public static Card NINE_DIAMONDS = new Card(Values.Nine, Suits.Diamonds);
    public static Card TEN_DIAMONDS = new Card(Values.Ten, Suits.Diamonds);
    public static Card JACK_DIAMONDS = new Card(Values.Jack, Suits.Diamonds);
    public static Card QUEEN_DIAMONDS = new Card(Values.Queen, Suits.Diamonds);
    public static Card KING_DIAMONDS = new Card(Values.King, Suits.Diamonds);
    public static Card SIX_HEARTS = new Card(Values.Six, Suits.Hearts);
    public static Card JACK_HEARTS = new Card(Values.Jack, Suits.Hearts);
    public static Card QUEEN_HEARTS = new Card(Values.Queen, Suits.Hearts);
    public static Card KING_HEARTS = new Card(Values.King, Suits.Hearts);
    public static Card TWO_SPADES = new Card(Values.Two, Suits.Spades);
    public static Card KING_SPADES = new Card(Values.King, Suits.Spades);

    // ks 6s kd 8d 4d 1d qc           1h -> jc
    public static List<ICard> GenerateTestDeck()
    {
      List<ICard> cards = new List<ICard>();
      for (int i = 1; i <= 4; i++)
      {
        for (int j = 1; j <= 13; j++)
        {
          cards.Add(new Card((Values)j, (Suits)i, false));
        }
      }

      return cards;
    }
  }
}
