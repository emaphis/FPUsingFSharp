//// Lesson 14 - Capstone 2 -  pg 160
module Capstone2.Program

open System
open Capstone2.Domain
open Capstone2.Operations


[<EntryPoint>]
let main argv =
    let mutable account =
           let customer =
               Console.Write "Enter customer name: "
               let name = Console.ReadLine()
               { Name = name }

           Console.Write "Please enter account id: "
           let accountID = Console.ReadLine()

           Console.Write "Enter opening balance: "
           let balance = Decimal.Parse (Console.ReadLine())
           { AccountID = accountID
             Owner = customer
             Balance = balance }

    let withdrawAudit = withdraw |> auditAs "withdraw"  Auditing.filesystemLog
    let depositwithAudit = deposit |> auditAs "deposit" Auditing.filesystemLog

    while true do
        let action =
            Console.WriteLine()
            printfn "Current balance is $%M" account.Balance
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            Console.ReadLine();

        if action = "x" then Environment.Exit 0

        let amount =
            Console.Write "Amount: "
            Decimal.Parse(Console.ReadLine())

        // Mutate account via an expresson
        account <-
            if   action = "d" then depositwithAudit amount account
            elif action = "w" then withdrawAudit amount account
            else account

    0 // return an integer exit code
