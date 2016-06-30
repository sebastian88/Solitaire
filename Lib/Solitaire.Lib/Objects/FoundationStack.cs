using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Objects.Interfaces;

namespace Solitaire.Lib.Objects
{
  public class FoundationStack : BaseStack, IPushableStack, IPopableStack
  {
    public FoundationStack(int stackPosition)
      : base(stackPosition)
    {
      _headOfStack = new FoundationStackHead(this);
    }

    public override BaseStack GetClonedStack()
    {
      return new FoundationStack(_stackPosition);
    }
  }
}
