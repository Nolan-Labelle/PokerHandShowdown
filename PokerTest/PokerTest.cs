using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLibrary;

namespace PokerTest
{
    [TestClass]
    public class PokerTest
    {
        [TestMethod]
        public void Test_DeckUseCard()
        {
            Card nineOfSpades = new Card("9S");
            Card jackOfClubs = new Card("JC");

            Deck deck = new Deck();

            Assert.IsFalse(deck.CardUsed(nineOfSpades));
            Assert.IsFalse(deck.CardUsed(jackOfClubs));

            Assert.IsTrue(deck.UseCard(nineOfSpades));
            Assert.IsFalse(deck.UseCard(nineOfSpades));

            Assert.IsTrue(deck.CardUsed(nineOfSpades));
            Assert.IsFalse(deck.CardUsed(jackOfClubs));

            Assert.IsFalse(deck.PutCardBack(jackOfClubs));
            Assert.IsTrue(deck.PutCardBack(nineOfSpades));

            Assert.IsFalse(deck.CardUsed(nineOfSpades));
            Assert.IsFalse(deck.CardUsed(jackOfClubs));

            Assert.IsTrue(deck.UseCard(nineOfSpades));
            Assert.IsTrue(deck.UseCard(jackOfClubs));

            Assert.IsTrue(deck.CardUsed(nineOfSpades));
            Assert.IsTrue(deck.CardUsed(jackOfClubs));

            deck.ResetDeck();

            Assert.IsFalse(deck.CardUsed(nineOfSpades));
            Assert.IsFalse(deck.CardUsed(jackOfClubs));
        }

        [TestMethod]
        public void Test_CardCompare()
        {
            Card nineOfSpades = new Card("9S");
            Card tenOfClubs = new Card("10C");
            Card sixOfDiamonds = new Card("6D");

            int expectedNineSix = 3;
            int expectedNineTen = -1;
            int expectedTenSix = 4;
            int expectedTenNine = 1;
            int expectedSixNine = -3;
            int expectedSixTen = -4;

            int actual;

            actual = nineOfSpades.Compare(sixOfDiamonds);
            Assert.AreEqual(expectedNineSix, actual);

            actual = nineOfSpades.Compare(tenOfClubs);
            Assert.AreEqual(expectedNineTen, actual);

            actual = tenOfClubs.Compare(sixOfDiamonds);
            Assert.AreEqual(expectedTenSix, actual);

            actual = tenOfClubs.Compare(nineOfSpades);
            Assert.AreEqual(expectedTenNine, actual);

            actual = sixOfDiamonds.Compare(nineOfSpades);
            Assert.AreEqual(expectedSixNine, actual);

            actual = sixOfDiamonds.Compare(tenOfClubs);
            Assert.AreEqual(expectedSixTen, actual);
        }

        [TestMethod]
        public void Test_CardToString()
        {
            string expectedString1 = "AS";
            string expectedString2 = "KC";
            string expectedString3 = "QD";
            string expectedString4 = "JH";
            string expectedString5 = "10S";
            string expectedString6 = "9H";

            Card actualCard = new Card(expectedString1);
            Assert.AreEqual(expectedString1, actualCard.ToString());

            actualCard = new Card(expectedString2);
            Assert.AreEqual(expectedString2, actualCard.ToString());

            actualCard = new Card(expectedString3);
            Assert.AreEqual(expectedString3, actualCard.ToString());
      
            actualCard = new Card(expectedString4);
            Assert.AreEqual(expectedString4, actualCard.ToString());

            actualCard = new Card(expectedString5);
            Assert.AreEqual(expectedString5, actualCard.ToString());

            actualCard = new Card(expectedString6);
            Assert.AreEqual(expectedString6, actualCard.ToString());
        }

        [TestMethod]
        public void Test_HandIsValid()
        {
            Deck deck = new Deck();

            Card[] testCards = new Card[5];
            testCards[0] = new Card("AS");
            testCards[1] = new Card("KC");
            testCards[2] = new Card("QD");
            testCards[3] = new Card("JH");
            testCards[4] = new Card("10S");
            //testCards[5] = new Card("9H");

            Hand hand = new Hand();

            Assert.IsFalse(hand.IsValid(deck));

            hand = new Hand(testCards);

            Assert.IsTrue(hand.IsValid(deck));

            hand.DrawCards(deck);

            Assert.IsFalse(hand.IsValid(deck));
        }

        [TestMethod]
        public void Test_HandToString()
        {
            Deck deck = new Deck();

            Card[] testCards = new Card[5];
            testCards[0] = new Card("AS");
            testCards[1] = new Card("KC");
            testCards[2] = new Card("QD");
            testCards[3] = new Card("JH");
            testCards[4] = new Card("10S");
            //testCards[5] = new Card("9H");

            Hand hand = new Hand(testCards);
            string expected = "AS, KC, QD, JH, 10S";

            Assert.AreEqual(expected, hand.ToString());

            testCards[0] = new Card("AS");
            testCards[1] = new Card("AS");
            testCards[2] = new Card("AS");
            testCards[3] = new Card("AS");
            testCards[4] = new Card("AS");

            hand = new Hand(testCards);
            expected = "AS, AS, AS, AS, AS";

            Assert.AreEqual(expected, hand.ToString());

            testCards[0] = new Card("AS");
            testCards[1] = new Card("AH");
            testCards[2] = new Card("AD");
            testCards[3] = new Card("AC");
            testCards[4] = new Card("2D");

            hand = new Hand(testCards);
            expected = "AS, AH, AD, AC, 2D";

            Assert.AreEqual(expected, hand.ToString());
        }

        [TestMethod]
        public void Test_HandEvaluate()
        {
            Deck deck = new Deck();

            Card[] testCards = new Card[5];
            testCards[0] = new Card("AS");
            testCards[1] = new Card("KS");
            testCards[2] = new Card("QS");
            testCards[3] = new Card("JS");
            testCards[4] = new Card("10S");
            //testCards[5] = new Card("9H");

            Hand hand = new Hand(testCards);
            hand.EvaluateHand();
            WinType expected = WinType.Flush;

            Assert.AreEqual(expected, hand.winType);

            testCards[0] = new Card("AS");
            testCards[1] = new Card("10C");
            testCards[2] = new Card("8D");
            testCards[3] = new Card("3H");
            testCards[4] = new Card("2S");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = WinType.HighCard;

            Assert.AreEqual(expected, hand.winType);

            testCards[0] = new Card("AS");
            testCards[1] = new Card("AH");
            testCards[2] = new Card("4D");
            testCards[3] = new Card("6C");
            testCards[4] = new Card("2D");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = WinType.Pair;

            Assert.AreEqual(expected, hand.winType);

            testCards[0] = new Card("AS");
            testCards[1] = new Card("AH");
            testCards[2] = new Card("AD");
            testCards[3] = new Card("KC");
            testCards[4] = new Card("2D");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = WinType.ThreeOfAKind;

            Assert.AreEqual(expected, hand.winType);
        }

        [TestMethod]
        public void Test_HandHighCard()
        {
            Deck deck = new Deck();

            Card[] testCards = new Card[5];
            testCards[0] = new Card("AS");
            testCards[1] = new Card("KS");
            testCards[2] = new Card("QS");
            testCards[3] = new Card("JS");
            testCards[4] = new Card("10S");
            //testCards[5] = new Card("9H");

            Hand hand = new Hand(testCards);
            hand.EvaluateHand();
            int expected = 14;

            Assert.AreEqual(expected, hand.GetHighCard());

            testCards[0] = new Card("JS");
            testCards[1] = new Card("10C");
            testCards[2] = new Card("8D");
            testCards[3] = new Card("3H");
            testCards[4] = new Card("2S");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = 11;

            Assert.AreEqual(expected, hand.GetHighCard());

            testCards[0] = new Card("6S");
            testCards[1] = new Card("10H");
            testCards[2] = new Card("4D");
            testCards[3] = new Card("6C");
            testCards[4] = new Card("2D");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = 6;

            Assert.AreEqual(expected, hand.GetHighCard());

            testCards[0] = new Card("6S");
            testCards[1] = new Card("10H");
            testCards[2] = new Card("4D");
            testCards[3] = new Card("6C");
            testCards[4] = new Card("10D");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = 10;

            Assert.AreEqual(expected, hand.GetHighCard());

            testCards[0] = new Card("KS");
            testCards[1] = new Card("AH");
            testCards[2] = new Card("AD");
            testCards[3] = new Card("KC");
            testCards[4] = new Card("KD");

            hand = new Hand(testCards);
            hand.EvaluateHand();
            expected = 13;

            Assert.AreEqual(expected, hand.GetHighCard());
        }

        [TestMethod]
        public void Test_HandKicker()
        {
            Deck deck = new Deck();

            Card[] testCards = new Card[5];
            testCards[0] = new Card("AS");
            testCards[1] = new Card("KS");
            testCards[2] = new Card("QS");
            testCards[3] = new Card("JS");
            testCards[4] = new Card("10S");
            //testCards[5] = new Card("9H");

            Hand hand = new Hand(testCards);
            hand.EvaluateHand();

            Assert.AreEqual(13, hand.GetKicker(1));
            Assert.AreEqual(12, hand.GetKicker(2));
            Assert.AreEqual(11, hand.GetKicker(3));
            Assert.AreEqual(10, hand.GetKicker(4));

            testCards[0] = new Card("JS");
            testCards[1] = new Card("10C");
            testCards[2] = new Card("8D");
            testCards[3] = new Card("3H");
            testCards[4] = new Card("2S");

            hand = new Hand(testCards);
            hand.EvaluateHand();

            Assert.AreEqual(10, hand.GetKicker(1));
            Assert.AreEqual(8, hand.GetKicker(2));
            Assert.AreEqual(3, hand.GetKicker(3));
            Assert.AreEqual(2, hand.GetKicker(4));

            testCards[0] = new Card("6S");
            testCards[1] = new Card("10H");
            testCards[2] = new Card("4D");
            testCards[3] = new Card("6C");
            testCards[4] = new Card("2D");

            hand = new Hand(testCards);
            hand.EvaluateHand();

            Assert.AreEqual(10, hand.GetKicker(1));
            Assert.AreEqual(4, hand.GetKicker(2));
            Assert.AreEqual(2, hand.GetKicker(3));

            testCards[0] = new Card("6S");
            testCards[1] = new Card("10H");
            testCards[2] = new Card("4D");
            testCards[3] = new Card("6C");
            testCards[4] = new Card("10D");

            hand = new Hand(testCards);
            hand.EvaluateHand();

            Assert.AreEqual(6, hand.GetKicker(1));
            Assert.AreEqual(6, hand.GetKicker(2));
            Assert.AreEqual(4, hand.GetKicker(3));

            testCards[0] = new Card("KS");
            testCards[1] = new Card("AH");
            testCards[2] = new Card("QD");
            testCards[3] = new Card("KC");
            testCards[4] = new Card("KD");

            hand = new Hand(testCards);
            hand.EvaluateHand();

            Assert.AreEqual(14, hand.GetKicker(1));
            Assert.AreEqual(12, hand.GetKicker(2));
        }

        [TestMethod]
        public void Test_Player()
        {
            Deck deck = new Deck();

            string expectedString1 = "Joe";
            string expectedString2 = "Sally";

            Card[] testCards = new Card[5];
            testCards[0] = new Card("AS");
            testCards[1] = new Card("KS");
            testCards[2] = new Card("QS");
            testCards[3] = new Card("JS");
            testCards[4] = new Card("10S");
            //testCards[5] = new Card("9H");

            Hand hand = new Hand(testCards);

            Player player = new Player(expectedString1);
            Assert.AreEqual(expectedString1, player.name);
            Assert.IsFalse(player.HasHand());

            player.AssignHand(hand, deck);

            Assert.IsTrue(player.HasHand());

            player = new Player(expectedString2, hand);
            Assert.AreEqual(expectedString2, player.name);
            Assert.IsTrue(player.HasHand());

            player.AssignHand(hand, deck);

            Assert.IsTrue(player.HasHand());

        }
    }
}
