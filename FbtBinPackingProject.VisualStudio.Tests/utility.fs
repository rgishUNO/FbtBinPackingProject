module Utility

open System
open System.Collections.Generic
open System.Linq
open System.Security.Cryptography
open System.Security.Cryptography.X509Certificates 
open System.Threading
open System.Threading.Tasks 

type Failure(failedIn : bool) =
    let mutable failed = failedIn
    let mutable message = String.Empty
    let mutable innerException = String.Empty
    member this.Failed with get() = failed and set(v) = failed <- v
    member this.Message with get() = message and set(v) = message <- v
    member this.InnerException with get() = innerException and set(v) = innerException <- v
    new() = 
        Failure(false)