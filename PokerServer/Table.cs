using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class Table : ITable
    {

        private short _capacity = 9;
        private Dictionary<short, Player> _seats;
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
            _seats = new Dictionary<short, Player>();
        }


        public void AddPlayer(Player p)
        {

            //throw new NotImplementedException();
        }

        public void RemovePlayer(Player p)
        {
            //throw new NotImplementedException();
        }

        public short PlayerCount()
        {
            return 4;
            //throw new NotImplementedException();
        }
    }

    public interface ITable
    {
        void AddPlayer(Player p);
        void RemovePlayer(Player p);
        short PlayerCount();

    }

}
