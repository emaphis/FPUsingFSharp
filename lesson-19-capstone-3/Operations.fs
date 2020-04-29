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



/// Functions for command-processing pipeline - pg 222,223

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

/// apply the appropriate action on the account and return the new account back out again.
/// Signature is compatible it fold.
let processCommand (account: Account) (command : (char * decimal)) =
     let comd, amount = command
     if comd = 'd' then deposit amount account
     elif comd = 'w' then withdraw amount account
     else account
