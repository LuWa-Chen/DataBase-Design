using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameColumnsHelper
    {
        public const int MAX_CLMN_NUM = 8;

        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void OpenConn(GameColumnResponse resp)
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
                resp.result = -2;
                con.Close();
            }
        }

        public string[] GetCommands(GameColumnsRequest req)
        {
            string basic_cmd = $"SELECT COLUMN_ID, USER_ID, FUN_GET_USER_NAME(USER_ID), COLUMN_TITLE, COLUMN_TEXT, COMMENT_NUM, POST_TIME, ROW_NUMBER() OVER (ORDER BY POST_TIME DESC) R FROM COLUMNS WHERE GAME_ID='{req.game_id}'";
            string command1 = $"SELECT COLUMN_ID, USER_ID, FUN_GET_USER_NAME(USER_ID), COLUMN_TITLE, COLUMN_TEXT, COMMENT_NUM, POST_TIME FROM ({basic_cmd}) WHERE R BETWEEN { MAX_CLMN_NUM * (req.page_num - 1) + 1 } AND { MAX_CLMN_NUM * req.page_num }";
            string command2 = $"SELECT COUNT(COLUMN_ID) FROM ({basic_cmd})";
            string[] commands = { command1, command2 };
            return commands;
        }

        public List<string> GetColumnImages(string cid)
        {
            OracleCommand cmd = con.CreateCommand();
            List<string> image_list = new List<string>();
            cmd.CommandText = $"SELECT COLUMN_PHOTO FROM COLUMN_PHOTOS WHERE COLUMN_ID='{cid}'";

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    image_list.Add(reader[0].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                image_list.Add("ERROR");
            }

            return image_list;
        }

        public GameColumnResponse GetColumns(GameColumnsRequest req)
        {
            GameColumnResponse resp = new GameColumnResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            string[] commands = GetCommands(req);

            /* basic steps */
            cmd.CommandText = commands[0];
            Dictionary<string, dynamic> column;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    column = new Dictionary<string, dynamic>();

                    column["column_id"] = reader[0].ToString();
                    column["uid"] = reader[1].ToString();
                    column["name"] = reader[2].ToString();
                    column["title"] = reader[3].ToString();
                    column["content"] = reader[4].ToString();

                    column["image_list"] = GetColumnImages(column["column_id"]);
                    if (column["image_list"].Contains("ERROR"))
                    {
                        resp.result = -1;
                        con.Close();
                        return resp;
                    }

                    column["reply_num"] = int.Parse(reader[5].ToString());
                    column["release_time"] = reader[6].ToString();

                    resp.columnList.Add(column);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -1;
                con.Close();
                return resp;
            }

            /* check if columns come to an end */
            cmd.CommandText = commands[1];
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int column_num = int.Parse(reader[0].ToString());
                    resp.is_end = column_num <= MAX_CLMN_NUM * req.page_num;
                    resp.result = 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -1;
            }

            con.Close();
            return resp;
        }
    }
}
