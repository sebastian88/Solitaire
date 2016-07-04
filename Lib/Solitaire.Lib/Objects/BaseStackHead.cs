using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public abstract class BaseStackHead : IStackable
  {
    protected IStackable _next;

    public BaseStackHead()
    {
      _next = this;
    }

    public IStackable GetNext()
    {
      return _next;
    }

    public bool IsHead()
    {
      return true;
    }

    public void SetNext(IStackable stackableItem)
    {
      _next = stackableItem;
    }

    public bool CanBeStackedOnByAnySuit()
    {
      return true;
    }

    public bool IsFaceUp()
    {
      return true;
    }

    public int SuitInt()
    {
      return -1;
    }

    public abstract int ValueInt();

    public IHeadOfStack GetHeadOfStack()
    {
      return this as IHeadOfStack;
    }

    public abstract bool Equals(IStackable other);

    public abstract object Clone();

    public abstract void SetStack(BaseStack stack);
  }
}
