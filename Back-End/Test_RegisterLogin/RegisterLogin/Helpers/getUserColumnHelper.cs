using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class getUserColumnHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(ref UserColumnResponse resp)
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
                resp.result = -1;
                con.Close();
            }
        }

        public UserColumnResponse getUserColumn(UserColumnRequest req)
        {
            UserColumnResponse resp = new UserColumnResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM GAME_USER WHERE ID='" + req.user_id + "'";
            OracleDataReader reader0 = cmd.ExecuteReader();
            if (!reader0.HasRows)
                resp.result = 0;       //查找用户id不存在
            else
            {
                try
                {
                    if (req.type == 1)
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM COLUMNS WHERE USER_ID='" + req.user_id + "'";
                        OracleDataReader reader2 = cmd.ExecuteReader();

                        if (reader2.Read())
                        {
                            resp.column_num = int.Parse(reader2[0].ToString());
                        }


                        if ((req.page_num - 1) * 8 < resp.column_num && resp.column_num <= req.page_num * 8)
                        {
                            resp.is_end = true;
                        }
                        else
                        {
                            resp.is_end = false;
                        }

                        cmd.CommandText = "SELECT COLUMN_ID ,COLUMN_TITLE,COLUMN_TEXT,POST_TIME FROM COLUMNS WHERE USER_ID='" + req.user_id + "' ORDER BY POST_TIME DESC OFFSET " + (req.page_num - 1) * 8 + "ROWS FETCH NEXT 8 ROWS ONLY";
                        OracleDataReader reader1 = cmd.ExecuteReader();

                        //查找成功，赋值变量
                        while (reader1.Read())
                        {
                            Column info = new Column();

                            info.column_id = reader1[0].ToString();
                            info.title = reader1[1].ToString();
                            info.content = reader1[2].ToString();
                            info.release_time = reader1[3].ToString();

                            resp.Column_list.Add(info);
                        }

                        resp.result = 1;
                        con.Close();
                        return resp;

                    }
                    else if (req.type == 2)
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM REPLY_COLUMN WHERE USER_ID='" + req.user_id + "'";
                        OracleDataReader reader3 = cmd.ExecuteReader();

                        if (reader3.Read())
                        {
                            resp.column_num = int.Parse(reader3[0].ToString());
                        }

                        if ((req.page_num - 1) * 8 < resp.column_num && resp.column_num <= req.page_num * 8)
                        {
                            resp.is_end = true;
                        }
                        else
                        {
                            resp.is_end = false;
                        }

                        cmd.CommandText = "SELECT COLUMN_ID ,REPLY_CONTENT,REPLY_TIME FROM REPLY_COLUMN WHERE USER_ID='" + req.user_id + "' ORDER BY REPLY_TIME DESC OFFSET " + (req.page_num - 1) * 8 + "ROWS FETCH NEXT 8 ROWS ONLY";
                        OracleDataReader reader4 = cmd.ExecuteReader();

                        //查找成功，赋值变量
                        while (reader4.Read())
                        {
                            Column info = new Column();

                            info.column_id = reader4[0].ToString();

                            cmd.CommandText = "SELECT COLUMN_TITLE FROM COLUMNS WHERE COLUMN_ID='" + info.column_id + "'";
                            OracleDataReader reader5 = cmd.ExecuteReader();
                            if (reader5.Read())
                            {
                                info.title = reader5[0].ToString();
                            }

                            info.content = reader4[1].ToString();
                            info.release_time = reader4[2].ToString();

                            resp.Column_list.Add(info);
                        }

                        resp.result = 2;
                        con.Close();
                        return resp;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = 0;
                }
            }
            con.Close();
            return resp;
        }
    }
}

