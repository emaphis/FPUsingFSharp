//// Lesson 24, Capstone 4  - pg. 284

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
    
let processCommand1 account (command, amount) =
    printfn ""
    let account =
        match command with
        | Deposit -> deposit1 amount account
        | Withdraw -> withdraw1 amount account
        | Exit  -> account
    printfn "Current balance is £%M" account.Balance
    account

let getAmount1 command =
    Console.WriteLine()
    Console.Write "Enter Amount: "
    command, Console.ReadLine() |> Decimal.Parse

let openingAccount1 = 
    { AccountId = Guid.Empty; Owner = { Name = "Fred" }; Balance = 100M }

let commands = [ 'w'; 'd'; 'x'; ]

commands
|> Seq.choose isValidCommand1
|> Seq.takeWhile (not << isStopCommand1)
|> Seq.map getAmount1
|> Seq.fold processCommand1 openingAccount1


/// 24.2.3 Tightening the model further

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


