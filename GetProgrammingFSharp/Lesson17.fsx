//// Lesson 17 - Maps, dictionary, sets

/// 17.1 Dictionaries

/// 17.1.1 Mutable dictionaries in F#

/// Listing 17.1 - Standard dictionary functionality in F#
open System.Collections.Generic

let inventory = Dictionary<string, float>()
inventory.Add("Apples", 0.33)
inventory.Add("Oranges", 0.23)
inventory.Add("Bananas", 0.45)

inventory.Remove "Oranges"
let bananas = inventory.["Bananas"]
let oranges = inventory.["Oranges"]  // exception


/// Listing 17.2 - Generic type inference with Dictionary
let inventory2 = Dictionary<_,_>()
inventory2.Add("Apples", 0.33)

let inventory3 = Dictionary()
inventory3.Add("Apples", 0.33)


/// 17.1.2 Immutable dictionaries
// Immutable C# dictinary

/// Listing 17.3 Creating an immutable IDictionary
let inventory4 : IDictionary<string, float> =
    ["Apples", 0.33; "Oranges", 0.23; "Bananas", 0.45 ]
    |> dict  // create the dictionary

inventory4.Count

let bananas1 = inventory4.["Bananas"]

// immutable - not supported
inventory4.Add("Pinaapples", 0.85)
inventory4.Remove("Baanas")


(* Quick check 17.1
1 What sort of situations would you use a dictionary for?
A- When look up needs to be fast, and item order isn't relevant.

2 How does F# syntax simplify creating dictionaries?
A- Using the dict funtion, you can create type parameters for dictionary creation.

3 Why might you use the dict function in F#?
A- for immuatable dictionaries - final truth
*)


/// 17.2 The F# Map
// immutable key value lookup

/// Listing 17.4 - Using the F# Map lookup
let inventory5 =
    [ "Apples", 0.33; "Oranges", 0.23; "Bannana", 0.45 ]
    |> Map.ofList
let apples = inventory5.["Apples"]
let pineapples = inventory5.["Pineapples"] // keyNotFoundException

let newInventory5 =
    inventory5
    |> Map.add "Pineapples" 0.87
    |> Map.remove "Apples"


/// 17.2.1 Useful Map functions
// List, Array, Seq - map filter iter partition

/// Listing 17.5 - Using the F# Map module functions
let cheapFruit, expensiveFruit =
    inventory5
    |> Map.partition(fun fuit cost -> cost < 0.3)

// page 201














