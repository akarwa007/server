using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Poker.Server
{
    public class Card
    {
        
        public Card(Suit s, Rank r)
        {
            this.Suit = s;
            this.Rank = r;
        }
        public Suit Suit
        {
            get;
            private set;
        }
        public Rank Rank
        {
            get;
            private set;
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
