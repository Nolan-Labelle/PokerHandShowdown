using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace PokerLibrary
{
    public class Card
    {
        private string value { get; set; }
        private string suit { get; set; }
        public bool isValid { get; set; }

        public Card(string input)
        {
            Regex valueRegex = new Regex(@"[2-9]|10|J|Q|K|A");
            Regex suitRegex = new Regex(@"S|H|D|C");
            Match valueMatch = valueRegex.Match(input);
            Match suitMatch = suitRegex.Match(input);

            if (valueMatch.Success && suitMatch.Success)
            {
                value = valueMatch.Value;
                suit = suitMatch.Value;
                Console.WriteLine("Successfully parsed {0} into Value {1} and Suit {2}", input, value, suit);
                isValid = true;
            }
            else
            {
                Console.WriteLine("Failed to parse {0}", input);
                value = null;
                suit = null;
                isValid = false;
            }
        }

        public bool Equals(Card card)
        {
            return (value.Equals(card.value) && suit.Equals(card.suit));
        }

        public override string ToString()
        {
            if (value != null && suit != null)
            {
                return string.Format("{0}{1}", value, suit);
            }
            else
            {
                Console.WriteLine("Empty Card, so giving empty string.");
                return "";
            }
        }
    }
}
