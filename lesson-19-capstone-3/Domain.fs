namespace Capstone3.Domain

open System;

/// Represents a Customer who owns an Account
type Customer =
    { Name : string }

/// Represents a bank account
type Account =
    { AccountID : Guid
      Owner : Customer
      Balance : decimal }

type Transaction =
    { Timestamp : DateTime
      Operation : string 
      Amount : decimal
      Accepted : bool }
