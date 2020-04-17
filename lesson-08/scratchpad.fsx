open System

/// 8.4.1 Your first function—calculating distances

/// Gets the distance to a given destination 
let getDistance (destination) =
    if destination = "Gas" then 10
    // fill in the blanks!
    elif destination = "Home"  then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown destination!"

// Couple of quick tests
getDistance("Home") = 25
getDistance("Stadium") = 25
getDistance("Office") = 50
getDistance("Gas") = 10


/// 8.4.2 Calculating petrol consumption

//assume that one unit of distance needs one unit of petrol
let calculateRemainingPetrol(currentPetrol:int, distance:int) : int =
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Oops! You’ve run out of petrol!" 

let petrol = 50
let remain1 = calculateRemainingPetrol(petrol, 30)
let remain2 = calculateRemainingPetrol(remain1, 30)
remain2


/// 8.4.4 Stopping at the gas station

/// Drives to a given destination given a starting amount of petrol
let driveTo (petrol, destination) =
    let distance = getDistance(destination)
    let remain = calculateRemainingPetrol(petrol, distance)
    if destination = "Gas" then remain + 50
    else remain

let a = driveTo(100, "Office")
let b = driveTo(a, "Stadium")
let c = driveTo(b, "Gas")
let answer = driveTo(c, "Home")
answer = 40
