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

        private ViewModel_Card _flop1, _flop2, _flop3, _turn, _river;
        private ViewModel_Card blank = new ViewModel_Card();
        private Tuple<ViewModel_Card, ViewModel_Card, ViewModel_Card, ViewModel_Card, ViewModel_Card> _board;
        // public List<Tuple<short,string,decimal>> listTable = new List<Tuple<short,string,decimal>>(); // seatno, playername,chipcount
        public ViewModel_Table(string UserName)
        {
            base.UserName = UserName;
        }
        public ViewModel_Table()
        {
           
        }
      
        public List<ViewModel_Seat> ListOfSeats
        {
            get;
            set;  
        }
        public Tuple<ViewModel_Card, ViewModel_Card, ViewModel_Card, ViewModel_Card, ViewModel_Card> Board
        {
            get
            {
                return _board;
            }
            set
            {
                _board = value;
            }
        }
        public ViewModel_Card Flop1
        {
            get
            {
                if (Board != null)
                    return Board.Item1;
                if (_flop1 == null)
                    return blank;
                return _flop1;
            }
            set
            {
                _flop1 = value;
            }
           
        }
        public ViewModel_Card Flop2
        {
            get
            {
                if (Board != null)
                    return Board.Item2;
                if (_flop2 == null)
                    return blank;
                return _flop2;
            }
            set
            {
                _flop2 = value;
            }
        }
        public ViewModel_Card Flop3
        {
            get
            {
                if (Board != null)
                    return Board.Item3;
                if (_flop3 == null)
                    return blank;
                return _flop3;
            }
            set
            {
                _flop3 = value;
            }
        }

        public ViewModel_Card Turn
        {
            get
            {
                if (Board != null)
                    return Board.Item4;
                if (_turn == null)
                    return blank;
                return _turn;
            }
            set
            {
                _turn = value;
            }
        }
        public ViewModel_Card River
        {
            get
            {
                if (Board != null)
                    return Board.Item5;
                if (_river == null)
                    return blank;
                return _river;
            }
            set
            {
                _river = value;
            }
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
