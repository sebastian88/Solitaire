using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;
using Solitaire.Lib.Enums;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class FoundationMoveTests
  {
    private IUnitOfWork _unitOfWork;

    private Card _TwoDiamonds;
    private Card _eightSpades;
    private Card _nineSpades;
    private Card _nineHearts;
    private Card _tenClubs;
    private Card _tenHearts;
    private Card _tenDiamonds;
    private Card _jackDiamonds;

    public FoundationMoveTests()
    {
      _unitOfWork = IoCContainer.GetTestUnitOfWork();
    }

    private void Setup()
    {
      _TwoDiamonds = new Card(Values.Two, Suits.Diamonds);
      _eightSpades = new Card(Values.Eight, Suits.Spades);
      _nineSpades = new Card(Values.Nine, Suits.Spades);
      _nineHearts = new Card(Values.Nine, Suits.Hearts);
      _tenClubs = new Card(Values.Ten, Suits.Clubs);
      _tenHearts = new Card(Values.Ten, Suits.Hearts);
      _tenDiamonds = new Card(Values.Ten, Suits.Diamonds);
      _jackDiamonds = new Card(Values.Jack, Suits.Diamonds);
    }

    [TestMethod]
    public void FoundationMoveTests_IsValid_EightSpadesOnTenClubs_ReturnFalse()
    {
      Setup();

      FoundationMove move = new HandToFoundationMove(_unitOfWork, _eightSpades, _tenClubs);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void FoundationMoveTests_IsValid_NineHeartsOnEightSpades_ReturnFalse()
    {
      Setup();

      FoundationMove move = new HandToFoundationMove(_unitOfWork, _nineHearts, _eightSpades);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void FoundationMoveTests_IsValid_NineSpadesOnEightSpades_ReturnsTrue()
    {
      Setup();

      FoundationMove move = new HandToFoundationMove(_unitOfWork, _nineSpades, _eightSpades);

      Assert.AreEqual(move.IsValid(), true);
    }

    [TestMethod]
    public void FoundationMoveTests_IsValid_TenHeartsOnNineHearts_ReturnTrue()
    {
      Setup();

      FoundationMove move = new HandToFoundationMove(_unitOfWork, _tenHearts, _nineHearts);

      Assert.AreEqual(move.IsValid(), true);
    }

    [TestMethod]
    public void FoundationMoveTests_IsValid_TwoDiamondsOnJackDiamonds_ReturnFalse()
    {
      Setup();

      FoundationMove move = new HandToFoundationMove(_unitOfWork, _TwoDiamonds, _jackDiamonds);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void FoundationMoveTests_IsValid_TenDiamondsOnJackDiamonds_ReturnFalse()
    {
      Setup();

      FoundationMove move = new HandToFoundationMove(_unitOfWork, _tenDiamonds, _jackDiamonds);

      Assert.AreEqual(move.IsValid(), false);
    }

  }
}
