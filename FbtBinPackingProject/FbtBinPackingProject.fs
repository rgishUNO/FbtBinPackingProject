module FbtBinPackingProject
open System.Drawing
open System.Runtime.CompilerServices

/// ------------ Units of Measure ---------------
[<Measure>] type centimeters
[<Measure>] type kilograms
[<Measure>] type degrees

//
// -------------   Model   --------------
//

type Details = 
  {
      Name: string
      Description: string
  }

type Dimensions =
  {
    Length: decimal<centimeters>
    Width: decimal<centimeters>
    Height: decimal<centimeters>
  }

 type ConvexHull =
  {
    Dimensions: Dimensions
  }

 type Point = 
  {
    X: decimal<centimeters>
    Y: decimal<centimeters>
    Z: decimal<centimeters>
  }

 type Origin =
  {
    Point : Point
  }

 type ExtremePoints = 
  {
    LeftFrontBottomPoint: Point
    RightBehindBottomPoint: Point
    LeftBehindTopPoint: Point
  }

type Orientation =
  {
      Phi: decimal<degrees>
      Theta: decimal<degrees>
  }

type Toy =
  {
      Details: Details
      Dimensions: Dimensions
      Weight: decimal<kilograms>
      Origin: Point
  }

type ToyWithExtremePoints =
  {
      Toy: Toy * ExtremePoints
  }

type ToyWithExtremePointsAndOrientations =
  {
      ToyWithExtremePoints: ToyWithExtremePoints * Orientation
  }

type OrderId =
   {
     OrderId: string
   }

type Order =
  {
      Details: Details
      OrderId: OrderId
      Toys: Toy list
  }

type Packing =
  {
      Order: Order
      SetOfExtremePoints: Point list
  }

type BoxId =
   {
     BoxId: string
   }

type Box =
  {
    Id: BoxId
    Details: Details
    Dimensions: Dimensions   
  }

type PackagedOrder = 
   | BoxedOrder of Order * Box

type Customer =
  {
      Details: Details
      Cart: Order option
  }

type World =
  {
      AvailableBoxes: Map<BoxId, Box>
      Customer: Customer
      Order: Order
  }

// ------------ Initial World --------------

let initialSetOfExtremePoints = 
  [
    {
        X = 0M<centimeters>
        Y = 0M<centimeters>
        Z = 0M<centimeters>
    }

  ]

let blankOrder =
  {
    Details =
      {
        Name = "";
        Description = ""
      }
    OrderId = 
      {
        OrderId = "";
      }
    Toys = 
      [
        {
          Details = 
            {
              Name = "";
              Description = ""
            }
          Dimensions = 
            {
              Length = 0M<centimeters>
              Width = 0M<centimeters>
              Height = 0M<centimeters>
            }
          Weight = 0M<kilograms>
          Origin =             
            { 
              X = 0M<centimeters>
              Y = 0M<centimeters>
              Z = 0M<centimeters>
            }
        }               
      ]
    }

let customerOrder =
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

let allBoxes = 
  [
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
             Length = 20M<centimeters>;
             Width = 15M<centimeters>;
             Height = 10M<centimeters>
          };
    }
    {
        Id = 
          {
              BoxId = "13";
          }
        Details =
          {
            Name = "Box13"
            Description = "Our second favorite box, Box #13."
          }          
        Dimensions =
          {
             Length = 35M<centimeters>;
             Width = 20M<centimeters>;
             Height = 30M<centimeters>
          };
    }    
  ]

let customer =
  {
    Details = 
      { 
        Name = "ValuedCustomer";
        Description = "Our very first customer."
      }
    Cart = None
  }

let gameWorld =
  {
    AvailableBoxes =
      allBoxes
      |> Seq.map (fun box -> (box.Id, box))
      |> Map.ofSeq
    Customer = customer
    Order = customerOrder       
  }

//
// -------------   Railway Oriented Programming   --------------
//

type Result<'TSucess, 'TFailure> =
  | Success of 'TSucess
  | Failure of 'TFailure

let bind processFunction lastResult =
  match lastResult with
  | Success s -> processFunction s
  | Failure f -> Failure f

let (>>=) x f =
  bind f x

let switch processFunction input =
  Success (processFunction input)

let displayResult result =
  match result with
  | Success s -> printf "%s" s
  | Failure f -> printf "%s" f

let getResult result =
  match result with
  | Success s -> s
  | Failure f -> f

//
// -------------   Logic   --------------
//

let getOrientation orientation =
  match (orientation.Theta, orientation.Phi) with
  |  (theta, phi) 
      when theta >= 0M<degrees> && theta <= 360M<degrees>
           && phi >= 0M<degrees> && theta <= 360M<degrees>
         -> Success orientation
  |  (_, _) -> Failure "Orientation does not exist"

let getMaxExtremePointsHeight (extremePoints:ExtremePoints) =
  let leftFrontRemoveMeasure = extremePoints.LeftFrontBottomPoint.Z / 1M<centimeters>
  let rightBehindRemoveMeasure = extremePoints.RightBehindBottomPoint.Z / 1M<centimeters>
  let leftBehindRemoveMeasure = extremePoints.LeftBehindTopPoint.Z / 1M<centimeters>
  let list = [leftFrontRemoveMeasure; rightBehindRemoveMeasure; leftBehindRemoveMeasure]
  let maxValueOfList = list |> List.max
  let maxWithMeasure = maxValueOfList * 1M<centimeters>
  maxWithMeasure

let getPackedToyConvexHullHeight packedToy =
  let packedToyConvexHullHeight = packedToy.Origin.Y + packedToy.Dimensions.Height
  packedToyConvexHullHeight

let getPackedToyConvexHullWidth packedToy =
  let packedToyConvexHullWidth = packedToy.Origin.X + packedToy.Dimensions.Width
  packedToyConvexHullWidth

let calculateConvexHull packedToy maxHeightSoFar = 
      let toyHeight = getPackedToyConvexHullHeight(packedToy)
      let list = [toyHeight; maxHeightSoFar]
      let maxValueOfList = list |> List.max
      maxValueOfList

let rec getHeightOfConvexHullOfCurrentlyPackedToys packedToys  maxHeightSoFar =
  match packedToys with
  | head :: tail ->
      let maxHeightWithMeasure = calculateConvexHull head maxHeightSoFar
      getHeightOfConvexHullOfCurrentlyPackedToys tail maxHeightWithMeasure
  | [] -> maxHeightSoFar

let getHeightOfConvexHullOfCurrentPacking (packedToys:Packing, maxHeightSoFar:decimal<centimeters>) =
  match packedToys.Order.Toys with
  | head :: tail ->
      let maxHeightWithMeasure = calculateConvexHull head maxHeightSoFar
      getHeightOfConvexHullOfCurrentlyPackedToys tail maxHeightWithMeasure
  | [] -> maxHeightSoFar

let volume length width height = 
  length * width * height

let rec getToysVolume (toys:Toy list, accumulator) =
  match toys with
  | head :: tail ->
      let toyVolumePacking = volume head.Dimensions.Length head.Dimensions.Width head.Dimensions.Height
      getToysVolume(tail, (accumulator + toyVolumePacking))
  | [] -> accumulator

let calculateConvexHullWithCurrentToy(extremePoint:Point, toy:Toy, convexHull:ConvexHull) =
  let toyLength = extremePoint.X + toy.Dimensions.Length
  let listLength = [toyLength; convexHull.Dimensions.Length]
  let maxValueOfListLength = listLength |> List.max

  let toyWidth = extremePoint.Y + toy.Dimensions.Width
  let listWidth = [toyWidth; convexHull.Dimensions.Width]
  let maxValueOfListWidth = listWidth |> List.max

  let toyHeight = extremePoint.Z + toy.Dimensions.Height
  let listHeight = [toyHeight; convexHull.Dimensions.Height]
  let maxValueOfListHeight = listHeight |> List.max

  let newConvexHull =
    {
      Dimensions = 
        {
          Length = maxValueOfListLength
          Width = maxValueOfListWidth
          Height = maxValueOfListHeight
        }
    }

  newConvexHull

let convexHullLength(extremePoint:Point, toy:Toy, currentConvexHullLengthMax) =
  let toyLength = extremePoint.X + toy.Dimensions.Length
  let list = [toyLength; currentConvexHullLengthMax]
  let maxValueOfList = list |> List.max
  maxValueOfList

let convexHullWidth(extremePoint:Point, toy:Toy, currentConvexHullWidthMax) =
  let toyWidth = extremePoint.Y + toy.Dimensions.Width
  let list = [toyWidth; currentConvexHullWidthMax]
  let maxValueOfList = list |> List.max
  maxValueOfList

let convexHullHeight(extremePoint:Point, toy:Toy, currentConvexHullHeightMax) =
  let toyHeight = extremePoint.Z + toy.Dimensions.Height
  let list = [toyHeight; currentConvexHullHeightMax]
  let maxValueOfList = list |> List.max
  maxValueOfList

let attemptingToDivideByZero volumeOfToysPackedSoFarIncludingCurrentToy =
  volumeOfToysPackedSoFarIncludingCurrentToy = 0M<centimeters^3>

/// Measure of height over the fill rate of the current convex hull
let packingIndex(length:decimal<centimeters>, width:decimal<centimeters>, height:decimal<centimeters>, volumeOfToysPackedSoFarIncludingCurrentToy:decimal<centimeters^3>) =
  let heightSquared = height * height
  let numerator = length * width * heightSquared
  let index =
    if (attemptingToDivideByZero volumeOfToysPackedSoFarIncludingCurrentToy) then
      numerator/(1M * 1M<centimeters^3>)
    else
      (numerator/volumeOfToysPackedSoFarIncludingCurrentToy)
  index

let maxPackingIndex x y = 
  let (a, b, c) = x
  let (d, e, f) = y
  if a < d then (d, e, f) else (a, b, c)

let rec calculateIndex (currentToy:Toy, extremePoint:Point, volume:decimal<centimeters^3>, convexHull:ConvexHull, maxSoFar) =
  let convexHullPoint = 
    calculateConvexHullWithCurrentToy(extremePoint, currentToy, convexHull)
  let lpj = convexHullPoint.Dimensions.Length
  let wpj = convexHullPoint.Dimensions.Width
  let hpj = convexHullPoint.Dimensions.Height
  let index = (packingIndex(lpj, wpj, hpj, volume), extremePoint, convexHullPoint)

  let max = maxPackingIndex index maxSoFar
  max

let rec calculateIndexViaListOfExtremePointsRecursive (currentToy:Toy, extremePoints:Point list, volume:decimal<centimeters^3>, maxIndexSoFar, convexHull:ConvexHull) =
  match extremePoints with
  | head :: tail ->
      let extremePointsIndex = calculateIndex(currentToy, head, volume, convexHull, maxIndexSoFar) 
      calculateIndexViaListOfExtremePointsRecursive(currentToy, tail, volume, extremePointsIndex, convexHull)
  | [] -> maxIndexSoFar

let  calculateIndexViaListOfExtremePoints (currentToy:Toy, setOfExtremePoints:Point list, volume:decimal<centimeters^3>, maxIndexSoFar, convexHull:ConvexHull) =
  match setOfExtremePoints with
  | head :: tail ->
      let extremePointsIndex = calculateIndex(currentToy, head, volume, convexHull, maxIndexSoFar)
      calculateIndexViaListOfExtremePointsRecursive(currentToy, tail, volume, extremePointsIndex, convexHull)
  | [] -> maxIndexSoFar

let calculateToyExtremePoints(currentToy: Toy) =
  let setOfExtremePoints = 
    [
      { 
        X = currentToy.Origin.X + currentToy.Dimensions.Length
        Y = currentToy.Origin.Y
        Z =  currentToy.Origin.Z
      }
      { 
        X = currentToy.Origin.X
        Y = currentToy.Origin.Y + currentToy.Dimensions.Width
        Z = currentToy.Origin.Z
      }
      { 
        X = currentToy.Origin.X
        Y = currentToy.Origin.Y
        Z = currentToy.Origin.Z + currentToy.Dimensions.Height
      }

    ]
  setOfExtremePoints

let revealDimensions dimensions =
  dimensions

let extractDimensionsFromBox (box : Box) =
  box.Dimensions

let noVolumeLeft remainingVolume =
  remainingVolume <= 0M<centimeters^3>

let blankPacking =
  {       
    SetOfExtremePoints = initialSetOfExtremePoints
    Order = blankOrder
  }

let doesBoxHaveEnoughFreeVolumeForToy (dimensions:Dimensions, toy:Toy, packing:Packing) =
  let toyVolume = volume toy.Dimensions.Length toy.Dimensions.Width toy.Dimensions.Height
  let boxVolume = volume dimensions.Length dimensions.Width dimensions.Height  
  let volumeLeft = boxVolume - toyVolume
  match volumeLeft with 
  |  i when noVolumeLeft i -> blankPacking
  |  _ -> packing

let tolerance = 0.001M<centimeters>

let roomInBoxForNewToy (dimensions:Dimensions, toy:Toy) =
  let lengthAvailability = dimensions.Length - (toy.Origin.X + toy.Dimensions.Length) >= tolerance
  let widthAvailability = dimensions.Width - (toy.Origin.Y + toy.Dimensions.Width) >= tolerance
  let heightAvailability = dimensions.Height - (toy.Origin.Z + toy.Dimensions.Height) >= tolerance
  lengthAvailability && widthAvailability && heightAvailability

let rec fitnessEvaluation (toys:Toy list, box:Box, packing:Packing, convexHull:ConvexHull, maxIndexSoFar) =
  let accumulator = 0M<centimeters^3>
  match toys with
  | head :: tail ->
      let currentHeightOfConvexHull = getHeightOfConvexHullOfCurrentPacking(packing, convexHull.Dimensions.Height)
      let packedToyVolume = getToysVolume(packing.Order.Toys, accumulator)
      let currentToyVolume = volume head.Dimensions.Length head.Dimensions.Width head.Dimensions.Height
      let inclusiveToyVolume = packedToyVolume + currentToyVolume
      let index = calculateIndexViaListOfExtremePoints(head, packing.SetOfExtremePoints, inclusiveToyVolume, maxIndexSoFar, convexHull)
      let fitVolumePacking = doesBoxHaveEnoughFreeVolumeForToy(box.Dimensions, head, packing)
      let (_, indexPoint, _) = index
      let newConvexHull = calculateConvexHullWithCurrentToy(indexPoint, head, convexHull)
      let setOfExtremePointsFound = 
        [
          {
            X = indexPoint.X
            Y = indexPoint.Y
            Z = indexPoint.Z
          }
        ]
      let filteredExtremePoints = (Set fitVolumePacking.SetOfExtremePoints) - (Set setOfExtremePointsFound) |> Set.toList
      let newPackedToy =
        {
          Details = head.Details
          Dimensions = head.Dimensions
          Weight = head.Weight
          Origin = indexPoint
        }
      let availability = roomInBoxForNewToy(box.Dimensions, newPackedToy)
      let newExtremePointsFromNewlyPackedToy = calculateToyExtremePoints(newPackedToy)
      let newExtremePoints = (Set filteredExtremePoints) + (Set newExtremePointsFromNewlyPackedToy) |> Set.toList
      let newOrder =
        {
          Details = fitVolumePacking.Order.Details
          OrderId = fitVolumePacking.Order.OrderId
          Toys = newPackedToy::fitVolumePacking.Order.Toys      
        }
      let newPacking =
        {       
          SetOfExtremePoints = newExtremePoints
          Order = newOrder
        }
      fitnessEvaluation(tail, box, newPacking, newConvexHull, index)
  | [] -> packing

let getPoint (point:Point) =
  match point with 
  |  point -> Success point

let getPacking (packing:Packing) =
  match packing with 
  |  packing -> Success packing

let getConvexHull (convexHull:ConvexHull) =
  match convexHull with 
  |  convexHull -> Success convexHull

let getBox world =
  match Some(world.AvailableBoxes |> Seq.head) with 
  |  Some (KeyValue(k, v)) -> Success v
  |  None -> Failure "Box does not exist"  

let blankBox =
     {
        Id = 
          {
              BoxId = "0";
          }
        Details =
          {
            Name = "Box0"
            Description = "Blank box"
          }
        Dimensions =
          {
             Length = 0M<centimeters>;
             Width = 0M<centimeters>;
             Height = 0M<centimeters>
          };
    }

let getBox2 world =
  match Some(world.AvailableBoxes |> Seq.head) with 
  |  Some (KeyValue(k, v)) -> v
  |  None -> blankBox  

let getToys world =
  match Some world.Order.Toys with 
  |  Some toys -> Success toys
  |  None -> Failure "Toys do not exist"

let executeFitnessEvaluation world =
  let packing =
    {       
      SetOfExtremePoints = initialSetOfExtremePoints
      Order = blankOrder
    }
  let box = getBox2 world
  let initialConvexHull =
    {
        Dimensions = 
          {
            Length = 0M<centimeters>
            Width = 0M<centimeters>
            Height = 0M<centimeters>
          }
    }
  let initialPoint = 
   {
      X = 0M<centimeters>
      Y = 0M<centimeters>
      Z = 0M<centimeters>
   }
  let maxIndexSoFar = (0M<centimeters>, initialPoint, initialConvexHull)
  fitnessEvaluation(world.Order.Toys, box, packing, initialConvexHull, maxIndexSoFar)

let getToy world =
  match Some(world.Order.Toys |> Seq.head) with 
  |  Some toy -> Success toy
  |  None -> Failure "Toy does not exist"

let describeDetails details =
  printf "\n\n%s\n\n%s\n\n" details.Name details.Description
  sprintf "\n\n%s\n\n%s\n\n" details.Name details.Description

let extractDetailsFromToy (toy : Toy) =
  toy.Details

let extractDetailsFromBox (box : Box) =
  box.Details

let describeCurrentToy world =
  getToy world
  |> (bind (switch extractDetailsFromToy) >> bind (switch describeDetails))

let describeCurrentBox world =
  getBox world
  |> (bind (switch extractDetailsFromBox) >> bind (switch describeDetails))  

gameWorld
|> describeCurrentBox
|> ignore //displayResult