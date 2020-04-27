/// Lesson 4 - saying a little, doing a lot  - pg 47

/// 4.1 Binding values in F#

// Listing 4.1 - Sample let bindings  - 49
let age = 35
let website = System.Uri "https://fsharp.org"
let add(first, second) = first + second


(* Now you try.  pg 50
You�ll now experiment with binding values to symbols:
1 Create a new F# script file.
2 Bind some values to symbols yourself:
  a A simple type (for example, string or int).
  b An object from within the BCL (for example, System.Random).
  c Create a simple one-line function that takes in no arguments and calls a function
    on the object that you created earlier (for example, random.Next()).
3 Remember to execute each line in the REPL by using Alt-Enter
*)
let str1 = "String 1";
let int2 = 2
let rdm3 = System.Random()
let fn4 = rdm3.Next

let int5 = fn4 100  // call fn4


/// 4.4.1 `let` isn't var!

// Listing 4.2 - Using let bindings  - 50
let foo() =
    let x = 10       // bind
    printfn "%d" (x + 20)   // use binding
    let x = "test"   // new binding
    let x = 50.0     // new binding
    x + 200.0

foo()


/// Quick check 4.1   - pg 51

// 1 Give at least two examples of values that can be bound to symbols with let.let x = 10
let x = 10
let y = fun a b -> a + b

// 2 What's the difference between let and var?
// let declares an alias for a value, var declares a reference to
// a memory location that can store values

// 3 Is F# a static or dynamic language?
// Static


/// 4.2 Scoping values

// Listing 4.4 - Scoping in F# - pg 52
open System

let doStuffWithTwoNumbers(first, second) =
    let added = first + second
    Console.WriteLine("{0} + {1} = {2}", first, second, added)
    let doubled = added * 2
    doubled

let ans1 = doStuffWithTwoNumbers(3, 4)
ans1


/// 4.2.1 Nested scopes
// Unmanaged scope

// Listing 4.5 - Unmanaged scope  - 53
let year = DateTime.Now.Year
let age2 = year - 1979
let estimatedAge' = sprintf "You are about %d years old" age2

estimatedAge'

// Listing 4.6 - Tightly bound scope - 54
let estimatedAge'' =                      // top level scope
    let age =                           // nested scope
        let year = DateTime.Now.Year    // visible only in nested scope
        year - 1979
    sprintf "You are about %d years old" age  // can't access year

estimatedAge''


/// 4.2.2 Nested functions

// functions as values

// Listing 4.7 - Nested (inner) functions  - pg 54
let estimatedAges(familyName, year1, year2, year3) =
    let calculatedAge yearOfBirth =
        let year = System.DateTime.Now.Year
        year - yearOfBirth
 
    let estimatedAge1 = calculatedAge year1
    let estimatedAge2 = calculatedAge year2
    let estimatedAge3 = calculatedAge year3
    
    let averageAge = (estimatedAge1 + estimatedAge2 + estimatedAge3) / 3
    sprintf "Average age for family %s id %d" familyName averageAge

// functions and classes that have a single public method as interchangeable
//  Fuctions vs. OO
// Constructor / single public method   - Arguments passed to the function
// Private fields                       - Local values
// Private methods                      - Local functions


/// Now you try - pg 55, 56
// See. lesson4Try.  Listing 4.8


/// Listing 4.9 -  Refactoring to functions - before - pg 56
let r = System.Random()
let nextValue = r.Next(1, 6)
let answer = nextValue + 10
answer

/// Listing 4.10 Refactoring to functions -- after  - pg 57
let generateRandomNumber max =  // function declaration added
    let r = System.Random()     // code indented 
    let nextValue = r.Next(1, max)  // values replaced parameters
    nextValue + 10              // return value as expression

let answer' = generateRandomNumber 6 
answer'


/// Quick check 4.2
// 1. Indicate scope with indentation.  Significant white-space
// 2. Yes. You can declare function in a nested scope. 
// 3. No. Don't user a return statment for a return value.


// Try this - pg 57.
// creating a set of functions that are deeply nested
// within one another. What happens if you call a function
// for example, Random.Next()

let level0 p q r s rand =
    let level1 t u v = 
        let level2 x y =
            let level3 z =
                z + System.Random().Next(rand)
            x + level3 y
        t + level2 u v
    p + level1 q r s

let ans2 = level0 1 2 3 4 100
ans2
