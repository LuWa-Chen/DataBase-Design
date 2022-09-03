using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameDetailProHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(ref GameDetailProResponse resp)
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
        public int GetCommentNum(GameDetailProRequest req)
        {
            int comment_num = 0;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(USER_ID) FROM COMMENTS WHERE GAME_ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                comment_num = 0;
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        comment_num = System.Convert.ToInt32(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
            return comment_num;
        }
        public string GetRateResult(GameDetailProRequest req)
        {
            int ratesum = 0;
            double ratedbl = 0;
            string RateResult = "";
            int comment_num = GetCommentNum(req);

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT RATING FROM COMMENTS WHERE GAME_ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                comment_num = 0;
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    while (reader.Read())
                    {
                        ratesum += System.Convert.ToInt32(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "暂无评论";
                }
            }

            ratedbl = System.Convert.ToDouble(ratesum) / (System.Convert.ToDouble(comment_num));
            if (comment_num != 0)
            {
                if (ratedbl >= 4.5)
                    RateResult = "好评如潮";
                else if (ratedbl >= 4.0)
                    RateResult = "特别好评";
                else if (ratedbl >= 3.0)
                    RateResult = "多半好评";
                else if (ratedbl >= 2.0)
                    RateResult = "褒贬不一";
                else if (ratedbl >= 1.0)
                    RateResult = "多半差评";
                else if (ratedbl >= 0.5)
                    RateResult = "特别差评";
                else
                    RateResult = "差评如潮";
            }
            else
                RateResult = "暂无评论";
            return RateResult;
        }
        public GameDetailProResponse getGameDetailPro(GameDetailProRequest req)
        {
            GameDetailProResponse resp = new GameDetailProResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = string.Format("SELECT NAME, PUBLISH_DATE, PRICE, PUBLISHER_ID FROM GAME WHERE ID={0}", req.game_id);
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                resp.result = 0;       //查找游戏id不存在
            else
            {
                try
                {
                    string pid = "";
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        resp.game_name = reader[0].ToString();
                        resp.publish_date = reader[1].ToString();
                        resp.price = System.Convert.ToDouble(reader[2].ToString());
                        pid = reader[3].ToString();

                        if (pid != "")          //pid存在
                            resp.result = 1;
                        else                    //pid不存在
                        {
                            resp.result = 0;
                            con.Close();
                            return resp;
                        }
                    }

                    /* 查找相关资源 */
                    //查找discount
                    cmd.CommandText = string.Format("SELECT DISCOUNT_RATE FROM DISCOUNT WHERE GAME_ID={0} AND start_time <= (select sysdate from dual) and end_time >= (select sysdate from dual)", req.game_id);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                        resp.discount = 1;
                    else if (reader.Read())
                        resp.discount = double.Parse(reader[0].ToString());

                    // 查找publisher
                    cmd.CommandText = string.Format("SELECT PUBLISHER_NAME FROM PUBLISHER WHERE ID={0}", pid);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                        resp.publisher = resp.developer = reader[0].ToString();

                    //查找video_list
                    cmd.CommandText = string.Format("SELECT VIDEO FROM GAME_VIDEOS WHERE GAME_ID={0}", req.game_id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        resp.video_list.Add(reader[0].ToString());

                    //查找image_list
                    cmd.CommandText = string.Format("SELECT AD_PHOTO FROM GAME_PHOTOS WHERE GAME_ID={0}", req.game_id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        resp.image_list.Add(reader[0].ToString());

                    //查找tag_list
                    cmd.CommandText = string.Format("SELECT TAG FROM GAME_TAGS WHERE ID={0}", req.game_id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        resp.tag_list.Add(reader[0].ToString());

                    //获取is_launched
                    cmd.CommandText = $"SELECT FUN_IS_AVAILABLE('{req.game_id}') FROM DUAL";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                        resp.is_launched = int.Parse(reader[0].ToString()) > 0 ? true : false;

                    resp.RateResult = GetRateResult(req);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                }
            }
            con.Close();
            return resp;
        }
    }
}
