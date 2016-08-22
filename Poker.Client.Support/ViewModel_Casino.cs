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
    public class ViewModel_Casino  : BaseViewModel
    {
        private List<ViewModel_Table> _list = new List<ViewModel_Table>();
      
        public ViewModel_Casino(string UserName)
        {
            base.UserName = UserName;
        }
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
        public void Replace(ViewModel_Table vm)
        {
            string tableno = vm.TableNo;
            ViewModel_Table replace = _list.Find(x => x.TableNo == tableno);
            if (replace != null)
            {
                _list.Remove(replace);
                _list.Add(vm);
            }

        }
        public ViewModel_Table GetLatest(ViewModel_Table vm)
        {
            if (vm == null)
                return vm;
            var fresh = _list.Find(x => x.TableNo == vm.TableNo);
            if (fresh != null)
                return fresh;
            return null;
        }
     
    }
}
