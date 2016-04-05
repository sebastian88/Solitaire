using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Models;

namespace Solitaire.Lib.FunctionalModels.Stacks.Impl
{
  public class TableauStack : BaseStack, PushableStack, PopableStack
  {

    public TableauStack() 
      : base()
    {
    }

    public TableauStack(List<Card> cards) 
      : base(cards)
    {
    }

    public int Count()
    {
      return _cards.Count;
    }

    public void TurnoverTopCardIfNecessary()
    {
      if (!IsEmpty())
        ViewTopCard().IsFaceUp = true;
    }
  }
}