//// Lesso 10 - Shaping data with Records

/// 10.1.1 Record basics

type Address =
    { Street : string
      Town : string
      City : string }
    

/// 10.1.2 Creating records

type Customer = 
    { Forename : string
      Surname : string
      Age : int
      Address : Address
      EmailAddress : string }

let customer =
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

(* Now you try
Let’s have a look at creating your own record type now:
1 Define a record type in F# to store data on a Car, such as manufacturer, engine
   size, number of doors, and so forth.
2 Create an instance of that record.
3 Experiment with formatting the record; use power tools to automatically format
  the record for you.
*)

type Car =
    { Manufacturer : string
      Engine : string
      Size : int
      Doors : int
      Colour : string }

let toyota =
   { Manufacturer = "Toyota"
     Engine = "four cylender"
     Size = 4
     Doors = 4
     Colour = "Sandalwood" }


(* Quick check 10.1
1 What is the default accessibility modifier for fields on records?
A- public

2 What is the difference between referential and structural equality?
A- Referential equality is equality of address location
 - Structural equality is an objects equality determined by the equality of the parts
*)


/// 10.2 Doing more with records

/// 10.2.1 Type inference with records

let address : Address =
    { Street = "The Street"
      Town = "The Town"
      City = "The City" }

let addressExplicit =
    { Address.Street = "The Street"
      Town = "The Town"
      City = "The City" }


/// 10.2.2 Working with immutable records

// Copy-and-update record syntax

// update age
let updatedCustomer =
     { customer with 
         Age = 31
         EmailAddress = "joe@bloggs.co.uk" }

let updateAge(customer, age) =
    { customer with Age = age }

let newCust = updateAge(customer, 32)


/// 10.2.3 Equality checking

let isSameAddress = (address = addressExplicit)
isSameAddress


(* Now you try
Let’s practically explore some of these features of records:
1 Define a record type, such as the Address type shown earlier.
2 Create two instances of the record that have the same values.
3 Compare the two objects by using =, .Equals, and System.Object.ReferenceEquals.
4 What are the results of all of them? Why?
5 Create a function that takes in a customer and, using copy-and-update syntax,
sets the customer’s Age to a random number between 18 and 45.
*)

type Address2 =
    { Street : string
      Town : string
      City : string
      Zip : string }

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

let eq1 = address1 = address2           // true
let eq2 = address.Equals(address2)      // false
let eq3 = System.Object.ReferenceEquals(address1, address2)  // false
// '='   for structural equality
// .Equals or .ReferenceEquals  test for referencial equality


System.Random().Next(27) + 18  // 18 ... 45

/// update age given emailAddress
let updateCustomerAge(customer) = 
    System.Console.WriteLine(customer.ToString())
    let newAge = System.Random().Next(27) + 18
    {customer with Age = newAge}

updateCustomerAge(customer)

(* Quick check 10.2
1 At runtime, what do records compile into?
A- .Net Classes

2 What is the default type of equality checking for records
A- Structural
*)

/// 10.3 Tips and tricks with records
/// 10.3.1 Refactoring
// TODO:

/// 10.3.2 Shadowing

let myHome = { Street = "The Street"; Town = "The Town"; City = "The City" }
let myHome = { myHome with City = "The Other City" }
let myHome = { myHome with City = "The Third City" }
// State decomplected from Identity


/// 10.3.3 When to use records


(* Quick check 10.3
1 What is shadowing?
A- Reusing a reference for a new (updated) value

2 When should you use records?
A- For data with many fields (>3) and data that represents a concept.
*)
