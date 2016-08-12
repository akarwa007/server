﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Server.ClientView
{
    public class TableView
    {
        public string GameName {get; set;}
        public string GameValue {get; set;}
        public string TableNo { get; set; }
        public int DealerPosition { get; set; }
       // private List<Tuple<short,string,decimal>> listTable = new List<Tuple<short,string,decimal>>(); // seatno, playername,chipcount
        private List<SeatView> listSeats = new List<SeatView>();
        public TableView(Table table)
        {
            GameName = table.GameName;
            GameValue = table.GameValue;
            TableNo = table.TableNo;
            foreach (var x in table.Seats)
            {
                //Tuple<short, string, decimal> t = new Tuple<short, string, decimal>(x.Key.SeatNumber,  x.Value.UserName, x.Value.ChipCount);
                //listTable.Add(t);
                SeatView sv = new SeatView(x.Key);
                listSeats.Add(sv);
            }
        }
        /*
        public List<Tuple<short, string, decimal>> TableDetails
        {
            get
            {
                return listTable;
            }
        }
        */
        public List<SeatView> ListOfSeats
        {
            get
            {
                return listSeats;

            }
        }
    }
}
