//// Lesson 13 try

/// Listing 13.6 - Injecting dependencies into functions

open System

type Customer =
    { Age : int }

// writer is first parameter for partial application
let printCustmerAge writer customer =
    if customer.Age < 13 then writer "Child"
    elif customer.Age < 20 then writer "Teenager!"
    else writer "Adult!"

/// Listing 13.7 Partially applying a function with dependencies

let cust = { Age = 25 }

printCustmerAge Console.WriteLine cust
printCustmerAge (fun str -> str.Length) cust

printCustmerAge Console.WriteLine { Age = 23 }

let printToConsole = printCustmerAge Console.WriteLine
printToConsole { Age = 21 }
printToConsole { Age = 12 }
printToConsole { Age = 18 }


/// Listing 13.8 Creating a dependency to write to a file
open System.IO

let path = @"C:\bin\output.txt"
let writeToFile text = File.WriteAllText(path, text)

let printToFile = printCustmerAge writeToFile
printToFile { Age = 21 }

let text = File.ReadAllText(path)
text = "Adult!"

(* Quick check 13.3
What’s the key difference between passing dependencies in F# and C#?
A- F# pases fuctions of the right signature
   C# passed classes implementing interfaces.
*)
