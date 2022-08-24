using System;
using Oracle.ManagedDataAccess.Client;
using Chat.Models;
using GameNews.Models;

namespace Chat.Helpers
{
    public class ChatHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public getChatHistoryResponse resp = new getChatHistoryResponse();
        public postChatHistoryResponse presp = new postChatHistoryResponse();

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
        public getChatHistoryResponse getChat(getChatHistoryRequest req)
        {
            resp.result = 0;
            openConn();
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT TIME,TEXT,SENDER_ID FROM CHAT_LIBRARY WHERE (SENDER_ID = '" + req.id_A+"' AND RECEIVER_ID = '"+req.id_B+"') OR (SENDER_ID = '"+req.id_B+"' AND RECEIVER_ID = '"+req.id_A+"') ORDER BY TIME";
            OracleDataReader reader = cmd.ExecuteReader();

            ChatHistory ch;
            ChatText ct;

            if (!reader.HasRows)
            {
                resp.result = 1;
                resp.message = "无消息记录!";
            }    
            else
            {
                try
                {
                    //查找成功，赋值变量
                    while (reader.Read())
                    {
                        ch = new ChatHistory();
                        ct = new ChatText();
                        ch.date = reader[0].ToString();
                        ct.text = reader[1].ToString();
                        ch.text = ct;
                        if (reader[2].ToString() == req.id_A)
                            ch.mine = true;
                        else
                            ch.mine = false;
                        resp.chat_history.Add(ch);
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
        public postChatHistoryResponse postChatHistory(postChatHistoryRequest req)
        {
            resp.result = 0;
            openConn();
            if (resp.result == -1)
                return presp;
            string date = DateTime.Now.ToLocalTime().ToString();

            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO chat_library VALUES('" + req.id_A + "','" + req.id_B + "',TO_DATE('" + date + "', 'YYYY-MM-DD HH24:MI:SS'),'" + req.text + "')";
            try
            {
                cen = cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                presp.message = e.Message;
                presp.result = 0;
            }

            if (cen == 0)
            {
                presp.result = 0;
                presp.message = "添加失败！";
            } 
            else
            {
                try
                {
                    cmd.CommandText = "commit";
                    cen = cmd.ExecuteNonQuery();
                    presp.result = 1;
                    presp.message = "添加成功！";
                }
                catch (Exception e)
                {
                    presp.message = e.Message;
                    presp.result = 0;
                }
            }
            con.Close();
            return presp;
        }
    }
}
