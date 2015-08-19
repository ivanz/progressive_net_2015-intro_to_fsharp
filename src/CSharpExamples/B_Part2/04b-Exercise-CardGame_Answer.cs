using System.Collections.Generic;

/*
A card is a combination of a Suit (Heart, Spade) and a Rank (Two, Three, ... King, Ace)

A hand is a list of cards

A deck  is a list of cards

A player has a name and a hand

A game consists of a deck and list of players

*/

namespace CSharpExamples.B_Part2.CardGame_Answer
{

    public enum Suit { Club, Diamond, Spade, Heart }
    public enum Rank { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class Card
    {
        public Card(Suit s, Rank r)
        {
            this.Suit = s;
            this.Rank = r;
        }
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
    }


    public class Hand
    {
        public Hand(IList<Card> cards)
        {
            this.Cards = cards;
        }
        public IList<Card> Cards { get; private set; }
    }

    public class Deck
    {
        public Deck(IList<Card> cards)
        {
            this.Cards = cards;
        }
        public IList<Card> Cards { get; private set; }
    }

    public class Player
    {
        public Player(string name, Hand hand)
        {
            this.Name = name;
            this.Hand = hand;
        }
        public string Name { get; private set; }
        public Hand Hand { get; private set; }
    }

    public class Game
    {
        public Game(Deck deck, IList<Player> players)
        {
            this.Deck = deck;
            this.Players = players;
        }
        public Deck Deck { get; private set; }
        public IList<Player> Players { get; private set; }
    }

}
