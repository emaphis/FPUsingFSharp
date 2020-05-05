//// Lesson 25 - Consuming C# from F# - pg 299

/// 25.1.1 Creating a hybrid solution

(*  Now you try - pg 300 
1 Open Visual Studio and create a new F# console application called Lesson-25.
2 Add a new C# class library called CSharpProject to the solution. Add a reference
to the CSharpProject from the FSharpProject by using the standard Add Reference
dialog box,
*)

(* Quick check 25.1  - pg 301
1 Can you share F# and C# projects in the same solution?
A- Yes
2 Name some types of assets that you can reference from an F# solution.
A- Assemblyes, other projects, BCL code
3 What kind of type is not well supported in F#?
A- Dynamic types
*)

/// 25.2 The Visual Studio experience

/// 25.2.1 Debugging
(* Now you try  - pg 302
Now try debugging F# in VS2015:
1 Set a breakpoint inside the definition C# Person class constructor.
2 Run the F# console application. Observe that the debugger hits with the name
value set to Tony.
3 Also, observe that the call stack is correctly preserved across the two languages,
*)

/// 25.2.2 Navigating across projects
/// 25.2.3 Projects and assemblies

(*
 25.2.4 Referencing assemblies in scripts

 #r    References a DLL for use within a script  -- #r @"C:\source\app.dll"
 #I    Adds a path to the #r search path         -- #I @"C:\source\"
 #load Loads and executes an F# .fsx or .fs file -- #load @"C:\source\code.fsx"

 Now you try - pg 304
 Now experiment with referencing an assembly within a script:
 1 Create a new script as a new solution item called Scratchpad.fsx.
 2 Open the script file and enter the following code.
 3 Execute the code in the script by using the standard Send to F# Interactive behavior.
   Notice the first line that’s output in FSI
 
 Listing 25.3 Consuming C# assemblies from an F# script
 *)

#r @"C:\Users\emaphis\source\repos\FPUsingFSharp\CSharpProject\bin\Debug\netcoreapp3.1\CSharpProject.dll"
open CSharpProject
let simon = Person "Simon"
simon.PrintName()

(* 25.2.5 Debugging scripts - pg 305

Now you try
Let’s debug the script that you already have open to see how it operates in VS2015:
1 With the script from listing 25.3 still open, right-click line 3 (the constructor call
  line) with your mouse.
2 From the pop-up menu, choose Debug with F# Interactive.
3 After a short delay, you’ll see the line highlighted, as shown in figure 25.5. From
  there, you can choose the regular Step Into code as usu
*)

(* Quick check 25.2 - pg 305
1 Can you debug across languages?
A- Yes,
2 Can you go to a definition across languages?
A- Yes.
3 How do you reference a library from within a script?
A- Using the #r directive
*)

/// 25.3 Working with OO constructs

/// Listing 25.4 Treating constructors as functions - pg 306
open CSharpProject

let longhand =
    ["Tony"; "Fred"; "Samantha"; "Brad"; "Sophie" ]
    |> List.map (fun name -> Person(name))

let shorthand = 
    ["Tony"; "Fred"; "Samantha"; "Brad"; "Sophie" ]
    |> List.map Person

/// 25.3.1 Working with interfaces
/// Listing 25.5 Treating constructors as functions - pg 307
open System.Collections.Generic

type PersonComparer() =
    interface IComparer<Person> with
        member this.Compare(x, y) = x.Name.CompareTo(y.Name)

let pComparer = PersonComparer() :> IComparer<Person>
pComparer.Compare(simon, Person "Fred")


(* Now you try - pg 307
F# Power Tools comes with a handy refactoring to implement an interface for you:
1 Enter the first three lines from listing 25.5.
2 Remove the with keyword from the third line.
3 Move the caret to the start of IComparer in the same line.
4 You’ll be presented with a smart tag (figure 25.6). Press Ctrl-period to open it.
5 Try both forms of generation. The first (nonlightweight) will generate an implementation
  with fully annotated type signatures; the latter will omit type annotations
  if possible and place method declarations with stub implementations on a
  single line.
*)
let personComparer =
    { new IComparer<Person> with
          member this.Compare(x: Person, y: Person): int = 
              raise (System.NotImplementedException())
          }         


/// 25.3.2 Object expressions

/// Listing 25.6 Using object expressions to create an instance of an interface pg. 308
let pComparer1 =
    { new IComparer<Person> with
          member this.Compare(x, y) = x.Name.CompareTo(y.Name) }


/// 25.3.3 Nulls, nullables, and options
/// Listing 25.7 Option combinators for classes and nullable types pg 308
open System

// Creating a selection fo null and non-null strings
// and value types.
let blank: string = null
let name = "Vera"
let number = Nullable 10
let blankAsOption = blank |> Option.ofObj
let nameAsOption = name |> Option.ofObj
let numberAsOption = number |> Option.ofNullable
let unsafeName = Some "Fred" |> Option.toObj


(* Quick check 25.3  - pg 309
1 What is an object expression?
A- F# ability to implement C# interfaces without creating a type
2 How do you convert between a nullable and an option in F#?
A-  Option.ofNullable and Option.toNullable
*)


(* Try this - pg 309
TODO:
*)




