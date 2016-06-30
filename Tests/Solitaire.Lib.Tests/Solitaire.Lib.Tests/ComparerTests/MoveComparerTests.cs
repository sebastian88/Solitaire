using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Comparers;
using Solitaire.Lib.Enums;

namespace Solitaire.Lib.Tests.ComparerTests
{
  [TestClass]
  public class MoveComparerTests
  {
    IUnitOfWork _unitOfWork;

    private Card _eightSpades;
    private Card _nineSpades;
    private Card _nineHearts;
    private Card _tenClubs;
    private Card _tenHearts;

    public MoveComparerTests()
    {
      _unitOfWork = IoCContainer.GetTestUnitOfWork();
    }

    private MoveComparer Setup()
    {
      _eightSpades = new Card(Values.Eight, Suits.Spades);
      _nineSpades = new Card(Values.Nine, Suits.Spades);
      _nineHearts = new Card(Values.Nine, Suits.Hearts);
      _tenClubs = new Card(Values.Ten, Suits.Clubs);
      _tenHearts = new Card(Values.Ten, Suits.Hearts);

      return new MoveComparer();
    }

    [TestMethod]
    public void MoveComparerTests_Equals_SameMove_ReturnTrue()
    {
      MoveComparer moveComparer = Setup();

      IMove firstMove =  new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts);
      IMove secondMove = new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts);

      Assert.IsTrue(moveComparer.Equals(firstMove, secondMove));
    }

    [TestMethod]
    public void MoveComparerTests_Equals_DifferentMove_ReturnFalse()
    {
      MoveComparer moveComparer = Setup();

      IMove firstMove = new HandToFoundationMove(_unitOfWork, _nineHearts, _nineSpades);
      IMove secondMove = new HandToFoundationMove(_unitOfWork, _tenHearts, _nineHearts);
      
      Assert.IsFalse(moveComparer.Equals(firstMove, secondMove));
    }

    [TestMethod]
    public void MoveComparerTests_Equals_ListOfMovesOneIsMatch_IsTrue()
    {
      MoveComparer moveComparer = Setup();

      List<IMove> moves = new List<IMove>()
      {
        new HandToFoundationMove(_unitOfWork, _nineHearts, _nineSpades),
        new HandToFoundationMove(_unitOfWork, _tenHearts, _nineHearts),
        new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts),
        new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts)
      };

      Assert.IsTrue(moves.Contains<IMove>(new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts), moveComparer));
    }

    [TestMethod]
    public void MoveComparerTests_Equals_ListOfMovesFirstIsMatch_IsTrue()
    {
      MoveComparer moveComparer = Setup();

      List<IMove> moves = new List<IMove>()
      {
        new HandToFoundationMove(_unitOfWork, _nineHearts, _nineSpades),
        new HandToFoundationMove(_unitOfWork, _tenHearts, _nineHearts),
        new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts),
        new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts)
      };

      Assert.IsTrue(moves.Contains<IMove>(new HandToFoundationMove(_unitOfWork, _nineHearts, _nineSpades), moveComparer));
    }

    [TestMethod]
    public void MoveComparerTests_Equals_ListOfMovesNoneAreMatch_IsFalse()
    {
      MoveComparer moveComparer = Setup();

      List<IMove> moves = new List<IMove>()
      {
        new HandToFoundationMove(_unitOfWork, _nineHearts, _nineSpades),
        new HandToFoundationMove(_unitOfWork, _tenHearts, _nineHearts),
        new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts),
        new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts)
      };

      Assert.IsFalse(moves.Contains<IMove>(new HandToFoundationMove(_unitOfWork, _nineHearts, _tenClubs), moveComparer));
    }
  }
}
