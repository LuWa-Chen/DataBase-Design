using System;
using Oracle.ManagedDataAccess.Client;
using GameNews.Models;
namespace News.Helpers
{
    public class NewsHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);

        public void openConn(ref GameNewsResponse resp)
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
        public GameNewsResponse getGameNews(GameNewsRequest req)
        {
            GameNewsResponse resp = new GameNewsResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;
            gamenew gn;
            
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select POST_COVER,POST_TITLE,POST_TIME,CONTENT_TEXT from GAME_POST where GAME_ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //查找游戏id不存在
                resp.message = "该游戏无推送";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    while (reader.Read())
                    {
                        gn = new gamenew();
                        gn.post_cover = reader[0].ToString();
                        gn.post_title = reader[1].ToString();
                        gn.post_time = reader[2].ToString();
                        gn.content_text = reader[3].ToString();
                        resp.gn.Add(gn);
                    }
                    resp.result = 1;
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
    }
}
