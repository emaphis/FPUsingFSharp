﻿//// Lesson 9 - Shaping data wtih Tuples - pg 101

/// 9.2 Tuple basics

/// Listing 9.3 - Returning aritrary data pairs in F#  -pg 103
let parseName(name: string) =
    let parts = name.Split(' ')
    let forename = parts.[0]
    let surname = parts.[1]
    forename, surname

let name = parseName("Issac Abraham")
let forename, surname = name;  // deconstructing
let fname, sname = parseName("Issac Abraham")

let a1 = "Issac", "Abraham", 35


// Now you try - pg 104
// Let’s do a bit of hands-on work with tuples:

// 1 Open a blank .fsx file for experimenting.
// 2 Create a new function, parse, which takes in a string person that has the format
//   playername game score (for example, Mary Asteroids 2500).
// 3 Split the string into separate values.
// 4 Convert the third element to an integer. You can use either System.Convert.ToInt32(),
//   System.Int32.Parse(), or the F# alias function for it, int().
// 5 Return a three-part tuple of name, game, and score and assign it to a value.
// 6 Deconstruct all three parts into separate values by using let a,b,c = syntax.
// 7 Notice that you can choose arbitrary names for each element

let parse (person: string) =        // 2
    let array = person.Split(' ')   // 3
    let name = array.[0]
    let game = array.[1]
    let score = int(array.[2])      // 4
    name, game, score               // 5

let a,b,c = parse("Mary Asteroids 2500")  // 6,7


/// 9.2.1 When should I use tuples?

(* Quick check 9.1  - pg. 105
1 How would you separate values in a tuple in F#?
A - Pattern matching to a comma separated list of vars

2 What is the main distinction between tuples in F# and C# 6?
A - F# has built in support, C# has library support  
*)


/// 9.3 More-complex tuples

/// 9.3.1 Tuple type signatures

let nameAndAge = "Joe", "Bloggs", 28
// val nameAndAge : string * string * int


/// 9.3.2 Nested tuples

// Listing 9.4 - Returning more-complex arbitrary data pairs in F# - pg 106
let nameAndAge' = ("Joe", "Bloggs"), 28
// val nameAndAge' : (string * string) * int
let name2, age = nameAndAge'  // deconstructing
let (forename1, surname1), theAge = nameAndAge'  // nested deconstruction

let type1 = nameAndAge'.GetType()
// System.Tuple`2[System.Tuple`2[System.String,System.String],System.Int32]


/// 9.3.3 Wildcards

// Listing 9.5 - Using wildcards with tuples  - pg 107
let nameAndAge3 = "Jane", "Smith", 25
let forename3, surname3, _ = nameAndAge3  // ignore 3rd element


/// 9.3.4 Type inference with tuples

// Listing 9.6 - Type inference with tuples in F#  - pg 107
let explicit : int * int = 10, 5 
let implicit = 10, 5
let _, _ = implicit  // ignore all elements

let addNumbers arguments =
    let a, b = arguments   // arguments is a tuple int * int
    a + b

let ints1 = addNumbers explicit
ints1 = 15

// Listing 9.7 - Genericized functions with tuples  - pg 107
let addNumbers' arguemnts =   // int * int * 'a * 'b -> int
    let a, b, c, _ = arguemnts  // deconstruct a four-part tuple
    a + b

let ints2 = addNumbers'(3, 4,'a','b')
ints2 = 7


(* Quick check 9.2
1 What is the type signature of nameAndAge in listing 9.4? Why?
A- val nameAndAge' : (string * string) * int
    The signature reflects the nested tuple

2 How many elements are in nameAndAge?
A- 2 the tuple and the int.

3 What is the purpose of the wildcard symbol?
A- To ignore the tuple at that position

4 How many wildcards can you use when deconstructing a tuple?
A- All?
*)


/// 9.4 Tuple best practices

/// 9.4.1 Tuples and the BCL

// Listing 9.8 - Implicit mapping of 'out' parameters to tuples - pg 109

// var number = "123";
// var result = 0;
// var parsed = System.Int32.TryParse(number, out result);
let number = "123"
let result, parsed = System.Int32.TryParse(number)


/// 9.4.3 When not to use tuples
// Tuples don't really have semantic meaning.
// Too many elements are hard to fallow

(* Quick check 9.3  - pg 110
1 What’s generally considered the maximum size you should use for a tuple?
A- maybe 3?  more is hard to follow

2 When should you be cautious of using tuples?
A- Causious of the use of the tuple
*)


(*  Try this  - pg 110
Look at methods in common BCL namespaces, and try to find some that you think
should be “tupled.” (Hint: Ones with out parameters are a good start!) Then, write a
function to load a filename and last-modified date from the filesystem, using a tuple as
the return type.
*)
let dir = System.IO.Directory.GetCurrentDirectory()
// dir = "C:\Users\emaphis\AppData\Local\Temp"

let getFile filePath =
    let out = System.IO.File.GetLastAccessTime filePath
    filePath, out.TimeOfDay

getFile "hello.txt"
