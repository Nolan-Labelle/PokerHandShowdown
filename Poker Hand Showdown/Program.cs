using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PokerLibrary;

namespace PokerHandShowdown
{
    class Program
    {
        private static Deck deck { get; set; }

        static void Main(string[] args)
        {
            deck = new Deck();
            List<Player> players = ReadInput();

            //do other things
        }

        private static List<Player> ReadInput()
        {
            List<Player> players = new List<Player>();

            Console.Write("Enter Player's Name: ");
            string playerName = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(playerName))
            {
                playerName.Trim();
                Player player = new Player(playerName);

                Hand tempHand = null;

                while (!player.HasHand())
                {
                    Console.WriteLine("Please a Hand of '5' cards separated by a comma with the format #$, where the # is a value from 2-10, or J, Q, K, or A, and the $ is the Suit, 'S'pades, 'H'earts, 'D'iamonds, 'C'lubs.");
                    string tempCardString = Console.ReadLine().ToUpper();

                    string[] tempCardArray = tempCardString.Split(',');

                    if (tempCardArray.Length == 5)
                    {
                        tempHand = GenerateHand(tempCardArray);
                    }
                    else
                    {
                        Console.WriteLine("Not Enough/Too Many Cards.");
                    }

                    if (tempHand != null && tempHand.CheckHand(deck))
                    {
                        player.SetHand(tempHand, deck);
                    }
                }
                Console.WriteLine("Player {0} With the hand {1} has been added.", player.name, player.hand.ToString());
                players.Add(player);

                Console.Write("Enter Player's Name: ");
                playerName = Console.ReadLine();
            }

            return players;
        }

        private static Hand GenerateHand(string[] cards)
        {
            Regex singleCardRegex = new Regex(@"(\s+|^)([2-9]|10|J|Q|K|A)(S|H|D|C)(,|$)");
            Match match;
            List<Card> hand = new List<Card>();

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = cards[i].Trim();
            }

            if (cards.Distinct().Count() != cards.Length)
            {
                return null;
            }

            foreach (var card in cards)
            {
                match = singleCardRegex.Match(card);
                if (match.Success)
                {
                    hand.Add(new Card(match.Value));
                }
                else
                {
                    return null;
                }
            }
            
            return new Hand(hand.ToArray());
        }
    }
}
