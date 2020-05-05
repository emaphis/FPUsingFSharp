﻿//// Lesson 24, Capstone 4  - pg. 284

//#load "Domain.fs"
//#load "Operations.fs"

//open Capstone4.Operations
//open Capstone4.Domain

open System

/// 24.2.1 Reviewing the existing command handler

/// Listing 24.1 The existing command execution pipeline - pg 285

/// 24.2.2 Adding a command handler with discriminated unions

type Command1 =
| Withdraw
| Deposit
| Exit

let tryParseCommand1 command =
    match command with
    | 'w' -> Some Withdraw
    | 'd' -> Some Deposit
    | 'x' -> Some Exit
    | _ -> None


type Customer1 = { Name : string }
type Account1 = { AccountId : Guid; Owner : Customer1; Balance : decimal }

let isValidCommand1 (cmd : char) = tryParseCommand1 cmd
    
let isStopCommand1 = (=) Exit
    
let withdraw1 amount account =
    if amount > account.Balance then account
    else { account with Balance = account.Balance - amount }
    
let deposit1 amount account =
    { account with Balance = account.Balance + amount }

let getAmount1 command =
    Console.WriteLine()
    Console.Write "Enter Amount: "
    command, Console.ReadLine() |> Decimal.Parse

    
let processCommand1 account (command, amount) =
    printfn ""
    let account =
        match command with
        | Deposit -> deposit1 amount account
        | Withdraw -> withdraw1 amount account
        | Exit  -> account
    printfn "Current balance is £%M" account.Balance
    account

let openingAccount1 = 
    { AccountId = Guid.Empty; Owner = { Name = "Fred" }; Balance = 100M }

let commands = [ 'w'; 'd'; 'x'; ]

commands
|> Seq.choose isValidCommand1
|> Seq.takeWhile (not << isStopCommand1)
|> Seq.map getAmount1
|> Seq.fold processCommand1 openingAccount1


/// 24.2.3 Tightening the model further
/// Listing 24.3 Creating a two-level discriminated union pg 288
type BankOperation2 = Deposit | Withdraw
type Command2 = AccountCmd of BankOperation2 | Exit

let tryParseCommand2 command =
    match command with
    | 'w' -> Some(AccountCmd Withdraw)
    | 'd' -> Some(AccountCmd Deposit)
    | 'x' -> Some Exit
    | _ -> None

let tryGetBankOperation cmd =
    match cmd with
    | Exit  -> None
    | AccountCmd cmd -> Some cmd

let isStopCommand2 = (=) Exit
    
let processCommand2 account command =
    printfn ""
    let account =
        match command with
        | Deposit -> printfn "Deposit"
        | Withdraw -> printfn "Withdraw"
 //   printfn "Current balance is £%M" account.Balance
    account

let commands2 = ['d'; 'a'; 'b'; 'k'; 'w'; 'd'; 'x'; 'w' ]

commands2
    |> List.choose tryParseCommand2
    |> List.takeWhile (not << isStopCommand2)
    |> List.choose tryGetBankOperation
//    |> Seq.map getAmount2
    |> List.fold processCommand2 () //openingAccount1


/// 24.3 Applying Option types with the outside world
/// 24.3.1 Parsing user input

let getAmount3 command =
    Console.WriteLine()
    Console.Write "Enter Amount: "
    command, Console.ReadLine() |> Decimal.Parse

let tryGetAmount3 (command: BankOperation2) =
  //  Console.WriteLine()
  //  Console.Write "Enter Amount: xxx:"
  //  let amount =  Console.ReadLine() |> Decimal.TryParse
    let amount =  true, 100M
    match amount with
    | true, amount -> Some(command, amount)
    | false, _ -> None


let processCommand3 account (command, amount) =
   // printfn ""
    let account =
        match command with
        | Deposit -> printfn "Deposit %M" amount
        | Withdraw -> printfn "Withdraw %M" amount
     //   printfn "Current balance is £%M" account.Balance
    account

let commands3 = ['d'; 'a'; 'b'; 'k'; 'w'; 'd'; 'x'; 'w' ]

commands3
    |> List.choose tryParseCommand2
    |> List.takeWhile (not << isStopCommand2)
    |> List.choose tryGetBankOperation
    |> List.map tryGetAmount3
    |> List.choose (fun x -> x)
    |> List.fold processCommand3 () //openingAccount1



/// Listing 24.5 Unintentionally hiding optionality with a default value pg 290

open System.IO

let accountsPath2 =
    let path = @"accounts"
    Directory.CreateDirectory path |> ignore
    path

let findAccountFolder2 owner =  
    let folders = Directory.EnumerateDirectories(accountsPath2, sprintf "%s_*" owner)
    if Seq.isEmpty folders then printfn "got here iiiii"; ""
    else
        let folder = Seq.head folders
        DirectoryInfo(folder).Name

findAccountFolder2 "Fred1"


///