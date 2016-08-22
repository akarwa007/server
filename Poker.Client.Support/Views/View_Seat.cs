using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Shared;

namespace Poker.Client.Support.Views
{
    public partial class View_Seat : UserControl
    {
        private ViewModel_Seat _vm_seat = null;
        Label labelChipsCount;
        Button btnFold;
        Button btnContinue;
        Bitmap A1, A2;

        public event JoinedTableHandler JoinedTableEvent;
        public View_Seat(ViewModel_Seat seat)
        {
            this._vm_seat = seat;
            A1 = View_Deck.Instance.GetCard(new A_Card(Rank.Ace, Suit.Club));
            A2 = View_Deck.Instance.GetCard(new A_Card(Rank.King, Suit.Heart));

            InitializeComponent();
        }
       
        private void View_Seat_Load(object sender, EventArgs e)
        {

            if (_vm_seat != null)
            {
                labelChipsCount = new Label();
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
                btnFold = new Button();
                btnContinue = new Button();
                btnFold.Text = "Fold";
                btnContinue.Text = "Continue";
                btnFold.Size = new Size(this.btnJoinLeave.Width/2,this.btnJoinLeave.Height);
                btnContinue.Size = new Size(this.btnJoinLeave.Width / 2, this.btnJoinLeave.Height);
                btnFold.Location = new Point(this.btnJoinLeave.Left, this.btnJoinLeave.Top);
                btnContinue.Location = new Point(btnFold.Left + btnFold.Width, btnFold.Top);

                btnFold.BackColor = Color.Green;
                btnContinue.BackColor = Color.ForestGreen;
                btnFold.Visible = false;
                btnContinue.Visible = false;
                //this.Controls.Add(btnFold);
                //this.Controls.Add(btnContinue);

                this.splitContainer1.Panel2.Controls.Add(this.btnFold);
                this.splitContainer1.Panel2.Controls.Add(this.btnContinue);


                labelChipsCount.Dock = DockStyle.Fill;
                this.splitContainer2.Panel2.Controls.Add(labelChipsCount);
                this.splitContainer2.Dock = DockStyle.Fill;
                this.splitContainer3.Dock = DockStyle.Fill;

                View_Card v_card1 = new View_Card();
                v_card1.Dock = DockStyle.Fill;
                v_card1.AutoSize = false;
                View_Card v_card2 = new View_Card();
                v_card2.Dock = DockStyle.Fill;
                v_card2.AutoSize = false;

               // this.splitContainer3.Panel1.Controls.Add(v_card1);
                //this.splitContainer3.Panel2.Controls.Add(v_card2);
            }

        }
        public void Assign_HoleCards(Tuple<A_Card,A_Card> holecards)
        {
            Bitmap a1 = View_Deck.Instance.GetCard(holecards.Item1);
            Bitmap a2 = View_Deck.Instance.GetCard(holecards.Item2);
          

            A1 = a1;
            A2 = a2;
           
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

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;
            if (A1 != null)
                e.Graphics.DrawImage(A1, 0, 0 ,c.Width, c.Height);
        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;
            if (A2 != null)
                e.Graphics.DrawImage(A2, 0, 0, c.Width, c.Height);
        }
    }
}
