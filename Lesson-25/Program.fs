//// Lesson 25 - Consuming C# from F# - pg 299

open System

/// Listing 25.2 Consuming C# code from F# - pg 301
[<EntryPoint>]
let main argv =
    let tony = CSharpProject.Person "Tony"
    tony.PrintName()
    0
