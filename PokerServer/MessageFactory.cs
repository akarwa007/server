using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Shared;

namespace Poker.Server
{
    public class MessageFactory
    {
        public MessageFactory()
        {

        }
        public static void SendCasinoMessage()  
        {
            Poker.Shared.Message m = new Shared.Message("CasinoUpdate", MessageType.CasinoUpdate);
            m.Content = ClientView.CasinoView.Serialize();
            foreach(PokerUser puser in PokerUserFactory.GetListPokerUsers())
            {
                puser.SendMessage(m);
            }
        }
        public static void SendCasinoMessage(PokerUser user)
        {
            Poker.Shared.Message m = new Shared.Message("CasinoUpdate", MessageType.CasinoUpdate);
            m.Content = ClientView.CasinoView.Serialize();
            if ((user != null) && (user.TcpClient != null) && (user.TcpClient.Connected))
            {
                user.SendMessage(m);
            }
           
        }
    }
  
}
