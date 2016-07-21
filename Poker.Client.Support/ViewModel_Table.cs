using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Client.Support
{
    public class ViewModel_Table
    {
        public string GameName {get; set;}
        public string GameValue {get; set;}
        public string TableNo { get; set; }
        public int DealerPosition { get; set; }
       // public List<Tuple<short,string,decimal>> listTable = new List<Tuple<short,string,decimal>>(); // seatno, playername,chipcount
        public ViewModel_Table()
        {
          
        }
        public List<Tuple<short, string, decimal>> TableDetails // seatno , playername , chipcount
        {
            get;
            set;
        }
    }
}
