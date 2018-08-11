using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    public class Hand
    {
        private Card[] cards { get; set; }

        public Hand()
        {                                              
            cards = new Card[5];                                          
        }                                                                 
                                                                          
        public Hand(Card[] cards)                                         
        {                                                                                                           
            this.cards = cards;                                                                                                                
        }                                                                 

        public bool CheckHand(Deck deck)
        {
            bool passed = true;

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null)
                {
                    if (deck.CardUsed(cards[i]))
                    {
                        passed = false;
                    }
                }
            }

            return passed;
        }

        public void DrawCards(Deck deck)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null)
                {
                    deck.UseCard(cards[i]);
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
    }
}
