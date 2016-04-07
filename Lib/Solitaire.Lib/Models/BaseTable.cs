using Solitaire.Lib.FunctionalModels.Hands;
using Solitaire.Lib.FunctionalModels.Stacks.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Models
{
  public class BaseTable
  {
    public List<TableauStack> Tableau { get; set; }
    public List<FoundationStack> Foundation { get; set; }
    public Hand Hand { get; set; }
  }
}
