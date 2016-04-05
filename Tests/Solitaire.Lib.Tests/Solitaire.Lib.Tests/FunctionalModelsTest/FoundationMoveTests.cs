using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.FunctionalModels.Moves;
using Solitaire.Lib.FunctionalModels.Moves.Impl;
using Solitaire.Lib.Models;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class FoundationMoveTests
  {
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
    }

    private void Setup()
    {
      _TwoDiamonds = new Card(Enums.Values.Two, Enums.Suits.Diamonds);
      _eightSpades = new Card(Enums.Values.Eight, Enums.Suits.Spades);
      _nineSpades = new Card(Enums.Values.Nine, Enums.Suits.Spades);
      _nineHearts = new Card(Enums.Values.Nine, Enums.Suits.Hearts);
      _tenClubs = new Card(Enums.Values.Ten, Enums.Suits.Clubs);
      _tenHearts = new Card(Enums.Values.Ten, Enums.Suits.Hearts);
      _tenDiamonds = new Card(Enums.Values.Ten, Enums.Suits.Diamonds);
      _jackDiamonds = new Card(Enums.Values.Jack, Enums.Suits.Diamonds);
    }

    [TestMethod]
    public void IsValid_EightSpadesOnTenClubs_ReturnFalse()
    {
      Setup();

      FoundationMove move = new FoundationMove(_eightSpades, _tenClubs);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void IsValid_NineHeartsOnEightSpades_ReturnFalse()
    {
      Setup();

      FoundationMove move = new FoundationMove(_nineHearts, _eightSpades);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void IsValid_NineSpadesOnEightSpades_ReturnsTrue()
    {
      Setup();

      FoundationMove move = new FoundationMove(_nineSpades, _eightSpades);

      Assert.AreEqual(move.IsValid(), true);
    }

    [TestMethod]
    public void IsValid_TenHeartsOnNineHearts_ReturnTrue()
    {
      Setup();

      FoundationMove move = new FoundationMove(_tenHearts, _nineHearts);

      Assert.AreEqual(move.IsValid(), true);
    }

    [TestMethod]
    public void IsValid_TwoDiamondsOnJackDiamonds_ReturnFalse()
    {
      Setup();

      FoundationMove move = new FoundationMove(_TwoDiamonds, _jackDiamonds);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void IsValid_TenDiamondsOnJackDiamonds_ReturnFalse()
    {
      Setup();

      FoundationMove move = new FoundationMove(_tenDiamonds, _jackDiamonds);

      Assert.AreEqual(move.IsValid(), false);
    }

  }
}
