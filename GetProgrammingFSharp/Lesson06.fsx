//// Lesson 6 - Working with immutable data


/// 6.2 Being explicit about mutation

/// 6.2.1 Mutability basics in F#

// Listing 6.1 - Creating immutable values in F#
let name = "issac"
name = "kate"  // opps boolean - false

// Listing 6.2 - Trying to mutate an immutable value
// name <- "kate"  // error

// Listing 6.3 - Creating a mutable variable
let mutable name' = "issac"

name' <- "kate"
name' = "kate"  // true
name'


/// 6.2.2 Working with mutable objects

// Now you try
// Listing 6.4 - Working with mutable objects
open System.Windows.Forms
let form = new Form()
form.Show()
form.Width <- 400
form.Height <- 400
form.Text <- "Hello from F#"

// Listing 6.5 - Short hand for setting properties of mutable objects
open System.Windows.Forms
let form2 = new Form(Text = "Hello from F# - 2", Width = 300, Height = 300)
form2.Show()


// Quick check 6.1
// 1 What keyword do you use to mark a value as mutable in F#?
// A - mutable
// 2 What is the difference between = in C# and F#?
// A - in C# '=' updates mutable variables, in F# '=' tests for equlity
// 3 What keyword do you use in F# to update the value of a mutable object?
// A - object <- value



/// 6.3 Modeling state

/// 6.3.1 Working with mutable state

// Listing 6.6 - Managing state with mutable variables
let mutable petrol = 100.0   // initial state

let drive(distance) =       // modify state through mutation
    if distance = "far" then petrol <- petrol / 2.0
    elif distance = "medium" then petrol <- petrol - 10.0
    else petrol <- petrol - 1.0

drive("far")    // modify state
drive("medium")
drive("short")

petrol   // check state


/// 6.3.2 Working with immutable data

// Listing 6.7 - Managing state with immutable values
let drive'(petrol, distance) =     // explicitly pass state
    if distance = "far" then petrol / 2.0
    elif distance = "medium" then petrol - 10.0
    else petrol - 1.0

let petrol' = 100.0
let firstState = drive'(petrol', "far")
let secondState = drive'(firstState, "medium")
let finalState = drive'(secondState, "short")
finalState = 39.0;;


 // Now you try
 let drive''(petrol, distance:int) =     // explicitly pass state
     if   distance >= 50 then petrol / 2.0
     elif distance >= 25 then petrol - 10.0
     elif distance > 0   then petrol - 1.0
     else petrol
 
let petrol'' = 100.0
let firstState' = drive''(petrol'', 75)
let secondState' = drive''(firstState', 35);;
let finalState' = drive''(secondState', 15) 
finalState' = 39.0


/// 6.3.3 Other benefits of immutable data

(* Quick check 6.2
1 How do you handle changes in state when working with immutable data?
A - You return new state from functions

2 What is a pure function?
A - A function that doesn't modify global state

3 What impact does working with immutable data have with multithreading code?
A - It makes it easier to work with since it doesn't create shared glbal stae
*)


/// Try this

// 1 Try modeling another state machine with immutable data—for example, a kettle
//   that can be filled with water, which is then poured into a teapot or directly into a
//   cup.

let kettle(amount, pour) =
    if pour > amount then 0
    else amount - pour
 
let amount = 10
let amount1 = kettle(amount, 3)
let amount2 = kettle(amount1, 4)
let amount3 = kettle(amount2, 4)

System.Console.WriteLine(amount3)


// 1 Try modeling another state machine with immutable data—for example, a kettle
//    that can be filled with water, which is then poured into a teapot or directly into a
///   cup.

let web1 = new System.Net.WebClient()
