//// Lesson 13 Now you try - pg 157

(*
Let’s see how to write a function that prints a specific message regarding the Customer’s
age to a variety of output streams, such as the console or the filesystem:
1 Create an empty script file and define a Customer record type (or continue below
  the existing script you’ve been working on).
2 Create a function, printCustomerAge, that takes in a Customer and, depending on the
  Customer’s age, prints out Child, Teenager, or Adult, using Console.WriteLine to output
  text to FSI. The signature should read as let printCustomerAge customer =.
3 Try calling the function, and ensure that it behaves as expected.
4 Identify the varying element of code. For us, this is the call to Console.WriteLine.
5 Replace all occurrences with the value writer. Initially, your code won’t compile,
  as there’s no value called writer.
6 Insert writer as the first argument to the function, so it now reads let printCustomer-
  Age writer customer =.
7 You’ll see that writer has been correctly identified as a function that takes in a
  string and returns ‘a. Now, any function that takes in a string can be used in place
  of Console.WriteLine.
*)

/// Listing 13.6 - Injecting dependencies into functions - pg 136

open System

type Customer =
    { Age : int }

/// 3. writer is first parameter for partial application
let printCustmerAge writer customer =
    if customer.Age < 13 then writer "Child"
    elif customer.Age < 20 then writer "Teenager!"
    else writer "Adult!"

/// Listing 13.7 Partially applying a function with dependencies -158

let cust = { Age = 25 }

printCustmerAge Console.WriteLine cust
printCustmerAge (fun str -> str.Length) cust

printCustmerAge Console.WriteLine { Age = 23 }

let printToConsole = printCustmerAge Console.WriteLine

printToConsole { Age = 21 }
printToConsole { Age = 12 }
printToConsole { Age = 18 }


/// Listing 13.8 Creating a dependency to write to a file  - pg 158
open System.IO

let path = @"C:\bin\output.txt"
let writeToFile text = File.WriteAllText(path, text)

let printToFile = printCustmerAge writeToFile
printToFile { Age = 21 }

let text = File.ReadAllText(path)
text = "Adult!"
