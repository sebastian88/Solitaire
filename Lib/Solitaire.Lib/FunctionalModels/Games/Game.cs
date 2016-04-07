using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.FunctionalModels.Tables;
using Solitaire.Lib.FunctionalModels.Moves;

namespace Solitaire.Lib.FunctionalModels.Games
{
  public class Game
  {
    private Table _table;
    private List<Move> _moves;

    public Game(Table table)
    {
      _table = table;
    }

    public Table GetTable()
    {
      return _table;
    }

    public bool NoRemainingMoves()
    {
      return false;
    }

    public void Run()
    {
      // find available moves

      // forward search
      
      // take one with highest value

      // turn over available cards
    }
  }
}
