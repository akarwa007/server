using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.IO;
using Poker.Shared;
using Poker.Common;

namespace Poker.Gateway
{
    public class TCPConnector
    {
        private bool listen = true;
        TcpListener listener = null;
        private object _locker_writer_queue = new object();
        private object _locker_reader_queue = new object();

       
        private Dictionary<TcpClient, Thread> _clients_readers = new Dictionary<TcpClient,Thread>();
        private Dictionary<TcpClient, Thread> _clients_writers = new Dictionary<TcpClient,Thread>();
        private Dictionary<TcpClient, Queue<Message>> _writer_queue = new Dictionary<TcpClient, Queue<Message>>();
        private Dictionary<TcpClient, Queue<Message>> _reader_queue = new Dictionary<TcpClient, Queue<Message>>();


        private MessageProcessor _MP = new MessageProcessor();
        
        private Action<Message> _funcStream;
        private Action<TcpClient> _func1;
        private createpokeruser_callback _callback_createpokeruser;
        public TCPConnector(Action<Message> func, Action<TcpClient> func1 ,  createpokeruser_callback callback1)
        {
            _funcStream = func;
            _func1 = func1;
            _callback_createpokeruser = callback1;
        }
        public void start()
        {
            Thread t = new Thread(startasync);
            t.Start();
        }
        public void stop()
        {
            this.listen = false;
            // Do a dummy connection to the listener to unblock it. 
            TcpClient client = new TcpClient("localhost", 8113);

        }
        public void cleanup_client(TcpClient client)
        {
            
            _clients_readers[client].Abort();
            _clients_readers.Remove(client);
            _clients_writers[client].Abort();
            _clients_writers.Remove(client);
            _writer_queue.Remove(client);
            _reader_queue.Remove(client);
        }
      
        private void startasync()
        {
            System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(ipAddress,8113);
            listener.Start(100);
            _funcStream(new Message("Server Started Successfully",MessageType.GeneralPurpose));
            while (listen)
            {
                TcpClient client = listener.AcceptTcpClient();
                
                forknewthread(client);
                Console.WriteLine("client connected");
            }
            if (listener != null)
                listener.Stop();
            _funcStream(new Message("Stopping the listening",MessageType.GeneralPurpose));
            Console.WriteLine("Stopping the listening");
        }
        private void forknewthread(TcpClient client)
        {
            _funcStream(new Message("Incoming new client request", MessageType.GeneralPurpose));
            /*
            Thread readerthread = new Thread(
               () => func_reader(client));
            _clients_readers.Add(client,readerthread);
            readerthread.Name = "ReaderThread";
            readerthread.Start();

            Thread writerthread = new Thread(
              () => func_writer(client));
            _clients_writers.Add(client, writerthread);
            writerthread.Name = "WriterThread";
            writerthread.Start();
            */
            //create a pokeruser 
            _callback_createpokeruser(client,_funcStream, "", "");
        }
        private void func_reader(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            MemoryStream ms = new MemoryStream();
            Queue<Message> _queue = null;
            lock (_locker_reader_queue)
            {
                if (!_reader_queue.ContainsKey(client))
                {
                    _queue = new Queue<Message>();
                    _reader_queue.Add(client, _queue);
                }
                else
                {
                    _queue = _reader_queue[client];
                }
            }
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
                            _func1(client);
                            break;
                        }
                        lock (_queue)
                        {
                            _queue.Enqueue(_message);
                            if (_message.MessageType == MessageType.PlayerSigningIn)
                            {
                                string[] arr = _message.Content.Split(':');
                                _callback_createpokeruser(client,_funcStream,arr[0], arr[1]);
                                Respond(_message, "Player Created", client);
                            }
                            else
                            {
                                Respond(_message, "Server says Action is good", client);
                            }
                            _funcStream(_message);
                           
                            Monitor.PulseAll(_queue);                         
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        private void Respond(Poker.Shared.Message message, string update , TcpClient client)
        {
            message.Content = update;
            
            Queue<Message> _queue = null;
            lock (_locker_writer_queue)
            {
                if (!_writer_queue.ContainsKey(client))
                {
                    _queue = new Queue<Message>();
                    _writer_queue.Add(client, _queue);
                }
                else
                {
                    _queue = _writer_queue[client];
                }
            }
            lock (_queue)
            {
                _queue.Enqueue(message);
                Monitor.PulseAll(_queue);
            }

        }
        private void func_writer(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            Queue<Message> _queue = null;
            lock (_locker_writer_queue)
            {
                if (!_writer_queue.ContainsKey(client))
                {
                    _queue = new Queue<Message>();
                    _writer_queue.Add(client, _queue);
                }
                else
                {
                    _queue = _writer_queue[client];
                }
            }
            while (client.Connected)
            {
                lock (_queue)
                {
                    while (_queue.Count == 0)
                    {
                        Monitor.Wait(_queue);
                    }
                    while (_queue.Count > 0)
                    {
                        Message message = _queue.Dequeue();
                        try
                        {
                            sw.WriteLine(message.Serialize());
                            sw.Flush();
                        }
                        catch (System.IO.IOException e)
                        {
                            // this means socket is closed. 
                            // cleanup_client(client);
                            _func1(client);
                            client.Close();
                        }
                        
                    }
                }
            }

        }
        public Dictionary<TcpClient, Queue<Message>> Incoming
        {
            get
            {
                return _reader_queue;
            }
        }
        public Dictionary<TcpClient, Queue<Message>> Outgoing
        {
            get
            {
                return _writer_queue;
            }
        }
    }
}
