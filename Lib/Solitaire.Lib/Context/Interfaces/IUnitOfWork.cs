using Solitaire.Lib.Config.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Context.Interfaces
{
  public interface IUnitOfWork
  {
    IAppConfig Config { get; }
  }
}
