//// Lesson 16 - Useful collection functions

/// 16.1 Mapping functions

/// 16.1.1 map
// mapping:('T -> 'U) -> list:'T list -> 'U list

open System

type Person =
    { Name : string
      Town : string }

let persons = 
    [ { Name = "Isaac"; Town = "London"}
      { Name = "Sara"; Town = "Birmingham" }
      { Name = "Tim"; Town = "London" }
      { Name = "Michelle"; Town = "Manchester" } ]


/// Listing 16.1 map

persons |> List.map (fun person -> person.Town)

let numbers = [ 1 .. 10 ]
let timesTwo n = n * 2

// evaluate ResizeArray with it's use so the compiler can
// infer it's types
let outputImperative = ResizeArray()

for number in numbers do
    outputImperative.Add (number |> timesTwo)

outputImperative

let outputFucntional = numbers |> List.map timesTwo

// Tuples in higher-order functions
[ "Issac", 30; "John", 25; "Sarah", 18; "Fay", 27 ]
|> List.map(fun (name, age) -> (age, name))


/// 16.1.2 iter
// like map but function must return unit.
// action:('T -> 'unit) -> list:'T list -> unit

persons |> List.iter (fun person -> printfn "Hello, %s" person.Town)

[ "Issac", 30; "John", 25; "Sarah", 18; "Fay", 27 ]
|> List.iter(fun (name, age) -> Console.WriteLine(name))


/// 16.1.3 collect
// mapping:('T -> 'U list) -> list:'T list -> 'U list
// flatMap

/// Listing 16.2 collect
type Order = { OrderId : int }
type Customer = { CustomerID : int; Orders : Order list; Town : string }
let customers : Customer list =
    [ { CustomerID = 10001
        Orders = [ {OrderId = 100};{OrderId = 103}; {OrderId = 120} ]
        Town = "London"};
      { CustomerID = 10010
        Orders = [ {OrderId = 105}; {OrderId = 102}; {OrderId = 107} ]
        Town = "Sheffeild" }
      { CustomerID = 10003
        Orders = []
        Town = "London" }
      { CustomerID = 10002
        Orders = [{OrderId = 104}; {OrderId = 115}]
        Town = "Newton" } ]
    
// collecting all orders for all customers into a single list
let orders : Order list = customers |> List.collect (fun c -> c.Orders)


/// 16.1.4 pairwise
// list:'T list -> ('T * 'T) lis

[ 1; 2; 3; 4; 5; 6 ] |> List.pairwise

/// Listing 16.3 Using pairwise within the context of a larger pipeline
open System

[ DateTime(2010,5,1)
  DateTime(2010,6,1)
  DateTime(2010,6,12)
  DateTime(2010,7,3) ]
  |> List.pairwise
  |> List.map(fun (a, b) -> a - b)
  |> List.map(fun time -> time.TotalDays)

[ 1; 2; 3; 4; 5; 6 ] |> List.windowed 3

(* Quick check 16.1
1 What is the F# equivalent of LINQ’s Select method?
A- map

2 What is the imperative equivalent to the iter function?
A- for-each loop

3 What does the pairwise function do?
A- It partitions a sequence into pairs
*)

/// 16.2 Grouping functions
// groups data into logical groups

/// 16.2.1 groupBy
// projection: ('T -> 'Key) -> list: 'T list -> ('Key * 'T list) list

persons |> List.groupBy (fun person -> person.Town)


/// 16.2.2 countBy
// projection: ('T -> 'Key) -> list: 'T list -> ('Key * int) list

persons |> List.countBy (fun person -> person.Town)


/// 16.2.3 partition
// predicate: ('T -> bool) -> list: 'T list -> ('T list * 'T list)

/// Listing 16.4 Splitting a collection in two based on a predicate

let londonCustomers, otherCustomers =
    customers
    |> List.partition(fun c -> c.Town = "London")


(* Quick check 16.2
1 When would you use countBy compared to groupBy?
A- groupBy groups elements into groups based on criteria, countBy counts instead of groups
2 Why would you use groupBy as opposed to partition?

*)


/// 16.3 More on collections

/// 16.3.1 Aggregates
// take a collection of items and merge them into a smaller
// collection of items (often just one).

/// Listing 16.5 Simple aggregation functions in F#
let numbers1 = [ 1.0 .. 10.0 ]
let _total = numbers1 |> List.sum
let _average = numbers1 |> List.average
let _max = numbers1 |> List.max
let _min = numbers1 |> List.min

/// 16.3.2 Miscellaneous functions
numbers1 |> List.find (fun num -> num = 5.0)
numbers1 |> List.head
numbers1 |> List.item(3)
numbers1 |> List.take(3)


/// 16.3.3 Converting between collections
// converting between lists, arrays and sequences

/// Listing 16.6 Converting between lists, arrays, and sequences
let numberOne =
    [ 1 .. 5 ]
    |> List.toArray
    |> Seq.ofArray
    |> Seq.head

(* Quick check 16.3
1 What is the F# equivalent to LINQ’s Aggregate method?
A- fold
2 What is the F# equivalent to LINQ’s Take method?
A- truncate
3 Give two reasons that you might need to convert between collection types in F#.
A-  Performance or resource reasons.
*)

/// Try this
open System.IO
let path = @"C:\src\"
let files = 
    Directory.GetDirectories(path)
    |> Array.map (fun dir -> Directory.GetFiles(dir)) 

Directory.EnumerateDirectories(path)
