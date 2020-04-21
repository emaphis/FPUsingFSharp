//// Lesson 12 - Organizing code without Classes

// Hint:
//  Place related types together in namespaces.
//  Place related stateless functions together in modules


/// 12.1.1 Namespaces in F#
// used to logically organize data types: records and modules

/// Accessing the System.IO namespace functionality
let file = @"C:\bin\jdk8.cmd"
System.IO.File.ReadLines file

open System.IO
File.ReadLines file

/// 12.1.2 Modules in F#
// namespaces can't hold functions, only modules can.

// - Modules are like static classes in C#.
// - Modules are like namespaces but can also store functions.

// module MyApplication.BusinessLogic.DataAccess
// is a DataAccess module in the MyApplication.BusinessLogic namespace

/// 12.1.3 Visualizing namespaces and modules

/// 12.1.4 Opening modules

/// 12.1.5 Namespaces vs. modules


(* Quick check 12.1
1 Can you store values in namespaces?
A- No only types or odules
2 Can you store types in modules?
A- Yes, types and functions
*)


/// 12.2 Moving from scripts to applications

// Now you try -- lesson-12 project

(* Quick check 12.2
1 In what order are files read for dependencies in F#?
A- Downward in the list, Dependencies should come first

2 When can you omit a module declaration in an F# file?
A- When it's the last file in the project.  usually with the main function
*)


/// 12.3 Tips for working with modules and namespaces
// - type and functions are always public in F# by defualt
// - Unless namespace are declared with a parent namespace they ae always
//   global namespace
// - use [<AutoOpen>] to automatically open a namespace
// - Scripts can declare functions outside of a module because an implicit
//   module is declared


/// Try this

#load "Calculators.fs"

open Calculators

let add1 = add 3 4
let sub2 = sub 3 4
let mul3 = mul 3 4
let div4 = div 3 4
