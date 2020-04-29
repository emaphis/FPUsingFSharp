//// Lesson 19 - Capstone 3 - Testing File
///


#load "Domain.fs"
#load "Operations.fs"

open Capstone3.Domain
open Capstone3.Operations
open System


let openingAccount =
   { Owner = {Name = "Charley"}; Balance = 0M; AccountID = "1001" }


let account =
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'z'; 'd' ] in
    commands
    |> List.filter isValidCommand
    |> List.takeWhile (not << isStopCommand)
    |> List.map getAmount
    |> List.fold processCommand openingAccount


Console.WriteLine(account)
