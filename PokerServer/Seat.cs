using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Poker.Server
{
    public class Seat : ISeat
    {
        private Player _player;
        private Table _table;
        private short _seatno;

        public Seat(Player p , Table t, short SeatNum)
        {
            _player = p;
            _table = t;
            _seatno = SeatNum;
            
        }

        public short SeatNumber
        {
            get { return _seatno; }
        }
        public bool RemovePlayer(Player p)
        {
            if (_player == p)
            {
                _player = null;
                return true;
            }
            throw new Exception("Trying to remove a player not seated on the seat");
            
           // throw new NotImplementedException();
        }

        public bool SeatPlayer(Player p)
        {
            _player = p;
            return true;
          //  throw new NotImplementedException();
        }
        public bool IsEmpty()
        {
            return (_player == null);
        }
    
    }
    public interface ISeat
    {
        bool RemovePlayer(Player p);
        bool SeatPlayer(Player p);

    }
   
}
