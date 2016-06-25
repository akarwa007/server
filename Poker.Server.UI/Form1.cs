using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Poker.Shared;
using Poker.Gateway;
using Poker.Common;

namespace Poker.Server
{
    public partial class Form1 : Form
    {
        private object _locker = new object();
        private bool _ServerStarted = false;
        TCPConnector conn = null;
        Dictionary<string,AsyncCallback> _listOfCallbacks = new Dictionary<string,AsyncCallback>();
        
        public Form1()
        {
            TableManager.Instance.CreateTable(10);
            InitializeComponent();
        }
        private bool ServerStarted
        {
            get
            {
                return _ServerStarted;
            }
            set
            {
                _ServerStarted = value;
                if (_ServerStarted)
                    this.btnStartServer1.Text = "Stop Server";
                else
                    this.btnStartServer1.Text = "Start Server";

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //Table t = new Table(4);
            Deck d = Deck.GetShuffledDeck();
            Tuple<Card, Card, Card, Card, Card> hand = new Tuple<Card, Card, Card, Card, Card>(
                                                        new Card(Suit.Diamond,Rank.Nine),
                                                        new Card(Suit.Club,Rank.King),
                                                        new Card(Suit.Club,Rank.Queen),
                                                        new Card(Suit.Club,Rank.Jack),
                                                        new Card(Suit.Club,Rank.Ten)
                                                        );
            HandRankings h = new HandRankings(hand);
            Console.WriteLine(d.serialize());

          
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void btnStartServer1_Click(object sender, EventArgs e)
        {  
            createplayer_callback callback1  = new createplayer_callback(PlayerFactory.Instance.CreatePlayer);
            // _listOfCallbacks.Add("createplayer",
            if (!this.ServerStarted)
            {// Start the Server
                conn = new TCPConnector(new Action<String>(AppendTextBox), callback1);
                conn.start();
                
                this.ServerStarted = true;
            }
            else
            {// Stop the server
                if (conn != null)
                {
                    conn.stop();
                    this.ServerStarted = false;
                }

            }
        }
        public void AppendTextBox(string value)
        {
          
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                    return;
                }
               // this.textBox1.Text += value;
                this.textBox1.AppendText(value);
                this.textBox1.AppendText(Environment.NewLine);
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btSendCasinoUpdate_Click(object sender, EventArgs e)
        {
            
            Poker.Shared.Message m = new Shared.Message("CasinoUpdate", MessageType.CasinoUpdate);
            m.Content = ClientView.CasinoView.Serialize();
            Console.WriteLine(m.Content);
            if (conn != null)
            {
                lock (conn.Outgoing)
                {
                    foreach(var x in conn.Outgoing.Values)
                    {
                        lock (x)
                        {
                            x.Enqueue(m);
                            Monitor.PulseAll(x);
                        }
                    }
                }
            }
        }

      
    }
}
