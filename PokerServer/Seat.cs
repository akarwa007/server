using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class Seat : ISeat
    {
        private Player _player;
        private Table _table;

        public Seat(Player p , Table t)
        {
            _player = p;
            _table = t;
        }


        public bool RemovePlayer()
        {
            _player = null;
            return true;
           // throw new NotImplementedException();
        }

        public bool SeatPlayer(Player p)
        {
            _player = p;
            return true;
          //  throw new NotImplementedException();
        }
    }
    public interface ISeat
    {
        bool RemovePlayer();
        bool SeatPlayer(Player p);

    }
   
}
