// Learn more about F# at http://fsharp.org

open System

// // 1.10 Free-standing programs

[<EntryPoint>]
let main argv =
    printfn "Hello, %s\n" argv.[0]
    0 // return an integer exit code
