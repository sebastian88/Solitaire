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
      _unitOfWork = IoCContainer.GetUnitOfWork();
    }

    private MoveComparer Setup()
    {
      _eightSpades = new Card(Enums.Values.Eight, Enums.Suits.Spades);
      _nineSpades = new Card(Enums.Values.Nine, Enums.Suits.Spades);
      _nineHearts = new Card(Enums.Values.Nine, Enums.Suits.Hearts);
      _tenClubs = new Card(Enums.Values.Ten, Enums.Suits.Clubs);
      _tenHearts = new Card(Enums.Values.Ten, Enums.Suits.Hearts);

      return new MoveComparer();
    }

    [TestMethod]
    public void MoveComparerTests_Equals_SameMove_ReturnTrue()
    {
      MoveComparer moveComparer = Setup();

      IMove firstMove = new HandToFoundationMove(_unitOfWork, _eightSpades, _nineHearts);
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
  }
}
