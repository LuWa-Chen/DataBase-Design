using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class OrderGetRequest
    {
        public string id { get; set; }
    }

    public class OrderGetResponse
    {
        public List<Dictionary<string, dynamic>> order_list { get; set; } = new List<Dictionary<string, dynamic>>();
        public int result { get; set; }
    }
}
