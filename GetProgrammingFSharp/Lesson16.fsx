//// Lesson 16 - Useful collection functions  - pg 186

/// 16.1 Mapping functions

/// 16.1.1 map
// mapping:('T -> 'U) -> list:'T list -> 'U list

/// Figure 16.1  - pg 187
open System

type Person =
    { Name : string
      Town : string }

let persons = 
    [ { Name = "Isaac"; Town = "London"}
      { Name = "Sara"; Town = "Birmingham" }
      { Name = "Tim"; Town = "London" }
      { Name = "Michelle"; Town = "Manchester" } ]

persons |> List.map (fun person -> person.Town)


/// Listing - 16.1 map  - pg 187

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

/// Figue 16.2  - pg 188
persons |> List.iter (fun person -> printfn "Hello, %s" person.Town)

[ "Issac", 30; "John", 25; "Sarah", 18; "Fay", 27 ]
|> List.iter(fun (name, age) -> Console.WriteLine(name))


/// 16.1.3 collect
// mapping:('T -> 'U list) -> list:'T list -> 'U list
// flatMap

/// Listing 16.2 collect  - pg 16.2
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

/// Figure 16.5  - pg 190
[ 1; 2; 3; 4; 5; 6 ] |> List.pairwise

/// Listing 16.3 - Using pairwise within the context of a larger pipeline  -pg 191
open System

[ DateTime(2010,5,1)
  DateTime(2010,6,1)
  DateTime(2010,6,12)
  DateTime(2010,7,3) ]
|> List.pairwise
|> List.map(fun (a, b) -> a - b)
|> List.map(fun time -> time.TotalDays)

  // windowed example
[ 1; 2; 3; 4; 5; 6 ] |> List.windowed 3


(* Quick check 16.1  - pg 191
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
// pg 193
let londonCustomers, otherCustomers =
    customers
    |> List.partition(fun c -> c.Town = "London")


(* Quick check 16.2  - pg 193
1 When would you use countBy compared to groupBy?
A- groupBy groups elements into groups based on criteria, countBy counts instead of groups
2 Why would you use groupBy as opposed to partition?
A- groupBy groups to an unlimited number pf groups, partition is binary
*)


/// 16.3 More on collections

/// 16.3.1 Aggregates
// take a collection of items and merge them into a smaller
// collection of items (often just one).

/// Listing 16.5 Simple aggregation functions in F#  - pg 194
let numbers1 = [ 1.0 .. 10.0 ]
let _total = numbers1 |> List.sum
let _average = numbers1 |> List.average
let _max = numbers1 |> List.max
let _min = numbers1 |> List.min

/// 16.3.2 Miscellaneous functions  - pg 184
let _five = numbers1 |> List.find (fun num -> num = 5.0)
let _one = numbers1 |> List.head
let _lst1 = numbers1 |> List.item(3)
let _lst2 = numbers1 |> List.take(3)
let _bool1 = numbers1 |> List.exists(fun num -> num > 3.0)
let _bool2 = numbers1 |> List.forall(fun num -> num >= 2.0)
let _bool3 = numbers1 |> List.contains(3.0)
let _lst3 = numbers1 |> List.filter(fun num -> num > 3.0)
let _cnt1 = numbers1 |> List.length
let _lst4  = numbers1 |> List.distinct 


/// 16.3.3 Converting between collections
// converting between lists, arrays and sequences

/// Listing 16.6 Converting between lists, arrays, and sequences - pg 195
let numberOne =
    [ 1 .. 5 ]
    |> List.toArray
    |> Seq.ofArray
    |> Seq.head


(* Quick check 16.3  - pg 196
1 What is the F# equivalent to LINQ’s Aggregate method?
A- fold
2 What is the F# equivalent to LINQ’s Take method?
A- truncate
3 Give two reasons that you might need to convert between collection types in F#.
A-  Performance or resource reasons.
*)

(* Try this - pg 196
Write a simple script that, given a folder path on the local filesystem, will return the
name and size of each subfolder within it. Use groupBy to group files by folder, before
using an aggregation function such as sumBy to total the size of files in each folder. Then,
sort the results by descending size. Enhance the script to return a proper F# record that
contains the folder name, size, number of files, average file size, and the distinct set of
file extensions within the folder.
*)
open System.IO

let path = @"C:\Testing\"

/// Represents a directory and a list of files that belong to that directory
/// - created by the groupBy function
type DirInfo = string * FileInfo list

/// get list names of subdirectories of passed dir
let getSubDirectories path =
    Directory.GetDirectories path
    |> Array.toList

/// given a dir get a list of fileInfo in directory
let getFileList dir =
    Directory.GetFiles(dir)
    |> Array.map (fun file -> FileInfo(file))
    |> Array.toList

/// get a list of files on a given path
/// list of files should be grouped by dir
let getSubDirs (path : string) : DirInfo list =
    getSubDirectories path          // get subdirectories of dir
    |> List.collect getFileList     // get list of files in directories
    |> List.groupBy (fun file -> file.DirectoryName)


let getFileSizes (subdir : DirInfo) =
    let dir, filelist = subdir
    let dirFileSizes =
        filelist |> List.map (fun file -> file.Length)
                 |> List.reduce (+)
    (dir, dirFileSizes)

let getDirSizes dirPath =
  let subdirs = getSubDirs dirPath
  let dirSizes = subdirs |> List.map (fun  dir -> (getFileSizes dir))
  dirSizes |> List.sortBy (fun (dir, size) -> size) |> List.rev

getDirSizes path

///////////////////////////////////////////////////////////
type DirRecord =
    { Name: string
      Size: int
      NumFiles: int
      AvgSize: float
      Extensions: string list }

let createDirRecord (dirInfo : DirInfo) =
    let path, files = dirInfo
    { Name = path; 
      Size = files |> List.sumBy (fun file -> int(file.Length))
      NumFiles = files.Length
      AvgSize = (files |> List.sumBy (fun file -> float(file.Length))) / float(files.Length)
      Extensions = files |> List.map (fun file -> file.Extension) |> List.distinct
    }

let getDirInfo dirPath =
    let subdirs = getSubDirs dirPath
    subdirs |> List.map (fun dirInfo -> createDirRecord dirInfo)
            |> List.sortByDescending (fun dir -> dir.Size)

getDirInfo path
