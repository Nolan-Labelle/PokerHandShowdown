using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PokerLibrary;

namespace Poker_Hand_Showdown
{
    class Program
    {
        static void GetPlayers(ref List<Player> players)
        {
            string tempCardString;
            Hand tempHand = new Hand();

            bool morePlayers = false;
            bool retryInput = false;
            int playerCounter = 0;

            Regex Regex = new Regex(@"([2-9]|10|J|Q|K|A)(S|H|D|C)");
            Match match;

            do
            {
                morePlayers = false;

                Console.Write("Player {0}'s Name: ", playerCounter + 1);
                players.Add(new Player(Console.ReadLine()));

                do
                {
                    retryInput = false;
                    players[playerCounter].Hand.wipeHand();

                    Console.WriteLine("Please input {0}s Hand of '5' cards with the format #$, where the # is a value from 2-10, or J, Q, K, or A, and the $ is the Suit, 'S'pades, 'H'earts, 'D'iamonds, 'C'lubs.", players[playerCounter].Name);
                    tempCardString = Console.ReadLine();

                    match = Regex.Match(tempCardString.ToUpper());
                    while (players[playerCounter].Hand.CardCount < 5 && match.Success)
                    {
                        players[playerCounter].Hand.insertCard(new Card(match.Value));
                        match.NextMatch();
                    }
                    if (players[playerCounter].Hand.CardCount < 5)
                    {
                        retryInput = true;
                        Console.WriteLine("Not enough cards, please make sure you input 5 cards.");
                    }
                    //also check to make sure no duplicate cards have been inputted.
                } while (retryInput == true);


                Console.Write("Add another player? (Y/y for yes, anything else is no)");
                match = Regex.Match(Console.ReadLine().ToUpper(), @"Y$");
                if (match.Success)
                {
                    morePlayers = true;
                    playerCounter++;
                }
            } while (morePlayers == true);
        }

        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            GetPlayers(ref players);
        }
    }
}
