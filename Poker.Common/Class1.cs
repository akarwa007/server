using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Poker.Common
{
    public delegate bool createplayer_callback(string username, string encrypted_pwd);

    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameName
    {
        Texas_Holdem,
        Omaha_Hi_Low,
        Seven_Card_Stud
    }
     [JsonConverter(typeof(StringEnumConverter))]
    public enum GameSubName
    {
        Limit_Holdem,
        No_Limit_Holdem
    }

}
