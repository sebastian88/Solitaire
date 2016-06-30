using Solitaire.Lib.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects.Interfaces
{
  public interface IMove : ICloneable
  {
    bool IsValid();
    int GetValue();
    IStackable GetTopCard();
    IStackable GetBottomCard();
  }
}
