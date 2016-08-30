using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Shared;

namespace Poker.Client.Support
{
    public class ViewModel_Card : BaseViewModel
    {
        public ViewModel_Card(A_Card card)
        {
            Rank = card.Rank;
            Suit = card.Suit;
        }
        public ViewModel_Card()
        {
            Rank = Rank.Blank;
            Suit = Suit.Blank;
        }
        public Rank Rank
        {
            get;set;
        }
        public Suit Suit
        {
            get;set;
        }
        public void Update(A_Card card)
        {
            Rank = card.Rank;
            Suit = card.Suit;
        }

    }
}
