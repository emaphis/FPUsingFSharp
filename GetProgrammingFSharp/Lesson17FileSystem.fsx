 //// Lesson 17 - Try this - pg 205
(* See Lesson 16 pg 193
Continuing from the previous lesson, create a lookup for all files within a folder so that
you can find the details of any file that has been read. Experiment with sets by identifying
file types in folders. What file types are shared between two arbitrary folders?
*)

open System.IO

let path = @"C:\Testing\"

/// Represents a directory and a list of files that belong to that directory
/// - created by the groupBy function
type DirInfo = string * FileInfo list

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
let getDirFileList (path : string) : DirInfo list =
    getSubDirectories path          // get subdirectories of dir
    |> List.collect getFileList     // get list of files in directories
    |> List.groupBy (fun file -> file.DirectoryName)


/// DirInfo functions

let getDirName (dirInfo : DirInfo) =
    let path, _ = dirInfo
    path

let getFileSizes (subdir : DirInfo) =
    let path, files = subdir
    files |> List.sumBy (fun file -> int(file.Length))

let getNumberFiles (dirInfo : DirInfo) =
    let _, files = dirInfo
    files.Length

let getAverageFileSize (dirInfo : DirInfo) =
    let sizeFiles = float(getFileSizes dirInfo)
    let numFiles = float(getNumberFiles dirInfo)
    sizeFiles / numFiles


// NOTE: Lession 17 - Change to Set instead of distinct
let getFileExtensions (dirInfo : DirInfo) : Set<string> =
    let _, files = dirInfo
    files |> List.map (fun file -> file.Extension)
          |> Set.ofList


//////////////////////////////////////////////////////////////

/// get a sorted list of directories sorted by total size of iles
let getDirSizes dirPath =
  let subdirs = getDirFileList dirPath
  subdirs |> List.map (fun dirInfo -> (fst dirInfo, dirInfo |> getFileSizes))
          |> List.sortByDescending (fun (dir, size) -> size)

//getDirSizes path


///////////////////////////////////////////////////////////
type DirRecord =
    { Name: string
      Size: int
      NumFiles: int
      AvgSize: float
      Extensions: Set<string> }   // NOTE: Lession 17 - Change to Set instead of distinct

let createDirRecord (dirInfo : DirInfo) =
    { Name = getDirName dirInfo
      Size = getFileSizes dirInfo
      NumFiles = getNumberFiles dirInfo
      AvgSize = getAverageFileSize dirInfo
      Extensions = getFileExtensions dirInfo }

let getDirInfoList dirPath =
    let subdirs = getDirFileList dirPath
    subdirs |> List.map (fun dirInfo -> createDirRecord dirInfo)
            |> List.sortByDescending (fun dir -> dir.Size)

//getDirInfoList path


///////////////////////////////////////
/// map and set functionality

// Maps.

/// A Map from directory names to FileInfo lists\
type DirInfoMap = Map<string, FileInfo list>

/// Create a Directory Map given a path
let getDirFileMap path : DirInfoMap =
    getDirFileList path
    |> Map.ofList

// Example directory map
let dirMap = getDirFileMap path

// Example lookup function using map functioality
let findFileNames (dirMap : DirInfoMap) path =
    let fileList = (dirMap.TryFind path).Value 
    fileList |> List.map (fun fileInfo -> fileInfo.Name)

findFileNames dirMap "C:\Testing\subdir3"


// Sets.
type DirRecMap = Map<string, DirRecord>

/// get a map of files on a given path
let getDirInfoMap dirPath =
    let subdirs = getDirFileList dirPath
    subdirs |> List.map (fun dirInfo -> (fst dirInfo, createDirRecord dirInfo))
            |> Map.ofList

let dirRecMap = getDirInfoMap path

let findFileExtensions (dirMap : DirRecMap) path =
    let dirRecord =  dirMap.TryGetValue path |> snd
    dirRecord.Extensions

let extenstions1 = findFileExtensions dirRecMap "C:\Testing\subdir1"  // set [".bat"; ".md"; ".txt"]
let extenstions2 = findFileExtensions dirRecMap "C:\Testing\subdir2"  // set [".md"; ".txt"]
let extenstions3 = findFileExtensions dirRecMap "C:\Testing\subdir3"  // set [".bat"; ".md"; ".txt"]

// extensions in common
extenstions1 |>Set.intersect extenstions2
extenstions1 |>Set.intersect extenstions3
extenstions2 |>Set.intersect extenstions3
