using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Shared;
using Poker.Gateway;

namespace Poker.Server
{
    public partial class Form1 : Form
    {
        private object _locker = new object();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            Table t = new Table(4);
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
            TCPConnector conn = new TCPConnector(new Action<String>(AppendTextBox));
            conn.start();
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
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
