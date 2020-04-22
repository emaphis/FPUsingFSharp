module Capstone2.Operations

open System
open Capstone2.Domain

/// Deposits an amount into an account
let deposit amount account =
    let newBalance = account.Balance + amount
    { account with Balance = newBalance }

/// Withdraws an amount of an account only if the balance hight enough
let withdraw amount (account : Account) =
    if amount > account.Balance then account
    else
        let newBalance = account.Balance - amount
        { account with Balance = newBalance }
