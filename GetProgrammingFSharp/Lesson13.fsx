//// Lesson 13 - Achieving code reuse in F#  - pg 149

// Reusing code with lambdas

(* Quick check 13.1  - pg 153
1 Name one difference between nominal and structural types.
A- nominal type a typed by names, structural types are differentiated by components

2 How do we pass logic between or across code in the OO world?
A- You pass logic with interfaces and classed implementing that interface.

3 How do we pass logic between or across code in the FP world?
A- Lamba (unnamed) functions
*)

/// 13.2 Implementing higher-order functions in F#

/// 13.2.1 Basics of higher-order functions

/// Now you try - pg 154
/// Listing 13.5 - Your first higher-order function in F#
type Customer =
    { Age : int }

// function to factor
let whereCustomersAreOver35 customers =
    seq {
        for customer in customers do
            if customer.Age > 35 then  // hard coded filter
                yield customer }

let where filter customers =
    seq {
        for customer in customers do
            if filter customer then
                yield customer }

let isOver35 customer = customer.Age > 35

let customers = [ { Age = 21 }; { Age = 35}; { Age = 36} ]

customers |> where isOver35
customers |> where (fun customer -> customer.Age > 35)


(* Quick check 13.2  - pg 156
1 Can F# infer the types of higher-order functions?
A- Yes
2 How can you easily identify higher-order function arguments in VS?
A- 
*)


/// 13.3 Dependencies as functions

/// Now you try - pg 157
// See Lesson13Try.fsx


(* Quick check 13.3  - pg 169
What’s the key difference between passing dependencies in F# and C#?
A- F# pases fuctions of the right signature
   C# passed classes implementing interfaces.
*)


(* Try this - pg 159
Create a set of functions that use another dependency in .NET—for example, working
with HTTP data by using WebClient. Write a function that takes in the HTTP client to POST
data to a URI. What’s the dependency? The WebClient class, or a function on the WebClient?
*)

open System.Net

let downloader (client : WebClient) (uri : string) =
    let text = client.DownloadString(uri)
    text

let webClient = new WebClient()

let webDownloader = downloader webClient

let text = webDownloader "https://fsharp.org"
