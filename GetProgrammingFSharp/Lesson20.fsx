//// Lesson 20 - Program flow in F#  - pg 231

/// 20.1  A tour around loops inF#

/// 20.1.1 for loops
/// Listing 20. - 1 for .. in loops in F# - pg 232

for number in 1 .. 10 do
    printfn "%d Hello!" number

for number in 10 .. -1 .. 1 do
    printfn "%d Hello!" number

let customerIds = [ 45 .. 99 ]

for customerId in customerIds do
    printfn "%d bought something!" customerId

for even in 2 .. 2.. 10 do
    printfn "%d is an even number!" even

/// 20.1.2 while loops
/// Listing 20.2 - while loops in F#  - pg 232
open System.IO
let reader = new StreamReader(File.OpenRead @"C:\Testing\File.txt")

while (not reader.EndOfStream) do
    printfn "%s" (reader.ReadLine())

reader.Close()



/// 20.1.3 Comprehensions
/// Listing 20.3 - Comprehensions in F# - pg 233
open System
let arrayOfChars = [| for c in 'a' .. 'z' -> Char.ToUpper c |]
let listOfSquares = [ for i in 1 .. 10 -> i * i]
let seqOfStrings = seq { for i in 2 .. 4 .. 20 -> sprintf "Number %d" i }


(* Quick check 20.1  - pg 234
1 What restriction does F# place on returning out of loops?
A- F# has no mechanism for breaking from a loop.
2 What is the syntax to perform for-each loops in F#?
A-
for 'element' in 'collection' do
   'whatever'

3 Can you use while loops in F#?
A- yes
*)

/// 20.2 Branching logic in F#
/// 20.2.1 Priming exercise—customer credit limits - pg 234

/// Listing 20.4 If/then expressions for complex logic - pg 235
let getLimit (score, years) =
    if score = "medium" && years = 1 then 500
    elif score = "good" && (years = 0 || years = 1) then 750
    elif score = "good" && years = 2 then 1000
    elif score = "good" then 2000
    else 250

let customer = "good", 1
getLimit customer

/// 20.2.2 - Say hello to pattern matching
/// Listing 20.5 - Our first pattern-matching example - pg 236
let limit =
    match customer with
    | "medium", 1 -> 500
    | "good", 0 | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250


(* Now you try  - pg 237
Let’s work through a simple example that illustrates how exhaustive pattern matching
works:
1 Open a new script file.
2 Create a function getCreditLimit that takes in a customer value. Don’t specify the
  type of the customer; let the compiler infer it for you.
3 Copy across the pattern-matching code from this sample that calculates the limit
  and return the limit from the function. Ensure that this compiles and that you can
  call it with a sample tuple; for example, ("medium", 1).
4 Remove the final (catchall) pattern (| _ -> 250).
5 Check the warning highlighted at the top of the match clause, as shown in figure 20.1
----------------------
1 Change the new pattern that you just created to match on (_, 1).
2 Move that clause to be the first pattern
*)
// 2, 3
let getCreditLimit customer =
    match customer with
   // | _, 1  -> 250        
    | "medium", 1 -> 500   // warning FS0026: This rule will never be matched
    | "good", 0 | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250  // 4,5 - warning FS0025: Incomplete pattern matches on this expression. For example, the value '(_,0)' may indicate a case not covered by the pattern(s).
//    | _, 0  -> 250
//    | _, 2  -> 250 // 7,8

getCreditLimit ("medium", 1) // 3
getCreditLimit ("bad", 3)    // 6 - Microsoft.FSharp.Core.MatchFailureException:

/// 20.2.4 Guards
/// Listing 20.6 - Using the when guard clause  - pg 254
let getCreditLimit2 customer =
    match customer with
    | "medium", 1 -> 500
    | "good", years when years < 2 -> 750
    | "good", _ -> 2000
    | _ -> 250


/// 20.2.5 Nested matches
/// 
let getCreditLimit3 customer =
    match customer with
    | "medium", 1 -> 500
    | "good", years ->
        match years with
        | 0 | 1 -> 750
        | 2  -> 1000
        | _  -> 2000
    | _ -> 250 


(* Quick check 20.2 - pg 239
1 What are the limitations of switch/case?
A- Limited types, limited ability to catch errors, not an experession

2 Why can unconstrained clauses such as if/then expressions lead to bugs?
A- If/then is too general so can't be checked for many errors.

3 What sort of support does pattern matching provide to ensure correctness?
A- Pattern matching can catch unmatched values/clauses
*)


/// 20.3 Flexible pattern matching

/// 20.3.1 Collections

(* Now - you try - pg 240
Let’s work through the preceding logic to see pattern matching over lists in action:
1 Create a Customer record type that has fields Balance : int and Name : string.
2 Create a function called handleCustomers that takes in a list of Customer records.
3 Implement the preceding logic by using standard if/then logic. You can use
  List.length to calculate the length of customers, or explicitly type-annotate the
  Customer argument as Customer list and get the Length property on the list.
4 Use failwith to raise an exception (for example, failwith "No customers supplied!").
5 Now enter the following pattern match version for comparison.
*)

type Customer =  // 1
    { Name : string
      Balance : int }

let handleCustomers (customers : Customer list) =
    let  length = customers.Length
    if   length = 0 then failwith "No customers supplied!"
    elif length = 1 then printfn "%s" customers.Head.Name
    elif length = 2 then printfn "%d" (customers.Head.Balance + 
                                       customers.Tail.Head.Balance) 
    else printfn "%d" length

[] |> handleCustomers

[ { Name = "Fred"; Balance = 100 } ] |> handleCustomers

[ { Name = "Fred"; Balance = 100 }
  { Name = "Ginger"; Balance = 200 }] |> handleCustomers

[ { Name = "Fred"; Balance = 100 };
  { Name = "Ginger"; Balance = 200 };
  { Name = "Softshoe"; Balance = 300 } ] |> handleCustomers


let handleCustomers2 (customers : Customer list) =
    match customers.Length with
    | 0  -> failwith "No customers supplied!"
    | 1  -> printfn "%s" customers.Head.Name
    | 2  -> printfn "%d" (customers.Head.Balance + 
                          customers.Tail.Head.Balance)
    | _  -> printfn "%d" customers.Length


[] |> handleCustomers2

[ { Name = "Fred"; Balance = 100 } ] |> handleCustomers2

[ { Name = "Fred"; Balance = 100 }
  { Name = "Ginger"; Balance = 200 }] |> handleCustomers

[ { Name = "Fred"; Balance = 100 };
  { Name = "Ginger"; Balance = 200 };
  { Name = "Softshoe"; Balance = 300 } ] |> handleCustomers

/// Listing 20.8 - Matching against lists  - pg 240
let handleCustomer3 customers =
    match customers with
    | [] -> failwith "No customers supplied!"
    | [ customer ] -> printfn "Single customer, name is %s" customer.Name
    | [ first; second ] ->
        printfn "Two customers, balance = %d"
             (first.Balance + second.Balance)
    | customers -> printfn "Customers supplied: %d" customers.Length

handleCustomer3 [] // throws exception
handleCustomer3 [ { Balance = 10; Name = "Joe" } ] // prints name



/// 20.3.2 Records
/// 20.9  - Pattern matching with records - pg 241
let getStatus customer =
    match customer with
    | { Balance = 0 } -> "Customer has empty balance!"
    | { Name = "Isaac"} -> "This is a great customer!"
    | { Name = name; Balance = 50 } -> sprintf "%s has a large balance!" name
    | { Name = name } -> sprintf "%s is a normal customer" name
    
{ Balance = 50; Name = "Joe" } |> getStatus

/// Listing 20.10 - Combining multiple patterns - pg 242
// 1 The list of customers has three elements.
// 2 The first customer is called “Tanya.”
// 3 The second customer has a balance of 25.

let example1 customers =
    match customers with
    | [ { Name = "Tanya" }; { Balance = 25}; _ ] -> "It's a match!"
    | _  -> "No match!"


(* Quick check 20.3  - pg 242
What collection types can you not pattern match against?
Lazy sequences can't be matched against
*)


/// 20.4 To match or not to match
/// Listing 20.11 - When to use if/then over match - pg 258

let customer1 = { Name = "Isaac"; Balance = 100 }

// The only time it’s simpler to use if/then is when you’re working
// with code that returns unit, and you’re implicitly missing the
// default branch.

/// much simpler
if customer1.Name = "Isaac" then printfn "Hello!"

match customer1.Name with
    | "Isaac" -> printfn "Hello!"
    | _ -> ()


(* Try this  - pg 243
Experiment with pattern matching over lists, tuples, and records. Start by creating a
random list of numbers of variable length and writing pattern matches to test whether
the list
* Is a specific length
* Is empty
* Has the first item equal to 5 (hint: use head/tail syntax here with ::)
Then experiment with pattern matching over a record. Continue with the filesystem
“Try this” exercise from the previous lessons; pattern match over data to check whether
a folder is large, based on average file size or count of files.
*)


