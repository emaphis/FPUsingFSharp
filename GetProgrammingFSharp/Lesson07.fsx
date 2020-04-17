//// 7 - EXPRESSIONS AND STATEMENTS


/// 7.1.2 Making life better through expressions

(*
Quick check 7.1
1 How often do expressions return a value?
A - Expressions always return values.

2 How often do statements use side effects?
A - Statements always use side effects

3 What is the smallest unit of expression in C#?
A - A method call.
*)


/// 7.2 Using expressions in F#

// workin with expressions

open System

let describeAge age =
    let ageDescription =
        if   age < 18 then "Child!"
        elif age < 65 then "Adult!"
        else "OAP!"

    let greeting = "Hello"
    Console.WriteLine("{0}! you are a '{1}'.", greeting, ageDescription)

describeAge 64


// 7.2.3 Introducing unit

//describeAge : age:int -> unit


/// Now you try

// 1 Create an instance of unit by using standard let binding syntax; the right-hand
//   side of the equals sign needs to be ().
let myUnit1 = ()

// 2 Call the describeAge function and assign the result of the function call to a separate
//   value.

let myUnit2 = describeAge 45

let bool1 =  myUnit1 = myUnit2
Console.WriteLine(bool1)


// 7.2.4  Discarding results

let writeTextToDisk text =
    let path = System.IO.Path.GetTempFileName()
    System.IO.File.WriteAllText(path, text)
    path

let createManyFiles() =
    ignore(writeTextToDisk "The quick brown fox jumped over the lazy dog")
    ignore(writeTextToDisk "The quick brown fox jumped over the lazy dog")
    writeTextToDisk "The quick brown fox jumped over the lazy dog"

createManyFiles()

(* Quick check 7.2
1 What is the difference between a function returning unit and a void method?
A - unit is a value that represents nothing.  void is a non return.

2 What is the purpose of the ignore function in F#?
A - ignore consumes a value ad returns a unit. It is used for functions
    that the programmer doesn't need. It is useful for sideeffecting procedures
*)


/// 7.3 Forcing statemelett-based evaluation


let printFirstName () =
    let name = System.Console.ReadLine()
    let firstName = name.Split(' ').[0]
    System.Console.WriteLine(firstName)

printFirstName()

// factored into expressions
let printFirstNameExp () =
    let getName () =
        System.Console.ReadLine()

    let getFirstName(name : string) =
        name.Split(' ').[0]

    let printFirstName(firstName : string) =
        System.Console.WriteLine(firstName)

    let name = getName ()
    let first = getFirstName(name)
    printFirstName(first)

printFirstNameExp ()
