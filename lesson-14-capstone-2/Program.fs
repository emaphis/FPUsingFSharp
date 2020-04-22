// Learn more about F# at http://fsharp.org

open System
open Capstone2.Domain
open Capstone2.Operations
open Capstone2.Auditing

let withdrawWithAudit = withdraw |> auditAs "withdraw" consoleAudit
let depositWithAudit = deposit |> auditAs "deposit" consoleAudit

[<EntryPoint>]
let main argv =
    printfn "Hello World from Capstone 2!"
    
    Console.Write "Enter customer name: "
    let name = Console.ReadLine()
    let customer = { Name = name }

    Console.Write "Enter account ID: "
    let accountID = Console.ReadLine()
    Console.Write "Enter account balance"
    let balance = Decimal.Parse(Console.ReadLine())
    let mutable account = 
        { AccountID = accountID
          Owner = customer
          Balance = balance }

    while true do
        printfn "\nCurrent balance is %M" account.Balance
        Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
        let action = Console.ReadLine();

        if action = "x" then Environment.Exit 0

        Console.Write "Amount: "
        let amount = Decimal.Parse(Console.ReadLine())

        account <-
            if   action = "d" then depositWithAudit  amount account
            elif action = "w" then withdrawWithAudit amount account
            else account

    0 // return an integer exit code
