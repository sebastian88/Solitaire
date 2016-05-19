using Solitaire.Lib.Config.Interfaces;

namespace Solitaire.Lib.Config
{
  public class TestConfig : IAppConfig
  {
    public int TableauToTableauMoveValue { get; private set; }
    public int TableauToFoundationMoveValue { get; private set; }
    public int HandToFoundationMoveValue { get; private set; }
    public int HandToTableauMoveValue { get; private set; }
    public int GameRecursionDepth { get; private set; }

    public TestConfig()
    {
      TableauToTableauMoveValue = 6;
      TableauToFoundationMoveValue = 6;
      HandToFoundationMoveValue = 6;
      HandToTableauMoveValue = 6;
      GameRecursionDepth = 7;
    }
  }
}
