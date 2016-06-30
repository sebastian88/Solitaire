using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public class TableauStackHead : BaseStackHead, IStackable, IHeadOfStack
  {
    private TableauStack _tableauStack;

    public TableauStackHead()
      : base()
    {
      this.SetNext(this);
    }

    public TableauStackHead(TableauStack tableauStack)
      : this()
    {
      _tableauStack = tableauStack;
    }

    public override void SetStack(BaseStack stack)
    {
      _tableauStack = stack as TableauStack;
    }

    public BaseStack GetStack()
    {
      return _tableauStack;
    }

    public override int ValueInt()
    {
      return 14;
    }

    public override object Clone()
    {
      return new TableauStackHead();
    }
  }
}
