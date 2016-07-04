using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire.Lib.Objects;

namespace Solitaire.Lib.Tests.ObjectTest
{
  [TestClass]
  public class StackHeadTests
  {
    [TestMethod]
    public void StackHeadTests_Equals_DifferentStackHeads_IsFalse()
    {
      FoundationStackHead foundationStackHead = new FoundationStackHead(new FoundationStack(1));
      TableauStackHead tableauStackHead = new TableauStackHead(new TableauStack(1));

      Assert.IsFalse(foundationStackHead.Equals(tableauStackHead));
    }

    [TestMethod]
    public void StackHeadTests_Equals_DifferentStackHeads2_IsFalse()
    {
      FoundationStackHead foundationStackHead = new FoundationStackHead(new FoundationStack(1));
      TableauStackHead tableauStackHead = new TableauStackHead(new TableauStack(1));

      Assert.IsFalse(tableauStackHead.Equals(foundationStackHead));
    }

    [TestMethod]
    public void StackHeadTests_Equals_EqualFoundationStackHeads_IsTrue()
    {
      FoundationStackHead foundationStackHead1 = new FoundationStackHead(new FoundationStack(1));
      FoundationStackHead foundationStackHead2 = new FoundationStackHead(new FoundationStack(1));

      Assert.IsTrue(foundationStackHead1.Equals(foundationStackHead2));
    }

    [TestMethod]
    public void StackHeadTests_Equals_DifferentFoundationStackHeads_IsTrue()
    {
      FoundationStackHead foundationStackHead1 = new FoundationStackHead(new FoundationStack(1));
      FoundationStackHead foundationStackHead2 = new FoundationStackHead(new FoundationStack(2));

      Assert.IsTrue(foundationStackHead1.Equals(foundationStackHead2));
    }

    [TestMethod]
    public void StackHeadTests_Equals_EqualTableauStackHeads_IsTrue()
    {
      TableauStackHead tableauStackHead1 = new TableauStackHead(new TableauStack(1));
      TableauStackHead tableauStackHead2 = new TableauStackHead(new TableauStack(1));

      Assert.IsTrue(tableauStackHead1.Equals(tableauStackHead2));
    }

    [TestMethod]
    public void StackHeadTests_Equals_DifferentTableauStackHeads_IsTrue()
    {
      TableauStackHead tableauStackHead1 = new TableauStackHead(new TableauStack(1));
      TableauStackHead tableauStackHead2 = new TableauStackHead(new TableauStack(2));

      Assert.IsTrue(tableauStackHead1.Equals(tableauStackHead2));
    }
  }
}
