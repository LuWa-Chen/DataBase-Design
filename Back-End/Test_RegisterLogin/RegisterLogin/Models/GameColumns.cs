using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameColumnsRequest
    {
        public string game_id { get; set; }
    }

    public class GameColumnResponse
    {
        public List<Dictionary<string, dynamic>> columnList { get; set; } = new List<Dictionary<string, dynamic>>();
    }
}
