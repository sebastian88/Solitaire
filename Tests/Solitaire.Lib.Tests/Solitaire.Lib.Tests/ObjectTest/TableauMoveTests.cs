using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.IoC;

namespace Solitaire.Lib.Tests.FunctionalModelsTest
{
  [TestClass]
  public class TableauMoveTests
  {
    private IUnitOfWork _unitOfWork;

    private Card _eightSpades;
    private Card _nineSpades;
    private Card _nineHearts;
    private Card _tenClubs;
    private Card _tenHearts;

    public TableauMoveTests()
    {
      _unitOfWork = IoCContainer.GetUnitOfWork();
    }

    private void Setup()
    {
      _eightSpades = new Card(Enums.Values.Eight, Enums.Suits.Spades);
      _nineSpades = new Card(Enums.Values.Nine, Enums.Suits.Spades);
      _nineHearts = new Card(Enums.Values.Nine, Enums.Suits.Hearts);
      _tenClubs = new Card(Enums.Values.Ten, Enums.Suits.Clubs);
      _tenHearts = new Card(Enums.Values.Ten, Enums.Suits.Hearts);
    }

    [TestMethod]
    public void TableauMoveTests_IsValid_NineSpadesOnTenHearts_ReturnsTrue()
    {
      Setup();

      TableauMove move = new HandToTableauMove(_unitOfWork, _nineSpades, _tenHearts);

      Assert.AreEqual(move.IsValid(), true);
    }

    [TestMethod]
    public void TableauMoveTests_IsValid_TenHeartsOnNineSpades_ReturnsFalse()
    {
      Setup();

      TableauMove move = new HandToTableauMove(_unitOfWork, _tenHearts, _nineSpades);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void TableauMoveTests_IsValid_EightSpadesOnTenClubs_ReturnsFalse()
    {
      Setup();

      TableauMove move = new HandToTableauMove(_unitOfWork, _eightSpades, _tenClubs);

      Assert.AreEqual(move.IsValid(), false);
    }

    [TestMethod]
    public void TableauMoveTests_IsValid_NineSpadesOnTenClubs_ReturnsFalse()
    {

      Setup();

      TableauMove move = new HandToTableauMove(_unitOfWork, _nineSpades, _tenClubs);

      Assert.AreEqual(move.IsValid(), false);
    }
  }
}
