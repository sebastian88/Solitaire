﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Config;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.Config.Interfaces;

namespace Solitaire.Lib.Context
{
  public class UnitOfWork : IUnitOfWork
  {
    private IAppConfig _config;
    public IAppConfig Config
    {
      get
      {
        if (_config == null)
          _config = new AppConfig();
        return _config;
      }
    }
  }
}
