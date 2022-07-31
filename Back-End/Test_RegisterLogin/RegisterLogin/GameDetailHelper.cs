using Oracle.ManagedDataAccess.Client;
using System.Collections;
using getGameInfo.Models;
using getGameName.Models;
using System;

namespace GameDetail
{
    public class GameDetailHelper
    {
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

        public getGameNameResponse findGameName(getGameNameRequest req)
        {
            openConn();
            getGameNameResponse resp = new getGameNameResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT NAME FROM GAME WHERE ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                resp.result = 0;       //查找游戏id不存在
            else
            {
                try
                {
                    reader.Read();
                    string name = reader[0].ToString();
                    resp.game_name = name;
                    resp.result = 1;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                }
            }

            return resp;
        }
        public getGameInfoResponse findGameInfo(getGameInfoRequest req)
        {
            openConn();
            getGameInfoResponse resp = new getGameInfoResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "select name,PUBLISH_DATE,PRICE,COVER,GENERAL_INTRO,tag from game natural join game_tags where id = '" + req.game_id + "'";
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
                        resp.game_name = reader[0].ToString();
                        resp.publish_date = reader[1].ToString();
                        resp.price = System.Convert.ToDouble(reader[2].ToString());
                        resp.cover = reader[3].ToString();
                        resp.general_intro = reader[4].ToString();
                        resp.tag.Add(reader[5].ToString());
                        resp.result = 1;
                    }
                    //将剩余的tag加入列表中
                    while (reader.Read())
                    {
                        resp.tag.Add(reader[5].ToString());
                    }
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
