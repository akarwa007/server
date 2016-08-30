using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Poker.Shared;

namespace Poker.Server
{
    public class Table : ITable
    {

        public object SynchronizeGame = new object();
        private short _capacity = 9;
        private Dictionary<Seat, Player> _seats;
        private Seat[] _Seats;
        private int _dealerPosition;
        private string _gameName; // e.g Texas Holdem
        private string _gameSubName; // e.g No Limit
        private string _gameValue; // e.g  2-5
        private string _tableNo; // e.g 45
        private Game _game;
        private GameManager _gameManager;
        public static int TableNumber = 1;
        
        private int _currentPosition = 0; // seat numbers start with 1
        public event Action<Table> TableUpdatedEvent;
        public Table(short capacity , string gamename, string gamesubname , string gamevalue, decimal minchips , decimal maxchips, Action<Table> tableUpdateEventHandler)
        {
            _gameName = gamename;
            _gameSubName = gamesubname;
            _gameValue = gamevalue;
            _game = new Game(minchips, maxchips);
            _gameManager = new GameManager(_game, this);
            _tableNo = "Table" + Table.TableNumber++;
            TableUpdatedEvent += tableUpdateEventHandler;
            if ((capacity < 2) || (capacity > 10))
                throw new Exception("Capacity cannot be less than 2 or greater than 10");
            _capacity = capacity;
            _Seats = new Seat[capacity+1];
            initialize();
            Console.WriteLine("default table cons with capacity of " + this._capacity);
        }
        private void RaiseEvent()
        {
           // TableUpdatedEvent?.Invoke(this);
            Delegate[] arr = TableUpdatedEvent.GetInvocationList();
            if (arr[0].Method.Name == "SendTableUpdateMessage")
            {
                arr[0].DynamicInvoke(this);
                arr[1].DynamicInvoke(this);
            }
            else
            {
                arr[1].DynamicInvoke(this);
                arr[0].DynamicInvoke(this);
            }


           


        }
        private void initialize()
        {
            _seats = new Dictionary<Seat, Player>();
            short count = this._capacity;
            short seatno = 1;
            Player empty = new Player(null,this);
            while (count > 0)
            {
                Seat s = new Seat(empty, this, seatno);
                _seats.Add(s, empty);
                _Seats[seatno] = s;
                count--;
                seatno++;
            }

        }
        public void AddPlayer(Player p)
        {
            // find next empty seat 
            foreach (Seat seat in _seats.Keys)
            {
                if (seat.IsEmpty())
                {
                    seat.SeatPlayer(p);
                    _seats[seat] = p;
                    break;
                }
            }   
            RaiseEvent();
        }
        private bool IsPlayerSeated(Player p)
        {
            var list =  _seats.Values;
            var found = list.Where(x => x.UserName == p.UserName);
            if ((found == null) || (found.Count() == 0))
                return false;
            return true;
        }
        public bool AddPlayer(Player p, short SeatNo)
        {
            bool success = false;
            if (IsPlayerSeated(p))
                RemovePlayer(p);
            if (_Seats[SeatNo].IsEmpty())
            {
                _Seats[SeatNo].SeatPlayer(p);
                _seats[_Seats[SeatNo]] = p;
                success = true;
            }
            RaiseEvent();
            return success;
        }
        public Player RemovePlayer(short seatNo)
        {
            Player removed = _Seats[seatNo].RemovePlayer();
            _seats[_Seats[seatNo]] = new Player(null, this); ;
            return removed;
        }
        public void RemovePlayerEx(short seatNo)
        {
            Player removed = RemovePlayer(seatNo);
            RemovedPlayer = removed;
            RaiseEvent();
        }
        public void RemovePlayerEx(Player p)
        {
            RemovePlayer(p);
            RemovedPlayer = p;
            RaiseEvent();

        }
        public void RemovePlayer(Player p)
        {
           // find player where he is seated and remove him 
            foreach (Seat seat in _seats.Keys)
            {
                if (_seats[seat] == p)
                {
                    _seats[seat] = new Player(null,this);
                    seat.RemovePlayer(p);
                    //RemovedPlayer = p;
                    break;
                }
            }
           
        }
        public Player RemovedPlayer
        {
            get;set;
        }
        public Dictionary<Seat, Player> Seats
        {
            get
            {
                return _seats;
            }
            set
            {
                _seats = value;
            }
        }
        public int DealerPosition
        {
            get { return _dealerPosition; }
            set
            {
                int fullseatcount = _seats.Count();

                _dealerPosition = value % fullseatcount;
                _currentPosition = (_dealerPosition + 1) % fullseatcount;
            }
        }
        public int SmallBlind_Position
        {
            get
            {
                int fullseatcount = _seats.Count();
                return (_dealerPosition + 1) % fullseatcount;
            }
        }
        public int BigBlind_Position
        {
            get
            {
                int fullseatcount = _seats.Count();
                return (_dealerPosition + 2) % fullseatcount;
            }
        }
        public string GameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                _gameName = value;
            }
        }
        public string GameSubName
        {
            get
            {
                return _gameSubName;
            }
            set
            {
                _gameSubName = value;
            }
        }
        public string GameValue
        {
            get
            {
                return _gameValue;
            }
            set
            {
                _gameValue = value;
            }
        }
        public string TableNo
        {
            get
            {
                return _tableNo;
            }
            set
            {
                _tableNo = value;
            }
        }
      
        public Tuple<Card, Card, Card> Flop
        {
            get
            {
                return _game.Flop;
            }

        }
        public Card Turn
        {
            get
            {
                return _game.Turn;
            }

        }
        public Card River
        {
            get
            {
                return _game.River;
            }

        }
        public int PlayerCount()
        {
            return _seats.Where(a => a.Key.IsEmpty() == false).Count();
        }
        public Player GetPlayer(PokerUser user)
        {
            var listOfPlayers = _seats.Values;
            var found = listOfPlayers.Where(x => x.UserName == user.UserName);
            if ((found == null) || (found.Count() == 0))
                return null;
            return found.FirstOrDefault();
        }
        public Player GetNextPlayer() // keeps serving players in a round robin fashion. 
        {
           int fullseatcount = _seats.Count();
           if (fullseatcount < 2)
               throw new Exception("No game possible with less than 2 players seated on a table");
           Seat nextseat = _seats.Where(a => (a.Key.SeatNumber-1) == this._currentPosition).FirstOrDefault().Key;

           while (nextseat.IsEmpty())
           {
              this._currentPosition = ( this._currentPosition + 1) % fullseatcount;
              nextseat = _seats.Where(a => (a.Key.SeatNumber-1) == this._currentPosition).FirstOrDefault().Key;
           }
            this._currentPosition = (this._currentPosition + 1) % fullseatcount;
            return _seats[nextseat];
        }
        public void ResetToSmallBlind()
        {
             this._currentPosition = SmallBlind_Position;
        }
      
    }

    public interface ITable
    {
        void AddPlayer(Player p);
        void RemovePlayer(Player p);
        int PlayerCount();
       


    }

}
