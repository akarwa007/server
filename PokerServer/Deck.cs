using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class Deck
    {
        private Card[] _cards = new Card[52];

        private Deck()
        {
            initialize();
        }
        private void initialize()
        {
            short index = 0;
            foreach (Suit s in Enum.GetValues(typeof(Suit)))              
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    _cards[index++] = new Card(s, r);
                }
            }
        }
        public void PrintDeck()
        {
            short index = 0;
            foreach (Card c in this._cards)
            {
                Console.WriteLine("card at index " + index + "  " + c.Suit.ToString() + "  " + c.Rank.ToString());
                index++;
            }
        }
        public static  Deck GetShuffledDeck()
        {
            Deck deck = new Deck();
            return deck;
        }
        

    }
}
