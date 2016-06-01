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
        
        public GameManager(Game game, Table table)
        {
            _game = game;
            _table = table;
        }
        public GameManager(Table table)
        {
            _game = new Game(100,500);
            _table = table;
        }
        public void Start()
        {
            if ((_game == null) || (_table == null))
                throw new Exception("Table or Game cannot be null");
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
                    ClientPlayerManager.RequestAction(player); // this connects to client and waits for response.
                   
                }
                playercount--;
            }

            // deal the flop 
           Tuple<Card,Card,Card> flop =  _game.DealFlop();
           ClientPlayerManager.SendFlop(_table, flop);

            // do bet collecting round

           _table.ResetToSmallBlind();
           playercount = _table.PlayerCount();
           while (playercount > 0)
           {
               Player player = _table.GetNextPlayer();
               if (player.InHand)
               {
                   ClientPlayerManager.RequestAction(player); // this connects to client and waits for response.

               }
               playercount--;
           }
            //deal the turn 
           Card turn = _game.DealTurn();
           ClientPlayerManager.SendTurn(_table, turn);

            // do bet collecting round

           _table.ResetToSmallBlind();
           playercount = _table.PlayerCount();
           while (playercount > 0)
           {
               Player player = _table.GetNextPlayer();
               if (player.InHand)
               {
                   ClientPlayerManager.RequestAction(player); // this connects to client and waits for response.

               }
               playercount--;
           }

            // deal the river
           Card river = _game.DealTurn();
           ClientPlayerManager.SendTurn(_table, river);

            // do the bet collecting round

           _table.ResetToSmallBlind();
           playercount = _table.PlayerCount();
           while (playercount > 0)
           {
               Player player = _table.GetNextPlayer();
               if (player.InHand)
               {
                   ClientPlayerManager.RequestAction(player); // this connects to client and waits for response.

               }
               playercount--;
           }
            // announce the winner

        }
    }
}
