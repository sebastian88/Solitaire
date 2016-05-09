using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Objects.Interfaces;

namespace Solitaire.Lib.Objects
{
  public class Game
  {
    private Table _table;
    private List<IMove> _moves;
    private List<List<IMove>> _branches;

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

    public List<IMove> GetBestMoveBranch()
    {
      _branches = new List<List<IMove>>();
      return PickBestMoveBranch();
    }

    private void FindAllMoveBranchesRecursive(Table table)
    {
      if (table.GetAvailableMoves().Count > 0)
        MakeAllAvailableMovesForTable(table);
      else
        _branches.Add(table.GetPastMoves());
    }

    private void MakeAllAvailableMovesForTable(Table table)
    {
      foreach (IMove move in table.GetAvailableMoves())
      {
        Table clone = (Table)table.Clone();
        clone.MakeMove(move);
        FindAllMoveBranchesRecursive(clone);
      }
    }

    private List<IMove> PickBestMoveBranch()
    {
      FindAllMoveBranchesRecursive(_table);
      List<IMove> bestBranch = new List<IMove>();
      int bestBranchScore = 0;
      foreach(List<IMove> branch in _branches)
      {
        int branchScore = 0;
        foreach (IMove move in branch)
        {
          branchScore =+ move.GetValue();
        }

        if (bestBranchScore < branchScore)
        {
          bestBranch = branch;
          bestBranchScore = branchScore;
        }
      }
      return bestBranch;
    }
  }
}
