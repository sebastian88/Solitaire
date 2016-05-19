using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Context.Interfaces;

namespace Solitaire.Lib.Objects
{
  public class Game
  {
    private Table _table;
    private List<SearchTreeBranch> _branches;
    private IUnitOfWork _unitOfWork;

    public Game(IUnitOfWork unitOfWork, List<Card> cards)
    {
      _unitOfWork = unitOfWork;
      _table = new Table(_unitOfWork, cards);
    }

    public Game(IUnitOfWork unitOfWork, Table table)
    {
      _unitOfWork = unitOfWork;
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
      _branches = new List<SearchTreeBranch>();
      return PickBestMoveBranch();
    }

    private List<IMove> PickBestMoveBranch()
    {
      FindAllMoveBranchesRecursive(_table, 0);
      return GetBestBranch();
    }

   public List<IMove> GetBestBranch()
    {
      foreach (SearchTreeBranch branch in _branches)
        branch.CalculateScore();

      return _branches.OrderByDescending(x => x.Score).FirstOrDefault().Moves;
    }

    public List<SearchTreeBranch> GetAllBranches()
    {
      _branches = new List<SearchTreeBranch>();
      FindAllMoveBranchesRecursive(_table, 0);
      return _branches;
    }

    private void FindAllMoveBranchesRecursive(Table table, int currentDepth)
    {
      currentDepth++;
      if (currentDepth <= _unitOfWork.Config.GameRecursionDepth
        && table.GetAvailableMoves().Count > 0)
        MakeAllAvailableMovesForTable(table, currentDepth);
      else
        AddToBranch(table.GetPastMoves());
    }

    private void MakeAllAvailableMovesForTable(Table table, int currentDepth)
    {
      foreach (IMove move in table.GetAvailableMoves())
      {
        Table clone = (Table)table.Clone();
        clone.MakeMove(move);
        FindAllMoveBranchesRecursive(clone, currentDepth);
      }
    }

    private void AddToBranch(List<IMove> moves)
    {
      _branches.Add(new SearchTreeBranch(moves));
    }
  }
}
