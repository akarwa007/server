using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Poker.Server
{
    public class Table : ITable
    {

        private short _capacity = 9;
        private Dictionary<Seat, Player> _seats;
        private int _dealerPosition;
        
        private int _currentPosition = 1; // seat numbers start with 1
        public Table()
        {
            initialize();
            Console.WriteLine("default table cons with capacity of " + this._capacity);
        }
        public Table(short capacity)
        {
            if ((capacity < 2) || (capacity > 10))
                throw new Exception("Capacity cannot be less than 2 or greater than 10");
            _capacity = capacity;
            initialize();
            Console.WriteLine("parameterized table cons with capcity of " + this._capacity );
        }
        private void initialize()
        {
            _seats = new Dictionary<Seat, Player>();
            short count = this._capacity;
            short seatno = 1;
            while (count > 0)
            {
                _seats.Add(new Seat(null, this, seatno++), null);
                count++;
            }


        }


        public void AddPlayer(Player p)
        {
            // find next empty seat 
            foreach (Seat seat in _seats.Keys)
            {
                if (seat.IsEmpty())
                {
                    seat.SeatPlayer(p);
                    _seats[seat] = p;
                    break;
                }
            }
           
        }

        public void RemovePlayer(Player p)
        {
           // find player where he is seated and remove him 
            foreach (Seat seat in _seats.Keys)
            {
                if (_seats[seat] == p)
                {
                    _seats[seat] = null;
                    seat.RemovePlayer(p);
                    break;
                }
            }
        }
        public int DealerPosition
        {
            get { return _dealerPosition; }
            set
            {
                int fullseatcount = _seats.Count();

                _dealerPosition = value % fullseatcount;
                _currentPosition = (_dealerPosition + 1) % fullseatcount;
            }
        }
        public int SmallBlind_Position
        {
            get
            {
                int fullseatcount = _seats.Count();
                return (_dealerPosition + 1) % fullseatcount;
            }
        }
        public int BigBlind_Position
        {
            get
            {
                int fullseatcount = _seats.Count();
                return (_dealerPosition + 2) % fullseatcount;
            }
        }
        public int PlayerCount()
        {
            return _seats.Where(a => a.Key.IsEmpty() == false).Count();
        }
        public Player GetNextPlayer() // keeps serving players in a round robin fashion. 
        {
           int fullseatcount = _seats.Count();
           if (fullseatcount < 2)
               throw new Exception("No game possible with less than 2 players seated on a table");
           Seat nextseat = _seats.Where(a => a.Key.SeatNumber == this._currentPosition).FirstOrDefault().Key;

           while (nextseat.IsEmpty())
           {
              this._currentPosition = ( this._currentPosition + 1) % fullseatcount;
              nextseat = _seats.Where(a => a.Key.SeatNumber == this._currentPosition).FirstOrDefault().Key;
           }
            
           return _seats[nextseat];
        }
        public void ResetToSmallBlind()
        {
             this._currentPosition = SmallBlind_Position;
        }
      
    }

    public interface ITable
    {
        void AddPlayer(Player p);
        void RemovePlayer(Player p);
        int PlayerCount();

    }

}
