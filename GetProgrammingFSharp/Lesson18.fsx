/// Lesson 18 - Folding your way to success

/// 18.1 Understanding aggregations and accumulators
/// Sum, Average, Min, Max, Count

/// Listing 18.1 - Example aggregation signatures
type Sum = int seq -> int
type Average = float seq -> float
type Count<'T> = 'T seq -> int

/// 18.1.1 Creating your first aggregation function
/// collection, accumulator, initial value
/// Listing 18.2 - Imperative implementation of sum
let sum' inputs =
    let mutable accumulator = 0
    for input in inputs do
        accumulator <- accumulator + input
    accumulator

sum' [ 1; 2; 3; 4; 5 ]


(* Now you try - pg 208
Try to create aggregation functions by using the preceding style for a couple of other
  aggregation functions:
1 Create a new .fsx script.
2 Copy the code from listing 18.2.
3 Create a function to calculate the length of a list (take any list from the previous
  lessons as a starting point!). The only thing that should change is the line that
  updates the accumulator.
4 Now do the same to calculate the maximum value of a list.
*) 
// 2.
let length inputs =
    let mutable accumulator = 0
    for _ in inputs do
        accumulator <- accumulator + 1
    accumulator

// 3.
length [] = 0
length [1] = 1
length [1; 2; 3] = 3

// 4.
let maxList inputs =
    let mutable accumulator = inputs |> Seq.head
    let larger x y = if x > y then x else y
    for input in inputs do
        accumulator <- larger accumulator input
    accumulator

maxList [ 3 ] = 3
maxList [ 1; 9; 3; 100; 4 ] = 100

(* Quick check 18.1
1 What is the general signature of an aggregation?
A-  seq<'a> -> 'b
2 What are the main components of any aggregation?
A- collection, accumulator, initial value
*)


/// 18.2 Saying hello to fold
// folder:( 'State -> 'T -> 'State) -> state:'State -> source:seq<'T> -> 'State
/// Listing 18.3 - Implementing sum through fold
let sum inputs =
    Seq.fold
        (fun state input -> state + input)
        0
        inputs

sum [ 1; 2; 3; 4; 5 ] = 15


/// Listing 18.4 Looking at fold with logging
let sum'' inputs =
    Seq.fold
        (fun state input ->
            let newState = state + input
            printfn 
                "Current state is %2d, input is %2d, new state is %2d"
                state input newState
            newState)
        0
        inputs

sum'' [ 1 .. 5 ] = 15

(* Now you try - pg 211
Next you’ll create a few aggregations of your own to improve your familiarity with fold:
1 Open your script from earlier.
2 Implement a length function by using fold.
3 Implement a max function by using fold.
*)
// 1 - length
let length' inputs =
    Seq.fold (fun state _ -> state + 1) 0 inputs

length' []  = 0
length' [ 1 ] = 1
length' [1; 2; 3] = 3

// 2 - max
let max' inputs =
    let larger x y = if x > y then x else y
    Seq.fold 
        (fun state input -> larger state input)
        (inputs |> Seq.head)
        inputs

max' [ 3 ] = 3
max' [ 1; 9; 3; 100; 4 ] = 100


/// 18.2.1 Making fold more readable

let sum1 inputs = 
    Seq.fold (fun state input -> state + input) 0 inputs

let sum2 inputs =
    inputs |> Seq.fold (fun state input -> state + input) 0

let sum3 inputs =
    (0, inputs) ||> Seq.fold (fun state input -> state + input)


/// 18.2.2 Using related fold functions

// foldBack - folds in reverse
// mapFold - combo of map and fold
// reduce - same a fold, bu first elemet is initial state.
// scan - generates intermediate results along with a final value
// unfold - generates a sequence from a starting sate


/// 18.2.3 - Folding instead of while loops
/// Listing - 18.6 Accumulating through a while loop

open System.IO
let mutable totalChars = 0
let sr = new StreamReader(File.OpenRead @"C:\bin\book.txt")
while (not sr.EndOfStream) do
    let line = sr.ReadLine()
    totalChars <- totalChars + line.ToCharArray().Length

//totalChars


/// Listing 18.7 - Simulating a collection through sequence expressions
/// using 'yield' to simulate a collection

open System.IO
let lines : string seq =
    seq {
        use sr = new StreamReader(File.OpenRead @"C:\bin\book.txt")
        while (not sr.EndOfStream) do
            yield sr.ReadLine() } ;;

let len = (0, lines) ||> Seq.fold(fun total line -> total + line.Length)


(* Quick check 18.2
1 What’s the difference between reduce and fold?
A- Reduce uses the first value of the sequence as the starting state value

2 Which two F# keywords are important in order to lazily generate sequences of data?
A- seq, yield
*)


/// 18.3 Composing functions with fold
/// a rule based parser
/// Listing 18.8 - Creating a list of rules

open System
type Rule = string -> bool * string

let rules : Rule list =
    [ fun text -> (text.Split ' ').Length = 3, "Must be three words"
      fun text -> text.Length <= 30, "Max lenth is 30 characters"
      fun text -> text
                |> Seq.filter Char.IsLetter
                |> Seq.forall Char.IsUpper, "All letters must be caps" ]


/// 18.3.1 - Composing rules manually
/// - Listing 18.9 - Manually building a super rule
/// concatinate rules ...
let validateManual (rules: Rule list) word =
    let passed, error = rules.[0] word     // testing the first rule
    if not passed then false, error
    else
        let passed, error = rules.[1] word
        if not passed then false, error
        else
            let passed, error = rules.[2] word
            if not passed then false, error
            else true, ""

let validateM = validateManual rules

/// 18.3.2 Folding functions together
let buildValidator (rules : Rule list) =
    rules
    |> List.reduce(fun firstRule secondRule ->
        fun word ->                             // higher-order function
            let passed, error = firstRule word      // run first rule
            if passed then                      // passed, move to next rule
                let passed, error = secondRule word
                if passed then true, "" else false, error
            else false, error)          // failed return error

let validate = buildValidator rules
let word1 = "HELLO FrOM F#"

validate word1
validateM word1
// val it : bool * string = (false, "All letters must be caps")

/// Now you tru - pg 217
// See Lesson18Parser.fs





///////////////////////////
let myFold folder state source =
    let mutable acc = state
    for input in source do
        acc <- folder acc input
    acc

myFold (+) 0 [ 1; 2; 3; 4; 5 ]

let ssss = seq<int>
///ssss.Add(1)

System.Console.WriteLine("Got here aaaa")