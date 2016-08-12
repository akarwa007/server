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
    public partial class View_Table : UserControl
    {
        ViewModel_Table _vm_table = null;
        public event JoinedTableHandler JoinedTableEvent;
        public View_Table(ViewModel_Table vm_table)
        {
            _vm_table = vm_table;

            InitializeComponent();
        }
        public string UserName
        {
            get; set;
        }
        private void RenderControls()
        {
            if (_vm_table != null)
            {
                
                Label     l1, l2, l3;
                l1 = new Label();
                l2 = new Label();
                l3 = new Label();

                l1.Text = _vm_table.GameName;
                l2.Text = _vm_table.GameValue;
                l3.Text = _vm_table.TableNo;

                l1.Location = new Point(this.Width / 2, this.Height / 2);
                l2.Location = new Point(l1.Location.X, l1.Location.Y + 22);
                l3.Location = new Point(l2.Location.X, l2.Location.Y + 22);

                l1.AutoSize = true;
                l2.AutoSize = true;
                l3.AutoSize = true;

                this.Controls.Add(l1);
                this.Controls.Add(l2);
                this.Controls.Add(l3);
            }

            short seatno = 1;
            View_Seat seat1 = null;
            
            
            ViewModel_Seat vm_seat = _vm_table.get_VM_Seat(seatno);
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
                vm_seat = _vm_table.get_VM_Seat(seatno);
                seat1 = new View_Seat(vm_seat);
                seat1.JoinedTableEvent += Seat_JoinedTableEvent;
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
                seatno++;
            }
            x = seatwidth;
            y = y1 - seatheight*2;
            while (count > 2)
            {
                x = x + seatwidth * 2;
                //y = y + 10;

                vm_seat = _vm_table.get_VM_Seat(seatno);
                seat1 = new View_Seat(vm_seat);
                seat1.JoinedTableEvent += Seat_JoinedTableEvent;
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
                seatno++;
            }
            x = 0;
            y = y1 / 2 - seatheight/2;
            while (count > 0)
            {
                x = x + x1 / 12;
                //y = y + 10;
                vm_seat = _vm_table.get_VM_Seat(seatno);
                seat1 = new View_Seat(vm_seat);
                seat1.JoinedTableEvent += Seat_JoinedTableEvent;
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
                seatno++;
            }
            seat1.Location = new Point(x1 - x1 / 10, seat1.Location.Y);
        }

        private void Seat_JoinedTableEvent(string TableNo, short SeatNo, decimal ChipCounts)
        {
            if (JoinedTableEvent != null)
                JoinedTableEvent.Invoke(TableNo, SeatNo, ChipCounts);
        }


        private void View_Table_Load(object sender, EventArgs e)
        {
            try
            {
                RenderControls();
            }
            catch(Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        private void View_Table_SizeChanged(object sender, EventArgs e)
        {
            this.Controls.Clear();
            
            RenderControls();

        }

     
    }
}
