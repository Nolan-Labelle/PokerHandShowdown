using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    public class Player
    {
        public string name { get; }
        public Hand hand { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.hand = null;
        }

        public Player(string name, Hand hand)
        {
            this.name = name;
            this.hand = hand;
        }

        public void AssignHand(Hand hand, Deck deck)
        {
            this.hand = hand;
            this.hand.DrawCards(deck);
        }

        public bool HasHand()
        {
            return hand == null ? false : true;
        }
    }
}
