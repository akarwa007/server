using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Poker.Client.Support;
using Poker.Client.Support.Views;
using Poker.Shared;

namespace PokerClient
{
    public partial class TestPlayer : UserControl
    {
        bool _Connected = false;
        Poker.Shared.Message _message;
        Queue<Poker.Shared.Message> _queue_outgoing = new Queue<Poker.Shared.Message>();
        Queue<Poker.Shared.Message> _queue_incoming = new Queue<Poker.Shared.Message>();
        List<Poker.Shared.Message> _queue_pending = new List<Poker.Shared.Message>();

        ShellForm _shellform;
        ViewModel_Casino _casinoModel;
        View_Casino _casinoView;
        public TestPlayer()
        {
            init();
            InitializeComponent();
        }
        private void init()
        {
            _shellform = new ShellForm();
            _casinoModel = new ViewModel_Casino();
            _casinoView = new View_Casino();
            _casinoView.UpdateModel(_casinoModel);
            _shellform.Controls.Add(_casinoView);
        }

        private bool Connected
        {
            get
            {
                return _Connected;
            }
            set
            {
                _Connected = value;
                if (_Connected)
                    btnConnect.Text = "Disconnect";
                else
                    btnConnect.Text = "Connect";
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient("localhost", 8113);

            if (client.Connected)
            {
                this.Connected = true;
                forknewthread(client);
            }
            
        }
        private void forknewthread( TcpClient client)
        {
            Thread readerthread = new Thread(
               () => func_reader(this.txtReceiever, client));

            readerthread.Start();

            Thread writerthread = new Thread(
              () => func_writer(client));

            writerthread.Start();
        }
        private Poker.Shared.Message GetMatch(Poker.Shared.Message message)
        {
            // go through pending queue 
            if (_queue_pending.Count > 0)
            {
                var match = _queue_pending.Where(x => x.MessageID == message.MessageID).FirstOrDefault();
                if (match != null)
                {
                    _queue_pending.Remove(match);
                }
                return match;
            }
            return null;
        }
        private void func_reader(RichTextBox txtbox, TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            while (client.Connected)
            {
                
                Poker.Shared.Message message;
                while ((message = MessageProcessor.Process(reader)) != null)
                {
                   // AppendTextBox(txtbox, message);
                    //check for callback
                    Poker.Shared.Message matching = GetMatch(message);
                    if (matching != null)
                    {
                        matching.Callback(message);
                    }
                    else
                    {
                        AppendTextBox(this.txtReceiever, message);
                        ProcessMessage(message);
                    }

                }
               

            }

        }
        private void ProcessMessage(Poker.Shared.Message message)
        {
            if (message.MessageType == MessageType.CasinoUpdate)
            {
                ViewModel_Casino vm = JsonConvert.DeserializeObject<ViewModel_Casino>(message.Content);
                _casinoView.UpdateModel(vm);
            }
        }
        public void AppendTextBox(RichTextBox txtbox, Poker.Shared.Message value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RichTextBox, Poker.Shared.Message>(AppendTextBox), new object[] { txtbox, value });
                return;
            }
            txtbox.Text += value.Content;
        }
        private void func_writer( TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
          
            while (client.Connected)
            {
                lock (_queue_outgoing)
                {
                    while (_queue_outgoing.Count == 0)
                    {
                        Monitor.Wait(_queue_outgoing);
                    }
                    while (_queue_outgoing.Count > 0)
                    {
                        Poker.Shared.Message message = _queue_outgoing.Dequeue();
                        if (message.Callback != null) // Should first ensure that message has been sent and then put in pending queue.
                            _queue_pending.Add(message);
                        string jsonString = message.Serialize();

                        sw.WriteLine(jsonString);
                        sw.Flush();
                        
                    }
                }
            }


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            lock (_queue_outgoing)
            {
                _queue_outgoing.Enqueue(_message);
                Monitor.PulseAll(_queue_outgoing);
            }
        }

        private void TestPlayer_Load(object sender, EventArgs e)
        {
            /*
            if (this.Connected)
                btnConnect.Text = "Disconnect";
            else
                btnConnect.Text = "Connect";
              */
           
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerSigningIn:" + this.txtUsername.Text;
            string content = txtUsername.Text + ":" + txtPassword.Text;
            _message = new Poker.Shared.Message(content, MessageType.PlayerSigningIn);
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerSigningOut:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerSigningOut);
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerJoiningGame:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerJoiningGame);
        }

        private void btnLeaveGame_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerLeavingGame:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerLeavingGame);
        }
        

        private void btnAction_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerAction:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerAction);
            _message.Callback += new AsyncCallback(ActionCallBack);
        }
        private void ActionCallBack(IAsyncResult result)
        {
            Poker.Shared.Message message = (Poker.Shared.Message)result;
          //  this.txtReceiever.AppendText(message.Content + Environment.NewLine);
            AppendTextBox(this.txtReceiever, message);
        }

        private void btnCasino_Click(object sender, EventArgs e)
        {
            _shellform.Show();
            
        }

    }
}
