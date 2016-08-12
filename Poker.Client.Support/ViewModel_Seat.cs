using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Client.Support
{
    public class ViewModel_Seat : BaseViewModel
    {

        public ViewModel_Seat(string UserName)
        {
            base.UserName = UserName;
        }
        public ViewModel_Seat()
        {
         
        }
        public short SeatNo
        {
            get;set;
        }
        public string TableNo
        {
            get;set;
        }
        public decimal ChipCounts
        {
            get;set;
        }
        public new string UserName
        {
            get;set;
        }
        public  string CurrentUserName
        {
            get; set;
        }
        public bool Joined
        {
            get;set;
        }
    }
}
