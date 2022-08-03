using System;
using Oracle.ManagedDataAccess.Client;
using GameNews.Models;
namespace News.Helpers
{
    public class NewsHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);

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
        public GameNewsResponse getGameNews(GameNewsRequest req)
        {
            openConn();
            GameNewsResponse resp = new GameNewsResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "select POST_COVER,POST_TITLE,POST_TIME,CONTENT_TEXT from GAME_POST where GAME_ID = '" + req.game_id + "' AND POST_INDEX =" + req.post_index;
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                resp.result = 0;       //查找游戏id不存在
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        resp.post_cover = reader[0].ToString();
                        resp.post_title = reader[1].ToString();
                        resp.post_time = reader[2].ToString();
                        resp.content_text = reader[3].ToString();
                        resp.result = 1;
                    }
                    //将剩余的tag加入列表中
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                }
            }
            return resp;
        }
    }
}
