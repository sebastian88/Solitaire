using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.StackModels
{
  public interface PushableStack
  {
    void PushTopCard(Card card);
  }
}
