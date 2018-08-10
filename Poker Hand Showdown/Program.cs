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
        static void Main(string[] args)
        {
            string tempName;
            string tempCardString;
            Card tempCard;
            Hand tempHand = new Hand();

            Regex Regex = new Regex(@"([2-9]|10|J|Q|K|A)(S|H|D|C)");
            Match match;


            Console.Write("Player 1's Name: ");
            tempName = Console.ReadLine();

            Console.WriteLine("Please input {0}s Hand of '5' cards with the format #$, where the # is a value from 2-10, or J, Q, K, or A, and the $ is the Suit, 'S'pades, 'H'earts, 'D'iamonds, 'C'lubs.", tempName);
            tempCardString = Console.ReadLine();

            match = Regex.Match(tempCardString.ToUpper());

        }
    }
}
