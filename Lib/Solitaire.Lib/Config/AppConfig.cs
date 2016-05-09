using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Config
{
  public class AppConfig : Interfaces.IAppConfig
  {
    public int TableauToTableauMoveValue { get; private set; }
    public int TableauToFoundationMoveValue { get; private set; }
    public int HandToFoundationMoveValue { get; private set; }
    public int HandToTableauMoveValue { get; private set; }

    public AppConfig()
    {
      TableauToTableauMoveValue = 6;
      TableauToFoundationMoveValue = 6;
      HandToFoundationMoveValue = 6;
      HandToTableauMoveValue = 6;
    }
  }
}
