- title : Thinking Functionally in .NET
- description : Basic functional programming in F# and C#
- author : Reed Copsey
- theme : night
- transition : default

***

### Thinking Functionally in .NET

* Goal: Embrace more functional programming concepts every day
* Who am I?: 
* Reed Copsey, Jr.
  * CTO - C Tech Development Corporation
  * Executive Director - F# Software Foundation
  * Twitter: @reedcopsey

***

### Why does it matter?

* Functional programming is on the rise
* Potentially better fit for most programs
* Advantages:
  * Conciseness
  * Convenience
  * Correctness

---

### From "C# Design Notes for Jan 21, 2015"

* https://github.com/dotnet/roslyn/issues/98

---

### Working with Data (1/3)

> Today’s programs are connected and trade in rich, structured data: it’s what’s on the wire, it’s what apps and services produce, manipulate and consume.


---

### Working with Data (2/3)

> Traditional object-oriented modeling is good for many things, but in many ways it deals rather poorly with this setup: it bunches functionality strongly with the data (through encapsulation), and often relies heavily on mutation of that state. It is "behavior-centric" instead of "data-centric".

---

### Working with Data (3/3)


> Functional programming languages are often better set up for this: data is immutable (representing information, not state), and is manipulated from the outside, using a freely growable and context-dependent set of functions, rather than a fixed set of built-in virtual methods. Let’s continue being inspired by functional languages, and in particular other languages – F#, Scala, Swift – that aim to mix functional and object-oriented concepts as smoothly as possible.

---

### Object Oriented vs Functional - Significant Difference

* OO
  * Behavior is fixed (virtual methods), types can be extended via subclassing
  * Encapsulation of state implies mutation
* Functional
  * Types are fixed, and represent data directly (DDD approach)
  * Behavior can be extended (external functions)
  

***

### Bindings and Functions

- _Bindings_ correlate a value with a name
- _Functions_ work on inputs and produce output
- (In F#) Organized into _modules_

#### 

    module SimpleExample =
        // Bind "number" to the value 2
        let number = 2

        // Bind a function 
        let sample1 x = x * 3 + 5

        // Apply the function and bind the result
        let result1 = sample1 42

- Function is just another value!
- In F#, same keyword (let) defines both


---

### All functions produce output

####

    let add5 value = value + 5
    let print value = printfn "Value is %d" value

- Unit type: Represents absence of a _specific_ value
- Different than "void"

####

    // The value of the unit type.
    ()
    // Is treated like any other data
    let u = ()

***

### Expressions

* Everything is an expression
* No statements!
  * Output unit if value "doesn't matter"
* Program is " expression of sub-expressions" vs "control flow"

####

    let makeString v =
        if v < 42 then
            "Less than 42"
        else
            "Greater or equal to 42"

---

### No return

* Expression vs control flow
* Last expression returns value

####

    let sq v = v * v
    let cube v = v * v * v

    let complexOperation v1 v2 =
        let sqA1 v = (sq v) + 1.0

        let v1 = sqA1 v1

        v1 + (cube v2) 

---

### Expression-bodied members (C#)

* Added in C# 6
* Expanded in C# 7

####

    [lang=C#]
    public class Location
    {
        private string locationName;
    
        public Location(string name) => Name = name;

        public string Name
        {
            get => locationName;
            set => locationName = value;
        } 
    }

***

### Higher order functions

* Function which takes function as param or returns function
* Functions are just values, HOF are just functions

####

    let sq v = v * v
    let cube v = v * v * v
    let add5 v = v + 5

    let printResult fn v =
        let res = fn v
        printfn "Result: %d" res

    printResult sq 42
    printResult add5 7
    printResult cube 3


---

### Practical HOF

####

    let sq v = v * v
    let values = [ 1 .. 5 ]

    let squares = List.map sq values

    for s in squares do
        printfn "R: %d" s


---

### LINQ in C#

####

    [lang=cs]
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            var values = new List<int> { 1, 2, 3, 4, 5 };

            // System.Linq extensions are HOF
            var squares = values.Select(Square);

            foreach(var s in squares)
            {
                Console.WriteLine($"R: {s}");
            }

            int Square(int value) => value * value;
        }
    }

***

### Referential Transparency

* Expression is referentially transparent if it can be replaced with it's corresponding value
* Requires purity
* No side effects
* Not enforced in .NET (even in F#)
  * Unit type makes side effects more obvious in F#
  * Requires developer care in .NET
* Easily testable
  * Same input = same output

---

### Example

####

    let sq v = v * v
    let values = [ 1 .. 5 ]

    // List.map and sq are referentially transparent
    // so we can replace this with it's value directly:

    // let squares = List.map sq values

    let squares = [ 1 ; 4 ; 9 ; 16 ; 25 ]

    for s in squares do
        printfn "R: %d" s



***

### Immutability

* Types and values are immutable _by default_ in F#

---

### Immutability

* Let bindings

####

    let value = 42
    let mutable value2 = 1

    // Assign new value to mutable
    value2 <- 42

    // Expression returning true!
    value2 = 42 

***

###  Rich type system

* Classes and interfaces like C#
* Tuples
* Records 
* Discriminated Unions 

--- 

### Records 


####

    type Person = { FirstName : string ; LastName : string }
    
    let reed = { FirstName = "Reed" ; LastName = "Copsey" }

    let christina = { reed with FirstName = "Christina" }


* Can be represented as class in C#
  * Sealed with no default ctor
  * Readonly properties
  * Structural equality
    * Equals 
    * GetHashCode
    * IEquatable<T> 
    * IStructuralEquatable
    * IComparable<T>

---

### Discriminated Union


####

    // Represents the suit of a playing card.
    // Can be one of 4 options
    type Suit = 
        | Hearts 
        | Clubs 
        | Diamonds 
        | Spades

---

### Discriminated Union


####

    // Can have data
    type Rank = 
        // Represents the rank of cards 2 .. 10
        | Value of int
        | Ace
        | King
        | Queen
        | Jack

        // Can implement object-oriented or static members.
        member this.IsFaceCard =
            match this with
            | King | Queen | Jack -> true
            | Ace | Value _ -> false

        static member GetAllRanks() = 
            [ yield Ace
              for i in 2 .. 10 do yield Value i
              yield Jack
              yield Queen
              yield King ]

---

### Rich Type System

####

    // This is a record type that combines a Suit and a Rank.
    type Card = { Suit: Suit; Rank: Rank }

    /// list representing all the cards in the deck.
    let fullDeck = 
        [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
              for rank in Rank.GetAllRanks() do 
                  yield { Suit=suit; Rank=rank } ]

---

### Rich Type System

####

    /// Convert a 'Card' object to a string.
    let showPlayingCard (c: Card) = 
        let rankString = 
            match c.Rank with 
            | Ace -> "Ace"
            | King -> "King"
            | Queen -> "Queen"
            | Jack -> "Jack"
            | Value n -> string n
        let suitString = 
            match c.Suit with 
            | Clubs -> "clubs"
            | Diamonds -> "diamonds"
            | Spades -> "spades"
            | Hearts -> "hearts"
        rankString  + " of " + suitString

    /// This example prints all the cards in a playing deck.
    let printAllCards() = 
        for card in fullDeck do 
            printfn "%s" (showPlayingCard card)

---

### Options

* Provides type safe mechanism for handling "missing" data
* No nulls!

####

    // Single case discriminated union
    type ZipCode = ZipCode of string

    type Customer = { Person : Person ; ZipCode : ZipCode option }

    let rCust = { Person = reed ; ZipCode = Some (ZipCode "04096") }

    let cCust = { Person = christina ; ZipCode = None }

--- 

### Options

* Use requires checking
* Option module provides helpers as HOF

####

    type ShippingCost =
        | Unknown
        | Amount of decimal

    let shippingCost cust =
        match cust.ZipCode with
        | None -> Unknown
        | Some (ZipCode zip) -> 
            match zip with
            | "04101" -> Amount 3.99m
            | "04102" -> Amount 4.49m
            | "04105" -> Amount 3.99m
            | "04096" -> Amount 4.99m
            | _ -> Unknown



*** 

### Putting it together

* Examples

***

### Thank you!

* Resources:
  * fsharp.org
    * Join for free - Active and friendly Slack team
  * Book recommendations
    * ![gpfs](images/gpfs.png) Get Programmiing with F# by Isaac Abraham       
    * ![gpfs](images/dmmf.jpg) Domain Modeling Made Functional by Scott Wlaschin     
* Reed Copsey, Jr.
  * Twitter: @reedcopsey

