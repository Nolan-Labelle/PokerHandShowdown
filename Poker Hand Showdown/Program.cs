using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


            Console.Write("Player 1's Name: ");
            tempName = Console.ReadLine();

            Console.WriteLine("Please input {0}s Hand usaing a value from 2-10, or J, Q, K, or A, and a Suit, 'S'pades, 'H'earts, 'D'iamonds, 'C'lubs.", tempName);
            do
            {
                Console.Write("Card 1 Value: ");
                tempCardString = Console.ReadLine();
                
            } while ();
        }
    }
}
