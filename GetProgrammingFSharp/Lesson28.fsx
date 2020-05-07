//// Lesson 28 - Architecting hybred language applications - pg 331

/// 28.1 Crossing language boundaries

/// 28.1.1 Accepting data from external systems

/// Listing 28.1 - A simple domain model for use within C# - pg 333
open System.Collections.Generic


type OrderItemRequest = { ItemId : int; Count : int }

type OrderRequest =
    { OrderId : int
      CustomerName : string     // manditory
      Comment : string   // optionals
      /// One of (email or telephone), or none
      EmailUpdates : string   // set of related properties
      TelephoneUpdates : string
      Items : IEnumerable<OrderItemRequest> }  // mandatory


/// Listing 28.2 - Modeling the same domain in F# - pg 333
type OrderId = OrderId of int
type ItemId = ItemId of int
type OrderItem = { ItemId : ItemId; Count : int }

type UpdatePreference =
    | EmailUpdates of string
    | TelephoneUpdates of string

type Order =
    { OrderId : OrderId
      CustomerName : string
      ContactPreference : UpdatePreference option
      Comment : string option
      Item : OrderItem list  }
    
/// Listing 28.3 - Validating and transforming data [g 334
let toOrder (orderRequest: OrderRequest) : Order =
    { OrderId =  OrderId orderRequest.OrderId
      CustomerName =
          match orderRequest.CustomerName with
          | null -> failwith "Customer name must be populated"
          | name -> name
      Comment = orderRequest.Comment |> Option.ofObj
      ContactPreference =
        match Option.ofObj orderRequest.EmailUpdates,
              Option.ofObj orderRequest.TelephoneUpdates with
        | None, None  -> None
        | Some email, None -> Some(EmailUpdates email)
        | None, Some phone -> Some(TelephoneUpdates phone)
        | Some _, Some _ -> failwith "Unable to proceed - only one of telephone and email sould be supplied"
      Item =
        orderRequest.Items
        |> Seq.map (fun item -> { ItemId = ItemId item.ItemId; Count = item.Count})
        |> Seq.toList }


/// 28.1.2 Playing to the strengths of a language

(* Quick check 28.1 - pg 336
1 What features from the F# type system might be missing for simpler domains such as
JSON or CSV?
A- Discrimated unions
2 Why should you consider having a rich internal, and simpler external, domain?
A- Richer F# internal domane alows for more precise, safer modelig
   Simpler external domain alows for other languages to use your domain
3 Can you name a scenario for which C# might be a better fit than F#?
A- GUI code that is inherantly oo and mutable
*)


/// 28.2 Case study—WPF monopoly

(* Quick check 28.2  - pg 341
1 Why are expressions useful from a development and testing point of view?
A- The have no side-effects so are easier to reason about and test. (repeatable results)
2 Are there any restrictions on exposing C#-compatible events from F#?
A- 
3 Is it easier to move from expression- to statement-based code or vice versa?
A- 
*)









