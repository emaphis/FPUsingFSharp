namespace Capstone3.Domain

/// Represents a Customer who owns an Account
type Customer =
    { Name : string }

/// Represents a bank account
type Account =
    { AccountID : string
      Balance : decimal
      Owner : Customer }
