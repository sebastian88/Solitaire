using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Config;

namespace Solitaire.Lib.Context
{
  public class UnitOfWork : Interfaces.IUnitOfWork
  {
    private AppConfig _config;
    public AppConfig Config
    {
      get
      {
        if (_config == null)
          _config = new Solitaire.Lib.Config.AppConfig();
        return _config;
      }
    }
  }
}
