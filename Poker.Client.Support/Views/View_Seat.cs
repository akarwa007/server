using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Poker.Shared;

namespace Poker.Client.Support.Views
{
    public partial class View_Seat : UserControl
    {
        private ViewModel_Seat _vm_seat = null;
        Label labelChipsCount;
        Button btnFold;
        Button btnCall;
        Button btnRaise;
       
        //Bitmap A1, A2;
        Color _panelswapcolor = Color.Salmon;
        Color origColor;
        int counter = 0;
        public event JoinedTableHandler JoinedTableEvent;
        public event ReceiveBetHandler ReceiveBetEvent;

        public View_Seat(ViewModel_Seat seat)
        {
            this._vm_seat = seat;
            this.labelChipsCount = new Label();
            this.btnFold = new Button();
            this.btnCall = new Button();
            this.btnRaise = new Button();
            if (this._vm_seat.HoleCard_1 == null)
                this._vm_seat.HoleCard_1 = View_Deck.Instance.GetBackCard();
            if (this._vm_seat.HoleCard_2 == null)
                this._vm_seat.HoleCard_2 = View_Deck.Instance.GetBackCard();

            InitializeComponent();
        }
       
        private void View_Seat_Load(object sender, EventArgs e)
        {

            if (_vm_seat != null)
            {
                
                labelChipsCount.Text = _vm_seat.UserName.ToString(); // add username , then switch to chipcounts

                if (_vm_seat.UserName != "Empty")
                {
                    this.btnJoinLeave.Enabled = false;
                    this.btnJoinLeave.BackColor = Color.Red;
                    this.btnJoinLeave.Text = _vm_seat.ChipCounts.ToString();
                }
                if (_vm_seat.UserName == this._vm_seat.CurrentUserName)
                {
                    this.btnJoinLeave.Enabled = true;
                    this.btnJoinLeave.BackColor = Color.Lime;
                    this._vm_seat.Joined = true;
                    this.btnJoinLeave.Text = "Leave";
                }
                // Add the two hidden buttons for "Fold" , "Continue"
                
                btnFold.Text = "Fold";
                btnCall.Text = "Call";
                btnRaise.Text = "Raise";
                btnFold.Font = new Font(btnFold.Font.FontFamily, 6);
                btnCall.Font = new Font(btnFold.Font.FontFamily, 6);
                btnRaise.Font = new Font(btnFold.Font.FontFamily, 6);
                /*
                btnFold.Size = new Size(this.btnJoinLeave.Width/3,this.btnJoinLeave.Height);
                btnFold.Location = new Point(this.btnJoinLeave.Left, this.btnJoinLeave.Top);
                btnCall.Size = new Size(this.btnJoinLeave.Width / 3, this.btnJoinLeave.Height);
                btnCall.Location = new Point(this.btnFold.Left+this.btnFold.Width, this.btnJoinLeave.Top);
                btnRaise.Size = new Size(this.btnJoinLeave.Width / 3, this.btnJoinLeave.Height);
                btnRaise.Location = new Point(this.btnCall.Left+this.btnCall.Width, this.btnJoinLeave.Top);
                */

                btnFold.Size = new Size(this.btnJoinLeave.Width, this.btnJoinLeave.Height);
                btnFold.Location = new Point(this.Left-this.Width, this.Top+this.Height);
                btnCall.Size = new Size(this.btnJoinLeave.Width, this.btnJoinLeave.Height);
                btnCall.Location = new Point(this.btnFold.Left + this.btnFold.Width, this.Top + this.Height);
                btnRaise.Size = new Size(this.btnJoinLeave.Width, this.btnJoinLeave.Height);
                btnRaise.Location = new Point(this.btnCall.Left + this.btnCall.Width, this.Top + this.Height);

                btnFold.BackColor = Color.LightGreen;
                btnFold.Visible = false;
                btnCall.BackColor = Color.Green;
                btnCall.Visible = false;
                btnRaise.BackColor = Color.LightGreen;
                btnRaise.Visible = false;

                
                btnFold.Click += Fold_Click;
                btnCall.Click += Call_Click;
                btnRaise.Click += Raise_Click;

                this.Parent.Controls.Add(this.btnFold);
                this.Parent.Controls.Add(this.btnCall);
                this.Parent.Controls.Add(this.btnRaise);

                this.btnCall.Visible = false;
                this.btnFold.Visible = false;
                this.btnRaise.Visible = false;

                /*
                this.splitContainer1.Panel2.Controls.Add(this.btnFold);
                this.splitContainer1.Panel2.Controls.Add(this.btnCall);
                this.splitContainer1.Panel2.Controls.Add(this.btnRaise);
                */

                labelChipsCount.Dock = DockStyle.Fill;
                this.splitContainer2.Panel2.Controls.Add(labelChipsCount);
                this.splitContainer2.Dock = DockStyle.Fill;
                this.splitContainer3.Dock = DockStyle.Fill;
            }

        }
        public void Assign_HoleCards(Tuple<A_Card,A_Card> holecards)
        {
            Bitmap a1 = View_Deck.Instance.GetCard(holecards.Item1);
            Bitmap a2 = View_Deck.Instance.GetCard(holecards.Item2);
          

            this._vm_seat.HoleCard_1 = a1;
            this._vm_seat.HoleCard_2 = a2;
            this.Invalidate();
           
        }
        public void SimulateRequestBet(string comment)
        {
            lock (this)
            {
                // Flash the seat with a time ticker
                this.origColor = this.splitContainer2.Panel2.BackColor;
                Console.WriteLine("Inside SimulateRequest for " + this._vm_seat.UserName + " for " + comment);
                
                this.Invoke((MethodInvoker)delegate {
                    counter = 75;
                    timer1.Enabled = true;
                    timer1.Interval = 200;
                    timer1.Start();
                  
                    this.btnCall.Visible = true;
                    this.btnFold.Visible = true;
                    this.btnRaise.Visible = true;
                 
                });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            this.labelChipsCount.Text = counter.ToString();
            if (this.splitContainer2.Panel2.BackColor == Color.Black)

                this.splitContainer2.Panel2.BackColor = Color.Red;
            else
                this.splitContainer2.Panel2.BackColor = Color.Salmon;

            if (counter <= 0)
            {
                StopTimer();
            }
        }
        private void StopTimer()
        {
            if (timer1.Enabled == false)
            return;

            timer1.Stop();
            timer1.Enabled = false;

            this.labelChipsCount.Text = this._vm_seat.UserName;
            this.splitContainer2.Panel2.BackColor = origColor;

            this.btnCall.Visible = false;
            this.btnFold.Visible = false;
            this.btnRaise.Visible = false;


            //this.Parent.Controls.Remove(btnCall);
            //this.Parent.Controls.Remove(btnFold);
            //this.Parent.Controls.Remove(btnRaise);
        }
        private void Fold_Click(object sender, EventArgs e)
        {
            if (this.ReceiveBetEvent != null)
                this.ReceiveBetEvent.Invoke(this._vm_seat.TableNo, -1);
            StopTimer();
        }
        private void Call_Click(object sender, EventArgs e)
        {
            if (this.ReceiveBetEvent != null)
                this.ReceiveBetEvent.Invoke(this._vm_seat.TableNo, 0);
            StopTimer();
        }
        private void Raise_Click(object sender, EventArgs e)
        {
            if (this.ReceiveBetEvent != null)
                this.ReceiveBetEvent.Invoke(this._vm_seat.TableNo, 1);
            StopTimer();
        }
        private void btnJoinLeave_Click(object sender, EventArgs e)
        {// this button click flips the Joined boolean property. 
            if (_vm_seat.Joined)
            {
                _vm_seat.Joined = false;
                this.labelChipsCount.Text = "Empty";
                this.btnJoinLeave.Text = "Join";
                // Leaving the table
                if (JoinedTableEvent != null)
                {
                    JoinedTableEvent.Invoke(this._vm_seat.TableNo, this._vm_seat.SeatNo, -1); // sending negative chipcount to indicate leaving
                }
            }
            else
            {
                _vm_seat.Joined = true;
                this.labelChipsCount.Text = this._vm_seat.CurrentUserName;
                this.btnJoinLeave.Text = "Leave";
                if (JoinedTableEvent != null)
                {
                    JoinedTableEvent.Invoke(this._vm_seat.TableNo, this._vm_seat.SeatNo, this._vm_seat.ChipCounts);
                }
            }
        }

      

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;
            if (this._vm_seat.HoleCard_1 != null)
                e.Graphics.DrawImage(this._vm_seat.HoleCard_1, 0, 0 ,c.Width, c.Height);
        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;
            if (this._vm_seat.HoleCard_1 != null)
                e.Graphics.DrawImage(this._vm_seat.HoleCard_2, 0, 0, c.Width, c.Height);
        }

      
    }

    public class BetEventArgs : EventArgs
    {
        private readonly decimal _betsize;

        public BetEventArgs(decimal betsize)
        {
            _betsize = betsize;
        }

        public decimal BetSize
        {
            get { return _betsize; }
        }
    }
}
