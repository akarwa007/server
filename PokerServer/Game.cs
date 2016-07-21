﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Server
{
    public class Game
    {
        private Deck _deck;
      
        private Tuple<Card, Card, Card> _flop = null;
        private Card _turn = null;
        private Card _river = null;
        private Tuple<Card, Card, Card, Card, Card> _board = null;
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
         

            return hand;
        }
        public Tuple<Card, Card, Card> GetFlop()
        {
            if (_flop == null)
            { 
            // burn a card
            Card burn = _deck.GetNext();
            Card card1 = _deck.GetNext();
            Card card2 = _deck.GetNext();
            Card card3 = _deck.GetNext();

            Tuple<Card, Card, Card> _flop = new Tuple<Card, Card, Card>(card1, card2, card3);
            
           }
           return _flop;
           
        }

        public Card GetTurn()
        {
            if (_turn == null)
            {
                Card burn = _deck.GetNext();
               _turn = _deck.GetNext();
               
            }
            return _turn;
        }
        public Card GetRiver()
        {
            if (_river == null)
            {
                Card burn = _deck.GetNext();
                _river = _deck.GetNext();
               
                _board = new Tuple<Card, Card, Card, Card, Card>(_flop.Item1, _flop.Item2, _flop.Item3, _turn, _river);
            }
            return _river;
        }
        public Tuple<Card, Card, Card, Card, Card> GetBoard()
        {
            // error checking , cannot call board until the river had been dealt.
            return _board;
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
