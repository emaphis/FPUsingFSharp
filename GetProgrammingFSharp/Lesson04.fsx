/// Lesson 3 - saying a little, doing a lot

/// 4.2 Binding values in F#

let age = 35
let website = System.Uri "https://fsharp.org"
let add(first, second) = first + second


/// Now you try.

let str1 = "String 1";
let int2 = 2
let rdm3 = System.Random()
let fn4 = rdm3.Next

let int5 = fn4 100  // call fn4


/// 4.4.1 `let` isn't var!

// rebinding
let foo() =
    let x = 10       // bind
    printfn "%d" (x + 20)   // use binding
    let x = "test"   // new binding
    let x = 50.0     // new binding
    x + 200.0

foo()

/// Quick check 4.1

// 1 Give at least two examples of values that can be bound to symbols with let.let x = 10
let x = 10
let y = fun a b -> a + b

// 2 What’s the difference between let and var?
// let declares an alias for a value, var declares a reference to
// a memory location that can store values

// Static

/// 4.2 Scoping values

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
let year = DateTime.Now.Year
let age2 = year - 1979
let estimatedAge' = sprintf "You are about %d years old" age2

estimatedAge'

// Tightly bound scope
let estimatedAge'' =                      // top level scope
    let age =                           // nested scope
        let year = DateTime.Now.Year    // visible only in nested scope
        year - 1979
    sprintf "You are about %d years old" age  // can't access year

estimatedAge''


/// 4.2.2 Nested functions

// functions as values
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


/// Now you try
// lesson4Try.


/// Refactoring - Listing 4.10

let r = System.Random()
let nextValue = r.Next(1, 6)
let answer = nextValue + 10

answer

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


// Try this
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
