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
        private const int MAX_CMT_NUM = 8;         //the upperbound number of comments fetched at a time
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

        public int GetMyView(string comment_id, string user_id)
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

            return view;
        }

        public void GetUserInfo(string uid, string game_id, Dictionary<string, dynamic> comment)
        {
            OracleCommand usercmd = con.CreateCommand();
            OracleDataReader reader;

            /* get profile photo, name, game_num */
            usercmd.CommandText = $"SELECT PROFILE_PHOTO, NAME, GAME_NUM FROM GAME_USER WHERE ID='{uid}'";
            reader = usercmd.ExecuteReader();
            if (reader.Read())
            {
                comment["head"] = reader[0].ToString();
                comment["name"] = reader[1].ToString();
                comment["id"] = uid;
                comment["game_num"] = int.Parse(reader[2].ToString());
            }

            /* get comment_num */
            usercmd.CommandText = $"SELECT COUNT(USER_ID) FROM COMMENTS WHERE USER_ID='{uid}' AND GAME_ID='{game_id}'";
            reader = usercmd.ExecuteReader();
            if (reader.Read())
                comment["comment_num"] = int.Parse(reader[0].ToString());

            /* get access time */
            usercmd.CommandText = $"SELECT ORDER_TIME, USER_ID FROM GAME_ORDER WHERE USER_ID='{uid}' AND GAME_ID='{game_id}'";
            reader = usercmd.ExecuteReader();
            if (reader.Read())
                comment["access_time"] = reader[0].ToString();

            /* get if game is accessed via cdk */
            usercmd.CommandText = $"SELECT VIA_CDK, USER_ID FROM OWNERSHIP WHERE USER_ID='{uid}' AND GAME_ID='{game_id}'";
            reader = usercmd.ExecuteReader();
            if (reader.Read())
                comment["via_cdk"] = int.Parse(reader[0].ToString());
        }

        public string[] GetFilterSql(GameCommentsRequest req)
        {
            string commandText = $"SELECT USER_ID, RATING, COMMENT_DATE, CONTENT, AGREE_NUM, DISAGREE_NUM, COMMENT_ID, ROW_NUMBER() OVER (ORDER BY {(req.comment_type == 1 ? "AGREE_NUM DESC": "COMMENT_DATE DESC")}) R ";
            if (req.filter_2 == 0)
                commandText += $"FROM COMMENTS WHERE GAME_ID = '{req.game_id}' ";
            else
                commandText += $"FROM COMMENTS NATURAL JOIN OWNERSHIP WHERE GAME_ID = '{req.game_id}' ";
            
            /* 3 filter conditions */
            string filter = "";
            //filter1
            switch (req.filter_1) 
            {
                case 1:
                    filter += "AND RATING >= 3 ";
                    break;
                case 2:
                    filter += "AND RATING < 3 ";
                    break;
                default:
                    break;
            }
            //filter2
            switch (req.filter_2)
            {
                case 1:
                    filter += "AND VIA_CDK=0 ";
                    break;
                case 2:
                    filter += "AND VIA_CDK=1 ";
                    break;
                default:
                    break;
            }
            //filter3
            switch (req.filter_3)
            {
                case 1:
                    filter += "AND COMMENT_DATE>=(SELECT SYSDATE - INTERVAL '7' DAY FROM DUAL) ";
                    break;
                case 2:
                    filter += "AND COMMENT_DATE>=(SELECT SYSDATE - INTERVAL '1' MONTH FROM DUAL) ";
                    break;
                default:
                    break; 
            }

            commandText += filter;

            string command1 = $"SELECT USER_ID, RATING, COMMENT_DATE, CONTENT, AGREE_NUM, DISAGREE_NUM, COMMENT_ID FROM ({commandText}) ";
            command1 += $"WHERE R BETWEEN {MAX_CMT_NUM * (req.page_no - 1) + 1} AND {MAX_CMT_NUM * req.page_no} ";

            string command2 = $"SELECT COUNT(USER_ID) FROM ({commandText})";
            string[] sqls = { command1, command2 };
            return sqls;
        }
        public GameCommentsResponse GetGameCommentsResponse(GameCommentsRequest req)
        {
            openConn();
            GameCommentsResponse resp = new GameCommentsResponse();
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;
            resp.result = 0;

            try
            {
                /* basic steps */
                string[] commands = GetFilterSql(req);
                cmd.CommandText = commands[0];      //get basic sql
                reader = cmd.ExecuteReader();     

                while (reader.Read())
                {
                    Dictionary<string, dynamic> comment = new Dictionary<string, dynamic>();

                    GetUserInfo(reader[0].ToString(), req.game_id, comment);

                    comment["rate"] = int.Parse(reader[1].ToString());
                    comment["date"] = reader[2].ToString();
                    comment["content"] = reader[3].ToString();

                    comment["good_count"] = int.Parse(reader[4].ToString());
                    comment["bad_count"] = int.Parse(reader[5].ToString());

                    int view = GetMyView(reader[6].ToString(), req.user_id);
                    comment["good"] = (view == 1);
                    comment["bad"] = (view == -1);
                    resp.comment_list.Add(comment);

                    resp.result++;
                }

                /* check is the last page or not */
                cmd.CommandText = commands[1];
                reader = cmd.ExecuteReader();
                resp.is_end = false;
                if (reader.Read())
                {
                    resp.comment_num = int.Parse(reader[0].ToString());
                    if (resp.comment_num <= MAX_CMT_NUM * req.page_no)
                        resp.is_end = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -1;
            }

            return resp;
        }
    }
}
