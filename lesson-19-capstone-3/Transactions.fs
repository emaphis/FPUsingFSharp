module Transactions
open System

open Capstone3.Domain

// Serialize a transaction
let serialized (transaction: Transaction) =
    sprintf "%O***%s***%M***%b"
        transaction.Timestamp
        transaction.Operation
        transaction.Amount
        transaction.Accepted

let deserialize (line : string) =
    let parts = line.Split([|"***"|], StringSplitOptions.None)
    { Timestamp = DateTime.Parse parts.[0]
      Operation = parts.[1]
      Amount = Decimal.Parse parts.[2]
      Accepted = Boolean.Parse parts.[3] }
