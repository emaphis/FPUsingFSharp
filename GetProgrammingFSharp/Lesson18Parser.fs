module Lesson18Parser

///  Now you try - pg 217

open System

/// Parsing rule
type Rule = string -> bool * string


/// Rules

/// Text must be three words
let threeWords (text: string) =
    printfn "Running 3-word rule…"
    (text.Split ' ').Length = 3, "Must be three words"

/// Text must be less than or equal to 30 characters
let maxLength30 (text: string) =
    printfn "Running max 30-char rule…"
    text.Length <= 30, "Max length is 30 characters"

/// Text must be all uppercase
let allUppercase (text: string) =
    printfn "Running all-uppercase rule…"
    text
    |> Seq.filter Char.IsLetter
    |> Seq.forall Char.IsUpper, "All letters must be caps" 

/// Text must not have digits
let notAnyDigits (text: string) =
    printfn "Running not-any-digits rule..."
    text |> Seq.forall (fun char -> not (Char.IsDigit char)), "Must have no digits"



/// Create composite rule

/// Folding functions together
let buildValidator (rules : Rule list) =
    rules
    |> List.reduce(fun firstRule secondRule ->
        fun word ->                             // higher-order function
            let passed, error = firstRule word      // run first rule
            if passed then                      // passed, move to next rule
                let passed, error = secondRule word
                if passed then true, "" else false, error
            else false, error)          // failed return error
