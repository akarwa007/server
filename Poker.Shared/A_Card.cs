using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Poker.Shared
{
    public  class A_Card
    {
        public A_Card(Rank r, Suit s)
        {
            Rank = r;
            Suit = s;
        }
        public Suit Suit
        {
            get;
            protected set;
        }
        public Rank Rank
        {
            get;
            protected set;
        }
        public string Serialize()
        {
            string jsonString = JsonConvert.SerializeObject(this);
            return jsonString;
        }
    }
    public enum Suit
    {
        Spade,
        Heart,
        Club,
        Diamond
    }
    public enum Rank
    {
        Ace = 0,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}
