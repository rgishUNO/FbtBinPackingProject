module ExptectoTests

open Expecto
open FbtBinPackingProject
open System.Diagnostics

let boxData =         
    {
        Id = 
          {
              BoxId = "12";
          }
        Details =
          {
            Name = "Box12"
            Description = "Our very favorite box, Box #12."
          }
        Dimensions =
          {
             Length = 30M<centimeters>;
             Width = 30M<centimeters>;
             Height = 30M<centimeters>
          };
    }

let resultFromBox box =
  match Some(box) with 
  |  Some box -> Success box.Id.BoxId
  |  None -> Failure "Box does not exist"     

[<Tests>]
let tests =
  testList "test group" [
    testCase "yes" <| fun _ ->
        let subject = "Hello World"
        Expect.equal subject "Hello World"
                    "The strings should equal"

    testCase "get box returns box 12" <| fun _ ->
        let actual : Result<string, string> = describeCurrentBox gameWorld
        let expected = resultFromBox boxData

        // printfn actual.ToString()

        Expect.equal expected (actual)
                    "Box 12 should be returned"
    ]

[<EntryPoint>]
let main args =
  runTestsInAssembly defaultConfig args