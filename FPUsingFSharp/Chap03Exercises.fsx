//// Chapter 4 - Tuples, records and tagged values -  pg 43.
/// Exercises


/// Exercise 3.1
// A time of day can be represented as a triple (hours, minutes, f) where f is either AM or PM
// – or as a record. Declare a function to test whether one time of day comes before another. For
// example, (11,59,"AM") comes before (1,15,"PM"). Make solutions with triples as well
// as with records. Declare the functions in infix notation.

type Half =
    | AM
    | PM

type Time1 =  int * int * Half

type Time2 =
    { Hours : int 
      Minutes : int
      Half : Half }

let (.<.) (time1 : Time1) (time2 : Time1) =
    let hours1, minutes1, half1 = time1
    let hours2, minutes2, half2 = time2
    if (half1 < half2)
    then true
    elif (half1 = half2 && hours1 < hours2)
    then true
    elif (half1 = half2 && hours1 = hours2 && minutes1 < minutes2)
    then true
    else false

(11,11,AM) .<. (11,11,PM) = true
(11,11,PM) .<. (11,11,AM) = false
(10,11,AM) .<. (11,11,AM) = true
(11,11,AM) .<. (10,11,AM) = false
(11,10,AM) .<. (11,11,AM) = true
(11,11,AM) .<. (11,10,AM) = false

;;
let (.<<.) (time1 : Time2) (time2 : Time2) =
    if (time1.Half < time2.Half)
    then true
    elif (time1.Half = time2.Half && time1.Hours < time2.Hours)
    then true
    elif (time1.Half = time2.Half && time1.Hours = time2.Hours && time1.Minutes < time2.Minutes)
    then true
    else false
 ;;
let time1 = { Hours = 11; Minutes = 11; Half = AM }
let time2 = { Hours = 11; Minutes = 11; Half = PM }
let time3 = { Hours = 10; Minutes = 11; Half = AM }
let time4 = { Hours = 11; Minutes = 11; Half = AM }
let time5 = { Hours = 11; Minutes = 10; Half = AM }
let time6 = { Hours = 11; Minutes = 11; Half = AM }

time1 .<<. time2 = true
time2 .<<. time1 = false
time3 .<<. time4 = true 
time4 .<<. time3 = false
time5 .<<. time6 = true
time6 .<<. time5 = false

