//// Lesson 19 - Capstone 3 - Testing File
///


#load "Domain.fs"
#load "Operations.fs"
#load "FileRepository.fs"
#load "Auditing.fs"

open Capstone3.Domain
open Capstone3.Operations
open System


/// Checks whether the command is one of (d)eposit, (w)ithdraw, ore(x)it.
let isValidCommand (command : char) =
    command = 'd' || command = 'w' || command = 'x'

/// Checks whether the command is the exit command.
let isStopCommand command = (command = 'x')

/// Takes in a command and converts it to a tuple of the command and
/// also an amount.
let getAmount command =
    let amount =
        if command = 'd' then 50M
        elif command = 'w' then 25M
        else 0M
    (command, amount)
;;
/// apply the appropriate action on the account and return the new account back out again.
/// Signature is compatible it fold.
let processCommand (account: Account) (command : (char * decimal)) =
     let comd, amount = command
     if comd = 'd' then deposit amount account
     elif comd = 'w' then withdraw amount account
     else account
;;


let withdrawWithAudit = auditAs "withdraw" Capstone3.Auditing.printTransaction withdraw
let depositWithAudit = auditAs "deposit" Capstone3.Auditing.printTransaction deposit

let openingAccount = { Owner = { Name = "Edward" }; Balance = 0M; AccountID = "1002" } 

let closingAccount =
    // Fill in the main loop here...
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]
    commands
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (not << isStopCommand)
    |> Seq.map getAmount
    |> Seq.fold processCommand openingAccount
//    openingAccount

Console.Clear()
printfn "Closing Balance:\r\n %A" closingAccount
Console.ReadKey() |> ignore
