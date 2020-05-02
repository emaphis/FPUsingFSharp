 //// Lesson 18 File System - Try this - pg. 218

(*
See Lesson 17 pg 205
Create a simple rules engine over the filesystem example from the previous lesson. The
engine should filter out files that don’t pass certain checks, such as being over a specific
file size, having a certain extension, or being created before a specific date. Have you
ever created any rules engines before? Try rewriting them in the style we defined in this
lesson
*)

open System
open System.IO

let path = @"C:\Testing\"

/// Represents a directory and a list of files that belong to that directory
/// - created by the groupBy function
type DirInfo = string * FileInfo list
//type DirInfo = { dir : string; files : FileInfo list }

/// get list names of subdirectories of passed dir
let getSubDirectories path =
    Directory.GetDirectories path
    |> Array.toList

/// given a dir get a list of fileInfo in directory
let getFileList dir =
    Directory.GetFiles(dir)
    |> Array.map (fun file -> FileInfo(file))
    |> Array.toList

/// get a list of files on a given path
/// list of files should be grouped by dir
let getDirFileList path : DirInfo list =
    getSubDirectories path          // get subdirectories of dir
    |> List.collect getFileList     // get list of files in directories
    |> List.groupBy (fun file -> file.DirectoryName)


/////////////////////////////////////////////////
/// A file system rule enging

/// Rule type
type FileRule = FileInfo -> bool * string

/// Folding rule functions together
let buildValidator rules : FileRule =
    rules
    |> List.reduce(fun firstRule secondRule ->
        fun word ->                             // higher-order function
            let passed, error = firstRule word  // run first rule
            if passed then                      // passed, move to next rule
                let passed, error = secondRule word
                if passed then true, "" else false, error
            else false, error)                  // failed return error

// example rules:
// * being over a specific file size
// * having a certain extension
// * being created before a specific date

let fileSize (size, message) : FileRule =
    fun file  -> file.Length  > size, message

let fileExt (extension, message) : FileRule =
    fun file -> file.Extension = extension, message

let fileDate (date, messege) : FileRule =
    let date1 = DateTime.Parse(date)
    fun file -> file.CreationTime = date1, messege

let ruleList =
    [ fileSize(3L, "File must be larger than 3")
      fileExt(".txt", "Files must be a text file")
      fileDate("4/27/2020 3:25:35 PM", "File must be created after ---") ]

let validator = buildValidator ruleList;;

let validateDir path =
    getDirFileList path
    |> List.collect (fun (dir, lst)  -> lst)
    |> List.map (fun file -> validator file)

let listoffiles = validateDir path
