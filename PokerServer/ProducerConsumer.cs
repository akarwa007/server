using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using Poker.Shared;


namespace Poker.Server
{
    public class ProducerConsumer
    {
        readonly object lockIncoming = new object();
        readonly object lockOutgoing = new object();

        Queue<Message> queueIncoming = new Queue<Message>();
        Queue<Message> queueOutgoing = new Queue<Message>();

        MessageProcessor _MP = new MessageProcessor();
        
        TcpClient _client;
        PokerUser _pokerUser;
        Thread IncomingThread;
        Thread OutgoingThread;

        private Action<String> _funcStream;
        public ProducerConsumer(TcpClient client, PokerUser user)
        {
            _client = client;
            _pokerUser = user;
            init();
        }
        private void init()
        {
           
             IncomingThread = new Thread(
               () => func_incoming(_client));

            IncomingThread.Name = "IncomingThread";
            IncomingThread.Start();

             OutgoingThread = new Thread(
              () => func_outgoing(_client));
            
            OutgoingThread.Name = "OutgoingThread";
            OutgoingThread.Start();
        }
        private void func_incoming(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
           
            while (client.Connected)
            {
                try
                {
                    Message _message;
                    //while ((line = reader.ReadLine()) != null)
                    while ((_message = _MP.Process(reader)) != null)
                    {
                        // Console.WriteLine(line);
                        if (_message == null)
                        {
                            // client has disconnected.
                            client.Close();
                            // cleanup_client(client);
                      
                            break;
                        }
                        ProduceIncoming(_message);
                    
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        private void func_outgoing(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
      
            while (client.Connected)
            {
                        Message m = ConsumeOutgoing();
                        try
                        {
                            sw.WriteLine(m.Serialize());
                            sw.Flush();
                        }
                        catch (System.IO.IOException e)
                        {
                            // this means socket is closed. 
                            // cleanup_client(client);
                          
                            client.Close();
                        }
            }

        }
        public void ProduceIncoming(Message o)
        {
            lock (lockIncoming)
            {
                queueIncoming.Enqueue(o);

                // We always need to pulse, even if the queue wasn't
                // empty before. Otherwise, if we add several items
                // in quick succession, we may only pulse once, waking
                // a single thread up, even if there are multiple threads
                // waiting for items.            
                Monitor.Pulse(lockIncoming);
            }
        }
        public void ProduceOutgoing(Message o)
        {
            lock (lockOutgoing)
            {
                queueOutgoing.Enqueue(o);

                // We always need to pulse, even if the queue wasn't
                // empty before. Otherwise, if we add several items
                // in quick succession, we may only pulse once, waking
                // a single thread up, even if there are multiple threads
                // waiting for items.            
                Monitor.Pulse(lockOutgoing);
            }
        }

        public Message ConsumeIncoming()
        {
            lock (lockIncoming)
            {
                // If the queue is empty, wait for an item to be added
                // Note that this is a while loop, as we may be pulsed
                // but not wake up before another thread has come in and
                // consumed the newly added object. In that case, we'll
                // have to wait for another pulse.
                while (queueIncoming.Count == 0)
                {
                    // This releases listLock, only reacquiring it
                    // after being woken up by a call to Pulse
                    Monitor.Wait(lockIncoming);
                }
                return queueIncoming.Dequeue();
            }
        }
        public Message ConsumeOutgoing()
        {
            lock (lockOutgoing)
            {
                // If the queue is empty, wait for an item to be added
                // Note that this is a while loop, as we may be pulsed
                // but not wake up before another thread has come in and
                // consumed the newly added object. In that case, we'll
                // have to wait for another pulse.
                while (queueOutgoing.Count == 0)
                {
                    // This releases listLock, only reacquiring it
                    // after being woken up by a call to Pulse
                    Monitor.Wait(lockOutgoing);
                }
                return queueOutgoing.Dequeue();
            }
        }
    }
}
