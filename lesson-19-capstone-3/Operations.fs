module Capstone3.Operations

open System
open Capstone3.Domain


/// Withdraws an amount of an account (if there are sufficient funds)
let withdraw amount account =
    if amount > account.Balance then account
    else { account with Balance = account.Balance - amount }

/// Deposits an amount into an account
let deposit amount account =
    { account with Balance = account.Balance + amount }

/// Runs some account operation such as withdraw or deposit with auditing.
let auditAs operationName audit operation amount account =
    let updatedAccount = operation amount account
    
    let accountIsUnchanged = (updatedAccount = account)

    let transaction =
        let transaction = { Operation = operationName; Amount = amount; Timestamp = DateTime.UtcNow; Accepted = true }
        if accountIsUnchanged then { transaction with Accepted = false }
        else transaction

    audit account.AccountID account.Owner.Name transaction
    updatedAccount  


let accumulateTransaction account transaction =
    if transaction.Operation = "deposit"
        then deposit transaction.Amount account
    elif transaction.Operation = "withdraw"
        then withdraw transaction.Amount account
    else account

/// Recreates an account from a list transactions
let loadAccount2 (owner, accountID, transactions) =
    let openingAccount = { AccountID = accountID; Owner = { Name = owner };  Balance = 0M }
    transactions
    |> List.sortBy (fun trans -> trans.Timestamp)
    |> List.fold (fun acct trans -> accumulateTransaction acct trans) openingAccount
