using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.StackModels.Impl
{
  public class BaseStack
  {
    private List<Card> _cards;

    public BaseStack()
    {
      _cards = new List<Card>();
    }

    public BaseStack(List<Card> cards)
    {
      _cards = cards;
    }
  }
}
