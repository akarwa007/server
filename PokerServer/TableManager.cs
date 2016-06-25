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
            _tables.Add(new Table(10,"TexasHodldem","NoLimit","2-5",200,500));
            _tables.Add(new Table(10, "TexasHodldem", "NoLimit", "2-5", 200, 500));
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
