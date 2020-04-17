open System
open Car

let getDestination() =
    Console.Write("Enter destination: ")
    Console.ReadLine()

let mutable petrol = 100;

[<EntryPoint>]
let main argv =
    while true do
        try
            let destination = getDestination()
            printfn "Trying to drive to %d" destination
            petrol <- driveTo(petrol, destination)
            printfn "Made it to %s! You have %d petrol left" destination petrol
        with ex -> printfn "Error: %s" ex.Message
    0
