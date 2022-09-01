using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class DiscountPublishRequest
    {
        public string name { get; set; }
        public double discount { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

    public class DiscountPublishResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
