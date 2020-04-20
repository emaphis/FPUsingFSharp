﻿/// Lesson 3 -  REPL Changing how we develop

/// 3.2.2 State in FSI

// Listing 3.1
let CurrenTime = System.DateTime.UtcNow
CurrenTime.TimeOfDay.ToString()


/// 3.3.1  Creating scripts in F#

let text = "Hello, world"
text.Length

(* Quick check 3.1
1 What does REPL stand for?
A-  Read Evaluate Print Loop

2 Name at least two conventional processes used for developing applications.
A- App harness, Test Driven Development

3 What is the F# REPL called?
A- FSI.exe
*)

/// 3.3 F# scripts in Visual Studio

let text1 = "Hello, world"


/// 3.3.3 Woring with functions in scripts

// Listing 3.2
let greetPerson name age =
    sprintf "Hello, %s. Your are %d years old" name age

let greeting = greetPerson "Fred" 25

(* Quick check 3.2
1 Do scripts need a project in order to run?
A- no

2 Give two reasons that you might use a script rather than coding directly into FSI.
A- For IDE features for editing code, and a way to dave work.
*)


/// Do this

// count the words in a string
let countWords(text : string) =
    let count = (text.Split (' ')).Length
    count

let cnt = countWords("ab cd ef")
