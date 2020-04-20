//// Lesson 5 - Trusting the Compiler


/// 5.1 Type inference as we know it

/// Quick check 5.1
// 1 The C# type-inference engine only infers interal valiable of methods
//   it doesn't infer the types of functon parameters and return values.
// 2 Dynamic typing types variables at run time.
//   Type inference is a static typing (compile time) system that figues
//   out the type of expression at compile time.


/// 5.2 F# type-inference basics

// Listing 5.6 - explicit type annotations in F#
let add (a:int, b:int) : int =
    let answer:int = a + b
    answer

// Listing 5.6 - Omitting the return type from a function in F#
let add' (a, b) = a + b

(* Now you try
Experiment with this code a little more to see how the compiler responds to code
changes:
1 Mix some type annotations on the function—for example, mark a as int and b as
  string. Does it compile?
2 Remove all the type annotations again, and rewrite the body to add an explicit
  value, such as the following:
    a + b + "hello"
3 Does this compile? What are the types? Why?
4 What happens if you call the function with an incompatible value?
*)


let add'' (a, b) =  // a:string * b:string -> string
    let answer = a + b + "hello"
    answer


// 5.2.1 Limitations of type inference

// Type-inference works best with native F# type and .NET types, C#
// defined types don't work as well.

// Listing 5.7 - Type inference when working with BCL types in F#

// let getLength (name) =  sprintf "Name is %d letters" name.Length
// doesn't compile, annotation required.
let getLength (name: string) = sprintf "Name is %d letters." name.Length
let foo(name) = "Hello! " + getLength(name)


// Classes and overloaded methods
// F# cant overload functions and use type inferece
// So class overloading doesn't always work.


/// 5.2.2 Type-inferred generics

// Listing 5.8 - Inferred type arguments in F#
open System.Collections.Generic

let numbers = List<_>()  // omit type argument
numbers.Add(10)
numbers.Add(20)

let otherNumbers = List()  // also illegal
otherNumbers.Add(10)
otherNumbers.Add(20)


// Listing 5.9 -  Automatic generaralization of a function
let createList(first, second) =
    let output = List()
    output.Add(first)
    output.Add(second)
    output

// val createList : first:'a * second:'a -> List<'a>

(* Quick check 5.2
1 How does F# infer the return type of a function?
A- I discovers the type of the last expression of the function.

2 Can F# infer types from the BCL?
A- No. 

3 Does F# allow implicit conversions between numeric types?
A- No. Conversions must be explicit.
*)


/// 5.3 Following the breadcrumbs

// Listing 5.10 - Complex type-inference example
let sayHello(someValue) =          // float -> string
    let innerFunction(number) =    // int -> string
        if number > 10 then "Isaac"
        elif number > 20 then "Fred"
        else "Sara"

    let resultOfInner =    // float to string
        if someValue < 10.0 then innerFunction(5)
        else innerFunction(15)

    "Hello " + resultOfInner

let result1 = sayHello(10.0)
let result2 = sayHello(20.5)


/// Quick check 5.3
// Type annotations can help spot and narrow down type errors.
// This is especially helpful when you explicitly know the type of an
// expression
