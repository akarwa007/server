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
    public partial class View_Table : UserControl
    {
        ViewModel_Table _vm_table = null;
        public View_Table(ViewModel_Table vm_table)
        {
            _vm_table = _vm_table;
            InitializeComponent();
        }
        private void RenderControls()
        {
            View_Seat seat1 = null;
            ViewModel_Seat vm_seat = new ViewModel_Seat();
            int count = 10;
            int x, y;


            int y1 = this.Height;
            int x1 = this.Width;

            int seatwidth = x1 / 13;
            int seatheight = y1 / 6;
            x = seatwidth;
            y = seatheight;
            while (count > 6)
            {
                x = x + seatwidth * 2;
                //y = y + 10;
                vm_seat = new ViewModel_Seat();
                seat1 = new View_Seat(vm_seat);
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
            }
            x = seatwidth;
            y = y1 - seatheight*2;
            while (count > 2)
            {
                x = x + seatwidth * 2;
                //y = y + 10;
                vm_seat = new ViewModel_Seat();
                seat1 = new View_Seat(vm_seat);
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
            }
            x = 0;
            y = y1 / 2 - seatheight/2;
            while (count > 0)
            {
                x = x + x1 / 12;
                //y = y + 10;
                vm_seat = new ViewModel_Seat();
                seat1 = new View_Seat(vm_seat);
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
            }
            seat1.Location = new Point(x1 - x1 / 10, seat1.Location.Y);
        }
        private void View_Table_Load(object sender, EventArgs e)
        {

            RenderControls();
        }

        private void View_Table_SizeChanged(object sender, EventArgs e)
        {
            this.Controls.Clear();
            RenderControls();

        }
    }
}
