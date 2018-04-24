namespace FbtBinPackingProject.VisualStudio.Tests

open FbtBinPackingProject
open Utility
open Microsoft.VisualStudio.TestTools.UnitTesting

type FitnessTestModule() =

    member x.FitnessTest(failedTest:Failure) =
          try
            executeFitnessEvaluation gameWorld |> ignore
          with
           | ex -> failedTest.Failed <- true; failedTest.Message <- ex.Message; failedTest.InnerException <- ex.InnerException.ToString();

     member x.All(failedTest:Failure) = 
       try
         x.FitnessTest(failedTest)
         false
       with 
         | _ -> true

 module FitnessTests =

  [<TestClass>]
  type UnitTest() = 

    [<TestMethod>]
    [<TestCategory("Integration")>]
    member x.FitnessTest() =
        let testModule = new FitnessTestModule()
        let mutable failure = new Failure();
        let failed = testModule.FitnessTest(failure)
        Assert.AreEqual("", failure.Message)
        Assert.AreEqual("", failure.InnerException)
        Assert.AreEqual(false, failure.Failed)