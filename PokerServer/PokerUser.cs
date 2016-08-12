using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Poker.Shared;
using Poker.Common;

namespace Poker.Server
{
    
    public class PokerUser
    {
        ProducerConsumer _producerconsumer;
        Action<Message> _incomingmessage_callback;
        public PokerUser()
        {

        }
        public PokerUser(TcpClient client, Action<Message> incomingmessage_callback ,  string username)
        {
            TcpClient = client;
            _incomingmessage_callback = incomingmessage_callback;
            UserName = username;
            validate();
            init();
        }
        private bool validate() // this function validates the state of the PokerUser class.
        {
            return false;
        }
        private void init()
        {
            _producerconsumer = new ProducerConsumer(TcpClient, this);
            RegisterForIncomingMessages(new RecieveMessageDelegate(ProcessIncomingMessage));
            RegisterForIncomingMessages(new RecieveMessageDelegate(_incomingmessage_callback));
            MessageFactory.SendCasinoMessage(this);
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
        public string UserName
        {
            get;
            set;
        }
        private void ProcessIncomingMessage(Message m)
        {
            if (m != null)
            {
                if (m.MessageType == MessageType.PlayerSigningIn)
                {
                    string[] arr = m.Content.Split(':');
                    UserName = arr[0];
                }
            }
        }
        public void SendMessage(Message m)
        {
            _producerconsumer.ProduceOutgoing(m);
        }
        public void RegisterForIncomingMessages(RecieveMessageDelegate handler)
        {
            _producerconsumer.ReceieveMessageHandler += handler;
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
