using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public class FoundationStackHead : BaseStackHead, IStackable, IHeadOfStack
  {
    private FoundationStack _foundationStack;

    public FoundationStackHead()
      : base()
    {
      SetNext(this);
    }

    public FoundationStackHead(FoundationStack foundationStack)
      : this()
    {
      _foundationStack = foundationStack;
    }

    public override void SetStack(BaseStack stack)
    {
      _foundationStack = stack as FoundationStack;
    }

    public BaseStack GetStack()
    {
      return _foundationStack;
    }

    public override int ValueInt()
    {
      return 0; 
    }

    public override object Clone()
    {
      return new FoundationStackHead();
    }
  }
}
