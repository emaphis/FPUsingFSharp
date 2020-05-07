module Chap02Examples

/// Values, operators, expressions and functions

/// 2.1 Numbers. Truth values. The unit type
// ints
let int1 = 0
let int2 = 1001
let int3 = -38890

// floats
let flt1 = 0.0
let flt2 = -7.235
let flt3 = 1.23e-17

// operators

let op1 = 2 - - -1

let op2 = 13 / -5
let op3 = 13 % -5

// Truth values

let bl1 = true
let bl2 = false

let bl3 = not true = false
let bl4 = true <> false

// The unit type

let unt = ()


/// 2.2 Operator precedence and association

//let pred1 = (- 2 - 5 * 7 > 3 - 1) = ((- 2) - (5 * 7)) > (3 - 1)


/// 2.3 Characters and strings

let chr1 = 'a'
let chr2 = ' '
let chr3 = '\\'

let str1 = "abcd----"
let str2 = "\"1234\""
let str3 = ""

// verbatim strings
let str4 = @"\\\\"
let str5 = "\\\\"

// functions on strings

let ans1 = String.length "1234"
let ans2 = String.length "\"1234\""
let ans3 = String.length ""

let text = "abcd---"

let ans4 = text + text  // concatenation
let ans5 = text + " "
let ans6 = text + " " + text

// indexing
let ans7 = "abc".[0]
let ans8 = "abc".[2]

// using 'string' to concatenate chars
let ans9 = string 'd'
let ans10 = "abc" + string 'd'
let ans11 = string -4
let ans12 = string 7.89
let ans13 = string true

let nameAge(name, age) =
    name + " is " + (string age) + " years old"

let ans14 = nameAge("Diane", 15+4)
let ans15 = nameAge("Philip", 1-4)

let ans16 = string (12, 'a')
let ans17 = string nameAge


/// 2.4 If-then-else expressions

// if exp1 then exp2 else exp3

let even n = n % 2 = 0

let adjString s = if even(String.length s)
                  then s else " " + s

let ans18 = adjString "123"
let ans19 = adjString "1234"

let rec gcd(m, n) =
    if m = 0 then n
    else gcd(n % m, m)


/// 2.5 Overloaded functions and operators

//let square (x: int) = x * x
//let square (x: float) = x * x

//let ovl1 = square 10
//let ovl2 = square 10.0

let ovl3 = abs -1
let ovl4 = abs -1.0
let ovl5 = abs -3.2f


// 2.6 Type inference

// float * int -> float
let rec power = function
    | (x, 0) -> 1.0
    | (x, n) -> x * power(x, n-1)


/// 2.7 Functions are first-class citizens

// The value of a function can be a function

let fn1 = (+) // int -> int -> int
// or associates right - int -> (int -> int)

let plusThree = (+) 3

let res1 = plusThree 5 
let res2 = ((+) 3) 5
let res3 = (+) 3 5

// The argument of a function can be a function

// concatenation
let f = fun y -> y+3
let g = fun x -> x*x

// h = (f . g)
let h = f << g
let res4 = h 4
// or
let res5 = ((fun y -> y+3) << (fun x -> x*x)) 4

// Declaration of higher-order functions

let weight ro = fun s -> ro * s ** 3.0

let waterWeight  = weight 1000.0

let res6 = waterWeight 1.0
let res7 = waterWeight 2.0

let methanolWeight = weight 786.5

let res8 = methanolWeight 1.0
let res9 = methanolWeight 2.0


/// 2.8 Closures

// (x, exp, env)

// environment
let pi = System.Math.PI
let circleArea r = pi * r * r

// env:
//  pi        -> 3.14...
// circleArea -> (r, pi*r*r, [pi -> 3.14...])

// circleArea closes over pi so a new definition of pi has no effect
//let pi = 0

let cl1 = circleArea 1.0


/// 2.9 Declaring prefix and infix operators

// infix:  ! % & * + - . / < = > ? @ ˆ | ˜
// prefix; + - +. -. & && % %%
//         ˜ ˜˜ ˜˜˜ ˜˜˜˜ (tilde characters

// .||.
let (.||.) p q = (p || q) && not (p && q)

let opr1 = (1 > 2) .||. (2 + 3 < 5)

// prefix operator is declared using a tilde aperator
let (~%%) x = 1.0 / x

let opr2 = %% 0.5


/// 2.10 Equality and ordering
// = <>

let eq1 = 3.5 = 2e-3

let eq2 = "abc" <> "ab"


let eqTxt x y =
    if x = y then "equal" else "not equal"
// when 'a : equality

let ord1 = eqTxt 3 4
let ord2 = eqTxt ' '  (char 32) 

// Ordering
// > >= < <=

let ordText' x y = if x > y then "greater"
                   else if x = y then "equal"
                   else "less"
// when 'a : comparison

// using matching with guards
let ordText x y = match compare x y with
                  | t when t > 0  -> "greater"
                  | 0             -> "equal" 
                  | _             -> "less"


/// 2.11 Function application operators |> and <|

// arg |> fct means fct arg
// fct <| arg means fct arg
