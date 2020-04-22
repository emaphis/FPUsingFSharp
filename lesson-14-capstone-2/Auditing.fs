module Capstone2.Auditing

open Capstone2.Domain
open Capstone2.Operations
open System.IO

/// Log to a file
let fileSystemAudit account message =
    let path = sprintf @"c:\bin\%s-%s.txt" account.Owner.Name account.AccountID
    File.AppendAllText(path, message)
    
/// Log to the console
let consoleAudit account message =
    printfn "Account:  %O: %s" account.AccountID message

let auditAs operationName auditFun operation amount account = 
    auditFun account (sprintf "preforming transaction: %s for amount: %M" operationName amount)
    let newAcct = operation amount account
    let isBalanceChanged = (newAcct.Balance.Equals(account.Balance))
    if isBalanceChanged 
    then auditFun account "transaction failed - no update"
    else auditFun newAcct (sprintf "transaction: %s completed - new balance: %M" operationName newAcct.Balance)
    newAcct
