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
  }
}
