using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class deletUserColumnRequest
    {
        public string user_id { get; set; }
        public string column_id { get; set; }
        public int type { get; set; }
    }

    public class deletUserColumnResponse
    {
        public int result { get; set; }
    }
}