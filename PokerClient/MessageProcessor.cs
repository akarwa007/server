using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Poker.Shared;

namespace PokerClient
{

    public class MessageProcessor
    {
        public MessageProcessor()
        {
        }

        public static Message Process(StreamReader reader )
        {
          
            String line = reader.ReadLine();
            Poker.Shared.Message m = Poker.Shared.Message.DeSerialize(line);
        
            return m;

        }
    }

}
