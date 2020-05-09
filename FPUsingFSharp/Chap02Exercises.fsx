module Chap02Exercises

/// Exercise 2.1
// Declare a function f: int -> bool such that f(n) = true exactly when n is divisible by 2
// or divisible by 3 but not divisible by 5. Write down the expected values of f(24), f(27), f(29)
// and f(30) and compare with the result. Hint: n is divisible by q when n%q = 0.

let f  = function
    | n when n % 5 = 0  -> false   // must be first
    | n when n % 2 = 0  -> true
    | n when n % 3 = 0  -> true
    | _                 -> false

f(24) = true
f(27) = true
f(29) = false
f(30) = false

// another version
let f2 (n) = n % 5 <> 0 && (n % 2 = 0 || n % 3 = 0)

f2(24) = true
f2(27) = true
f2(29) = false
f2(30) = false


/// Exercise 2.2
// Declare an F# function pow: string * int -> string, where:
//    pow(x,n) = s . s . ...... . s
// where number of concatenations are == n

let rec pow = function
    | (_, 0) -> ""
    | (s, n) -> s + pow (s, n-1)

let pw0 = pow ("ab", 0) = ""
let pw1 = pow ("ab", 1) = "ab"
let pw2 = pow ("ab", 3) = "ababab"


/// Exercise 2.3
// Declare the F# function isIthChar: string * int * char -> bool where the value of
// isIthChar(str, i, ch) is true if and only if ch is the i’th character in the string str (numbering
// starting at zero).

let isIthChar (str : string, i, ch)  =
    if i < 0 || i > str.Length then false
    else str.[i] = ch

isIthChar("abcd", 1, 'b') = true
isIthChar("abcd", 2, 'b') = false


/// Exercise 2.4
/// Declare the F# function occFromIth: string * int * char -> int where
///      occFromIth(str, i, ch) = the number of occurrences of character ch
///                               in positions j in the string str with j ≥ i.
/// Hint: the value should be 0 for i ≥ size str.
let rec occFromIth (str : string, i, ch) =
    if i >= str.Length then 0
    else (if isIthChar(str, i, ch) then 1 else 0) + occFromIth(str, i+1, ch)
//
occFromIth("abcdecc", 0, 'c') = 3
occFromIth("abcdecc", 4, 'c') = 2
occFromIth("abcdecc", 6, 'c') = 1


/// Exercise 2.5
/// Declare the F# function occInString: string * char -> int where
///       occInString(str, ch) = the number of occurrences of character ch
///                              in the string str.
let occInString (str, ch) = occFromIth (str, 0, ch)
 
occInString("abcdcecc", 'c') = 4

/// Exercise 2.6 
/// Declare the F# function notDivisible: int * int -> bool where
///    notDivisible(d, n) is true if and only if d is not a divisor of n.
/// For example notDivisible(2,5) is true, and notDivisible(3,9) is false.
let notDivisible(d, n) = n % d <> 0

notDivisible(2, 5) = true
notDivisible(3, 9) = false

/// Exercise  2.7 
// 1. Declare the F# function test: int * int * int -> bool. The value of test(a, b, c),
//    for a ≤ b, is the truth value of:
//
//        notDivisible(a, c)
//    and notDivisible(a + 1, c)
//    ...
//    and notDivisible(b, c)
//
// 2. Declare an F# function prime: int -> bool, where prime(n) = true, if and only if n
//    is a prime number.
// 3. Declare an F# function nextPrime: int -> int, where nextPrime(n) is the smallest
//    prime number > n.

// 1.
let rec test(lower, upper, num) =
    if lower > upper then true
    else notDivisible(lower, num) &&  test (lower+1, upper, num)

// 2.
let isPrime num =
    num > 1 &&
    test(2, num-1, num)

// 3.
let rec findNextPrime n =
    if isPrime (n + 1) then n + 1
    else findNextPrime (n + 1)

findNextPrime 3 = 5
findNextPrime 4 = 5
findNextPrime 5 = 7
findNextPrime 7 = 11
findNextPrime 11 = 13


/// Exercise 2.8
let rec bin = function
    | (_, 0) -> 1
    | (n, k) when n = k  -> 1
    | (n, k) -> bin (n-1, k-1) + bin (n-1, k)

bin(0,0) = 1
bin(1,0) = 1
bin(1,1) = 1
bin(2,0) = 1
bin(2,1) = 2
bin(2,2) = 1
bin(4,2) = 6


/// Exercise 2.9
// Concider the declaration

let rec f' = function
    | (0,y) -> y
    | (x,y) -> f'(x-1, x*y)

// 1. determine the type of `f`
// f : int * int -> int

// 2. For which arguments does the evaluation of f terminate?
// 'f' terminates for (0,_) -> x = 0

// 3. Write the evaluation steps for f'(2,3).
// f(2, 3)
// f(2-1, 2*3)
// f(1, 6)
// f(1-1, 1*6)
// f(0, 6)
// 8
f'(2,3) = 6

// 4. What is the mathematical meaning of f(x, y)?
// factorial function - tail recursive.


/// Exercise 2.10
/// Consider the following declaration:
let test2(c, e) = if c then e else 0

// 1. What is the type of test?
// test2 : bool * int -> int

// 2. What is the result of evaluating test(false, fact(-1))
// test2(false, fact(-1))
// ..... error ......
let bomb(n) = failwith "error calling function"
test2(false, bomb(-1))

// 3. Compare this with the result of evaluating
// if false then fact -1 else 0
// 0
(if false then bomb(-1) else 0) = 0


/// Exercise 2.11
// Declare a function VAT: int -> float -> float such that the value VAT n x is obtained
// by increasing x by n percent.

// Declare a function unVAT: int -> float -> float such that
//     unVAT n (VAT n x) = x

// Hint: Use the conversion function float to convert an int value to a float value.

let VAT tax price = price * (1.0 + float tax / 100.0)

let unVAT tax total = total / (1.0 + float tax/ 100.0)

VAT 30 10.0 = 13.0
unVAT 30 13.0 = 10.0
unVAT 30 (VAT 30 10.0) = 10.0  // idempotent.


/// Exercise 2.12
/// Declare a function min of type (int -> int) -> int. The value of min(f) is the smallest
/// natural number n where f(n) = 0 (if it exists).

// TODO:


/// Exercise 2.13
// The functions curry and uncurry of types
//    curry : (’a * ’b -> ’c) -> ’a -> ’b -> ’c
//    uncurry : (’a -> ’b -> ’c) -> ’a * ’b -> ’c
// are defined in the following way:
//
// curry f is the function g where g x is the function h where h y = f(x, y).
//
// uncurry g is the function f where f(x, y) is the value h y for the function h = g x.
//
// Write declarations of curry and uncurry

let curry fn = fun x y -> fn(x,y)
let uncurry fn = fun (x,y) -> fn x y


let curriedAdd x y = x+y
let unCurriedAdd (x,y) = x+y

(curry unCurriedAdd) 3 4  = 7
(uncurry curriedAdd) (3,4) = 7
