module File1

open System

/// Chaper 1 exercises.


// 1.1 Declare a function g: int -> int, where g(n) = n + 4.

let g n = n + 4

let g_Test0 = g 0 = 4
let g_Test1 = g 4 = 8


// 1.2 Declare a function h: float * float -> float, where h(x, y) =
//     (x2 + y2). Hint: Use the function System.Math.Sqrt.

let h (x, y) = Math.Sqrt (x*x + y*y)

let h_Exmp0 = h(2., 2.)  // 2.828427125


// 1.3 Write function expressions corresponding to the functions g and h in
// the exercises 1.1 and 1.2.

let g' = function n -> n + 4
let h' = function (x, y) -> Math.Sqrt (x**2. + y**2.)

// 1.4 Declare a recursive function f: int -> int, where
//         f(n) = 1+2+· · · + (n − 1) + n
//     for n ≥ 0. (Hint: use two clauses with 0 and n as patterns.)
//     State the recursion formula corresponding to the declaration.
//     Give an evaluation for f(4).

let rec f  = function
    | 0 -> 0
    | x -> x + f (x-1) 

let f_Test1 = f 4

(*

f(0) = 0
f(n) = n + f(n - 1) when n > 0

f 4 -> f (4 - 1) + 4
    -> f 3 + 4
    -> f (3 - 1) + 3 + 4
    -> f 2 + 3 + 4
    -> f (2 - 1) + 2 + 3 + 4
    -> f 1 + 2 + 3 + 4
    -> f (1 - 1) + 1 + 2 + 3 + 4
    -> f 0 + 1 + 2 + 3 + 4
    -> 0 + 1 + 2 + 3 + 4
    -> 10
*)


// 1.5 The sequence F0, F1, F2, . . . of Fibonacci numbers is defined by:
//         F0 = 0
//         F1 = 1
//         Fn = Fn−1 +Fn−2
//     Thus, the first members of the sequence are 0, 1, 1, 2, 3, 5, 8, 13, . . ..
//     Declare an F# function to compute Fn. Use a declaration with three clauses, where the patterns
//     correspond to the three cases of the above definition.
//     Give an evaluations for F4.

let fib = function
    | 0 -> 0
    | 1 -> 1
    | n -> f(n-1) + f(n-2)

// 0, 1, 1, 2, 3, 5, 8, 13,
let fib_0 = fib 0 = 0
let fib_1 = fib 1 = 1
let fib_2 = fib 2 = 1


// 1.6 Declare a recursive function sum: int * int -> int, where
//         sum(m, n) = m + (m + 1) + (m + 2) + · · · + (m + (n − 1)) + (m + n)
//     for m ≥ 0 and n ≥ 0. (Hint: use two clauses with (m,0) and (m,n) as patterns.)
//      Give the recursion formula corresponding to the declaration.

let rec sum = function
    | (m, 0) -> m
    | (m, n) -> (m+n) + sum(m, n-1)

// sum(m, n) = (m + 0) + (m + 1) + (m + 2) + · · · + (m + (n − 1)) + (m + n)

// sum(3,0) = (3+0) -> 3
let sumTest1 = sum(3,0) = 3

// sum(3, 2) = (3 + 0) + (3 + 1) + (3 + 2)
//              3 + 4 + 5 -> 12
let sumTest2 = sum(3,2) = 12


// 1.7 Determine a type for each of the expressions:

// Given
let rec fact = function // int -> int
| 0 -> 1
| n -> n * fact(n-1)

let rec power = function  // float* int -> float
| (m, 0) -> 1.0
| (n, i) -> n * power(n, i-1)

//       (System.Math.PI, fact -1)  : no answer
//       fact(fact 4)               : int - overflow
//       power(System.Math.PI, fact 2)  : float
//       (power, fact)          : : (float * int -> float) * (int -> int)


// 1.8 Consider the declarations:
//       let a = 5;;
//       let f a = a + 1;;
//       let g b = (f b) + a;;
//     Find the environment obtained from these declarations and write the evaluations of the expressions
//     f 3 and g 3.

// env = |  a -> 5
//       |  f ->  fun : int -> int
//       |  g ->  fun : int -> int

