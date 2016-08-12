using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Server
{
    public  class TableManager : ITableList
    {
        private static TableManager _instance = new TableManager();
        private List<ITable> _tables = new List<ITable>();
        private TableManager()
        {
        }
        public static TableManager Instance
        {
            get
            {
                return _instance;
            }
        }
        public void CreateTable(short capacity)
        {
            Table t = new Table(10, "TexasHodldem", "NoLimit", "2-5", 200, 500);
            
            Player p1 = new Player("Alok");
            Player p2 = new Player("Alka");
            Player p3 = new Player("Anshu");
            Player p4 = new Player("Anand");
            Player p5 = new Player("Anurag");
            Player p6 = new Player("Tans");
            Player p7 = new Player("Veer");
            t.AddPlayer(p1);
            t.AddPlayer(p2);
            t.AddPlayer(p3);
            t.AddPlayer(p4);
            t.AddPlayer(p5);
            t.AddPlayer(p6);
            t.AddPlayer(p7);

            _tables.Add(t);
            //_tables.Add(new Table(10, "TexasHodldem", "NoLimit", "2-5", 200, 500));
            _tables.Add(new Table(10, "TexasHodldem", "Limit", "1-2", 200, 500));
            _tables.Add(new Table(10, "TexasHodldem", "Limit", "3-6", 200, 500));
            _tables.Add(new Table(10, "TexasHodldem", "Limit", "4-8", 200, 500));
            _tables.Add(new Table(10, "Omaha", "HiLow", "1-2", 200, 500));
            _tables.Add(new Table(10, "Omaha", "HiLow", "2-5", 200, 500));
            _tables.Add(new Table(10, "Omaha", "HiLow", "2-5", 200, 500));
            _tables.Add(new Table(10, "Omaha", "HiLow", "2-5", 200, 500));
        }
       public  IEnumerable<ITable> GetRunningTables()
        {
            return _tables;
        }
    }
    public interface ITableList
    {
       // List<ITable> GetRunningTables();
        IEnumerable<ITable> GetRunningTables();
    }
}
