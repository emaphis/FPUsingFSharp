module Capstone2.Auditing

open Capstone2.Domain
open System.IO
    
/// Log to the console
let consoleLog account message =
    printfn "Account:  %O: %s" account.AccountID message

/// Log to file system
let filesystemLog account message =
    let filePath = sprintf @"c:\Testing\%s-%O.txt" account.Owner.Name account.AccountID
    File.AppendAllLines(filePath, [ message ])
