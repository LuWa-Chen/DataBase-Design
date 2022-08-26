using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class CDKExchangeRequest
    {
        public string user_id { get; set; }
        public string cdk_value { get; set; }
        public string order_time { get; set; }
    }
    public class CDKExchangeResponse
    {
        public int result { get; set; }
    }
}
