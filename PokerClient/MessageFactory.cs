﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Shared;

namespace PokerClient
{
    public class MessageFactory
    {
        static PokerUserC _user;
        Dictionary<MessageType, Action<Message>> _dict = new Dictionary<MessageType, Action<Message>>();
        public MessageFactory(PokerUserC user)
        {
            _user = user;
        }
        public  void SendTableJoinMessage(string TableNo,short SeatNo,decimal ChipCount)
        {
            string content = TableNo + ":" + SeatNo + ":" + ChipCount;
            Poker.Shared.Message m = new Poker.Shared.Message(content, MessageType.PlayerJoiningGame);
            if (_user != null)
            {
                _user.SendMessage(m);
            }
        }
        public void RegisterCallback(Action<Message> callback , MessageType messageType)
        {
            try
            {
                if (!_dict.ContainsKey(messageType))
                    _dict[messageType] = callback;
                else
                    _dict[messageType] += callback;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ProcessMessage(Poker.Shared.Message message)
        {
            if (message.MessageType == MessageType.CasinoUpdate)
            {
                _dict[message.MessageType].Invoke(message);
            }
        }

    }

}
