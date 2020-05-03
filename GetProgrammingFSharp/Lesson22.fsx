//// Lesson 22 - Fixing the billion-dollar mistace 257
/// The Option type

/// 22.1 Working with missing values

/// 22.1.2 Nullable types in .NET

(* Quick check 22.1 - pg 277
1 Why can’t C# prevent obvious null references?
A- null is a legal value for a reference.
2 How does the nullable type improve matters when working with data that might be missing?
A-
*)

/// 22.2 Improving matters with the F# type system

/// 22.2.1 Mandatory data in F#
/// Listing 22.4 - Trying to set an F# type value to null - pg 278

type Whatever = { What : string }
type Whoever = { Who : Whatever }

//let myWhatever = { Who = null }
// error FS0043: The type 'Whatever' does not have 'null' as a proper value

/// 22.2.2 The option type
/// Listing 22.5 - Sample code to calculate a premium - pg 263

let number : int = 10
let maybeANumber : int option = Some 10

let calculateAnnualPremiumUsd score =
    match score with
    | Some 0  -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None ->
        printf "No score supplied! Using temporary premium"
        300

calculateAnnualPremiumUsd (Some 250)
calculateAnnualPremiumUsd None


(*  Now you try  - pg 2564
Let’s see how to model the dataset from listing 22.1:
1 Create a record type to match the structure of the customer.
2 For the optional field’s type, use an option (either int option or
  Option<int>).
3 Create a list that contains both customers, using [ a; b ] syntax.
4 Change the function in listing 22.5 to take in a full customer object
  and match the
SafetyScore field on it.
*)

// 1
type Customer =
    { Name : string
      SafetyScore : int option   // 2
      YearPassed :  int }

let drivers =
    [ { Name = "Fred Smith"; SafetyScore = Some 550; YearPassed = 1980 }
      { Name = "Jane Dunn"; SafetyScore = None;  YearPassed = 1980 } ]


let calculateAnnualPremiumUsd2 customer =
    let score = customer.SafetyScore
    match score with
    | Some 0  -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None ->
        printf "No score supplied! Using temporary premium"
        300

drivers |> List.map calculateAnnualPremiumUsd2


(* Quick check 22.2 - pg 264
1 Can you get null reference exceptions in F#?
A- Yes.  Dereferencing C# objects
2 How should you safely dereference a value that’s wrapped in an option?
A- Pattern matching,  using .Value is dangerous
*)


/// 22.3 Using the Option module

/// 22.3.1 Mapping
/// Option.map - mapping:('T -> 'U) -> option:'T option -> 'U option

/// Listing 22.6 - Matching and mapping  - pg 265


let describe score =
    if score > 200 then "Safe" else "High rist"

let description customer =
    match customer.SafetyScore with
    | Some score -> Some(describe score)
    | None -> None

let description2 customer =
    customer.SafetyScore
    |> Option.map(fun score -> describe score)

description drivers.[0]
description drivers.[1]
description2 drivers.[0]
description2 drivers.[1]


/// 22.3.2 Binding
/// Option.binder - binder:('T -> 'U option) -> option:'T option -> 'U option

/// Listing 22.7 - Chaining functions that return an option with Option.bind - pg 266

let tryFindCustomer cId = if cId = 10 then Some drivers.[0] else None
let getSafetyScore customer = customer.SafetyScore
let score = tryFindCustomer 10 |> Option.bind getSafetyScore


/// 22.3.3 Filtering
/// Option.filter - predicate:('T -> bool) -> option:'T option -> 'T option

/// Listing 22.8 - Filtering on options  - pg 267
let test1 = Some 5 |> Option.filter (fun x -> x > 5)
let test2 = Some 5 |> Option.filter (fun x -> x = 5)


/// 22.3.4 Other Option functions
/// count, exists

(* Quick check 22.3  - pg 267
1 When should you use Option.map rather than an explicit pattern match?
A- If the None case returns None, then you can use Option.map
2 What’s the difference between Option.map and bind?
A-  Option.map takes a function that doesn't return an Option
*)

/// 22.4 Collections and options
// Option.toList
// List.choose - chooser:('T -> 'U option) -> list:'T list -> 'U list

(* Now you try - pg 268
1 Create a function tryLoadCustomer that takes in a customer ID. If the ID is between 2
  and 7, return an optional string "Customer <id>" (for example, "Customer 4"). Otherwise,
  return None.
2 Create a list of customer IDs from 0 to 10.
3 Pipe those customer IDs through List.choose, using the tryLoadCustomer as the
  higher-order function.
4 Observe that you have a new list of strings, but only for existing customers.
*)

// 1
let tryLoadCustomer customerID =
    match customerID with
    | customerID when customerID >= 2 && customerID <= 7 ->
        Some(sprintf "Customer %i" customerID)
    | _ -> None

// 2
let listCustomerID = [ 0 .. 10 ]

// 3, 4
let newList = listCustomerID |> List.choose tryLoadCustomer

(* Quick check 22.4   - pg 269
Why are collection try functions safer to use than LINQ’s orDefault methods?
A- The LINQ orFunctions return null on failiure.
*)

(* Try this  - pg 269
Write an application that displays information on a file on the local hard disk. If the file
isn’t found, return None. Have the caller code handle both scenarios and print an appropriate
response to the console. Or, update the rules engine code from previous lessons
so that instead of returning a blank string for the error message when a rule passes, it
returns None. You’ll have to also update the failure case to a Some error message!
 *)
//////////////////////////////////////////
/// File System

open System
open System.IO

let path1 = @"C:\Testing\subdir3\test08.bat"
let path2 = @"C:\Testing\subdir3\test08.txt"

let getFileInfo path =
    match IO.File.Exists(path) with
    | true ->  Some(FileInfo(path))
    | _ -> None

let bbb = getFileInfo(path1)

let nameFile path =
    match getFileInfo path with
    | Some inf -> sprintf "File: %s" inf.Name
    | None  -> "File doesn't exist"

let name1 = nameFile path1
let name2 = nameFile path2


////////////////////////////////////////////////////
/// Rules Engine  - from Lesson22 - pg 272

open System

// Option represents an error condition
type Rule = String -> string option

/// Rules

/// Text must be three words
let threeWords(text: string)  =
    match (text.Split ' ').Length = 3 with
    | true -> None
    | _    -> Some("Must be three words")

/// Text must be less than or equal to 30 characters
let maxLength30 (text: string) =
    match text.Length <= 30 with
    | true -> None
    | _    -> Some("Max length is 30 characters")

/// Text must be all uppercase
let allUppercase (text: string) =
    let ret = text |> Seq.filter Char.IsLetter
                   |> Seq.forall Char.IsUpper
    match ret with
    | true -> None
    | _    -> Some("All letters must be caps" )

/// Text must not have digits
let notAnyDigits (text: string) =
    let ret = text |> Seq.forall (fun char -> not (Char.IsDigit char))
    match ret with
    | true -> None
    | _    -> Some("Must have no digits")


/// Create composite rule
let buildValidator (rules : Rule list) =
    rules
    |> List.reduce(fun firstRule secondRule ->
        fun word ->          // higher-order function
            match firstRule word with
                | None ->
                    match secondRule word with
                    | None -> None
                    | Some(str) -> Some(str)
                | Some(str) -> Some(str))

let rules : Rule list =
    [ threeWords; maxLength30; allUppercase; notAnyDigits ]

let validate rules text =
    match (buildValidator rules) text with
    | None      -> printfn "Text passed"
    | Some(str) -> printfn "Text failed: %s" str

/// examples

// should pass
validate rules "HELLO FROM F#"

// fail capital letters
validate rules "HELLO FrOM F#"

// fail - max length
validate rules "HELLOFROMF# ITSA FUNCTIONALLANGUAGE"

// fail - three words
validate rules "ONE TWO THREE FOR"

// fail - digts
validate rules "HELLO FROM F8"
