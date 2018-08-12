using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    enum WinType
    {
        HighCard,
        Pair,
        ThreeOfAKind,
        Flush
    }
    public class Hand
    {
        private Card[] cards { get; set; }
        public int winType { get; set; } 

        public Hand()
        {                                              
            cards = new Card[5];                                          
        }                                                                 
                                                                          
        public Hand(Card[] cards)                                         
        {                                                                                                           
            this.cards = cards;                                                                                                                
        }                                                                 

        public bool IsValid(Deck deck)
        {
            bool passed = true;

            foreach (var card in cards)
            {
                if (card != null)
                {
                    if (deck.CardUsed(card))
                    {
                        passed = false;
                    }
                }
            }

            return passed;
        }

        public void DrawCards(Deck deck)
        {
            foreach (var card in cards)
            {
                if(card != null)
                {
                    deck.UseCard(card);
                }
            }
        }

        public override string ToString()
        {
            string returnString = "";

            for (int i = 0; i < cards.Length; i++)
            {
                if (i != 0)
                {
                    returnString = string.Concat(returnString, ", ");
                }
                returnString = string.Concat(returnString, cards[i].ToString());
            }

            return returnString;
        }

        public bool IsFlush()
        {
            string firstSuit = cards[0].suit;

            return cards.Skip(1).All(x => firstSuit.Equals(x.suit));
        }
    }
}
