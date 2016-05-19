using Solitaire.Lib.Config;
using Solitaire.Lib.Config.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Context
{
  public class TestUnitOfWork : IUnitOfWork
  {
    private IAppConfig _config;
    public IAppConfig Config
    {
      get
      {
        if (_config == null)
          _config = new TestConfig();
        return _config;
      }
    }
  }
}
