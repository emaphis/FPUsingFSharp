//// Lesson 17 - Maps, dictionary, sets  - pg 197

/// 17.1 Dictionaries

/// 17.1.1 Mutable dictionaries in F#

/// Listing 17.1 - Standard dictionary functionality in F#  - pg 198
open System.Collections.Generic

let inventory = Dictionary<string, float>()
inventory.Add("Apples", 0.33)
inventory.Add("Oranges", 0.23)
inventory.Add("Bananas", 0.45)

inventory.Remove "Oranges"

let bananas = inventory.["Bananas"]
let oranges = inventory.["Oranges"]  // exception


/// Listing 17.2 - Generic type inference with Dictionary  - pg 198
let inventory2 = Dictionary<_,_>()  // explicit placeholder for generic type arguments
inventory2.Add("Apples", 0.33)

let inventory3 = Dictionary()    // omitting generic type arguments
inventory3.Add("Apples", 0.33)


/// 17.1.2 Immutable dictionaries
// Immutable C# dictinary

/// Listing 17.3 Creating an immutable IDictionary  - pg 199
let inventory4 : IDictionary<string, float> =
    ["Apples", 0.33; "Oranges", 0.23; "Bananas", 0.45 ]
    |> dict  // create the dictionary

inventory4.Count

let bananas1 = inventory4.["Bananas"]

// immutable - not supported
inventory4.Add("Pinaapples", 0.85)
inventory4.Remove("Baanas")


(* Quick check 17.1  - pg 199
1 What sort of situations would you use a dictionary for?
A- When look up needs to be fast, and item order isn't relevant.

2 How does F# syntax simplify creating dictionaries?
A- Using the dict funtion, you can create type parameters for dictionary creation.

3 Why might you use the dict function in F#?
A- for immuatable dictionaries - final truth
*)


/// 17.2 The F# Map
// immutable key value lookup

/// Listing 17.4 - Using the F# Map lookup  - pg 200
let inventory5 =
    [ "Apples", 0.33; "Oranges", 0.23; "Bannana", 0.45 ]
    |> Map.ofList  // list to map

let apples = inventory5.["Apples"]
let pineapples = inventory5.["Pineapples"] // keyNotFoundException

// return new maps
let newInventory5 =
    inventory5
    |> Map.add "Pineapples" 0.87
    |> Map.remove "Apples"


/// 17.2.1 Useful Map functions
// List, Array, Seq - map, filter, iter, partition

/// Listing 17.5 - Using the F# Map module functions  - pg 201
let cheapFruit, expensiveFruit =
    inventory5
    |> Map.partition(fun fuit cost -> cost < 0.3)


(* Now you try - page 201
Now you’re going to create a lookup for all the root folders on your hard disk and the
times that they were created:
1 Open a blank script.
2 Get a list of all directories within the C:\ drive on your computer (you can use
  System.IO.Directory.EnumerateDirectories). The result will be a sequence of strings
3 Convert each string into a full DirectoryInfo object. Use Seq.map to perform the
  conversion.
4 Convert each DirectoryInfo into a tuple of the Name of the folder and its Creation-
  TimeUtc, again using Seq.map.
5 Convert the sequence into a Map of Map.ofSeq.
6 Convert the values of the Map into their age in days by using Map.map. You can subtract
  the creation time from the current time to achieve this.
*)

open System
open System.IO

let rootDirs =
    let now = DateTime.UtcNow;
    Directory.EnumerateDirectories(@"c:\")                   // 2.
    |> Seq.map (fun path -> DirectoryInfo path)              // 3.
    |> Seq.map (fun dir -> (dir.Name, dir.CreationTimeUtc))  // 4.
    |> Map.ofSeq                                             // 5.
    |> Map.map (fun key time -> (now - time).Days)           // 6.

for dir in rootDirs do
    Console.WriteLine(dir)


(* Quick check 17.2  - pg 202
1 What’s the main difference between Dictionary and Map?
A- Map is immutable, Dictionary is mutable

2 When should you use Dictionary over Map?
A- Dictionary has better performance, has better interoperability 
*)


/// 17.3 Sets

/// Listing 17.6 - Creating a set from a sequence  - pg 203
let myBasket = [ "Apples"; "Apples"; "Apples"; "Bananas"; "Pineapples"  ]
let fruitsILike = myBasket |> Set.ofList

/// Listing 17.7 - Comparing List- and Set-based operations  - pg 203
let yourBasket = [ "Kiwi"; "Bananas"; "Grapes" ]
let allFruitsList = (myBasket @ yourBasket) |> List.distinct

let fruitsYouLike = yourBasket |> Set.ofList
let allFruits = fruitsILike + fruitsYouLike


/// Listing 17.8 - Sample Set-based operations - pg 204

// gets fruits in A that are not in B
let fruitsJustForMe = allFruits - fruitsYouLike

// Gets fruits that exist in both A and B
let fruitsWeCanShare = fruitsILike |> Set.intersect fruitsYouLike

// Are all fruits in A also in B
let doILikeAllYOurFruis = fruitsILike |> Set.isSubset fruitsYouLike


(* Quick check 17.3   - pg 204
What function might you use to simulate simple set-style behavior in a list?
A- Distinct , DistinctBy
*)

(* Try this - pg 205
// TODO: See Lesson 16 pg 205
Continuing from the previous lesson, create a lookup for all files within a folder so that
you can find the details of any file that has been read. Experiment with sets by identifying
file types in folders. What file types are shared between two arbitrary folders?
*)
