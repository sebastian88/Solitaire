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

    public Game(IUnitOfWork unitOfWork, List<ICard> cards)
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
      List<IMove> movesToMake = GetBestMoveBranch();
      do
      {
        MakeMovesUntillCardIsTurned(movesToMake);
        movesToMake = GetBestMoveBranch();
      } while (movesToMake.Count > 0);
    }

    private void MakeMovesUntillCardIsTurned(List<IMove> movesToMake)
    {
      foreach (IMove move in movesToMake)
      {
        _table.MakeMove(move);
        if (_table.UpTurnEndTableauCards())
          break;
      }
    }

    public List<IMove> GetBestMoveBranch()
    {
      _branches = new List<SearchTreeBranch>();
      return PickBestMoveBranch();
    }

    private List<IMove> PickBestMoveBranch()
    {
      FindAllMoveBranchesRecursive(GetTableWithNoPreviousMoves(), 0);
      return GetBestBranch();
    }

    private Table GetTableWithNoPreviousMoves()
    {
      Table tableWithNoPreviousMoves = (Table)_table.Clone();
      tableWithNoPreviousMoves.SetPastMoves(new List<IMove>());
      return tableWithNoPreviousMoves;
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
