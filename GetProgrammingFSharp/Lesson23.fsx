//// Lesson 23 - Business Rules as code - pg 270
///

/// 23.1 Specific types in F#

/// Listing 23.1 A sample F# record representing a sample customer - pg 271
// A customer can be contacted by email, phone, or post.

type Customer1 =
    { CustomerId : string
      Email : string
      Telephone : string
      Address : string }


/// 23.1.1 Mixing values of the same type
/// Listing 23.2 Creating a customer through a helper function - pg 272
// oh mai
let createCustomer1 customerId email telephone address =
    { CustomerId = telephone
      Email = customerId
      Telephone = address
      Address = email }

let customer1 =
    createCustomer1 "C-123" "nick1@myemail.com" "029-293-23" "1 The Street"


/// 23.1.2 Single-case discriminated unions
/// Listing 23.3 - Creating a wrapper type via a single-case discriminated union = pg 272
type Address1 = Address1 of string
let myAddress1 = Address1 "1 The Street"

// let isSame = (myAddress = "1 The Street")  // doesn't compile
let (Address1 addressData) = myAddress1


(* Now you try - pg 273
1 Start with an empty script, creating the Customer record and createCustomer function
  (with the incorrect assignments).
2 Create four single-case discriminated unions, one for each type of string you
  want to store (CustomerId, Email, Telephone, and Address).
3 Update the definition of the Customer type so that each field uses the correct wrapper
  type. Make sure you define the wrapper types before the Customer type!
4 Update the callsite to createCustomer so that you wrap each input value into the correct
  DU; you’ll need to surround each “wrapping” in parentheses (see figure 23.1).
  If you’ve done this correctly, you’ll notice that your code immediately stops
  working.
5 Fix the assignments in the createCustomer function and you’ll see that as if by magic
  all the errors disappear.
6 Replace the three single-case DUs with the new ContactDetails type.
7 Update the Customer type by replacing the three optional fields with a single field
  of type ContactDetails.
8 Update the createCustomer function. It now needs to take in only two arguments,
  the CustomerId and the ContactDetails.
9 Update the callsite as appropriate; for example:
10 Add a new field to your Customer that contains an optional ContactDetail, and
   rename your original ContactDetail field to PrimaryContactDetails
11 Update the createCustomer function and callsite as appropriate
*)

/// Listing 23.4 - Creating wrapper types for contact details - pg 274

type CustomerId = CustomerId of string
type Email = Email of string
type Telephone = Telephone of string
type Address = Address of string

type ContactDetails =
    | Address of string
    | Telephone of string
    | Email of string

/// Listing 23.5  pg 276,277
 type Customer =
    { CustomerId : CustomerId
      PrimaryContactDetails : ContactDetails
      SecondaryContactDetails : ContactDetails option }

let createCustomer customerId primaryContactDetails secondaryContactDetails =
    { CustomerId =  customerId
      PrimaryContactDetails = primaryContactDetails
      SecondaryContactDetails = secondaryContactDetails }

let customer =
    createCustomer
        (CustomerId "C-123")
        (Email "nick1@myemail.com")
        None


(* Quick check 23.1  - pg 277
1 What’s the benefit of single-case DUs over raw values?
A- Single-case DUs can be typechecked more accurately than just a simple value
2 When working with single-case DUs, when should you unwrap values?
A- Unwrap values when the need displayed, or accessed (calculated)
*)


/// 23.2 Encoding business rules with marker types
// use Options

/// Listing 23.6 - Creating custom types to represent business states - pg 278
type GenuineCustomer = GenuineCustomer of Customer

/// Listing 23.7 Creating a function to rate a customer - pg 270
let validCustomer customer =
    match customer.PrimaryContactDetails with
    | Email e when e.EndsWith "SuperCorp.com" -> Some(GenuineCustomer customer)
    | Address _ | Telephone _ -> Some(GenuineCustomer customer)
    | Email _ -> None

let sendwelcomeEmail (GenuineCustomer customer) =
    printf "Hello, %A, and welcome to our site!" customer.CustomerId

// sendwelcomeEmail customer4 // doesn't compile

// compiles
customer
|> validCustomer
|> Option.map sendwelcomeEmail

/// 23.2.1 When and when not to use marker

(*  Quick check 23.2  - pg 281
1 Why don’t you create wrapper types such as single-case DUs in C#?
A- Too much boilerplate
2 What benefit do you get from using single-case discriminated unions as marker types?
A- You get an extro lavel of typecheckin on special values
3 When should you wrap up raw values into single-case discriminated unions?
A- When tget represents specific types
*)

/// 23.3 Results vs. exceptions

/// Listing 23.8 - Creating a result type to encode success or failure - pg 281

let insertContactUnsafe contactDetails =
    if contactDetails = (Email "nicki@myemail.com") then
        { CustomerId = CustomerId "ABC"
          PrimaryContactDetails = contactDetails
          SecondaryContactDetails = None }
    else
        failwith "Unable to Insert - customer already exists."

type Result<'a> =
| Success of 'a
| Failure of string

let insertContact contactDetails =
    if contactDetails = (Email "nicki@myemail.com") then
        Success { CustomerId = CustomerId "ABC"
                  PrimaryContactDetails = contactDetails
                  SecondaryContactDetails = None }
    else failwith "Unable to Insert - customer already exists."

match insertContact (Email "nicki@myemail.com") with
| Success customerId -> printf "Saved with %A" customerId
| Failure error -> printfn "Unable to save: %s" error


(* Try this  - pg 283
Look at an existing domain you’ve written in C#, and try to see where you might benefit
from using options and single-case DUs in your model. Try to port the domain over to
F# and see its impact!
TODO:
*)
