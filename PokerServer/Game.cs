using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class Game
    {
        private Deck _deck;
        private List<Tuple<Card, Card>> _playerhands = new List<Tuple<Card, Card>>();
        private Tuple<Card, Card, Card> _flop;
        private Card _turn;
        private Card _river;
        private Tuple<Card, Card, Card, Card, Card> _board;
        private decimal _minStartingChipsPerPlayer;
        private decimal _maxStartingChipsPerPlayer;
        private decimal _potSize = 0;
        public Game(decimal minChips, decimal maxChips)
        {
            if (minChips < 0)
                throw new Exception("Min chips per person cannot be negative");
            _minStartingChipsPerPlayer = minChips;
            _maxStartingChipsPerPlayer = maxChips;
        }
        private void initialize()
        {
            _deck = Deck.GetShuffledDeck();
        }
        public   Tuple<Card,Card> DealPlayerHand()
        {
            Card card1 = _deck.GetNext();
            Card card2 = _deck.GetNext();
            Tuple<Card,Card> hand = new Tuple<Card,Card>(card1,card2);
            _playerhands.Add(hand);

            return hand;
        }
        public Tuple<Card, Card, Card> DealFlop()
        {
            // burn a card
           Card burn =  _deck.GetNext();
           Card card1 = _deck.GetNext();
           Card card2 = _deck.GetNext();
           Card card3 = _deck.GetNext();

           Tuple<Card, Card,Card> flop = new Tuple<Card, Card,Card>(card1, card2,card3);
           _flop = flop;
           return flop;
           
        }

        public Card DealTurn()
        {
            Card burn = _deck.GetNext();
            Card turn = _deck.GetNext();
            _turn = turn;
            return turn;
        }
        public Card DealRiver()
        {
            Card burn = _deck.GetNext();
            Card river = _deck.GetNext();
            _river = river;
            _board = new Tuple<Card, Card, Card, Card, Card>(_flop.Item1, _flop.Item2, _flop.Item3, _turn, _river);
            return river;
        }
        public decimal AddToPot(decimal amount)
        {
            if (amount < 0)
                throw new Exception("Pot amount cannot be negative");
            _potSize += amount;
            return _potSize;
        }
        public decimal PotSize
        {
            get
            {
                return _potSize;
            }
        }
    }
}
