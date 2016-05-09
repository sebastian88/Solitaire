using Solitaire.Lib.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Context.Interfaces;

namespace Solitaire.Lib.IoC
{
  public class IoCContainer
  {
    private static IUnitOfWork _unitOfWork;
    public static IUnitOfWork GetUnitOfWork()
    {
      if(_unitOfWork == null)
        _unitOfWork = new UnitOfWork();
      return _unitOfWork;
    }

    public static void DisposeUnitOfWork()
    {

    }
  }
}
