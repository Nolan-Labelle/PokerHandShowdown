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
            bool playAgain = true;

            while (playAgain)
            {
                playAgain = false;
                deck = new Deck();

                List<Player> players = ReadInput();

                for (int i = 0; i < players.Count; i++)
                {
                    players[i].hand.EvaluateHand();
                }

                //players = (List<Player>)players.OrderByDescending(x => x.hand.winType);

                int howManyWin = players.OrderByDescending(x => x.hand.winType).GroupBy(x => x.hand.winType).FirstOrDefault().Count();

                if(howManyWin < 2)
                {
                    //easy, only 1 wins.
                    WinGame(players[0]);
                }
            }
        }

        private static void WinGame(Player player)
        {
            Console.WriteLine("Player {0} wins with a {1} in their hand of {2}", player.name, player.hand.winType.ToString(), player.hand.ToString());
        }

        private static List<Player> ReadInput()
        {
            List<Player> players = new List<Player>();

            // tell the user to press Enter when done
            Console.WriteLine("Please enter a player's name, then enter a hand of 5 cards separated by a comma with the format #$, where the # is a value from 2 to 10 or J, Q, K, or A; and the $ is the suit, e.g 'S'pades, 'H'earts, 'D'iamonds, 'C'lubs.");
            Console.WriteLine("When you are finished entering players, press enter when prompted for another player.");
            Console.Write("Enter player's name: ");
            string playerName = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(playerName))
            {
                playerName.Trim();
                Player player = new Player(playerName);

                Hand hand = null;

                while (!player.HasHand())
                {
                    Console.WriteLine("");
                    string cardsString = Console.ReadLine().ToUpper();

                    string[] cards = cardsString.Split(',');

                    if (cards.Length == 5)
                    {
                        hand = GenerateHand(cards);
                    }
                    else
                    {
                        Console.WriteLine("There must be 5 cards.");
                    }

                    if (hand != null && hand.IsValid(deck))
                    {
                        player.AssignHand(hand, deck);
                    }
                    else
                    {
                        Console.WriteLine("Invalid cards.");
                    }
                }
                Console.WriteLine("Player {0} with the hand {1} has been added.", player.name, player.hand.ToString());
                players.Add(player);

                Console.Write("Enter the next player's name: ");
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
