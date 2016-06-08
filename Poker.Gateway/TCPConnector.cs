using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.IO;

namespace Poker.Gateway
{
    public class TCPConnector
    {
        private bool listen = true;
        private object _locker_queue = new object();

        private Dictionary<TcpClient, Thread> _clients_readers = new Dictionary<TcpClient,Thread>();
        private Dictionary<TcpClient, Thread> _clients_writers = new Dictionary<TcpClient,Thread>();
        private Dictionary<TcpClient, Queue<Message>> _writer_queue = new Dictionary<TcpClient, Queue<Message>>();

        private MessageProcessor _MP = new MessageProcessor();

        public TCPConnector()
        {

        }
        public void start()
        {
            Thread t = new Thread(startasync);
            t.Start();
        }
        private void startasync()
        {
            TcpListener listener = new TcpListener(8113);
            listener.Start(100);
            while (listen)
            {
                TcpClient client = listener.AcceptTcpClient();
                forknewthread(client);
                Console.WriteLine("client connected");
            }
            Console.WriteLine("Done listening");
        }
        private void forknewthread(TcpClient client)
        {
            Thread readerthread = new Thread(
               () => func_reader(client));
            _clients_readers.Add(client,readerthread);

            readerthread.Start();

            Thread writerthread = new Thread(
              () => func_writer(client));
            _clients_writers.Add(client, writerthread);

            writerthread.Start();
        }
        private void func(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

        }
        private void func_reader(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            while (client.Connected)
            {
                String message = sr.ReadToEnd();
                _MP.Process(message);
            }

        }
        private void func_writer(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            Queue<Message> _queue = null;
            lock (_locker_queue)
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
                        sw.Write(message.Serialize());
                    }
                }
            }

        }
    }
}
