//// Lesson 15 - Working with collection in F#  - pg 173

/// 15.1 F# collection basics

/// Listing 15.1 A sample dataset of football results - pg 174
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

//results.Length


/// Now you try - pg 175
// TODO:


/// 15.1.1 In-place collection modifications

/// Listing 15.2 An imperative solution to a calculation over data - pg 175

/// Evaluate generic datatype 'summary' with the code that uses it so
/// the compiler can calculate it's datattype.....
open System.Collections.Generic

type TeamSummary = { Name : string; mutable AwayWins : int }
let summary = ResizeArray()

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

for teamSummary in summary do
    System.Console.WriteLine(teamSummary)


/// 15.1.2 The collection modules
//  List Array Seq

// let newData = Collection.funcion predicate data


/// 15.1.3 Transformation pipelines

/// Listing 15.4 A declarative solution to a calculation over data - pg 178

/// What us away win
let isAwayWin result = result.AwayGoals > result.HomeGoals

results
|> List.filter isAwayWin
|> List.countBy(fun result -> result.AwayTeam)
|> List.sortByDescending(fun (_, awayWins) -> awayWins)


/// 15.1.4 Debugging pipelines
// Execute each stage of the pipline 

(* Now you try - pg 180
Let's see how to work through the pipeline that you’ve just created, as per figure 15.4:
1 To make life easier, before executing each of the next steps, clear the FSI output
  by right-clicking over FSI and choosing Clear All (not Reset!)
2 In the REPL, with the code from listing 15.4 at the ready, execute the first line of
  the pipeline (results) by using Alt-Enter. You’ll see all six results sent to FSI.
3 Repeat the process, but this time highlight two lines so that you execute both
  results and filter.
4 Do the same again to include countBy.
5 As you execute each subset of the pipeline, building up to the end, compare the
  results with that of figure 15.2
*)


/// 15.1.5 Compose, compose, compose

(* Quick check 15.1  - pg 181
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

//  IEnumberable<T> |> Seq.toList

/// 15.2.2 Using .NET arrays

/// Listing 15.5 - Working with .NET arrays in F#  - pg 182
let numbersArray = [| 1; 2; 3; 4; 6 |]  // standard BCL array
let firstNumber = numbersArray.[0]
let firstThreeNumbers = numbersArray.[0 .. 2]
numbersArray.[0] <- 99
numbersArray.[0] = 99


/// 15.2.3 Immutable lists

/// Listing 15.6 - Working with F# lists - pg 183
let numbers = [ 1; 2; 3; 4; 5; 6 ]  // int list
let numbersQuick = [ 1 .. 6 ]
let head::tail = numbers
let moreNumbers = 0 :: numbers
let evenMoreNumbers = moreNumbers @ [ 7 .. 9 ]


/// 15.2.4 Comparing and contrasting collections

(* Quick check 15.2  - pg 185
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

/// Try this - pg 185
// SELECT * FROM Customers
// WHERE Customer.ID > 10000
// ORDER BY Customer.LastName

// Customers
// |> List.Filter (ID -> Customer.ID > 1000)
// |> List.Sort (Customer.ID -> )
