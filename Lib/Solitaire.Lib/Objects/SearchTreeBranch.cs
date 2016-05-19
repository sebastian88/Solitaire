using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Objects
{
  public class SearchTreeBranch
  {
    public int Score { get; set; }


    public List<IMove> Moves { get; set; }

    public SearchTreeBranch(List<IMove> moves)
    {
      Moves = moves;
    }

    public void CalculateScore()
    {
      Score = 0;
      foreach (IMove move in Moves)
        Score = +move.GetValue();
    }
  }
}
