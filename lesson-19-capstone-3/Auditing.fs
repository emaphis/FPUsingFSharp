module Capstone3.Auditing

open Capstone3.Domain
open System.IO

/// Logs to the console
let printTransaction _ accountID trans =
    printf "Account: %O: %s of %M (approved: %b)" accountID trans.Operation trans.Amount trans.Accepted

// Logs to both console and file system
let composedLogger = 
    let loggers =
        [ FileRepository.writeTransaction
          printTransaction ]
    fun accountId owner message ->
        loggers
        |> List.iter(fun logger -> logger accountId owner message)
 