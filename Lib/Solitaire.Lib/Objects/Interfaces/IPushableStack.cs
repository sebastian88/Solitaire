using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects.Interfaces
{
  public interface IPushableStack
  {
    void PushTopCard(ICard card);
  }
}
