module Transactions

open Capstone3.Domain

let serialized (transaction: Transaction) =
    sprintf "%O***%s***%M***%b"
        transaction.Timestamp
        transaction.Operation
        transaction.Amount
        transaction.Accepted
