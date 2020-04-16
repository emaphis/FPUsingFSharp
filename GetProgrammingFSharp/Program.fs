// A basic first program.

open System

let items = [| "item"; "item"; "item"; "item" |]

[<EntryPoint>]
let main argv =
    let items = argv.Length
    printfn "Passed in %d, Items: %A" items argv
    0 // return an integer exit code
