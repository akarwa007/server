using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public class GameManager
    {
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


        }
    }
}
