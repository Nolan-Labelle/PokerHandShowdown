using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PokerLibrary
{
    public class Deck
    {
        private bool[] cards  { get; set; }
        private Dictionary<string, int> mapping { get; set; }

        public Deck()
        {
            cards = Enumerable.Repeat(false, 52).ToArray();
            mapping = new Dictionary<string, int>();

            mapping.Add("2S", 0);
            mapping.Add("3S", 1);
            mapping.Add("4S", 2);
            mapping.Add("5S", 3);
            mapping.Add("6S", 4);
            mapping.Add("7S", 5);
            mapping.Add("8S", 6);
            mapping.Add("9S", 7);
            mapping.Add("10S", 8);
            mapping.Add("JS", 9);
            mapping.Add("QS", 10);
            mapping.Add("KS", 11);
            mapping.Add("AS", 12);

            mapping.Add("2H", 13);
            mapping.Add("3H", 14);
            mapping.Add("4H", 15);
            mapping.Add("5H", 16);
            mapping.Add("6H", 17);
            mapping.Add("7H", 18);
            mapping.Add("8H", 19);
            mapping.Add("9H", 20);
            mapping.Add("10H", 21);
            mapping.Add("JH", 22);
            mapping.Add("QH", 23);
            mapping.Add("KH", 24);
            mapping.Add("AH", 25);

            mapping.Add("2C", 26);
            mapping.Add("3C", 27);
            mapping.Add("4C", 28);
            mapping.Add("5C", 29);
            mapping.Add("6C", 30);
            mapping.Add("7C", 31);
            mapping.Add("8C", 32);
            mapping.Add("9C", 33);
            mapping.Add("10C", 34);
            mapping.Add("JC", 35);
            mapping.Add("QC", 36);
            mapping.Add("KC", 37);
            mapping.Add("AC", 38);

            mapping.Add("2D", 39);
            mapping.Add("3D", 40);
            mapping.Add("4D", 41);
            mapping.Add("5D", 42);
            mapping.Add("6D", 43);
            mapping.Add("7D", 44);
            mapping.Add("8D", 45);
            mapping.Add("9D", 46);
            mapping.Add("10D", 47);
            mapping.Add("JD", 48);
            mapping.Add("QD", 49);
            mapping.Add("KD", 50);
            mapping.Add("AD", 51);
        }

        public bool useCard(Card card)
        {
            bool used = false;
            int index;

            if(mapping.TryGetValue(card.getSring(), out index))
            {
                if (!cards[index])
                {
                    cards[index] = true;
                    used = true;
                }
                else
                {
                    Console.WriteLine("This card is already used.");
                }
            }

            return false;
        }

        public void resetDeck()
        {
            for(int i = 0; i < cards.Length; i++)
            {
                cards[i] = false;
            }
        }
    }

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

        public string getSring()
        {
            if(Value != null && Suit != null)
            {
                return string.Format("{0}{1}", Value, Suit);
            }
            else
            {
                return "";
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

        public bool checkHand()
        {
            bool passed = true;



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
            Hand = new Hand();
        }
        public Player(string name, Hand hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}
