using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PokerLibrary
{
    public class Card
    {
        private string Value { get; set; }
        private string Suit { get; set; }

        public Card(string input)
        {
            Regex valueRegex = new Regex(@"[2-9]|10|J|Q|K|A");
            Regex suitRegex = new Regex(@"S|H|D|C");
            Match valueMatch = valueRegex.Match(input);
            Match suitMatch = suitRegex.Match(input);

            if (valueMatch.Success && suitMatch.Success)
            {
                Value = valueMatch.Groups[1].Value;
                Suit = suitMatch.Groups[1].Value;
                Console.WriteLine("Successfully parsed {0} into Value {1} and Suit {2}", input, Value, Suit);
            }
            else
            {
                Console.WriteLine("Failed to parse {0}", input);
                Value = null;
                Suit = null;
            }
        }
    }

    public class Hand
    {
        private Card[] Cards { get; set; }
        public int CardCount { get; set; }

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

        public void wipeHand()
        {
            CardCount = 0;
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new Hand();
        }
        public Player(string name, Hand hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}
