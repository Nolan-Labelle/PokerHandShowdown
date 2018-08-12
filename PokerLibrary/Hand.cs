using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    public enum WinType
    {
        HighCard,
        Pair,
        ThreeOfAKind,
        Flush
    }

    public class Hand
    {
        private Card[] cards { get; set; }
        public WinType winType { get; set; } 

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
                else
                {
                    passed = false;
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

        public void EvaluateHand()
        {
            if (IsFlush())
            {
                winType = WinType.Flush;
            }
            else
            {
                int highestMatches = cards.GroupBy(x => x.numericValue)
                                    .OrderByDescending(x => x.Count())
                                    .FirstOrDefault().Count();

                switch (highestMatches)
                {
                    case 1:
                        //no pair
                        winType = WinType.HighCard;
                        break;
                    case 2:
                        //1 pair
                        winType = WinType.Pair;
                        break;
                    case 3:
                        //3 of a kind
                        winType = WinType.ThreeOfAKind;
                        break;
                    case 4:
                        //4 of a kind (functionally the same)
                        winType = WinType.ThreeOfAKind;
                        break;
                }
            }
        }

        public bool IsFlush()
        {
            string firstSuit = cards[0].suit;

            return cards.Skip(1).All(x => firstSuit.Equals(x.suit));
        }

        public int GetHighCard()
        {
            if (winType == WinType.Flush || winType == WinType.HighCard)
            {
                return cards.OrderByDescending(x => x.numericValue)
                            .FirstOrDefault().numericValue;
            }
            else if (winType == WinType.Pair)
            {
                return cards.OrderByDescending(x => x.numericValue)
                            .GroupBy(x => x.numericValue)
                            .Where(x => x.Count() > 1)
                            .OrderByDescending(x => x.Count())
                            .FirstOrDefault().ElementAt(0).numericValue;
            }
            else if (winType == WinType.ThreeOfAKind)
            {
                return cards.OrderByDescending(x => x.numericValue)
                            .GroupBy(x => x.numericValue)
                            .Where(x => x.Count() > 2)
                            .OrderByDescending(x => x.Count())
                            .FirstOrDefault().ElementAt(0).numericValue;
            }
            return -1; 
        }

        public int GetKicker(int timesCalled)
        {
            if ((winType == WinType.Flush || winType == WinType.HighCard) && timesCalled <= 4) 
            {
                return cards.OrderByDescending(x => x.numericValue)
                            .ElementAt(timesCalled).numericValue;
            }
            else if (winType == WinType.Pair && timesCalled <= 3)
            {
                return cards.OrderByDescending(x => x.numericValue)
                            .GroupBy(x => x.numericValue)
                            .OrderByDescending(x => x.Count())
                            .SelectMany(x => x)
                            .Skip(2)//ignore the first two that were looked at then sort by numeric value (in case there was a pair of low, but a higher single)
                            .OrderByDescending(x => x.numericValue)
                            .ElementAt(timesCalled-1).numericValue;
            }
            else if (winType == WinType.ThreeOfAKind && timesCalled <= 2)
            {
                //redundant as no 2 people would have the same 3 of a kind, but good if wanting to expand to more decks.
                return cards.OrderByDescending(x => x.numericValue)
                            .GroupBy(x => x.numericValue)
                            .OrderByDescending(x => x.Count())
                            .SelectMany(x => x)
                            .Skip(3)
                            .OrderByDescending(x => x.numericValue)
                            .ElementAt(timesCalled-1).numericValue;
            }
            else
            {
                //the times called are too many for each win type, which means that it has searched all 5.
                return -1;
            }
        }
    }
}
