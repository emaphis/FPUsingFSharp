//// Leson 21 - Modeling Relationsips in F#  - pg 244

/// 21.1 Composition in F#

/// Listing 21.1 Composition with records in F# - pg 245
type Disk' = { SizeGb : int }
type Computer =
    { Manufacturer : string
      Disks' : Disk' list }

let MyPc =
    { Manufacturer = "Computers Inc."
      Disks' =
        [ { SizeGb = 100 }
          { SizeGb = 250 }
          { SizeGb = 500 } ] }


/// 21.1.1 Modeling a type hierarchy

/// 21.2 Discriminated unions in F#
// sum types, case classes, algerbraic types

///Listing 21.2 - Discriminated unions in F#   - pg 246
type Disk =
| HardDisk of RPM:int * Platters:int
| SolidState
| MMC of NumberOfPins:int
;;


/// 21.2.1 Creating instances of DUs
/// Now you try  - pg 247
let aHardDisk = HardDisk(RPM = 250, Platters = 7)
let anMMC = MMC (NumberOfPins = 5)
let anSSD = SolidState

let args = 250,7
let myHardDisk = HardDisk args
let myMMC = MMC 5
let myHardDiskShort = HardDisk(250, 7)


/// Listing 21.5 - Pattern matching on values  - pg 249
//let findDisk disk =
//    match disk with
//    | HardDisk(5400, 5) -> "Seeking very slowly!"
//    | HardDisk(rpm, 7) -> sprintf "I have 7 spindles and RPM %d!" rpm
//    | MMC 3 -> "Seeking. I have 3 pins!"


(* Now you try - pg 249
Let’s now see how to write a function that performs pattern matching over a discriminated
 union:
1 Create a function, describe, that takes in a hard disk.
2 The function should return texts as follows:
  a If an SSD, say, “I’m a newfangled SSD.”
  b If an MMC with one pin, say, “I have only 1 pin.”
  c If an MMC with fewer than five pins, say, “I’m an MMC with a few pins.”
  d Otherwise, if an MMC, say, “I’m an MMC with <pin> pins.”
  e If a hard disk with 5400 RPM, say, “I’m a slow hard disk.”
  f If the hard disk has seven spindles, say, “I have 7 spindles!”
  g For any other hard disk, state, “I’m a hard disk.”
*)
// 1.
let describe disk =
    match disk with
    | SolidState  -> "I'm a newfagled SSD"  // 2
    | MMC(NumberOfPins = 1) -> "I have only one pin"
    | MMC(NumberOfPins = pins) when pins < 5 -> "I'm an MMC with few pins"
    | MMC(NumberOfPins = pins) -> sprintf "I'm an MMC with %d pins" pins
    | HardDisk(5400, _) -> "I'm a slow hard disk"
    | HardDisk(_, 7) -> "I have 7 spindles!"
    | HardDisk(_,_) -> "I'm a hard disk."

let d1 = describe aHardDisk
let d2 = describe anMMC
let d3 = describe anSSD
let d4 = describe myHardDisk
let d5 = describe myHardDiskShort
let d6 = describe myMMC


(* Quick check 21.1  - pg 250
1 What is the OO equivalent of discriminated unions?
A- Inheritance with final classes, Polymorphism

2 Which language feature in F# do you use to test which case of a DU a value is?
A- Pattern matching

3 Can you add new cases to a DU later in your code?
A- No. Only in stucture that declares the DU.
*)

/// 21.3 Tips for working with discriminated unions

/// 21.3.1 Nested DUs
/// Listing 21.6 - Nested discriminated unions pg 251
type MMCDisk =
| RsMMc
| MmcPlus
| SecureMMC

type Disk1 =
| MMC of MMCDisk * NumberOfPins:int

let seek disk =
    match disk with
    | MMC(MmcPlus, 3)-> "Seeking quietly but slowly"
    | MMC(MmcPlus, n) ->  sprintf "Seekig slowly with %i pins" n
    | MMC(SecureMMC, 6) -> "Seeking quietly with 6 pins"
    | MMC(SecureMMC, n) -> sprintf "Seeking quietly with %i pins" n
    | MMC(RsMMc, 4) -> "Seeking with 4 pins"
    | MMC(RsMMc, n) -> sprintf "Seeking with %i pins" n


/// 21.3.2 Shared fields
/// Listing 21.7 Shared fields using a combination of records and discriminated
/// unions  pg - 252
type DiskInfo =
    { Manufacturer : string  // shared fields
      SizeGb : int
      DiskData : Disk }  // varying field

type Computer1 =
    { Manufacturer : string
      Disks : DiskInfo list }

let myPc =
    { Manufacturer = "Computers Inc."
      Disks =
            [ { Manufacturer = "HardDisks Inc."
                SizeGb = 100
                DiskData = HardDisk(5400, 7) }
              { Manufacturer = "SuperDisks Corp."
                SizeGb = 250
                DiskData = SolidState } ] }

/// 21.3.3 Printing out DUs
printf "%A" myPc

(* Quick check 21.2  - pg 253
1 How do you model shared fields in a discriminated union?
A- By including the shared fields and the DU in a record.
2 Can you create one discriminated union with another one?
A-  Yes. By nesting.
*)

/// 21.4 More about discriminated unions

/// 21.4.1 Comparing OO hierarchies and discriminated unions

/// 21.4.2 Creating enums
/// Listing 21.8 - Creating an enum in F#  - pg 255

type Printer =
    | Inkjet = 0
    | Laserjet = 1
    | DotMatrix = 2

let printerName (printer: Printer) =
    match printer with
    | Printer.Inkjet  -> "Inkjet"
    | Printer.Laserjet  -> "Laserjet"
    | Printer.DotMatrix -> "Dot Matrix"
    | _   -> "Who knows?"

let printer1 = Printer.Laserjet
let name1 = printerName printer1


(* Quick check 21.3  - pg 255
1 When should you not use a discriminated union?
A- Rapidly changing models with many cases. Large hierarchies
2 Why do you need to always place a wildcard handle for enums?

*)

(* Try this  - pg 272
Take any example domain model you’ve recently written in OO and try to model it
using a combination of discriminated unions, tuples, and records. Alternatively, try to
update the rules engine you looked at earlier in the book, so that instead of returning a
tuple of the rule name and the error, it returns a Pass or Fail discriminated union, with
the failure case containing the error message.
*)

// from Lesson 18 pg 227

open System

/// Parsing rule
type Return =
| Pass
| Fail of  message:string

type Rule = string -> Return

/// Rules

/// Text must be three words
let threeWords(text: string)  =
    match (text.Split ' ').Length = 3 with
    | true -> Pass
    | _    -> Fail("Must be three words")

/// Text must be less than or equal to 30 characters
let maxLength30 (text: string) =
    match text.Length <= 30 with
    | true -> Pass
    | _    -> Fail("Max length is 30 characters")

/// Text must be all uppercase
let allUppercase (text: string) =
    let ret = text |> Seq.filter Char.IsLetter
                   |> Seq.forall Char.IsUpper
    match ret with
    | true -> Pass
    | _    -> Fail("All letters must be caps" )

/// Text must not have digits
let notAnyDigits (text: string) =
    let ret = text |> Seq.forall (fun char -> not (Char.IsDigit char))
    match ret with
    | true -> Pass
    | _    -> Fail("Must have no digits")


/// Create composite rule
let buildValidator (rules : Rule list) =
    rules
    |> List.reduce(fun firstRule secondRule ->
        fun word ->          // higher-order function
            match firstRule word with
                | Pass ->
                    match secondRule word with
                    | Pass -> Pass
                    | Fail(str) -> Fail(str)
                | Fail(str) -> Fail(str))

let rules : Rule list =
    [ threeWords; maxLength30; allUppercase; notAnyDigits ]

let validate rules text =
    match (buildValidator rules) text with
    | Pass      -> printfn "Text passed"
    | Fail(str) -> printfn "Text failed: %s" str

/// examples

// should pass
validate rules "HELLO FROM F#"

// fail capital letters
validate rules "HELLO FrOM F#"

// fail - max length
validate rules "HELLOFROMF# ITSA FUNCTIONALLANGUAGE"

// fail - three words
validate rules "ONE TWO THREE FOR"

// fail - digts
validate rules "HELLO FROM F8"
