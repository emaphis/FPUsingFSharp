//// Lesson 19 - Capstone 3 -  pg 219

/// 19.2 Removing mutability

#load "Domain.fs"
#load "Operations.fs"

open Capstone3.Domain
open Capstone3.Operations
open System

/// Listing 19.1 - Creating a functional pipeline for commands - pg 221
/// Now try this pg 222 - implement command
/// Listing 19.2 - Sample functions for command-processing pipeline - pg 222,223

/// Checks whether the command is one of (d)eposit, (w)ithdraw, ore(x)it.
let isValidCommand1 (command : char) =
    command = 'd' || command = 'w' || command = 'x'

/// Checks whether the command is the exit command.
let isStopCommand1 command = (command = 'x')

/// Takes in a command and converts it to a tuple of the command and
/// also an amount.
let getAmount1 command =
    let amount =
        if command = 'd' then 50M
        elif command = 'w' then 25M
        else 0M
    (command, amount)

/// apply the appropriate action on the account and return the new account back out again.
/// Signature is compatible it fold.
let processCommand1 (account: Account) (command : (char * decimal)) =
     let comd, amount = command
     if comd = 'd' then deposit amount account
     elif comd = 'w' then withdraw amount account
     else account

 /////////////////////

let openingAccount =
   { Owner = {Name = "Charley"}; Balance = 0M; AccountID = Guid.Empty }


let account1 =
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'z'; 'd' ] in
    commands
    |> List.filter isValidCommand1
    |> List.takeWhile (not << isStopCommand1)
    |> List.map getAmount1
    |> List.fold processCommand1 openingAccount


account1

