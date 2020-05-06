//// Lesson 27 - Exposing F# types and functions to C# - pg 321

/// Listing 27.1 An F# record to be accessed from C#
namespace Model

/// A standard F# record of a Car.
type Car =
    { /// The number of wheels on the car.
      Wheels : int
      
      /// The brand of the car.
      Brand : string
      
      /// The x/y demensions of the car in meters
      Dimensions : float * float }

/// A vehicle of sone sort
type Vehicle =

/// A car is a type of vehicle.
| Motorcar of Car

/// A bike is also a type of vehicle
| Motorbike of Name:string * EngineSize:float

/// Listing 27.3 Exposing a module of functions to C# - pg 327
module Functions =
    /// Creates a car
    let CreateCar wheels brand x y =
        { Wheels = wheels; Brand = brand; Dimensions = x, y }

    /// Creates a car with four wheels
    let CreateFourWheeledCar = CreateCar 4

    /// Describes a vehicle
    let Describe vehicle =
        match vehicle with
        | Motorcar c -> printfn "This is a car with %d wheels!" c.Wheels
        | Motorbike(_, size) -> printfn "This is a bike wiht engine %f" size
