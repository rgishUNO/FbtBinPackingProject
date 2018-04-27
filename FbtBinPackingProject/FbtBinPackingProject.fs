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
      SetOfExtremePoints: ExtremePoints list
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
      LeftFrontBottomPoint =
        { 
          X = 0M<centimeters>
          Y = 0M<centimeters>
          Z = 0M<centimeters>
        }
      RightBehindBottomPoint =
        { 
          X = 0M<centimeters>
          Y = 0M<centimeters>
          Z = 0M<centimeters>
        }
      LeftBehindTopPoint = 
        { 
          X = 0M<centimeters>
          Y = 0M<centimeters>
          Z = 0M<centimeters>
        }
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
              Length = 30M<centimeters>
              Width = 12M<centimeters>
              Height = 17M<centimeters>
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
              Length = 60M<centimeters>
              Width = 25M<centimeters>
              Height = 35M<centimeters>
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
             Length = 30M<centimeters>;
             Width = 30M<centimeters>;
             Height = 30M<centimeters>
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
      let toyHeightRemoveMeasure = toyHeight / 1M<centimeters>
      let heightSoFarRemoveMeasure = maxHeightSoFar / 1M<centimeters>
      let list = [toyHeightRemoveMeasure; heightSoFarRemoveMeasure]
      let maxValueOfList = list |> List.max
      let maxHeightWithMeasure = maxValueOfList * 1M<centimeters>
      maxHeightWithMeasure

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
  let maxValueOfListLenth = listLength |> List.max

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
          Length = maxValueOfListLenth
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

/// Measure of height over the fill rate of the current convex hull
let packingIndex(length:decimal<centimeters>, width:decimal<centimeters>, height:decimal<centimeters>, volumeOfToysPackedSoFarIncludingCurrentToy:decimal<centimeters^3>) =
  let heightSquared = height * height
  let numerator = length * width * heightSquared
  let index =
    if (volumeOfToysPackedSoFarIncludingCurrentToy = 0M<centimeters^3>) then
      numerator/(1M * 1M<centimeters^3>)
    else
      (numerator/volumeOfToysPackedSoFarIncludingCurrentToy)
  index

let maxPackingIndex x y = 
  let (a, b) = x
  let (c, d) = y
  if a < c then (c, d) else (a, b)

let rec calculateIndex (currentToy:Toy, extremePoints:ExtremePoints, volume:decimal<centimeters^3>, convexHull:ConvexHull) =
  let convexHullLeftBehindTopPoint = 
    calculateConvexHullWithCurrentToy(extremePoints.LeftBehindTopPoint, currentToy, convexHull)
  let lpj = convexHullLeftBehindTopPoint.Dimensions.Length
  let wpj = convexHullLeftBehindTopPoint.Dimensions.Width
  let hpj = convexHullLeftBehindTopPoint.Dimensions.Height
  let indexLeftBehindTopPoint = (packingIndex(lpj, wpj, hpj, volume), extremePoints.LeftBehindTopPoint)

  let convexHullLeftFrontBottomPoint = 
    calculateConvexHullWithCurrentToy(extremePoints.LeftFrontBottomPoint, currentToy, convexHull)
  let lpj = convexHullLeftFrontBottomPoint.Dimensions.Length
  let wpj = convexHullLeftFrontBottomPoint.Dimensions.Width
  let hpj = convexHullLeftFrontBottomPoint.Dimensions.Height
  let indexLeftFrontBottomPoint = (packingIndex(lpj, wpj, hpj, volume), extremePoints.LeftFrontBottomPoint)

  let convexHullRightBehindBottomPoint = 
    calculateConvexHullWithCurrentToy(extremePoints.RightBehindBottomPoint, currentToy, convexHull)
  let lpj = convexHullRightBehindBottomPoint.Dimensions.Length
  let wpj = convexHullRightBehindBottomPoint.Dimensions.Width
  let hpj = convexHullRightBehindBottomPoint.Dimensions.Height
  let indexRightBehindBottomPoint = (packingIndex(lpj, wpj, hpj, volume), extremePoints.RightBehindBottomPoint)

  let max1 = maxPackingIndex indexLeftBehindTopPoint indexLeftFrontBottomPoint
  let max2 = maxPackingIndex max1 indexRightBehindBottomPoint
  max2

let rec calculateIndexViaListOfExtremePointsRecursive (currentToy:Toy, extremePoints:ExtremePoints list, volume:decimal<centimeters^3>, maxIndexSoFar, convexHull:ConvexHull) =
  match extremePoints with
  | head :: tail ->
      let extremePointsIndex = calculateIndex(currentToy, head, volume, convexHull)
      let maxValue = max maxIndexSoFar extremePointsIndex     
      calculateIndexViaListOfExtremePointsRecursive(currentToy, tail, volume, maxValue, convexHull)
  | [] -> maxIndexSoFar

let  calculateIndexViaListOfExtremePoints (currentToy:Toy, setOfExtremePoints:ExtremePoints list, volume:decimal<centimeters^3>, maxIndexSoFar, convexHull:ConvexHull) =
  match setOfExtremePoints with
  | head :: tail ->
      let extremePointsIndex = calculateIndex(currentToy, head, volume, convexHull)
      let maxValue = max maxIndexSoFar extremePointsIndex 
      calculateIndexViaListOfExtremePointsRecursive(currentToy, tail, volume, maxValue, convexHull)
  | [] -> maxIndexSoFar

let calculateToyExtremPoints(currentToy: Toy) =
  let setOfExtremePoints = 
    [
      {
        LeftFrontBottomPoint =
          { 
            X = currentToy.Origin.X + currentToy.Dimensions.Length
            Y = currentToy.Origin.Y
            Z =  currentToy.Origin.Z
          }
        RightBehindBottomPoint =
          { 
            X = currentToy.Origin.X
            Y = currentToy.Origin.Y + currentToy.Dimensions.Width
            Z = currentToy.Origin.Z
          }
        LeftBehindTopPoint = 
          { 
            X = currentToy.Origin.X
            Y = currentToy.Origin.Y
            Z = currentToy.Origin.Z + currentToy.Dimensions.Height
          }
      }
    ]
  setOfExtremePoints

let rec fitnessEvaluation (toys:Toy list, box:Result<Box, string>, packing:Packing, convexHull:ConvexHull, maxIndexSoFar) =
  let accumulator = 0M<centimeters^3>
  match toys with
  | head :: tail ->
      let currentHeightOfConvexHull = getHeightOfConvexHullOfCurrentPacking(packing, convexHull.Dimensions.Height)
      let newConvexHull =
        {
            Dimensions = 
              {
                Length = 0M<centimeters>
                Width = 0M<centimeters>
                Height = currentHeightOfConvexHull
              }
        }
      let packedToyVolume = getToysVolume(packing.Order.Toys, accumulator)
      let currentToyVolume = volume head.Dimensions.Length head.Dimensions.Width head.Dimensions.Height
      let inclusiveToyVolume = packedToyVolume + currentToyVolume
      let index = calculateIndexViaListOfExtremePoints(head, packing.SetOfExtremePoints, inclusiveToyVolume, maxIndexSoFar, newConvexHull)
      let (_, indexPoint) = index
      let setOfExtremePointsFound = 
        [
          {
            LeftFrontBottomPoint = indexPoint
            RightBehindBottomPoint =
              { 
                X = 0M<centimeters>
                Y = 0M<centimeters>
                Z = 0M<centimeters>
              }
            LeftBehindTopPoint = 
              { 
                X = 0M<centimeters>
                Y = 0M<centimeters>
                Z = 0M<centimeters>
              }
          }
        ]
      let filteredExtremePoints = (Set packing.SetOfExtremePoints) - (Set setOfExtremePointsFound) |> Set.toList
      let newPackedToy =
        {
          Details = head.Details
          Dimensions = head.Dimensions
          Weight = head.Weight
          Origin = indexPoint
        }
      let newExtremePointsFromNewlyPackedToy = calculateToyExtremPoints(newPackedToy)
      let newExtremPoints = (Set filteredExtremePoints) + (Set newExtremePointsFromNewlyPackedToy) |> Set.toList
      let newOrder =
        {
          Details = packing.Order.Details
          OrderId = packing.Order.OrderId
          Toys = newPackedToy::packing.Order.Toys      
        }
      let newPacking =
        {       
          SetOfExtremePoints = newExtremPoints
          Order = newOrder
        }
      fitnessEvaluation(tail, box, newPacking, newConvexHull, maxIndexSoFar)
  | [] -> []

let getBox world =
  match Some(world.AvailableBoxes |> Seq.head) with 
  |  Some (KeyValue(k, v)) -> Success v
  |  None -> Failure "Box does not exist"  

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
  let box = getBox world
  let initialConvexHull =
    {
        Dimensions = 
          {
            Length = 0M<centimeters>
            Width = 0M<centimeters>
            Height = 0M<centimeters>
          }
    }
  let intitialPoint = 
   {
      X = 0M<centimeters>
      Y = 0M<centimeters>
      Z = 0M<centimeters>
   }
  let maxIndexSoFar = (0M<centimeters>, intitialPoint)
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