using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class ClientPlayerManager
    {
        public ClientPlayerManager()
        {
        }
        public static void RequestAction(Player p)
        { 
            // this will wait with the client player and seek an action 

        }
        public static void SendFlop(Table t, Tuple<Card,Card,Card> flop)
        {
        }
        public static void SendTurn(Table t, Card turn)
        {
        }
        public static void SendRiver(Table t, Card river)
        {
        }
    }
}
