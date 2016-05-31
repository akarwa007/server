using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    public  class TableManager : ITableList
    {
        private static TableManager _instance = new TableManager();
        private List<ITable> _tables = new List<ITable>();
        private TableManager()
        {
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
