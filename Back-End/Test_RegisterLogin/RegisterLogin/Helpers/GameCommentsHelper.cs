using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameCommentsHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        private const int MAX_CMT_NUM = 10;         //the upperbound number of comments fetched at a time
        public void openConn()
        {
            try
            {
                con.Open();
                Console.WriteLine("Successfully connected to Oracle Database");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection Failed!");
                Console.WriteLine(e);
            }
        }

        public Dictionary<string, bool> GetMyView(string comment_id, string user_id)
        {
            OracleCommand subcmd = con.CreateCommand();
            subcmd.CommandText = $"SELECT LIKE_CMT FROM COMMENT_LIKES WHERE USER_ID={user_id} AND COMMENT_ID={comment_id}";
            OracleDataReader reader = subcmd.ExecuteReader();

            int view = 0;
            if (reader.Read())
                view = int.Parse(reader[0].ToString());

            Dictionary<string, bool> my_view = new Dictionary<string, bool>();
            my_view["good"] = (view == 1);
            my_view["bad"] = (view == -1);

            return my_view;
        }

        public GameCommentsResponse GetGameCommentsResponse(GameCommentsRequest req)
        {
            openConn();
            GameCommentsResponse resp = new GameCommentsResponse();
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            try
            {
                /* fetch top MAX_CMT_NUM comments order by likes */
                cmd.CommandText = $"SELECT USER_ID, RATING, COMMENT_DATE, CONTENT, AGREE_NUM, DISAGREE_NUM, COMMENT_ID " +
                    $"FROM COMMENTS WHERE GAME_ID='{req.game_id}' AND ROWNUM<={MAX_CMT_NUM} ORDER BY AGREE_NUM DESC";
                reader = cmd.ExecuteReader();     

                while (reader.Read())
                {
                    Dictionary<string, dynamic> comment = new Dictionary<string, dynamic>();

                    comment["creator_id"] = reader[0].ToString();

                    comment["detail"] = new Dictionary<string, dynamic>();
                    comment["detail"]["rate"] = int.Parse(reader[1].ToString());
                    comment["detail"]["date"] = reader[2].ToString();
                    comment["detail"]["content"] = reader[3].ToString();

                    comment["rate"] = new Dictionary<string, dynamic>();
                    comment["rate"]["good_count"] = int.Parse(reader[4].ToString());
                    comment["rate"]["bad_count"] = int.Parse(reader[5].ToString());

                    comment["my_view"] = GetMyView(reader[6].ToString(), req.user_id);
                    resp.comment_list.Add(comment);
                }

                resp.result = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = 0;
            }

            return resp;
        }
    }
}
