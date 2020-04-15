module Chap02Exercises


/// 2.1 Declare a function f: int -> bool such that f(n) = true exactly when n is divisible by 2
//      or divisible by 3 but not divisible by 5. Write down the expected values of f(24), f(27), f(29)
//      and f(30) and compare with the result. Hint: n is divisible by q when n%q = 0.

let f  = function
    | n when n % 5 = 0  -> false   // must be first
    | n when n % 2 = 0  -> true
    | n when n % 3 = 0  -> true
    | _                 -> false

let f1 = f(24) = true
let f2 = f(27) = true
let f3 = f(29) = false
let f4 = f(30) = false


/// 2.2  Declare an F# function pow: string * int -> string, where:
//           pow(x,n) = s . s . ...... . s
//           where number of concatenations are == n

let rec pow (s, n) =
    match (s, n) with
    | (s, 0) -> ""
    | (s, n) -> s + pow (s, n-1)

let pw0 = pow ("a", 0) = ""
let pw1 = pow ("a", 1) = "a"
let pw2 = pow ("a", 3) = "aaa"
let pw3 = pow ("ab", 3) = "ababab"


// 2.3 Declare the F# function isIthChar: string * int * char -> bool where the value of
//     isIthChar(str, i, ch) is true if and only if ch is the i’th character in the string str (numbering
//     starting at zero).

let isIthChar (str : string, i, ch)  = str.[i] = ch
    

// 2.4 Declare the F# function occFromIth: string * int * char -> int where
//          occFromIth(str, i, ch) = the number of occurrences of character ch
//                                   in positions j in the string str with j ≥ i.
//     Hint: the value should be 0 for i ≥ size str.

let rec occFromIth (str : string, i, ch) =
    if i >= str.Length then 0
    else (if str.[i] = ch then 1 else 0 ) + occFromIth(str, i+1, ch)
//
let occ1 = occFromIth("abcdecc", 4, 'c') = 2


// 2.5 Declare the F# function occInString: string * char -> int where
//           occInString(str, ch) = the number of occurrences of character ch
//                                  in the string str.

let occInString (str, ch) = occFromIth (str, 0, ch)
 
