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

//let (x,0) = ((3,"a"), 0)
//  FS0025: Incomplete pattern matches on this expression. For example, the
//  value '(_,1)' may indicate a case not covered by the pattern(s).

// let (x,0) = (3,2)
// Microsoft.FSharp.Core.MatchFailureException: The match cases were incomplete
//    at <StartupCode$FSI_0032>.$FSI_0032.main@()

// wildcard pattern
let ((_,x1),_,z1) = ((1,true), (1,2,3), false)

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


/// 3.4 Records - pg 50
// a generalized tuple where each component is identified by a label

type Person =
    { age : int
      birthday : int * int
      name : string
      sex : string }

let john =
    { name = "John"
      age = 29
      sex = "M"
      birthday = (2,11)  }

john.birthday  = (2,11)
john.sex = "M"


// Equality and ordering

// equality is defined compoent wise
john = {age = 29; name = "John";
        sex = "M"; birthday = (2,11)}
// true

// order is based on lexicograpical orders using the ordering of the labels.
type T1 = { a : int; b : string }
let v1 = { a=1; b="abc" }
let v2 = { a=2; b="ab" }

v1 < v2

type T2 = { b : string; a : int }
let v1' = { a=1; b="abc"}
let v2' = { a=2; b="ab" }

v1' > v2'  // lebel 'b' appears first.


// Record patterns.

let sue = { age = 19; name = "Sue";  sex="F"; birthday= (24, 12) }

let { name = x3; age = y3; sex = s3; birthday =(d3,m3) }  = sue

let age { age=a; name=_; sex=_; birthday=_} = a

age sue = 19
age john = 29

let isYoungLady { age=a; sex=s; name=_; birthday=_ } =
                                a < 25 && s = "F"

isYoungLady sue
isYoungLady john


/// 3.5 Example: Quadratic equations  - pg 52

type Equation = float * float * float
type Solution = float * float
// solve : Equation -> Solution

// Error handling
exception Solve

let solve1 (a, b, c) =
    if b*b-4.0*a*c < 0.0 || a = 0.0 then raise Solve
    else ((-b + sqrt (b*b-4.0*a*c)) / (2.0*a),
          (-b - sqrt (b*b-4.0*a*c)) / (2.0*a))

solve1(1.0, 0.0, 1.0)
// FSI_0019+Solve: Exception of type 'FSI_0019+Solve' was thrown

solve1(1.0, 1.0, -2.0) = (1.0, -2.0)
solve1(2.0, 8.0, 8.0) = (-2.0, -2.0)

// using failwith string
let solve2 (a, b, c) =
    if b*b-4.0*a*c < 0.0 || a = 0.0
    then failwith "discriminant is negative of a=0.0"
    else ((-b + sqrt (b*b-4.0*a*c)) / (2.0*a),
          (-b - sqrt (b*b-4.0*a*c)) / (2.0*a))

solve2 (0.0, 1.0, 2.0)
// System.Exception: discriminant is negative of a=0.0


/// 3.6 Locally declared identifiers.

let solve3 (a, b, c) =
    let disc = b*b-4.0*a*c 
    if disc < 0.0 || a = 0.0
    then failwith "discriminant is negative of a=0.0"
    else ((-b + sqrt disc) / (2.0*a),
          (-b - sqrt disc) / (2.0*a))


let solve4 (a, b, c) =
    let sqrtD =
        let disc = b*b-4.0*a*c
        if disc < 0.0 || a = 0.0
        then failwith "discriminant is negative of a=0.0"
        else sqrt disc
    ((-b + sqrtD / (2.0*a), (-b - sqrtD) / (2.0*a)))


let solve5 (a, b, c) =
    let disc = b*b-4.0*a*c
    if disc < 0.0 || a = 0.0
    then failwith "discriminant is negative of a=0.0"
    else let sqrtD = sqrt disc
         ((-b + sqrtD / (2.0*a), (-b - sqrtD) / (2.0*a)))


/// 3.7 Example: Rational numbers. Invariants
// q = a/b, where a and b are integers and b != 0.

// Representation, Invariant.
// (a,b) where b>0 and the fraction a/b is irreducible.

type Qnum = int * int   // (a,b) where b>0 and gcd(a,b) = 1

let rec gcd = function
    | (0,n) -> n
    | (m,n) -> gcd(n % m, m)

// Operators
/// cancels common divisors and thereby reduces any fraction with non-zero denominator to the normal form
let canc(p,q) =
    let sign = if p*q < 0 then -1 else 1
    let ap = abs p
    let aq = abs q
    let d = gcd(ap, aq)
    (sign * (ap / d), aq / d)

/// check for division by zero
let mkQ = function
    | (_,0) -> failwith "Division by zero"
    | pr  -> canc pr

let (.+.) (a,b) (c,d) = canc(a*d + b*c, b*d)   // Additon
let (.-.) (a,b) (c,d) = canc(a*d - b*c, b*d)   // Subtraction
let (.*.) (a,b) (c,d) = canc(a*d * b*c, b*d)   // Multiplication
let (./.) (a,b) (c,d) = (a,b) .*. mkQ(d,c)     // Division
let (.=.) (a,b) (c,d) = (a,b) = (c,d)          // Equality

let toString (p:int, q:int) = (string p) + "/" + (string q)

// examples
let q1 = mkQ(2,-3)
let q2 = mkQ(5,10)
let q3 = q1 .+. q2
toString (q1 .-. q3 ./. q2)


/// 3.8 Tagged values. Constructors  - pg 58

type Shape = | Circle of float  // value constructors
             | Square of float
             | Triangle of float * float * float


let cr1 = Circle 1.2
let cr2 = Circle (8.0 - 2.0*3.4)

// Equality and ordering
Circle 1.2 = Circle(1.0 + 0.2)  = true

Circle 1.2 = Square 1.2  = false

Circle 1.2 < Circle 1.0  = false

Circle 1.2 < Square 1.2  = true    // hmmm...
Triangle(1.0, 1.0, 1.0) > Square 4.0  = true


// Constructors in patterns

let area = function
    | Circle r         -> System.Math.PI * r * r
    | Square a         -> a * a
    | Triangle (a,b,c) ->
            let S = (a + b + c) / 2.0
            sqrt(S * (S-a) * (S-b) * (S-c))


/// A constructor mathces iself only in a pattern match
/// while other identifiers match any value.

area (Circle 1.2)
area (Triangle(3.0,4.0,5.0))

// Invariant for the representation of shapes

/// Shape invariants
let isShape = function
    | Circle r        -> r > 0.0
    | Square a        -> a > 0.0
    | Triangle(a,b,c) ->
        a > 0.0 && b > 0.0 && c > 0.0
        && a < b + c && b < c + a && c < a + b


let area2 x = if not (isShape x)
              then failwith "not a leagal shape"
              else match x with
                    | Circle r         -> System.Math.PI * r * r
                    | Square a         -> a * a
                    | Triangle (a,b,c) ->
                            let S = (a + b + c) / 2.0
                            sqrt(S * (S-a) * (S-b) * (S-c))

area2 (Triangle(3.0,4.0,5.0))
area2 (Triangle(3.0,4.0,7.5))


/// 3.9 Enumeration types  - pg 62

// type Colour = Red | Blue | Green | Yellow | Purple
type Colour =
    | Red
    | Blue   // Value constructors need not have any argument
    | Green
    | Yellow
    | Purple

Green
Circle 1.2

// Functions declared on enumeration types may be declared by pattern matching
let niceColour = function
    | Red   -> true
    | Blue  -> true
    | _     -> false

niceColour Purple = false

// update example from pg. 4
type Month = January | February | March | April
             | May | June | July | August | September
             | October | November | December

let daysOfMonth = function
    | February -> 28
    | April | June | September | November -> 30
    | _ -> 31



/// 3.10 Exceptions - pg 63.
//  try e with match

let solveText eq =
    try
        string(solve1 eq)
    with
    | Solve -> "No solutions"

solveText (1.0, 1.0, -2.0)
// val it : string = "(0.5, -2)"
solveText (1.0, 0.0, 1.0)
// val it : string = "No solutions"

try
    toString(mkQ(2,0))
with
| Failure s -> s
// val it : string = "Division by zero"


/// 3.11 Partial functions, The option type

type 'a myOption = MyNone | MySome of 'a

Some false
Some (1, "a")
None

// remove some
let what1 = Option.get(Some (1, "a"))
let what2 = Option.get(Some 1)

// let what3 = Option.get(None) + 1
// System.ArgumentException: The option value was None (Parameter 'option')

let rec fact = function
    | 0 -> 1
    | n -> n * fact (n-1)


let optFact' n =
    if n < 0 then None else Some(fact n)

optFact' 5
optFact' -2

// using Option.get
let rec optFact = function
    | 0  -> Some 1
    | n when n > 0 -> Some (n * Option.get(optFact (n-1)))
    | _  -> None
