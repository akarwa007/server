using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Client.Support
{
    public class ViewModel_Table : BaseViewModel
    {
        public string GameName {get; set;}
        public string GameValue {get; set;}
        public string TableNo { get; set; }
        public int DealerPosition { get; set; }
       // public List<Tuple<short,string,decimal>> listTable = new List<Tuple<short,string,decimal>>(); // seatno, playername,chipcount
        public ViewModel_Table(string UserName)
        {
            base.UserName = UserName;
        }
        public ViewModel_Table()
        {
           
        }
        /*
        public List<Tuple<short, string, decimal>> TableDetails // seatno , playername , chipcount
        {
            get;
            set;
        }
        */
        public List<ViewModel_Seat> ListOfSeats
        {
            get;
            set;  
        }
        public ViewModel_Seat get_VM_Seat(short seatno)
        {
            foreach(ViewModel_Seat vm_seat in ListOfSeats)
            {
                if (vm_seat.SeatNo == seatno)
                {
                    vm_seat.CurrentUserName = this.UserName;
                    return vm_seat;
                }
            }
            return null;
        }
    }
}
