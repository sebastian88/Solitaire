using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Config;

namespace Solitaire.Lib.Context.Impl
{
  public class UnitOfWork : Solitaire.Lib.Context.UnitOfWork
  {
    private AppConfig _config;
    public AppConfig Config
    {
      get
      {
        if (_config == null)
          _config = new Solitaire.Lib.Config.Impl.AppConfig();
        return _config;
      }
    }
  }
}
