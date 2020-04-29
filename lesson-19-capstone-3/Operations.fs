module Capstone3.Operations

open System
open Capstone3.Domain


/// Withdraws an amount of an account only if the balance hight enough
let withdraw amount account =
    if amount > account.Balance then account
    else
        let newBalance = account.Balance - amount
        { account with Balance = newBalance }

/// Deposits an amount into an account
let deposit amount account =
    let newBalance = account.Balance + amount
    { account with Balance = newBalance }


let auditAs operationName audit operation amount account =
    audit account (sprintf "%O preforming a %s operation for $%M" DateTime.UtcNow operationName amount)
    let updatedAccount = operation amount account

    let accountIsChanged = (updatedAccount = account)

    if accountIsChanged
    then audit account (sprintf "%O: Transaction rejected!" DateTime.UtcNow)
    else audit account (sprintf "%O: Transaction accepted! Balance is noe $%M" DateTime.UtcNow updatedAccount.Balance)

    updatedAccount
