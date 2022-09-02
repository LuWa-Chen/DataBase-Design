using System;
using Oracle.ManagedDataAccess.Client;
using PublishComment.Models;
namespace PublishComment.Helpers
{
    public class PublishCommentHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(PublishCommentResponse resp)
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
        public int HasNoPublished(PublishCommentRequest req, PublishCommentResponse resp)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT RATING FROM COMMENTS WHERE USER_ID = '"+req.user_id+"' AND GAME_ID = '"+req.game_id+"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return 1;       //说明无评论
            else
            {
                resp.result = -1;
                resp.messsage = "该用户已发表过该游戏评论";
                return 0;
            }
        }
        public string getNextID(PublishCommentRequest req, PublishCommentResponse resp)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COMMENT_ID FROM COMMENTS WHERE USER_ID = 'testuser0' AND GAME_ID = '0000000001'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = -1;
                resp.messsage = "无法查询到下一个comment_id";
                return "";
            }
            else
            {
                reader.Read();
                string NextID = reader[0].ToString();
                int outid = System.Convert.ToInt32(NextID);
                outid += 1;
                string AddID = outid.ToString("00000000000");

                int cen = 0;
                cmd.CommandText = "UPDATE COMMENTS SET COMMENT_ID = '"+ AddID + "' WHERE USER_ID = 'testuser0' AND GAME_ID = '0000000001'";
                cen = cmd.ExecuteNonQuery();
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = -1;  //修改失败
                    resp.messsage = "修改comment记录失败";
                    NextID = "";
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.messsage = e.Message.ToString();
                        resp.result = -1;
                        NextID = "";
                    }
                }
                return NextID;
            }
        }
        public PublishCommentResponse PublishComment(PublishCommentRequest req)
        {
            PublishCommentResponse resp = new PublishCommentResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -2)
                return resp;

            int pnb = HasNoPublished(req, resp);
            if (pnb == 0)//若用户已经发表过评论
                return resp;

            string nowid = "";
            nowid = getNextID(req, resp);
            if (nowid == "")//增加comment_id失败
            {
                con.Close();
                return resp;
            }                

            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO COMMENTS VALUES('" + req.user_id + "','" + req.game_id + "'," + req.rating + ",'" + req.content + "',0,0,'" + nowid + "',to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))";
            cen = cmd.ExecuteNonQuery();
            if (cen == 0)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.result = -1;  //修改失败
                resp.messsage = "添加新comment失败";
            }
            else
            {
                try
                {
                    cmd.CommandText = "COMMIT";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 0;
                    resp.messsage = "添加成功";
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.messsage = e.Message.ToString();
                    resp.result = -1;
                }
            }
            con.Close();
            return resp;
        }
    }
}
