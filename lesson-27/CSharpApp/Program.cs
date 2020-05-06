//// Lesson 27 - Exposing F# types and functions to C# - pg 321
using Model;
using System;
using Microsoft.FSharp.Core;

namespace CSharpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new Car(4, "Supacars", Tuple.Create(1.5, 3.5));
            var wheeles = car.Wheels;

            var bike = Vehicle.NewMotorbike("Harley", 4.0);
            var car2 = Vehicle.NewMotorcar(car);

            Console.WriteLine("A car: {0}", car);
            Console.WriteLine("A bike: {0}", bike);

            Functions.Describe(bike);
            Functions.Describe(car2);

            var car3 = Functions.CreateCar(4, "Chevy", 1.5, 3.5);
            var car4 =
                Functions.CreateFourWheeledCar
                    .Invoke("Chevy")
                    .Invoke(1.5)
                    .Invoke(3.5);

            // Woring with Options
            var optionalCar = FSharpOption<Car>.Some(car);
            var isNone = FSharpOption<Car>.get_IsNone(car);
            var isNotSome = FSharpOption<Car>.get_IsSome(optionalCar);
            Console.WriteLine("{0} : {1} : {2}", optionalCar, isNone, isNotSome);

        }
    }
}
