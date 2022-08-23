using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class CartUpdateRequest
    {
        public string user_id { get; set; }
        public string game_id { get; set; }
        public int op_type { get; set; }
    }
    public class CartUpdateResponse
    {
        public int result { get; set; }
    }
}
