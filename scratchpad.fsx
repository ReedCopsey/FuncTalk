// Represents the suit of a playing card.
// Can be one of 4 options
type Suit = 
    | Hearts 
    | Clubs 
    | Diamonds 
    | Spades
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

// This is a record type that combines a Suit and a Rank.
type Card = { Suit: Suit; Rank: Rank }

/// list representing all the cards in the deck.
let fullDeck = 
    [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
          for rank in Rank.GetAllRanks() do 
              yield { Suit=suit; Rank=rank } ]
