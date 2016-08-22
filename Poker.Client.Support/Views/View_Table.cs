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
using Poker.Shared.Utils;

namespace Poker.Client.Support.Views
{
    public partial class View_Table : UserControl
    {
        ViewModel_Table _vm_table = null;
        public event JoinedTableHandler JoinedTableEvent;
        View_Card VCard_flop1, VCard_flop2, VCard_flop3, VCard_turn, VCard_river;
        Label lflop1, lflop2, lflop3, lturn, lriver;
        short myseat = -1; // means not occupying any seat
        Dictionary<short, View_Seat> Dict_View_Seats = new Dictionary<short, View_Seat>();
        public View_Table(ViewModel_Table vm_table)
        {
            _vm_table = vm_table;

            InitializeComponent();
        }

        private void View_Table_Paint(object sender, PaintEventArgs e)
        {
            return;
            SolidBrush sb = new SolidBrush(Color.DarkGreen);
            Point point;
            Size size;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is View_Seat)
                {
                     point = new Point(ctrl.Left - 1, ctrl.Top - 1);
                     size = new Size(ctrl.Width + 1, ctrl.Height + 1);
                   // e.Graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(point, size));
                    // e.Graphics.FillEllipse(sb, new Rectangle(point, size));
                }
            }
            
            //e.Graphics.DrawRectangle(SystemPens.ControlDark,new Rectangle(point,size));
           // e.Graphics.FillEllipse(sb, new Rectangle(point,size));
        }

        public string UserName
        {
            get
            {
                return _vm_table.UserName;
            }
            set { }
        }
        private void RenderControls()
        {
            int x, y;

            int y1 = this.Height;
            int x1 = this.Width;

            int seatwidth = x1 / 13;
            int seatheight = y1 / 6;

            x = seatwidth;
            y = seatheight;

            if (_vm_table != null)
            {
                int delta_width = seatwidth / 4;
                int delta_shift = seatwidth / 2;

                this.VCard_flop1 = new View_Card();
                this.VCard_flop1.Size = new Size(this.Width / 13, this.Height / 6);
                int seatloc_1_x = delta_shift + seatwidth * 3;
                this.VCard_flop1.Location = new Point(seatloc_1_x, y1/2 - seatheight/2);
                this.Controls.Add(VCard_flop1);

                
                this.VCard_flop2 = new View_Card();
                this.VCard_flop2.Size = new Size(this.Width / 13, this.Height / 6);
                int seatloc_2_x = seatloc_1_x + seatwidth + delta_width;
                this.VCard_flop2.Location = new Point(seatloc_2_x, y1 / 2 - seatheight / 2);
                this.Controls.Add(VCard_flop2);

                this.VCard_flop3 = new View_Card();
                this.VCard_flop3.Size = new Size(this.Width / 13, this.Height / 6);
                int seatloc_3_x = seatloc_2_x + seatwidth + delta_width;
                this.VCard_flop3.Location = new Point(seatloc_3_x, y1 / 2 - seatheight / 2);
                this.Controls.Add(VCard_flop3);

                this.VCard_turn = new View_Card();
                this.VCard_turn.Size = new Size(this.Width / 13, this.Height / 6);
                int seatloc_4_x = seatloc_3_x + seatwidth + delta_width;
                this.VCard_turn.Location = new Point(seatloc_4_x, y1 / 2 - seatheight / 2);
                this.Controls.Add(VCard_turn);

                this.VCard_river = new View_Card();
                this.VCard_river.Size = new Size(this.Width / 13, this.Height / 6);
                int seatloc_5_x = seatloc_4_x + seatwidth + delta_width;
                this.VCard_river.Location = new Point(seatloc_5_x, y1 / 2 - seatheight / 2);
                this.Controls.Add(VCard_river);

                int yy = y1 / 2 - seatheight / 2 + seatheight + seatheight/6;
                lflop1 = new Label();
                lflop1.Text = "flop";
                lflop1.Width = seatwidth;
                lflop1.TextAlign = ContentAlignment.MiddleCenter;
                lflop1.Location = new Point(seatloc_1_x, yy);
                this.Controls.Add(lflop1);

                lflop2 = new Label();
                lflop2.Text = "flop";
                lflop2.Width = seatwidth;
                lflop2.TextAlign = ContentAlignment.MiddleCenter;
                lflop2.Location = new Point(seatloc_2_x, yy);
                this.Controls.Add(lflop2);

                lflop3 = new Label();
                lflop3.Text = "flop";
                lflop3.Width = seatwidth;
                lflop3.TextAlign = ContentAlignment.MiddleCenter;
                lflop3.Location = new Point(seatloc_3_x, yy);
                this.Controls.Add(lflop3);

                lturn = new Label();
                lturn.Text = "turn";
                lturn.Width = seatwidth;
                lturn.TextAlign = ContentAlignment.MiddleCenter;
                lturn.Location = new Point(seatloc_4_x, yy);
                this.Controls.Add(lturn);

                lriver = new Label();
                lriver.Text = "river";
                lriver.Width = seatwidth;
                lriver.TextAlign = ContentAlignment.MiddleCenter;
                lriver.Location = new Point(seatloc_5_x, yy);
                this.Controls.Add(lriver);

                Label l1, l2, l3;
                l1 = new Label();
                l2 = new Label();
                l3 = new Label();

                l1.Text = _vm_table.GameName;
                l2.Text = _vm_table.GameValue;
              

                l1.Width = this.Width;
                l1.TextAlign = ContentAlignment.MiddleCenter;
                l1.Font = new Font("Arial", 24, FontStyle.Bold);
                l1.Location = new Point(0, 0);
                l1.Size = new Size(this.Width, seatheight);
                

                l3.Text = _vm_table.TableNo;
                l3.Width = this.Width;
                l3.TextAlign = ContentAlignment.MiddleCenter;
                l3.Font = new Font("Arial", 24, FontStyle.Bold);
                l3.Location = new Point(0, y1-seatheight);
                l3.Size = new Size(this.Width, seatheight);
               

            

                this.Controls.Add(l1);
               // this.Controls.Add(l2);
                this.Controls.Add(l3);
            }
            
            
            /*
            CircularProgressBar.CircularProgressBar c = new CircularProgressBar.CircularProgressBar();
            c.Value = 50;
            c.Location = new Point(100, 100);
            c.Size = new Size(200, 200);
            c.AnimationSpeed = 5000;
            
            c.MarqueeAnimationSpeed = 1000;
            c.StartAngle = 0;
            c.Step = 5;
            c.Style = ProgressBarStyle.Marquee;
             this.Controls.Add(c);
           */


            short seatno = 1;
            View_Seat seat1 = null;
            
            
            ViewModel_Seat vm_seat = _vm_table.get_VM_Seat(seatno);
            int count = 10;
          
            while (count > 6)
            {
                x = x + seatwidth * 2;
                //y = y + 10;
                vm_seat = _vm_table.get_VM_Seat(seatno);
                seat1 = new View_Seat(vm_seat);
                Dict_View_Seats[seatno] = seat1;
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
                Dict_View_Seats[seatno] = seat1;
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
                Dict_View_Seats[seatno] = seat1;
                seat1.JoinedTableEvent += Seat_JoinedTableEvent;
                seat1.Size = new Size(seatwidth, seatheight);
                seat1.Location = new Point(x, y);
                this.Controls.Add(seat1);
                count--;
                seatno++;
            }
            seat1.Location = new Point(x1 - x1 / 10, seat1.Location.Y);
        }
        private short MySeatNo        
        {
           get
            {
                var y = _vm_table.ListOfSeats.Find(x => x.UserName == this.UserName);
                if (y != null)
                    return y.SeatNo;
                else
                    return -1;
            }
        }
        private void Seat_JoinedTableEvent(string TableNo, short SeatNo, decimal ChipCounts)
        {
            myseat = SeatNo;
            if (JoinedTableEvent != null)
                JoinedTableEvent.Invoke(TableNo, SeatNo, ChipCounts);
        }
        public void ProcessMessage(Poker.Shared.Message message)
        {
            if (message == null)
                return;
            switch(message.MessageType)
            {
                case MessageType.TableSendHoleCards:
                    OnReceiveHoleCards(PConvert.ToHoleCards(message));
                    break;
                case MessageType.TableSendFlop:
                    OnReceiveFlop(PConvert.ToFlop(message));
                    break;
                case MessageType.TableSendTurn:
                    OnReceiveTurn(PConvert.ToSingleCard(message));
                    break;
                case MessageType.TableSendRiver:
                    OnReceiveRiver(PConvert.ToSingleCard(message));
                    break;
                default:
                    throw new Exception("Not sure what message is this");
            }
        }
        public void OnReceiveHoleCards(Tuple<A_Card, A_Card> holecards)
        {
            if (MySeatNo > 0)
            {
                Dict_View_Seats[MySeatNo].Assign_HoleCards(holecards);
            }
            Console.WriteLine("OnReceiveHoleCards " + holecards.ToString());
        }
        public void OnReceiveFlop(Tuple<A_Card,A_Card,A_Card> flop)
        {
            Console.WriteLine("OnReceiveFlop " + flop.ToString());
        }
        public void OnReceiveTurn(A_Card turn)
        {
            Console.WriteLine("OnReceiveTurn " + turn.ToString());
        }
        public void OnReceiveRiver(A_Card river)
        {
            Console.WriteLine("OnReceiveRiver " + river.ToString());
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
