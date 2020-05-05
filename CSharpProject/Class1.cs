//// Lesson 25 - Consuming C# from F# - pg 299

using System;

namespace CSharpProject
{
    /// <summary>
    /// Listing 25.1 A simple C# class - pg 301
    /// </summary>
    public class Person
    {
        public string Name { get; private set; }
        
        public Person(String name)
        {
            Name = name;
        }

        public void PrintName()
        {
            Console.WriteLine($"My name is {Name}");
        }
    }
}
