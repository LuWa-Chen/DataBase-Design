using System;
using Oracle.ManagedDataAccess.Client;
using PublisherGameInfo.Models;
using System.Collections.Generic;
namespace PublisherGameInfo.Helpers
{
    public class PublisherGameInfoHelper
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
        public string getPublisherID(string publisher, PublisherGameInfoResponse resp)
        {
            string PublisherID = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM PUBLISHER WHERE PUBLISHER_NAME = '"+publisher+"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //查找的发行商不存在
                resp.response = "查找的发行商不存在！";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        PublisherID = reader[0].ToString();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                    resp.response = e.Message;
                }
            }
            return PublisherID;
        }
        public int getPublisherGameCount(string PublisherID, PublisherGameInfoResponse resp)
        {
            int count = -1;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(ID) FROM GAME WHERE PUBLISHER_ID = '"+PublisherID+"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //统计发行商游戏数量时错误
                resp.response = "统计发行商游戏数量时错误！";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        count = System.Convert.ToInt32(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                    resp.response = e.Message;
                }
            }
            resp.game_count = count;
            return count;
        }
        public List<string> getPublisherGameTag(string GameID, PublisherGameInfoResponse resp)
        {
            List<string> tag = new List<string>();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT TAG FROM GAME_TAGS WHERE ID = '" + GameID + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //统计发行商游戏数量时错误
                resp.response = "获取游戏" + GameID + "tag时错误！";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    while(reader.Read())
                    {
                        tag.Add(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                    resp.response = e.Message;
                }
            }
            return tag; 
        }
        public double getGameDiscount(string GameID, PublisherGameInfoResponse resp)
        {
            double rate = 1;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT DISCOUNT_RATE FROM DISCOUNT WHERE GAME_ID = '" + GameID + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                //说明游戏没有折扣信息
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        rate = System.Convert.ToDouble(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                    resp.response = e.Message;
                }
            }
            return rate;
        }
        public PublisherGameDetail getGameInfo(string GameID, PublisherGameInfoResponse resp)
        {
            PublisherGameDetail pgd = new PublisherGameDetail();
            double rate = getGameDiscount(GameID, resp);
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = " SELECT NAME,PUBLISH_DATE,PRICE,COVER FROM GAME WHERE ID = '" + GameID + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;     
                resp.response = "获取游戏" + GameID + "信息时错误！";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        pgd.game_id = GameID;
                        pgd.game_name = reader[0].ToString();
                        pgd.publish_date = reader[1].ToString();
                        pgd.pre_price = System.Convert.ToDouble(reader[2].ToString());
                        pgd.now_price = pgd.pre_price * rate;
                        pgd.discount = rate;
                        pgd.tag = getPublisherGameTag(GameID, resp);
                        pgd.image = reader[3].ToString();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                    resp.response = e.Message;
                }
            }
            return pgd;
        }
        public PublisherGameInfoResponse getPublisherGameInfo(PublisherGameInfoRequest req)
        {
            openConn();
            PublisherGameInfoResponse resp = new PublisherGameInfoResponse();

            string PublisherID = getPublisherID(req.publisher, resp);
            if(PublisherID == "")//查找发行商错误
            {
                con.Close();
                return resp;
            }

            int count = 0;
            count = getPublisherGameCount(PublisherID, resp);
            if(count == -1||count == 0)//统计数量错误或发行商无游戏
            {
                con.Close();
                return resp;
            }

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM GAME WHERE PUBLISHER_ID = '" + PublisherID + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            string TempGameID = "";
            if (!reader.HasRows)
            {
                resp.result = 0;       //该发行商没有游戏(该分支一般不会出现,出现说明数据库数据有误)
                resp.response = "该发行商没有游戏(该错误出现说明数据库数据有误)";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    while (reader.Read())
                    {
                        TempGameID = reader[0].ToString();
                        resp.games.Add(getGameInfo(TempGameID, resp));
                    }
                    resp.result = 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                    resp.response = e.Message;
                }
            }
            con.Close();
            return resp;
        }
    }
}

