﻿using System;
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

        public GameRankResponse getGameRank(GameRankRequest req)
        {
            openConn();
            GameRankResponse resp = new GameRankResponse();
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
                        return resp;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        resp.result = 0;
                    }
                }
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
                        return resp;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        resp.result = 0;
                    }
                }
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
                        return resp;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        resp.result = 0;
                    }
                }
                return resp;
            }
            else
            {
                resp.result = 0;
                return resp;
            }    
        }
    }
}