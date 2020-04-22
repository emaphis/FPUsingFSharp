//// Lesson 15 - Working with collection in F#

/// 15.1 F# collection basics

/// Listing 15.1 A sample dataset of football results

type FootballResult =
    { HomeTeam : string
      AwayTeam : string
      HomeGoals : int
      AwayGoals : int }

let create (ht, hg) (at, ag) =
    { HomeTeam = ht
      AwayTeam = at
      HomeGoals = hg
      AwayGoals = ag }

// F# List
let results =
    [ create ("Messiville", 1) ("Ronaldo City", 2)
      create ("Messiville", 1) ("Bale Town", 3)
      create ("Bale Town", 3) ("Ronaldo City", 1)
      create ("Bale Town", 2) ("Messiville", 1)
      create ("Ronaldo City", 4) ("Messiville", 2)
      create ("Ronaldo City", 1) ("Bale Town", 2) ]


/// 15.1.1 In-place collection modifications

/// Listing 15.2 An imperative solution to a calculation over data
open System.Collections.Generic

type TeamSummary = { Name : string; mutable AwayWins : int }
let summary = ResizeArray<TeamSummary>()

for result in results do
    if result.AwayGoals > result.HomeGoals then
        let mutable found = false
        for entry in summary do
            if entry.Name = result.AwayTeam then
                found <- true
                entry.AwayWins <- entry.AwayWins + 1
            if not found then
                summary.Add { Name = result.AwayTeam; AwayWins = 1 }

let comparer =    // Custom IComparer for sorting based on away wins
    { new IComparer<TeamSummary> with
        member this.Compare(x,y) =
            if x.AwayWins > y.AwayWins then -1
            elif x.AwayWins < y.AwayWins then 1
            else 0 }

summary.Sort(comparer)
summary.ToString()


/// 15.1.2 The collection modules
//  List Array Seq

// let newData = Collection.funcion predicate data


/// 15.1.3 Transformation pipelines

/// Listing 15.4 A declarative solution to a calculation over data

/// What us away win
let isAwayWin result = result.AwayGoals > result.HomeGoals

results
|> List.filter isAwayWin
|> List.countBy(fun result -> result.AwayTeam)
|> List.sortByDescending(fun (_, awayWins) -> awayWins)


/// 15.1.4 Debugging pipelines
// Execute each stage of the pipline 

/// 15.1.5 Compose, compose, compose

(* Quick check 15.1
1 What are the three main collection modules in F#?
A- List, Array, Seq.
2 Why is the input collection the last argument to collection functions?
A- So speciallized collection functions can be build with partial application.
 - Answer given - piping
3 What are some of the problems with processing collections imperatively?
A- Complex to reason ablut, 
*)


/// 15.2 Collection types in F#

/// 15.2.1 Working with sequences
/// Sequences are effectively an alias for the IEnumerable<T>

/// 15.2.2 Using .NET arrays

/// Listing 15.5 Working with .NET arrays in F#
let numbersArray = [| 1; 2; 3; 4; 6 |]  // standard BCL array
let firstNumber = numbersArray.[0]
let firstThreeNumbers = numbersArray.[0 .. 2]
numbersArray.[0] <- 99
numbersArray.[0] = 99


/// 15.2.3 Immutable lists

/// Listing 15.6 Working with F# lists

let numbers = [ 1; 2; 3; 4; 5; 6 ]
let numbersQuick = [ 1 .. 6 ]
let head::tail = numbers
let moreNumbers = 0 :: numbers
let evenMoreNumbers = moreNumbers @ [ 7 .. 9 ]

/// 15.2.4 Comparing and contrasting collections

(* Quick check 15.2
1 How does seq relate to IEnumerable<T>?
A- seq functions operate on IEnumerable<T>

2 How do higher-order functions relate to collection pipelines?
A- HO functions taking functions and date collections form the bases and
 - framework for pipelines

3 What are the main differences between an imperative and functional approach to working
with collections?
A- Imperative operations mutate data collections for efficiency.
 - Functional opperation produce new data collections for each step.
*)

/// Try this
// SELECT * FROM Customers
// WHERE Customer.ID > 10000
// ORDER BY Customer.LastName

// Customers
// |> List.Filter (ID -> Customer.ID > 1000)
// |> List.Sort (Customer.ID -> )
