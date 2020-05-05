/// Lesson 25
/// 25.2.4 Referencing assemblies in scripts

//#r @"C:\Users\emaphis\source\repos\FPUsingFSharp\CSharpProject\bin\Debug\netcoreapp3.1\CSharpProject.dll"
#r @"CSharpProject\bin\Debug\netcoreapp3.1\CSharpProject.dll"
open CSharpProject
let simon = Person "Simon"
simon.PrintName()
