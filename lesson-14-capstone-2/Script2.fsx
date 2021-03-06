﻿
/// 14.8 Referencing files from scripts

#load "Domain.fs"
#load "Operations.fs"
#load "Auditing.fs"

open Capstone2.Operations
open Capstone2.Domain
open Capstone2.Auditing
open System

let withdraw = withdraw |> auditAs "withdraw" consoleAudit
let deposit = deposit |> auditAs "deposit" consoleAudit

let customer = { Name = "Charley"}
let account = { AccountID = "10003"; Owner = customer; Balance = 90M }

account
|> withdraw 50M
|> deposit 50M
|> deposit 100M
|> withdraw 50M
|> withdraw 350M
