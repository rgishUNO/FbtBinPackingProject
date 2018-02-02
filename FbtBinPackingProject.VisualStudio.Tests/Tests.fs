namespace FbtBinPackingProject.VisualStudio.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open FbtBinPackingProject


[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);
