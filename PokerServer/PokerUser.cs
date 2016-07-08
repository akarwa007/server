using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Poker.Shared;

namespace Poker.Server
{
    
    public class PokerUser
    {
        public PokerUser()
        {

        }
        internal TcpClient TcpClient
        {
            get;set;
        }
        public List<Player> PlayerInstances
        {
            get;set;
        }
        public Queue<Message> Incoming
        {
            get;set;
        }
        public Queue<Message> Outgoing
        {
            get;set;
        }
        public decimal TotalChips
        {
            get;set;
        }
        public decimal ChipsAvailable
        {
            get;set;
        }
    }

    
}
