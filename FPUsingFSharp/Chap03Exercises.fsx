//// Chapter 4 - Tuples, records and tagged values -  pg 43.

let add x y = x + y
let add3 = add 3

/// 3.2 Tuples  - pg 43.
/// ordered collection of n values (v1, v2, .... vn)

// examples
(10, true) 
(("abc", 1), -3)

// Tuple expressions

(1<2, "abc", 1, 1-4)    //  bool * string * int * int
(true, "abc")           // bool * string
((2<1, "abc"), 3-2, -3) // (bool * string) * int * int

// declarations
let tp1 = ((1<2, "abc"), 3-2, -3)  // val tp1 : (bool * string) * int * int
let tp2 = (2>1, "abc", 3-2, -3)

// Tuples are individual values
// tubple values may be predivined values
let t1 = (true, "abc")
let t2 = (t1, 1, -3)
// val t2 : (bool * string) * int * int = ((true, "abc"), 1, -3)

// Equality
("abc", 2, 4, 9) = ("ABC", 2, 4, 9) = false
(1, (2, true)) = (2-1, (2, 2>1)) = true

// type mismatch
// (1, (2,true)) = (1, 2, 2>1)

// Ordering - lexicogaphically
(1,"a") < (1, "ab")  = true
(2, "a") < (1, "ab") = false

// other operators
('a', ("b", true), 10.0) >= ('a', ("b", false), 0.0)  = true

compare ("abcd", (true, 1)) ("abcd", (false, 2))  = 1


// Tuple patterns
let (x,n) = (3,2)

let (x,0) = ((3,"a"), 0)
//  FS0025: Incomplete pattern matches on this expression. For example, the
//  value '(_,1)' may indicate a case not covered by the pattern(s).

// let (x,0) = (3,2)
// Microsoft.FSharp.Core.MatchFailureException: The match cases were incomplete
//    at <StartupCode$FSI_0032>.$FSI_0032.main@()

// wildcard pattern
let ((_,x),_,z) = ((1,true), (1,2,3), false)

// no multiple occurrences
//let (x, x) = (1, 1)
// Chap03Exercises.fsx(35,9): error FS0038: 'x' is bound twice in this pattern


/// 3.2 Polymorphism  - pg 48

let swap (x,y) = (y,x)
// val swap : x:'a * y:'b -> 'b * 'a

swap ('a', "ab")  = ("ab", 'a')
swap ((1,3), ("ab", true))

fst  // ('a * 'b -> 'a)
snd  // ('a * 'b -> 'b)


/// 3.3 Exanple: Generic vectors

let (~-.) (x:float,y:float)  = (-x,-y)
let (+.) (x1, y1) (x2,y2) = (x1+x2,y1+y2): float*float
let (-.) v1 v2 = v1 +. -. v2
let ( *.) x (x1,y1) = (x*x1, x*y1): float*float
let (&.) (x1,y1) (x2,y2) = x1*x2 + y1*y2: float
let norm(x1:float,y1:float) = sqrt(x1*x1+y1*y1)

let a = (1.0, -2.0)
let b = (3.0, 4.0)

let c = 2.0 *. a -. b
let d = c &. a
let e = norm b
