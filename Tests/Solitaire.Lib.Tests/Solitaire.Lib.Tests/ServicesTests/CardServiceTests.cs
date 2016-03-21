using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Models;
using Solitaire.Lib.Services;

namespace Solitaire.Lib.Tests.ServicesTests
{
  [TestClass]
  public class CardServiceTests
  {
    private CardService _cardService;
    public CardServiceTests()
    {
      _cardService = new CardService();
    }

    [TestMethod]
    public void IsValidTableauMove_NineSpadesOnTenClubs_ReturnsTrue()
    {
      Card nineSpades = new Card(Enums.Values.Nine, Enums.Suits.Spades);
      Card tenClubs = new Card(Enums.Values.Ten, Enums.Suits.Clubs);
      
      bool IsValidTableauMove = _cardService.IsValidTableauMove(nineSpades, tenClubs);

      Assert.AreEqual(IsValidTableauMove, false);
    }

    [TestMethod]
    public void IsValidTableauMove_TenClubsOnNineSpades_ReturnsFalse()
    {
      Card tenClubs = new Card(Enums.Values.Ten, Enums.Suits.Clubs);
      Card nineSpades = new Card(Enums.Values.Nine, Enums.Suits.Spades);

      bool IsValidTableauMove = _cardService.IsValidTableauMove(tenClubs, nineSpades);

      Assert.AreEqual(IsValidTableauMove, false);
    }

    [TestMethod]
    public void IsValidTableauMove_EightSpadesOnTenClubs_ReturnsFalse()
    {
      Card eightSpades = new Card(Enums.Values.Eight, Enums.Suits.Spades);
      Card tenClubs = new Card(Enums.Values.Ten, Enums.Suits.Clubs);
      
      bool IsValidTableauMove = _cardService.IsValidTableauMove(eightSpades, tenClubs);

      Assert.AreEqual(IsValidTableauMove, false);
    }

    [TestMethod]
    public void IsValidTableauMove_NineSpadesOnTenSpades_ReturnsFalse()
    {
      Card nineSpades = new Card(Enums.Values.Nine, Enums.Suits.Spades);
      Card tenSpades = new Card(Enums.Values.Ten, Enums.Suits.Spades);
      
      bool IsValidTableauMove = _cardService.IsValidTableauMove(nineSpades, tenSpades);

      Assert.AreEqual(IsValidTableauMove, false);
    }

    [TestMethod]
    public void IsValidTableauMove_NineHeartsOnTenSpades_ReturnsFalse()
    {
      Card nineHearts = new Card(Enums.Values.Nine, Enums.Suits.Hearts);
      Card tenSpades = new Card(Enums.Values.Ten, Enums.Suits.Spades);

      bool IsValidTableauMove = _cardService.IsValidTableauMove(nineHearts, tenSpades);

      Assert.AreEqual(IsValidTableauMove, true);
    }
  }
}
