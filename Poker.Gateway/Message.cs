using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Gateway
{
    public class Message
    {
        MessageType _messageType;
        MessageDirection _messageDirection;

        public Message()
        {
        }
        public String Serialize()
        {
            return "Not implemented";
        }
        public void DeSerialize(String s)
        {
        }
    }
    public enum MessageType
    {
        PlayerJoiningGame,
        PlayerLeavingGame,
        PlayerSigningIn,
        PlayerSigningOut,
        PlayerAction
    }
    public enum MessageDirection
    {
        Incoming,
        Outgoing
    }


}
