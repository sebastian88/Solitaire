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
  public class TableauToTableauMove : TableauMove, IMove
  {
    public TableauToTableauMove(IUnitOfWork unitOfWork, IStackable topCard, IStackable bottomCard)
      : base (unitOfWork, topCard, bottomCard)
    {
    }

    public int GetValue()
    {
      return _unitOfWork.Config.TableauToTableauMoveValue;
    }
  }
}
