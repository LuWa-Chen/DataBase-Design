using System;
using Oracle.ManagedDataAccess.Client;
using GameColumnReplies.Models;

namespace GameColumnReplies.Helpers
{
    public class GameColumnRepliesHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public getGameColumnRepliesResponse resp = new getGameColumnRepliesResponse();
        public postGameColumnRepliesResponse postresp = new postGameColumnRepliesResponse();

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
                resp.message = e.Message;
                resp.result = -1;
                con.Close();
            }
        }
        public string getUserName(string id)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT NAME FROM GAME_USER WHERE ID = '" + id + "'";
            OracleDataReader reader = cmd.ExecuteReader();

            string name = "";
            if (!reader.HasRows)
            {
                resp.result = 0;
                resp.message = "无法根据" + id + "查询到用户名字!";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        name = reader[0].ToString();
                    }
                }
                catch (Exception e)
                {
                    resp.message = e.Message;
                    resp.result = 0;
                }
            }
            return name;
        }
        public int UpdateNum(string id)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE COLUMNS SET COMMENT_NUM = COMMENT_NUM + 1 WHERE COLUMN_ID = '" + id + "'";
            int cen = 0;
            try
            {
                cen = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resp.message = e.Message;
                resp.result = 0;
                return 0;
            }

            try
            {
                cmd.CommandText = "COMMIT";
                cen = cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                resp.message = e.Message;
                resp.result = 0;
                return 0;
            }
        }
        public getGameColumnRepliesResponse getReply(getGameColumnRepliesRequest req)
        {
            resp.result = 0;
            openConn();
            if (resp.result == -1)
                return resp;

            if (UpdateNum(req.column_id) == 0)
                return resp;

            string name = "";
            ColumnReplies cr;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT USER_ID,reply_content,reply_time FROM reply_column WHERE COLUMN_ID = '" + req.column_id + "' order by reply_time";
            OracleDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                resp.result = 1;
                resp.message = "无回复记录!";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    while (reader.Read())
                    {
                        cr = new ColumnReplies();
                        name = getUserName(reader[0].ToString());
                        if (name == "")
                            return resp;
                        cr.name = name;
                        cr.content = reader[1].ToString();
                        cr.release_time = reader[2].ToString();
                        cr.user_id = reader[0].ToString();
                        resp.CR.Add(cr);
                    }
                    resp.result = 1;
                    resp.message = "成功！";
                }
                catch (Exception e)
                {
                    resp.message = e.Message;
                    resp.result = 0;
                }
            }
            con.Close();
            return resp;
        }
        public postGameColumnRepliesResponse postReply(postGameColumnRepliesRequest req)
        {
            resp.result = 0;
            openConn();
            if (resp.result == -1)
                return postresp;

            OracleCommand cmd = con.CreateCommand();
            string date = DateTime.Now.ToLocalTime().ToString();
            cmd.CommandText = "INSERT INTO REPLY_COLUMN VALUES('" + req.column_id + "','" + req.user_id + "','" + req.content + "',TO_DATE('" + date + "', 'YYYY-MM-DD HH24:MI:SS'))";
            int cen = 0;
            try
            {
                cen = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                postresp.result = 0;
                postresp.message = e.Message;
            }
            try
            {
                cmd.CommandText = "COMMIT";
                cen = cmd.ExecuteNonQuery();
                postresp.result = 1;
                postresp.message = "成功！";
            }
            catch (Exception e)
            {
                postresp.message = e.Message;
                postresp.result = 0;
            }
            con.Close();
            return postresp;
        }
    }
}
