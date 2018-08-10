using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    enum Deck
    {
        Card2H,
        Card3H,
        Card4H,
        Card5H,
        Card6H,
        Card7H,
        Card8H,
        Card9H,
        Card10H,
        CardJH,
        CardQH,
        CardKH,
        CardAH,
        Card2C,
        Card3C,
        Card4C,
        Card5C,
        Card6C,
        Card7C,
        Card8C,
        Card9C,
        Card10C,
        CardJC,
        CardQC,
        CardKC,
        CardAC,
        Card2D,
        Card3D,
        Card4D,
        Card5D,
        Card6D,
        Card7D,
        Card8D,
        Card9D,
        Card10D,
        CardJD,
        CardQD,
        CardKD,
        CardAD,
        Card2S,
        Card3S,
        Card4S,
        Card5S,
        Card6S,
        Card7S,
        Card8S,
        Card9S,
        Card10S,
        CardJS,
        CardQS,
        CardKS,
        CardAS,
        END_OF_DECK
    }
    public struct Card
    {
        private int Value { get; set; }
        private char Suit { get; set; }
        public Card(int value, char suit)
        {
            Value = value;
            Suit = suit;
        }
    }

    public class Hand
    {
        private Card[] Cards { get; set; }
        private int CardCount { get; set; }
        public Hand()
        {
            CardCount = 0;
        }
        public Hand(Card card1, Card card2, Card card3, Card card4, Card card5)
        {
            Cards[0] = card1;
            Cards[1] = card2;
            Cards[2] = card3;
            Cards[3] = card4;
            Cards[4] = card5;
            CardCount = 5;
        }
        public bool insertCard(Card card)
        {
            bool passed = true;
            if(CardCount >= 5)
            {
                passed = false;
            }
            else
            {
                Cards[CardCount] = card;
                CardCount++;
            }
            return passed;
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public Player(string name)
        {
            Name = name;
        }
        public Player(string name, Hand hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}
