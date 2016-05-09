using Solitaire.Lib.Config;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public class TableauToFoundationMove : FoundationMove, IMove
  {
    public TableauToFoundationMove(IUnitOfWork unitOfWork, Card topCard, Card bottomCard)
      : base (unitOfWork, topCard, bottomCard)
    {
    }

    public int GetValue()
    {
      return _unitOfWork.Config.TableauToFoundationMoveValue;
    }
  }
}
