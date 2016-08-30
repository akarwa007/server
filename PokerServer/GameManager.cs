using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Poker.Server
{
    public class GameManager
    {
        private AutoResetEvent event1 = new AutoResetEvent(true);
        private Game _game;
        private Table _table;
        private bool _GameInPogress = false;
       
        
        public GameManager(Game game, Table table)
        {
            _game = game;
            _table = table;
            _table.TableUpdatedEvent += _table_TableUpdatedEvent;
        }
        public GameManager(Table table)
        {
            _game = new Game(100,500);
            _table = table;
            _table.TableUpdatedEvent += _table_TableUpdatedEvent;
        }

        private void _table_TableUpdatedEvent(Table table)
        {
            if ((table.PlayerCount() >= 3) && (!_GameInPogress))
                Start();
        }

        public void Start()
        {
            if ((_game == null) || (_table == null))
                throw new Exception("Table or Game cannot be null");
            _GameInPogress = true;
            _table.DealerPosition = 1;
            //deal hole cards to seated players
            int playercount = _table.PlayerCount();

            while (playercount > 0)
            {
                Player player = _table.GetNextPlayer();
                player.AssignHoleCards( _game.DealPlayerHand());
                playercount--;
            }
            // do bet collecting round 
            _table.ResetToSmallBlind();
            playercount = _table.PlayerCount();
            while (playercount > 0)
            {
                Player player = _table.GetNextPlayer();
                if (player.InHand)
                {
                    lock (_table.SynchronizeGame)
                    {
                        MessageFactory.RequestAction(_table, player, "preflop");
                        Monitor.Wait(_table.SynchronizeGame,20000);                    
                    }                  
                }
                playercount--;
            }

            // deal the flop 
           Tuple<Card,Card,Card> flop =  _game.GetFlop();
           MessageFactory.SendFlop(_table, flop);

            // do bet collecting round

           _table.ResetToSmallBlind();
           playercount = _table.PlayerCount();
           while (playercount > 0)
           {
               Player player = _table.GetNextPlayer();
               if (player.InHand)
                { 
                    lock (_table.SynchronizeGame)
                    {
                        MessageFactory.RequestAction(_table, player, "postflop");
                        Monitor.Wait(_table.SynchronizeGame, 20000);
                    }
                }
               playercount--;
           }
            //deal the turn 
           Card turn = _game.GetTurn();
            MessageFactory.SendTurn(_table, turn);

            // do bet collecting round

           _table.ResetToSmallBlind();
           playercount = _table.PlayerCount();
           while (playercount > 0)
           {
               Player player = _table.GetNextPlayer();
               if (player.InHand)
               {
                    lock (_table.SynchronizeGame)
                    {
                        MessageFactory.RequestAction(_table, player, "postturn");
                        Monitor.Wait(_table.SynchronizeGame, 20000);
                    }
                }
               playercount--;
           }

            // deal the river
           Card river = _game.GetRiver();
            MessageFactory.SendRiver(_table, river);

           // do the bet collecting round

           _table.ResetToSmallBlind();
           playercount = _table.PlayerCount();
           while (playercount > 0)
           {
               Player player = _table.GetNextPlayer();
               if (player.InHand)
               {
                    lock (_table.SynchronizeGame)
                    {
                        MessageFactory.RequestAction(_table, player, "postriver");
                        Monitor.Wait(_table.SynchronizeGame, 20000);
                    }
                }
               playercount--;
           }
           // announce the winner

        }
    }
}
