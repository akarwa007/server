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

        public event JoinedTableHandler JoinedTableEvent;
        public View_Seat(ViewModel_Seat seat)
        {
            this._vm_seat = seat;
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

                this.splitContainer3.Panel1.Controls.Add(v_card1);
                this.splitContainer3.Panel2.Controls.Add(v_card2);
            }

        }
      
        private void btnJoinLeave_Click(object sender, EventArgs e)
        {// this button click flips the Joined boolean property. 
            if (_vm_seat.Joined)
            {
                _vm_seat.Joined = false;
                this.labelChipsCount.Text = "Empty";
                this.btnJoinLeave.Text = "Join";
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
    }
}
