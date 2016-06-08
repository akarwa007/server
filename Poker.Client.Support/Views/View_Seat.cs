using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Client.Support.Views
{
    public partial class View_Seat : UserControl
    {
        private ViewModel_Seat _vm_seat = null;
        public View_Seat(ViewModel_Seat seat)
        {
            this._vm_seat = seat;
            InitializeComponent();
        }

        private void View_Seat_Load(object sender, EventArgs e)
        {
            View_Card v_card = new View_Card();
            v_card.Dock = DockStyle.Fill;
            v_card.AutoSize = false;
            this.Controls.Add(v_card);
        }
    }
}
