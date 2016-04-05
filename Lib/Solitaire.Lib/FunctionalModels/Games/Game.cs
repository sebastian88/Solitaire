using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.FunctionalModels.Tables;

namespace Solitaire.Lib.FunctionalModels.Games
{
  public class Game
  {
    private Table _table;

    public Game(Table table)
    {
      _table = table;
    }
  }
}
