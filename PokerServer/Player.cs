using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class Player
    {
        private static object _synchronize = new object();
        private decimal _chipcount = 0;
        public Player()
        {
        }
        private void initialize()
        {

        }
        public void AddMoney(decimal money)
        {
            lock (Player._synchronize)
            {
                if (money < 0)
                    throw new Exception("Money cannot be negative");
                this._chipcount += money;
            }
        }
        public void RemoveMoney(decimal money)
        {
            lock (Player._synchronize)
            {
                if (money > this._chipcount)
                    throw new Exception("Money to remove is more than chip count");
                this._chipcount -= money;
            }
        }
    }
}
