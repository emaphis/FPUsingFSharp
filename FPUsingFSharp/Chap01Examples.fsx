module Chap01Examples

/// values, expressions, types and declarations, including recursive function declarations

/// 1.1 Values, types, identifiers and declarations
let price = 125

/// 1.2 Simple function declarations

(* Area of circle with radious of `r` *)
// circleArea(r) = πr2.
let circleArea r = System.Math.PI * r * r


/// 1.3 Anonymous functions. Function expressions

let circleArea1 = fun r -> System.Math.PI * r * r


// Function expressions with patterns

let month1 =
    function
    | 2 -> 28  // February
    | 4 -> 30  // April
    | 6 -> 30  // June
    | 9 -> 30  // September
    | 11 -> 30 // November
    | _ -> 31  // All other months


let daysOfMonth = function
    | 2 -> 28 // February
    | 4|6|9|11 -> 30 // April, June, September, November
    | _ -> 31 // All other months


/// 1.4 RecursionS

// 0! = 1                       (Clause 1)
// n! = n · (n − 1)! for n > 1  (Clause 1)

let rec fact = function
    | 0 -> 1
    | n -> n * fact (n-1)

fact 10 |> ignore


/// 1.5 Pairs

let a = (2.0,3)
// pattern
let (x,y) = a

// pow -> pair(x,n)
let rec power = function
    | (x,0) -> 1.0    
    | (x,n) -> x * power(x, n-1)


/// 1.6 Types and type checking

// if 'f' : T1 -> T2 and e : T1
// then f(e) : T2


/// 1.7 Bindings and environments

let a1 = 3
let b1 = 8.0
// creates th envionment
// evn2  a1 -> 3
//       b1 -> 7.0

// extension to envirionment
let c1 = (2, 8)
let circleArea2 r = System.Math.PI * r * r


// 1.8 Euclid's algorithm

let rec gcd = function
    | (0,n) -> n
    | (m,n) -> gcd(n % m, m)


// 1.10 Free-standing programs
// see Programs.fs
