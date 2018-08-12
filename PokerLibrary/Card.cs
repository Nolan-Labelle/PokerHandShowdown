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
        public int numericValue { get; }
        public string displayName { get; }
        public string suit { get; }

        public Card(string input)
        {
            Regex valueRegex = new Regex(@"[2-9]|10|J|Q|K|A");
            Regex suitRegex = new Regex(@"S|H|D|C");
            Match valueMatch = valueRegex.Match(input);
            Match suitMatch = suitRegex.Match(input);

            if (valueMatch.Success && suitMatch.Success)
            {
                suit = suitMatch.Value;
                displayName = valueMatch.Value + suitMatch.Value;

                int value;
                if (Int32.TryParse(valueMatch.Value, out value))
                {
                    numericValue = value;
                }
                else
                {
                    switch (valueMatch.Value)
                    {
                        case "J":
                            numericValue = 11;
                            break;
                        case "Q":
                            numericValue = 12;
                            break;
                        case "K":
                            numericValue = 13;
                            break;
                        case "A":
                            numericValue = 14;
                            break;
                    }
                }

                Console.WriteLine("Successfully parsed {0} into Value {1} and Suit {2}", input, value, suit);
            }
        }

        public int Compare(Card card)
        {
            return numericValue - card.numericValue;
        }

        public override string ToString()
        {
            return displayName;
        }
    }
}
