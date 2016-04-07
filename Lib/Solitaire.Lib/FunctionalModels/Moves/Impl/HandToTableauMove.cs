using Solitaire.Lib.Config;
using Solitaire.Lib.Context;
using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.FunctionalModels.Moves.Impl
{
  public class HandToTableauMove : TableauMove, Move
  {
    public HandToTableauMove(UnitOfWork unitOfWork, Card topCard, Card bottomCard)
      : base (unitOfWork, topCard, bottomCard)
    {
    }

    public int GetValue()
    {
      return _unitOfWork.Config.HandToTableauMoveValue;
    }
  }
}
