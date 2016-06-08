using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Shared
{
    public abstract class A_Card
    {
        public A_Card()
        {
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
