module FbtBinPackingProject

/// ------------ Units of Measure ---------------
[<Measure>] type inches

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
    Length: decimal<inches>
    Width: decimal<inches>
    Height: decimal<inches>
  }

type Toy =
  {
      Details: Details
      Dimensions: Dimensions
      Orientation: string
      Weight: decimal
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
      Toys = [
                {
                  Details = {
                              Name = "Toy#1Name";
                              Description = "Toy#1Description"
                            };
                  Dimensions = {
                                 Length = 12M<inches>;
                                 Width = 5M<inches>;
                                 Height = 7M<inches>
                               };
                  Orientation = "northwest";
                  Weight = 7M
                };
                {
                  Details = {
                              Name = "Toy#2Name";
                              Description = "Toy#2Description"
                            };                  
                  Dimensions = {
                                 Length = 24M<inches>;
                                 Width = 10M<inches>;
                                 Height = 14M<inches>
                               };
                  Orientation = "southhwest";
                  Weight = 14M
                }                
             ];
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
             Length = 12.125M<inches>;
             Width = 12M<inches>;
             Height = 12M<inches>
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
             Length = 13.5M<inches>;
             Width = 8.75M<inches>;
             Height = 12M<inches>
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

let getToy world =
  match Some(world.Order.Toys |> Seq.head) with 
  |  Some toy -> Success toy
  |  None -> Failure "Toy does not exist"

let getBox world =
  match Some(world.AvailableBoxes |> Seq.head) with 
  |  Some (KeyValue(k, v)) -> Success v
  |  None -> Failure "Box does not exist"  

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