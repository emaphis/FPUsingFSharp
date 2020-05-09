////  Getting started -  pg 1.

/// values, expressions, types and declarations, including recursive function declarations

/// 1.1 Values, types, identifiers and declarations - pg 1
let price = 125
price * 20

/// 1.2 Simple function declarations

System.Math.PI

/// Area of circle with radious of `r` -
/// circleArea(r) = πr2.
let circleArea r = System.Math.PI * r * r

let ca0 = circleArea 0.
let ca1 = circleArea 1.0
let ca2 = circleArea (2.0)


/// 1.3 Anonymous functions. Function expressions - pg 4

fun r -> System.Math.PI * r * r
(fun r -> System.Math.PI * r * r) 2.0

let circleArea1 = fun r -> System.Math.PI * r * r

circleArea1 3.0


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

daysOfMonth 2 = 28
daysOfMonth 3 = 31


/// 1.4 Recursion  - pg 6

/// factorial
// 0! = 1                       (Clause 1)
// n! = n · (n − 1)! for n > 1  (Clause 1)

// recursive declaration
let rec fact = function
    | 0 -> 1
    | n -> n * fact (n-1)

fact 5  = 120
fact 10 = 3628800
fact 15


/// 1.5 Pairs  - pg 11

let a = (2.0,3)
// pattern
let (x,y) = a


// pow -> pair(x,n)
let rec power = function
    | (x,0) -> 1.0    
    | (x,n) -> x * power(x, n-1)

power (5.0, 0)
power (5.0, 1)
power (5.0, 5)
power a


/// 1.6 Types and type checking  - pg 13

// if 'f' : T1 -> T2 and e : T1
// then f(e) : T2


/// 1.7 Bindings and environments  - pg 14

let a1 = 3
let b1 = 8.0
// creates th envionment
// evn2  a1 -> 3
//       b1 -> 7.0

// extension to envirionment
let c1 = (2, 8)
let circleArea2 r = System.Math.PI * r * r


// 1.8 Euclid's algorithm  - pg 15

let example1 (n, m) = (n / m) * m + (n % m)
example1 (4, 5) = 4
(4 / 5) * 5 + (4 % 5) = 4

/// Euclid's algorithm
let rec gcd = function
    | (0,n) -> n
    | (m,n) -> gcd(n % m, m)

gcd (4, 5) = 1
gcd (12, 27) = 3
gcd (36, 116) = 4


/// 1.9 Evaluations and environments  - pg 17.


// 1.10 Free-standing programs  - pg 19
// see Programs.fs
