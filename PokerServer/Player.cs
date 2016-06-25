﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Poker.Server
{
    public class Player
    {
        private static object _synchronize = new object();
        private decimal _chipcount = 0;
        private Tuple<Card, Card> _holecards;
        private bool _playerStillInHand = false;
        private string _username;
        public Player(string username)
        {
            _username = username;
        }
        private void initialize()
        {

        }
        public string UserName
        {
            get
            {
                return _username;
            }
        }
        public decimal ChipCount
        {
            get
            {
                return _chipcount;
            }
        }
        public string  serialize()
        {
            var string1 = "";
            return string1;
        }
        public void AssignHoleCards(Tuple<Card,Card> holecards)
        {
            _holecards = holecards;
            _playerStillInHand = true;
        }
        public bool InHand
        {
            get
            {
                return _playerStillInHand;
            }
        }
        public void FoldHand()
        {
            _playerStillInHand = false;
        }
        public void Bet(decimal betamount)
        {
            RemoveMoney(betamount);
        }
        public void AddMoney(decimal money)
        {
            lock (Player._synchronize)
            {
                if (money < 0)
                    throw new Exception("Money cannot be negative");
                this._chipcount += money;
            }
        }
        public void RemoveMoney(decimal money)
        {
            lock (Player._synchronize)
            {
                if (money > this._chipcount)
                    throw new Exception("Money to remove is more than chip count");
                this._chipcount -= money;
            }
        }
    }
}
