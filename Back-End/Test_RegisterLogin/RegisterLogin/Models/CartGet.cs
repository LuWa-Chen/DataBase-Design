using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class CartGetRequest
    {
        public string user_id { get; set; }
    }
    public class CartGetResponse
    {
        public List<Dictionary<string, dynamic>> game_list { get; set; } = new List<Dictionary<string, dynamic>>();
        public int result { get; set; }
    }
}
