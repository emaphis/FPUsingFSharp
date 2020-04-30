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

/// 19.4 Rehydrating an account from disk
/// Now you try - page 226


let createTransaction date operation amount accepted =
    { Timestamp = DateTime.Parse(date)
      Operation = operation
      Amount = amount
      Accepted = accepted }

let testtransactions =
    [ createTransaction "4/30/2020 2:59:15 PM" "deposit" 50M true
      createTransaction "4/30/2020 2:59:51 PM" "withdraw" 20M false
      createTransaction "4/30/2020 2:59:25 PM" "deposit" 100M true
      createTransaction "4/30/2020 2:59:46 PM" "withdraw" 120M true
      createTransaction "4/30/2020 2:59:31 PM" "withdraw" 20M true ]

;;

//  fold example
let addInts acc num = acc + num

let accumInts lst =
    lst |> List.fold (fun acc num -> addInts acc num) 0

[1; 2; 3; 4] |> accumInts  = 10
List.fold (fun acc num -> acc + num) 0 [1; 2; 3; 4] = 10


let accumulateTransaction account transaction =
    if transaction.Operation = "deposit"
        then deposit transaction.Amount account
    elif transaction.Operation = "withdraw"
        then withdraw transaction.Amount account
    else account

;;
let loadAccount2 (owner, accountID, transactions) =
    let openingAccount = { AccountID = accountID; Owner = { Name = owner };  Balance = 0M }
    transactions
    |> List.sortBy (fun trans -> trans.Timestamp)
    |> List.fold (fun acct trans -> accumulateTransaction acct trans) openingAccount

let acct2 = loadAccount2 ("Romney1", Guid.Empty, testtransactions)
;;