
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects.Interfaces
{
  public interface IStackable : IEquatable<IStackable>, ICloneable
  {
    IStackable GetNext();
    void SetNext(IStackable stackableItem);
    bool IsHead();
    bool IsFaceUp();
    int ValueInt();
    int SuitInt();
    bool CanBeStackedOnByAnySuit();
    IHeadOfStack GetHeadOfStack();
  }
}
