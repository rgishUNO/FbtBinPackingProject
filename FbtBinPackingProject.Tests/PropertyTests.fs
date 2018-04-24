module PropertyTests

open FsCheck.Xunit
open Swensen.Unquote
open FbtBinPackingProject

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
           Length = 12.125M<centimeters>;
           Width = 12M<centimeters>;
           Height = 12M<centimeters>
        };
  }

let box =
  match Some(boxData) with 
  |  Some boxData -> Success boxData.Id.BoxId
  |  None -> Failure "Box does not exist"

let resultFromBox box =
  match Some(box) with 
  |  Some box -> Success box.Id.BoxId
  |  None -> Failure "Box does not exist" 

[<Property>]
let ``Given a box when a small order``
  (randomBox: Box) =

  let actual : Result<string, string> = describeCurrentBox gameWorld

  let expected = resultFromBox randomBox
  expected =! actual