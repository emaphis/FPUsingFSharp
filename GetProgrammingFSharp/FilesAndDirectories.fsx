//// File and Directory handling in F#

open System
open System.IO

let directory = Directory.GetCurrentDirectory()
// "C:\Users\xxx\AppData\Local\Temp"

let path = @"c:\bin"

// pg 88
let writeTextToDisk text =
    let path = Path.GetTempFileName()
    File.WriteAllText(path, text)
    path

let createManyFiles() =
    ignore(writeTextToDisk "The quick brown fox jumped over the lazy dog")
    ignore(writeTextToDisk "The quick brown fox jumped over the lazy dog")
    writeTextToDisk "The quick brown fox jumped over the lazy dog"

createManyFiles()


// pg 110 Try this
// function to load a filename and last-modified date from the filesystem, using a tuple as
// the return type.
let path1 = @"c:src\test.csv"

let loadFile path =
    let file = FileInfo(path)
    let lastModifiedDate = file.LastWriteTime
    let fileName = file.Name
    fileName, lastModifiedDate.Date

let fileName, modDate = loadFile path1

// pg 129 - Ch11 - Now you try

let writeToFile (date : System.DateTime) filename text =
    let name = date.ToString("yyMMdd") + "-" + filename
    File.WriteAllText(name, text)

let writeToday = writeToFile DateTime.UtcNow.Date
let writeTommorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.)

// pg131 - Ch11 
let time =
    Directory.GetCurrentDirectory()
    |> Directory.GetCreationTime


// pg 139 - ch12 
let path2 = @"c:\src\test.csv"
let lines2 = File.ReadLines path2
for elem in lines2 do
    printfn "fruit: %s" elem

let lines3 = File.ReadAllLines path2
for elem in lines3 do
    printfn "fruit: %s" elem


// pg201 - Ch17 Now you try

let rootDirs =
    let now = DateTime.UtcNow;
    Directory.EnumerateDirectories(@"c:\")
    |> Seq.map (fun path -> DirectoryInfo path)
    |> Seq.map (fun dir -> (dir.Name, dir.CreationTimeUtc))
    |> Map.ofSeq
    |> Map.map (fun key time -> (now - time).Days)

for dir in rootDirs do
    Console.WriteLine(dir)



let toDirWithCreationDate path =
    (path, Directory.GetCreationTime(path))

let date1 = toDirWithCreationDate(path)

// list all files in a directory, convert to list
let files1 = Directory.GetFiles(path) |> List.ofSeq

// write out file information
for path in files1 do
    let file = new FileInfo(path)
    printfn "%s\t\t%d\t%O"
        file.Name
        file.Length
        file.CreationTime

let readFile() =
    let lines = File.ReadAllLines(@"c:\src\test.csv")
    let printLine (line: string) =
        let items = line.Split(',')
        printfn "%O %O %O"
            items.[0]
            items.[1]
            items.[2]
    Seq.iter printLine lines

do readFile()
