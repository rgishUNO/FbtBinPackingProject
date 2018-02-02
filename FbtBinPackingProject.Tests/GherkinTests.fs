module GherkinTests

open TickSpec
open NUnit.Framework
open FbtBinPackingProject

let mutable order = customerOrder

let [<Given>] ``a customer purchases an order``() = 
    ()

let [<Given>] ``said customer puts (.*) toys into the his or her cart``() = 
    order

let [<When>] ``all available box selections are available`` () =  
    allBoxes
      
let [<Then>] ``the order should ideal fit into (.*) Box 12`` (n:Result<string, string>) =     
    let passed = (describeCurrentToy gameWorld = n)
    Assert.True(passed)