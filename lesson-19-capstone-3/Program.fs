//// Lesson 19 - Capstone 3 -  pg 219
module Capstone3.Program

open System
open Capstone3.Domain
open Capstone3.Operations


let withdrawWithAudit = auditAs "withdraw" Auditing.composedLogger withdraw
let depositWithAudit = auditAs "deposit"   Auditing.composedLogger deposit

/// Checks whether the command is one of (d)eposit, (w)ithdraw, ore(x)it.
let isValidCommand (command : char) =
    command = 'd' || command = 'w' || command = 'x'

/// Checks whether the command is the exit command.
let isStopCommand command = (command = 'x')

let consoleCommands = seq {
    while true do
        Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
        yield Console.ReadKey().KeyChar
        Console.WriteLine() }

/// Takes in a command and converts it to a tuple of the command and
/// also an amount.
let getAmountConsole command =
    let amount : decimal =
        Console.Write "\nPlease enter amount:"
        let line = Console.ReadLine()
        Decimal.Parse(line)
    Console.WriteLine()
    (command, amount)

/// apply the appropriate action on the account and return the new account back out again.
/// Signature is compatible it fold.
let processCommand (account: Account) (command: char, amount: decimal) =
    printfn ""
    let account =
        if command = 'd' then depositWithAudit amount account
        elif command = 'w' then withdrawWithAudit amount account
        else account
    printfn "Current balance is $%M" account.Balance
    account


[<EntryPoint>]
let main _ =
    let name =
        Console.Write "Please enter your name: "
        Console.ReadLine()
    
    let openingAccount = { Owner = { Name = name }; Balance = 0M; AccountID = Guid.Empty } 
    
    printfn "Current balance is £%M" openingAccount.Balance

    let closingAccount =
        // Fill in the main loop here...    
        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmountConsole
        |> Seq.fold processCommand openingAccount
 
    //Console.Clear()
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0

