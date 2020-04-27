//// Lesson 10 - Shaping data with Records - pg 111


/// 10.1 POCOs done right: records in F#

/// 10.1.1 Record basics

// Listing 10.4 - Immutable and structural equality record in F# - pg 114
type Address =
    { Street : string
      Town : string
      City : string }
    

/// 10.1.2 Creating records

// Listing 10.5 - Constructing a nested record in F#  - pg 115
type Customer =   // Declaring the Customr record type
    { Forename : string
      Surname : string
      Age : int
      Address : Address
      EmailAddress : string }

let customer =    // Creating a Customer with Address inline
    { Forename = "Joe"
      Surname = "Bloggs"
      Age = 30
      Address = 
        { Street = "The Street"
          Town = "The Town"
          City = "The City" }
      EmailAddress = "joe@bloggs.com" }


let name1  = customer.Surname
let street1 = customer.Address.Street

(* Now you try - pg 117
Let’s have a look at creating your own record type now:
1 Define a record type in F# to store data on a Car, such as manufacturer, engine
   size, number of doors, and so forth.
2 Create an instance of that record.
3 Experiment with formatting the record; use power tools to automatically format
  the record for you.
*)

// 1.
type Car =
    { Manufacturer : string
      Engine : string
      Size : int
      Doors : int
      Colour : string }

// 2.
let toyota =
   { Manufacturer = "Toyota"
     Engine = "four cylender"
     Size = 4
     Doors = 4
     Colour = "Sandalwood" }


(* Quick check 10.1  - pg 117
1 What is the default accessibility modifier for fields on records?
A- public

2 What is the difference between referential and structural equality?
A- Referential equality is equality of address location
 - Structural equality is an objects equality determined by the equality of the parts
*)


/// 10.2 Doing more with records

/// 10.2.1 Type inference with records

/// Listing 10.6 - Providing explicit types for constructing records - pg 118
let address : Address =  // Explicity declaring the type of the value
    { Street = "The Street"
      Town = "The Town"
      City = "The City" }

let addressExplicit =
    { Address.Street = "The Street" // Explicity declaring the type of the field
      Town = "The Town"
      City = "The City" }


/// 10.2.2 Working with immutable records

// Listing 10.7 - Copy-and-update record syntax - pg 119
// update age based on email address
let updatedCustomer =
     { customer with 
         Age = 31
         EmailAddress = "joe@bloggs.co.uk" }

// update passed customer
let updateAge(customer, age) =
    { customer with Age = age }

let newCust = updateAge(customer, 32)


/// 10.2.3 Equality checking

// Listing 10.8 - Comparing two records in F#  - pg 120
let isSameAddress = (address = addressExplicit)  // compare records using = operator
isSameAddress = true


(* Now you try - pg 120
Let’s practically explore some of these features of records:
1 Define a record type, such as the Address type shown earlier.
2 Create two instances of the record that have the same values.
3 Compare the two objects by using =, .Equals, and System.Object.ReferenceEquals.
4 What are the results of all of them? Why?
5 Create a function that takes in a customer and, using copy-and-update syntax,
  sets the customer’s Age to a random number between 18 and 45.
6 The function should then print the customer’s original and new age, before
  returning the updated customer record.
*)

type Address2 =   // 1.
    { Street : string
      Town : string
      City : string
      Zip : string }

// 2.
let address1 =
    { Street = "The Street"
      Town = "The Town"
      City = "The City"
      Zip = "55555" }

let address2 =
    { Street = "The Street"
      Town = "The Town"
      City = "The City"
      Zip = "55555" }

// 3.
let eq1 = address1 = address2           // true
let eq2 = address1.Equals(address2)     // true
let eq3 = System.Object.ReferenceEquals(address1, address2)  // false

// 4.
// '=', .Equals()  for structural equality
// .ReferenceEquals  test for referencial equality

// 5.
System.Random().Next(27) + 18  // 18 ... 45

/// update age given emailAddress
let updateCustomerAge(customer) = 
    let newAge = System.Random().Next(27) + 18
    {customer with Age = newAge}

let newCust1 = updateCustomerAge(customer)

// 6.
let updateCustomerAge2(customer, newAge) = 
    printfn "Age = %d, newAge = %d" customer.Age newAge
    {customer with Age = newAge}

let newCust2 = updateCustomerAge2(customer, 31)


(* Quick check 10.2  - pg 121
1 At runtime, what do records compile into?
A- .Net Classes

2 What is the default type of equality checking for records
A- Structural
*)

/// 10.3 Tips and tricks with records
/// 10.3.1 Refactoring

/// 10.3.2 Shadowing

do  // pg 112S 
    let myHome = { Street = "The Street"; Town = "The Town"; City = "The City" }
    let myHome = { myHome with City = "The Other City" }
    let myHome = { myHome with City = "The Third City" }
    myHome |> ignore
    ()

// State decomplected from Identity


/// 10.3.3 When to use records


(* Quick check 10.3  - pg 123
1 What is shadowing?
A- Reusing a reference for a new (updated) value

2 When should you use records?
A- For data with many fields (>3) and data that represents a concept.
*)

(* Try this  - pg 124
1 Model the Car example from lesson 6, but use records to model the state of the Car.
2 Take an existing set of classes that you have in an existing C# project and map as
records in F#. Are there any cases that don’t map well?
*)

// 1
type Car2 = { petrol : int }

/// Gets the distance to a given destination 
let getDistance (destination) =
    if destination = "Gas" then 10
    elif destination = "Home"  then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown destination!"

//assume that one unit of distance needs one unit of petrol
let calculateRemainingPetrol(car: Car2, distance: int) : int =
    if car.petrol >= distance then car.petrol - distance
    else failwith "Oops! You’ve run out of petrol!"

/// Drives to a given destination given a starting amount of petrol
let driveTo (car, destination) =
    let distance = getDistance(destination)
    let remain = calculateRemainingPetrol(car, distance)
    if destination = "Gas" then remain + 50
    else remain


// 2 - TODO:
