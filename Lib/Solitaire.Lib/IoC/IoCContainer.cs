using Solitaire.Lib.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.IoC
{
  public class IoCContainer
  {
    private static UnitOfWork _unitOfWork;
    public static UnitOfWork GetUnitOfWork()
    {
      if(_unitOfWork == null)
        _unitOfWork = new Solitaire.Lib.Context.Impl.UnitOfWork();
      return _unitOfWork;
    }

    public static void DisposeUnitOfWork()
    {

    }
  }
}
