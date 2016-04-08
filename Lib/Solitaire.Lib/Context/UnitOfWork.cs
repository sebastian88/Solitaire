using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Context
{
  public interface UnitOfWork
  {
    Solitaire.Lib.Config.AppConfig Config { get; }
  }
}
