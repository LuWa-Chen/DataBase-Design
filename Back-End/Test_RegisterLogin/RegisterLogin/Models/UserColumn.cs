using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class Column
    {
        public string column_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string release_time { get; set; }
    }
    public class UserColumnRequest
    {
        public string user_id { get; set; }
        public int type { get; set; }
        public int page_num { get; set; }
    }

    public class UserColumnResponse
    {
        public List<Column> Column_list { get; set; } = new List<Column>();
        public bool is_end { get; set; }
        public int column_num { get; set; }
        public int result { get; set; }
        
    }
}