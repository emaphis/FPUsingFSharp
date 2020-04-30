
module Capstone3.FileRepository

open Capstone3.Domain
open System.IO
open System

let private accountsPath =
    let path = @"accounts"
    Directory.CreateDirectory path |> ignore
    path
let private findAccountFolder owner =    
    let folders = Directory.EnumerateDirectories(accountsPath, sprintf "%s_*" owner)
    if Seq.isEmpty folders then ""
    else
        let folder = Seq.head folders
        DirectoryInfo(folder).Name

let private buildPath(owner, accountId:Guid) = 
    let file = sprintf @"%s\%s_%O" accountsPath owner accountId
    file

/// Logs to the file system
let writeTransaction accountID owner (trans : Transaction) =
    let path = buildPath(owner, accountID)
    path |> Directory.CreateDirectory |> ignore
    let filePath = sprintf "%s/%d.txt" path (trans.Timestamp.ToFileTimeUtc())
    let line = sprintf "%O***%s***%M***%b" trans.Timestamp trans.Operation trans.Amount trans.Accepted
    File.WriteAllText(filePath, line)
 