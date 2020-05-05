//// Lesson 24, Capstone 4  - pg. 284

module Capstone4.Program

open System
open Capstone4.Domain
open Capstone4.Operations

let withdrawWithAudit = auditAs "withdraw" Auditing.composedLogger withdraw
let depositWithAudit = auditAs "deposit" Auditing.composedLogger deposit
let tryLoadAccountFromDisk = FileRepository.tryFindTransactionsOnDisk >> Option.map Operations.loadAccount

[<AutoOpen>]
module CommandParsing =
    type BankOperation = Deposit | Withdraw
    type Command = AccountCmd of BankOperation | Exit

    let tryParseCommand command =
           match command with
           | 'w' -> Some(AccountCmd Withdraw)
           | 'd' -> Some(AccountCmd Deposit)
           | 'x' -> Some Exit
           | _ -> None

    let tryGetBankOperation cmd =
        match cmd with
        | Exit  -> None
        | AccountCmd cmd -> Some cmd


[<AutoOpen>]
module UserInput =
    let commands = seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
            Console.WriteLine() }
    
    let getAmount command =
        Console.WriteLine()
        Console.Write "Enter Amount: "
        command, Console.ReadLine() |> Decimal.Parse

[<EntryPoint>]
let main _ =
    let openingAccount =
        Console.Write "Please enter your name: "
        let owner = Console.ReadLine()

        match tryLoadAccountFromDisk owner with
        | Some account -> account
        | None -> { AccountId = Guid.NewGuid()
                    Balance = 0M
                    Owner = { Name = owner } }
    
    printfn "Current balance is £%M" openingAccount.Balance

    let processCommand account (command, amount) =
        printfn ""
        let account =
            match command with
            | Deposit -> depositWithAudit amount account
            | Withdraw -> withdrawWithAudit amount account
        printfn "Current balance is £%M" account.Balance
        account

    let closingAccount =
        commands
        |> Seq.choose tryParseCommand
        |> Seq.takeWhile ((<>) Exit)
        |> Seq.choose tryGetBankOperation
        |> Seq.map getAmount
        |> Seq.fold processCommand openingAccount
    
    printfn ""
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0
