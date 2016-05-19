using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib;
using Solitaire.Lib.Utils;

namespace Solitaire.Lib.Objects
{
  public class Card : IEquatable<Card>, ICloneable
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


    public bool Equals(Card other)
    {
      return IsSameValue(other) && IsSameSuit(other);
    }

    public bool IsSameSuit(Card other)
    {
      return SuitInt == other.SuitInt;
    }

    public bool IsSameValue(Card other)
    {
      return ValueInt == other.ValueInt;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public override string ToString()
    {
      return Value.ToString() + " " + Suit.ToString() + "(" + (IsFaceUp ? "faceup" : "facedown") + ")";
    }
  }
}
