using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib;

namespace Solitaire.Lib.Models
{
  public class Card
  {
    public bool IsFaceUp { get; set; }
    public Enums.Values Value { get; set; }
    public int ValueInt { get { return (int)Value; } set { Value = (Enums.Values)value; } }
    public Enums.Suits Suit { get; set; }
    public int SuitInt { get { return (int)Suit; } set { Suit = (Enums.Suits)value; } }

    public Card(Enums.Values value, Enums.Suits suit)
    {
      Value = value;
      Suit = suit;
      IsFaceUp = true;
    }
    public Card(Enums.Values value, Enums.Suits suit, bool isFaceUp)
      : this(value, suit)
    {
      IsFaceUp = isFaceUp;
    }
  }
}
