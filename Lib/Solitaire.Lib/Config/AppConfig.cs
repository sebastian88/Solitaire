using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Config
{
  public interface AppConfig
  {
    int TableauToTableauMoveValue { get; }
    int TableauToFoundationMoveValue { get; }
    int HandToFoundationMoveValue { get; }
    int HandToTableauMoveValue { get; }
  }
}
