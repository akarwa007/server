using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Server
{
    public static class ClientConnections
    {
        private static object _locker = new object();
        private static Dictionary<Player, PlayerConnection> _connectionList = new Dictionary<Player, PlayerConnection>();

        public static void AddPlayerConnection(Player p ,PlayerConnection pc)
        {
            lock (_locker)
            {
                _connectionList.Add(p, pc);
            }
        }
        public static void RemovePlayerConnection(Player p, PlayerConnection pc)
        {
            lock (_locker)
            {
                _connectionList.Remove(p);
            }
        }
        public static PlayerConnection GetPlayerConnection(Player p)
        {
            lock (_locker)
            {
                return _connectionList[p];
            }
        }
    }
}
