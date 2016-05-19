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
    private static IUnitOfWork _testUnitOfWork;

    public static IUnitOfWork GetTestUnitOfWork()
    {
      if(_testUnitOfWork == null)
        _testUnitOfWork = new TestUnitOfWork();
      return _testUnitOfWork;
    }

    public static IUnitOfWork GetUnitOfWork()
    {
      if (_unitOfWork == null)
        _unitOfWork = new TestUnitOfWork();
      return _unitOfWork;
    }

    public static void DisposeUnitOfWork()
    {

    }
  }
}
