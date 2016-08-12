using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Server.ClientView
{
    public class SeatView
    {
        public SeatView(Seat seat)
        {
            SeatNo = seat.SeatNumber;
            TableNo = seat.TableNo;
            ChipCounts = seat.ChipCounts;
            UserName = seat.UserName;
        }
        public short SeatNo
        {
            get; set;
        }
        public string TableNo
        {
            get; set;
        }
        public decimal ChipCounts
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
    }
}
