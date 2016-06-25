using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Poker.Client.Support
{
    public class ViewModel_Casino
    {
        private List<ViewModel_Table> _list = new List<ViewModel_Table>();
      
        public ViewModel_Casino()
        {
        }
     
        public List<ViewModel_Table> ListOfTables
        {
            get
            {
                return _list;
            }
        }
    }
}
