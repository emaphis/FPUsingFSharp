//// Lesson 5 - Trusting the Compiler


/// 5.1 Type inference as we know it

/// Quick check 5.1
// 1 The C# type-inference engine only infers interal valiable of methods
//   it doesn't infer the types of functon parameters and return values.
// 2 Dynamic typing types variables at run time.
//   Type inference is a static typing (compile time) system that figues
//   out the type of expression at compile time.


/// 5.2 F# type-inference basics

// explicit type annotations
let add (a:int, b:int) : int =
    let answer:int = a + b
    answer

let add' (a, b) = a + b

let add'' (a, b) =
    let answer = a + b + "hello"
    answer

// 5.2.1 Limitations of type inference

// Type-inference works best with native F# type and .NET types, C#
// defined types don't work as well.

let getLength (name : string) =
    sprintf "Name is %d letters" name.Length

// Classes and overloaded methods
// F# cant overload functions and use type inferece
// So class overloading doesn't always work.


// 5.2.2 Type-inferred generics

open System.Collections.Generic

let numbers = List<_>()
numbers.Add(10)
numbers.Add(20)

let otherNumbers = List()
otherNumbers.Add(10)
otherNumbers.Add(20)

// Automatic generaralization of a function
let createList(first, second) =
    let output = List()
    output.Add(first)
    output.Add(second)
    output


/// 5.3 Following the breadcrumbs

let sayHello(someValue) =
    let innerFunction(number) =
        if number > 10 then "Isaac"
        elif number > 20 then "Fred"
        else "Sara"

    let resultOfInner =
        if someValue < 10.0 then innerFunction(5)
        else innerFunction(15)

    "Hello " + resultOfInner


sayHello(10.0)

/// Quick check 5.3
// Type annotations can help spot and narrow down type errors.
// This is especially helpful when you explicitly know the type of an
// expression
