using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameRankHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(GameRankResponse resp)
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

        public GameRankResponse getGameRank(GameRankRequest req)
        {
            GameRankResponse resp = new GameRankResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;
                        
            OracleCommand cmd = con.CreateCommand();
            if(req.Rank_name=="hot")
            {
                cmd.CommandText = "SELECT ID FROM (SELECT * FROM GAME ORDER BY TOT_DEAL_NUM DESC) WHERE ROWNUM <='" + req.Number + "'";
                OracleDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                    resp.result = 0;       //查找游戏id不存在
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        while (reader.Read())
                        {
                            resp.id_list.Add(reader[0].ToString());
                        }
                        resp.result = 1;
                        con.Close();
                        return resp;
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
            else if(req.Rank_name=="new")
            {
                cmd.CommandText = "SELECT ID FROM (SELECT * FROM GAME WHERE (SYSDATE - INTERVAL '30' DAY)<= PUBLISH_DATE ORDER BY TOT_DEAL_NUM DESC) WHERE ROWNUM <='" + req.Number + "'";
                OracleDataReader reader1 = cmd.ExecuteReader();

                if (!reader1.HasRows)
                    resp.result = 0;       //查找游戏id不存在
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        while (reader1.Read())
                        {
                            resp.id_list.Add(reader1[0].ToString());
                        }
                        resp.result = 1;
                        con.Close();
                        return resp;
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
            else if(req.Rank_name=="praise")
            {
                cmd.CommandText = "SELECT ID FROM (SELECT * FROM GAME ORDER BY LIKE_NUM DESC) WHERE ROWNUM <='" + req.Number + "'";
                OracleDataReader reader2 = cmd.ExecuteReader();

                if (!reader2.HasRows)
                    resp.result = 0;       //查找游戏id不存在
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        while (reader2.Read())
                        {
                            resp.id_list.Add(reader2[0].ToString());
                        }
                        resp.result = 1;
                        con.Close();
                        return resp;
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
            else if (req.Rank_name == "recommend")
            {
                cmd.CommandText = "SELECT ID FROM (SELECT * FROM GAME ORDER BY TOT_DEAL_NUM DESC,LIKE_NUM DESC) WHERE ROWNUM <='" + req.Number + "'";
                OracleDataReader reader3 = cmd.ExecuteReader();

                if (!reader3.HasRows)
                    resp.result = 0;       //查找游戏id不存在
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        while (reader3.Read())
                        {
                            resp.id_list.Add(reader3[0].ToString());
                        }
                        resp.result = 1;
                        con.Close();
                        return resp;
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
            else if (req.Rank_name == "later")
            {
                cmd.CommandText = "SELECT ID FROM (SELECT * FROM GAME WHERE (SYSDATE + INTERVAL '30' DAY)>= PUBLISH_DATE AND PUBLISH_DATE>SYSDATE) WHERE ROWNUM <='" + req.Number + "'";
                OracleDataReader reader4 = cmd.ExecuteReader();

                if (!reader4.HasRows)
                    resp.result = 0;       //查找游戏id不存在
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        while (reader4.Read())
                        {
                            resp.id_list.Add(reader4[0].ToString());
                        }
                        resp.result = 1;
                        con.Close();
                        return resp;
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
            else
            {
                resp.result = 0;
                con.Close();
                return resp;
            }    
        }
    }
}