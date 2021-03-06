﻿//// Lesson 11 - Building composable functions - pg 125

/// 11.1 Partial function application

// Listing 11.1 - Passing arguments with and without brackets - pg 126
let tupleAdd(a,b) = a + b   // tupled: int * int -> int
let ans1 = tupleAdd (5,10)

let curriedAdd a b = a + b  // curried: int -> int -> int
let ans2 = curriedAdd 5 10


/// Listing 11.2 - Callig a curried function is steps - pg 127
let add first second = first + second  // curried function
let addFive = add 5             // creating  fucntion using partial application
let fifteen = addFive 10
fifteen = 15

(* Quick check 11.1  - pg 128
What’s the difference between a curried function and a tupled function?
A- Tupled functions take one arguement, a tuple. A curried function takes
    multiple arguments and can be partially applied.
*)


/// 11.2 Constraining functions

/// Listing 11.3 - Explicitly creating wrapper functions in F# - pg 128
open System
let buildDt year month day = DateTime(year, month, day)

// wraper functions
let buildDtThisYear month day = buildDt DateTime.UtcNow.Year month day
let buildDtThisMonth day = buildDtThisYear DateTime.UtcNow.Month day


/// Listing 11.4 - Creating wrapper functions by currying - pg 128
let buildDtThisYear' = buildDt DateTime.UtcNow.Year
let buildDtThisMonth' = buildDtThisYear' DateTime.UtcNow.Month


(* Now you try - pg 129
Create a simple wrapper function, writeToFile, for writing data to a text file:
1 The function should take in three arguments in this specific order:
  a date—the current date
  b filename—a filename
  c text—the text to write out
2 The function signature should be written in curried form (with spaces separating
  the arguments).
3 The body should create a filename in the form {date}-{filename}.txt. Use the System
  .IO.File.WriteAllText function to save the contents of the file.
4 You can either manually construct the path by using basic string concatenation,
  or use the sprintf function.
5 You should construct the date part of the filename explicitly by using the ToString
  override—for example, ToString("yyMMdd"). You need to explicitly annotate the type
  of date as System.DateTime.
*)
/// Listing 11.5 - Creating your first curried function - pg 130
open System.IO
// 1. 2. 3. 4. 5.
let writeToFile (date : DateTime) filename text =
    let path = sprintf "%s-%s.txt" (date.ToString "yyMMdd") filename
    File.WriteAllText(path, text)

// writeToFile : date:DateTime -> filename:string -> text:string -> unit


/// Listing 11.6 - Creating constrained functions  - pg 130
let writeToToday = writeToFile DateTime.UtcNow.Date
let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.)
let writeToTodayHelloWorld = writeToToday "hello-world"

//writeToToday "first-file" "The quick brown fox jumped over the lazy dog"
//writeToTomorrow "second-file" "The quick brown fox jumped over the lazy dog"
//writeToTodayHelloWorld "The quick brown fox jumped over the lazy dog"


(* Quick check 11.2  - pg 130
Name at least two differences between C# methods and F# let-bound
functions.
A- Functions are static, methods can be static or not-static
   Functions can use currying but can't be overloaded
*)


/// 11.2.1 Pipelines
// calling function in ordered fashion

// Listing 11.7 - Calling functions arbitrarily  - pg 131
let checkCreation (creationDate : DateTime) =
    if (DateTime.UtcNow - creationDate).TotalDays > 7. then sprintf "Old"
    else sprintf "New"

// calling functions arbitrarily
let time =
    let directory = Directory.GetCurrentDirectory()  // temporary value
    Directory.GetCreationTime directory              // use temporary value

checkCreation time

// Listing 11.8 - Simplistic chaining of functions - pg 131
checkCreation(
    Directory.GetCreationTime(
        Directory.GetCurrentDirectory()))

// Listing 11.9 - chaining three function together using the pipelin operator 132
Directory.GetCurrentDirectory()
|> Directory.GetCreationTime
|> checkCreation


/// 11.2.2 Custom fonts
// Consolas vs Fira Code for rendering special operators


(* Now you try - pg 134
Let’s revisit the simple driving and petrol example from lesson 6 and see whether you
can make the code more elegant by using pipelines. Recall that the original code looked
something like the following.
*)
let drive distance petrol =
    if distance = "far" then petrol / 2.0
    elif distance = "medium" then petrol - 10.0
    else petrol - 1.0

/// Listing 11.11 Review of existing petrol sample - old version
let startingPetrol = 100.0

let petrol1 = drive "far" startingPetrol
let petrol2 = drive "medium" petrol1
let petrol3 = drive "short" petrol2

petrol3 = 39.0

/// Listing 11.12 Using pipelines to implicitly pass chained state - pg 134
let startingPetrol2 = 100.0

let endingPetrol =
    startingPetrol2
    |> drive "far"
    |> drive "medium"
    |> drive "short"

endingPetrol = 39.0


(* Quick check 11.3  - pg 135
1 Which argument to a function is one that can be flipped over a pipeline?
A- The final argument is the one passed down the chain

2 Can you use C# or VB .NET methods with the pipeline?
A- Single parameter method
*)


/// 11.3 Composing functions together

/// Listing 11.13 - Automatically composing functions  - pg 135
let checkCurrentDirectoryAge =
    Directory.GetCurrentDirectory
    >> Directory.GetCreationTime
    >> checkCreation      // create a function by composing a set of fucntions to gether

let description = checkCurrentDirectoryAge ()


(* Quick check 11.4  - pg 136
1 What operator do you use for composing two functions together?
A- >>

2 What rule do you need to adhere to in order to compose two functions together?
A- Functions have to take and return the sam type
*)

/// Try this  - pg 137
// TODO:
