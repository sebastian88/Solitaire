using Solitaire.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects.Interfaces
{
  public interface ICard : IStackable, ICloneable
  {
    void TurnFaceUp();
    Values Value();
    Suits Suit();
  }
}
