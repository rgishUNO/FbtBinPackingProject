namespace FbtBinPackingProject.VisualStudio.Tests

open FbtBinPackingProject
open Utility
open Microsoft.VisualStudio.TestTools.UnitTesting

type FitnessTestModule() =


    member x.AlgorithmTest(failedTest:Failure) =
          try
            let customerOrder_3_8_4_4 =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#3Name";
                          Description = "Toy#3Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    } 
                  ]
                }
            let customerOrder_3_8_4_2Mix =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 2M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 3M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#3Name";
                          Description = "Toy#3Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    } 
                  ]
                }
            let customerOrder_WuExample =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 6.4M<centimeters>
                          Width = 6.4M<centimeters>
                          Height = 5.2M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 2.1M<centimeters>
                          Width = 1.5M<centimeters>
                          Height = 4.6M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                  ]
                }
            let allBoxes_20_15_10 = 
              [
                {
                    Id = 
                      {
                          BoxId = "20_15_10";
                      }
                    Details =
                      {
                        Name = "Box12"
                        Description = "20 X 15X 10"
                      }
                    Dimensions =
                      {
                         Length = 20M<centimeters>;
                         Width = 15M<centimeters>;
                         Height = 10M<centimeters>
                      };
                }   
              ]
            let allBoxes_WuExample = 
                          [
                            {
                                Id = 
                                  {
                                      BoxId = "14_14_11.5";
                                  }
                                Details =
                                  {
                                    Name = "Box12"
                                    Description = "14 X 14 X 11.5"
                                  }
                                Dimensions =
                                  {
                                     Length = 14M<centimeters>;
                                     Width = 14M<centimeters>;
                                     Height = 11.5M<centimeters>
                                  };
                            }   
                          ]
            let allBoxes_WuExample2 = 
                          [
                            {
                                Id = 
                                  {
                                      BoxId = "8_14_11.5";
                                  }
                                Details =
                                  {
                                    Name = "Box8"
                                    Description = "8 X 14 X 11.5"
                                  }
                                Dimensions =
                                  {
                                     Length = 8M<centimeters>;
                                     Width = 14M<centimeters>;
                                     Height = 11.5M<centimeters>
                                  };
                            }   
                          ]
            let allBoxes_WuExample3 = 
                          [
                            {
                                Id = 
                                  {
                                      BoxId = "6.57_12_11.5";
                                  }
                                Details =
                                  {
                                    Name = "Box6.57"
                                    Description = "6.57 X 14 X 11.5"
                                  }
                                Dimensions =
                                  {
                                     Length = 6.57M<centimeters>;
                                     Width = 14M<centimeters>;
                                     Height = 11.5M<centimeters>
                                  };
                            }   
                          ]
            let customerOrder_WuExample2 =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 6.4M<centimeters>
                          Width = 6.4M<centimeters>
                          Height = 5.2M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 4.1M<centimeters>
                          Width = 3M<centimeters>
                          Height = 4.6M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#3Name";
                          Description = "Toy#3Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 4.1M<centimeters>
                          Width = 6M<centimeters>
                          Height = 4.6M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#4Name";
                          Description = "Toy43Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 4.1M<centimeters>
                          Width = 6M<centimeters>
                          Height = 4.6M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                  ]
                }            
            let gameWorld2 =
              {
                AvailableBoxes =
                  allBoxes_WuExample3
                  |> Seq.map (fun box -> (box.Id, box))
                  |> Map.ofSeq
                Customer = customer
                Order = customerOrder_WuExample2       
              }
            executeAlgorithmEvaluation gameWorld2 |> ignore
          with
           | ex -> failedTest.Failed <- true; failedTest.Message <- ex.Message; failedTest.InnerException <- ex.InnerException.ToString();

    member x.FitnessTest(failedTest:Failure) =
          try
            let customerOrder_3_8_4_4 =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#3Name";
                          Description = "Toy#3Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    } 
                  ]
                }
            let customerOrder_3_8_4_2Mix =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 2M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 3M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#3Name";
                          Description = "Toy#3Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 8M<centimeters>
                          Width = 4M<centimeters>
                          Height = 4M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    } 
                  ]
                }
            let customerOrder_WuExample =
              {
                Details =
                  {
                    Name = "Order#1Name";
                    Description = "Order#1Description"
                  }
                OrderId = 
                  {
                    OrderId = "Order#1";
                  }
                Toys = 
                  [
                    {
                      Details = 
                        {
                          Name = "Toy#1Name";
                          Description = "Toy#1Description"
                        }
                      Dimensions = 
                        {
                          Length = 6.4M<centimeters>
                          Width = 6.4M<centimeters>
                          Height = 5.2M<centimeters>
                        }
                      Weight = 1M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                    {
                      Details = 
                        {
                          Name = "Toy#2Name";
                          Description = "Toy#2Description"
                        };                 
                      Dimensions = 
                        {
                          Length = 2.1M<centimeters>
                          Width = 1.5M<centimeters>
                          Height = 4.6M<centimeters>
                        }
                      Weight = 2M<kilograms>
                      Origin =             
                        { 
                          X = 0M<centimeters>
                          Y = 0M<centimeters>
                          Z = 0M<centimeters>
                        }
                    }
                  ]
                }
            let allBoxes_20_15_10 = 
              [
                {
                    Id = 
                      {
                          BoxId = "20_15_10";
                      }
                    Details =
                      {
                        Name = "Box12"
                        Description = "20 X 15X 10"
                      }
                    Dimensions =
                      {
                         Length = 20M<centimeters>;
                         Width = 15M<centimeters>;
                         Height = 10M<centimeters>
                      };
                }   
              ]
            let allBoxes_WuExample = 
                          [
                            {
                                Id = 
                                  {
                                      BoxId = "14_14_11.5";
                                  }
                                Details =
                                  {
                                    Name = "Box12"
                                    Description = "14 X 14 X 11.5"
                                  }
                                Dimensions =
                                  {
                                     Length = 14M<centimeters>;
                                     Width = 14M<centimeters>;
                                     Height = 11.5M<centimeters>
                                  };
                            }   
                          ]
            let allBoxes_WuExample2 = 
                          [
                            {
                                Id = 
                                  {
                                      BoxId = "8_14_11.5";
                                  }
                                Details =
                                  {
                                    Name = "Box8"
                                    Description = "8 X 14 X 11.5"
                                  }
                                Dimensions =
                                  {
                                     Length = 8M<centimeters>;
                                     Width = 14M<centimeters>;
                                     Height = 11.5M<centimeters>
                                  };
                            }   
                          ]
            let gameWorld2 =
              {
                AvailableBoxes =
                  allBoxes_WuExample2
                  |> Seq.map (fun box -> (box.Id, box))
                  |> Map.ofSeq
                Customer = customer
                Order = customerOrder_WuExample       
              }
            executeFitnessEvaluation gameWorld2 |> ignore
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
        let failed = testModule.AlgorithmTest(failure)
        Assert.AreEqual("", failure.Message)
        Assert.AreEqual("", failure.InnerException)
        Assert.AreEqual(false, failure.Failed)