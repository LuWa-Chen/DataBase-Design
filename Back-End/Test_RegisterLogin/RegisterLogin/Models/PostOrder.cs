using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class PostOrderRequest
    {
        public string user_id { get; set; }
        public string game_id { get; set; }
        public int via_cdk { get; set; }
        public int pay_amount { get; set; }
        public string recept_id { get; set; }

    }

    public class PostOrderResponse
    {
        public string order_id { get; set; }
        public string order_time { get; set; }
        public int result { get; set; }

    }
}