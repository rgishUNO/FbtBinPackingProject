module Console

open FbtBinPackingProject

[<EntryPoint>]
let main args =
  let packing =
    {       
      SetOfExtremePoints = initialSetOfExtremePoints
      Order = blankOrder
    }
  let box = getBox gameWorld
  let initialConvexHull =
    {
        Dimensions = 
          {
            Length = 0M<centimeters>
            Width = 0M<centimeters>
            Height = 0M<centimeters>
          }
    }
  executeFitnessEvaluation gameWorld |> ignore
  let box = describeCurrentBox gameWorld
  let myint = 7
  myint