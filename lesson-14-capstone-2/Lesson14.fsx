//// Lesson 14 - Capstone 2

(*  14.1 Defining the problem
1 The application should allow a customer to deposit and withdraw from an
  account that the customer owns, and maintain a running total of the balance in
  the account.

2 If the customer tries to withdraw more money than is in the account, the transaction
  should be declined (the balance should stay as is).

3 The system should write out all transactions to a data store when they’re
  attempted. The data store should be pluggable (filesystem, console, and so forth).

4 The code shouldn’t be coupled to, for example, the filesystem or console input. It
  should be possible to access the code API directly without resorting to a console
  application.

5 Another developer will review your work, and that developer should be able to
  easily access all of the preceding components in isolation from one another.

6 The application should be an executable as a console application.

7 On startup, the system should ask for the customer’s name and opening balance.
  It then should create (in memory) an account for that customer with the specified
  balance.

8 The system should repeatedly ask whether the customer wants to deposit or
  withdraw money from the account.

9 The system should print out the updated balance to the user after every
  transaction.
*)

///14.3  Getting started
// Create project Lesson14

/// 14.4 Creating a domain

#load "Domain.fs"
open Capstone2.Domain

let customer = { Name = "Mary"}
let account = 
    { AccountID = "10001"
      Balance = 100M
      Owner = customer }


/// 14.5 Creating behaviors

#load "Operations.fs"
open Capstone2.Operations

let acct1 = withdraw 50M account
let amt1 = acct1.Balance
amt1 = 50M

let acct2 = deposit 100M acct1
let amt2 = acct2.Balance
amt2 = 150M

let acct3 = account |> deposit 50M |> withdraw 25M |> deposit 10M 
acct3.Balance = 135M

/// 14.6 Abstraction and reuse through higher-order functions

#load "Auditing.fs"
open Capstone2.Auditing

consoleAudit account "Testing console audit"
// fileSystemAudit account "Testing file audit"

/// 14.6.1 Adapting code with higher-order functions

/// Listing 14.6 Partially applying a curried function

let account2 = { AccountID = "10002"; Owner = { Name = "Fred"}; Balance = 100M }

account2
    |> deposit 100M
    |> withdraw 50M

let withdrawWithConsoleAudit = auditAs "withdraw" consoleAudit withdraw
let depositWithConsoleAudit = auditAs "deposit" consoleAudit deposit

account2
    |> depositWithConsoleAudit 100M
    |> withdrawWithConsoleAudit 50M
