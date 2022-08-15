using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class RecommendedGamesHelper
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

        public RecommendedGamesResponse getRecommendedGames(RecommendedGamesRequest req)
        {
            openConn();
            RecommendedGamesResponse resp = new RecommendedGamesResponse();
            OracleCommand cmd = con.CreateCommand();


            cmd.CommandText = "SELECT TAG FROM GAME_TAGS WHERE ID='" + req.game_id + "' AND TAG IN(SELECT TAG FROM (SELECT USER_ID,TAG FROM OWNERSHIP, GAME_TAGS WHERE OWNERSHIP.GAME_ID=GAME_TAGS.ID) WHERE USER_ID='" + req.user_id + "')";  //搜索当前游戏与用户已购买游戏相同的TAG
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                try
                {
                    int num = 0;   //记录已推荐的游戏数，这里设置推荐上限为4，可根据需求修改
                                   //查找成功，赋值变量
                    while (reader.Read())
                    {
                        cmd.CommandText = "SELECT ID FROM GAME_TAGS WHERE TAG='" + reader[0].ToString() + "' AND ID <> '" + req.game_id + "' AND ID NOT IN(SELECT GAME_ID FROM OWNERSHIP WHERE USER_ID='" + req.user_id + "')";  //根据搜索的TAG来搜索用户未购买的游戏，同时不包括当前游戏
                        OracleDataReader reader1 = cmd.ExecuteReader();
                        while (reader1.Read())
                        {
                            resp.game_list.Add(reader1[0].ToString());
                            num++;
                            if (num == 4)
                                break;
                        }
                        if (num == 4)
                            break;
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
            else    //若用户未购买游戏则推荐同类型的游戏
            {
                cmd.CommandText = "SELECT TAG FROM GAME_TAGS WHERE ID='" + req.game_id + "'";
                OracleDataReader reader2 = cmd.ExecuteReader();

                try
                {
                    int num = 0;   //记录已推荐的游戏数，这里设置推荐上限为4，可根据需求修改

                    //查找成功，赋值变量
                    while (reader2.Read())
                    {
                        cmd.CommandText = "SELECT ID FROM GAME_TAGS WHERE TAG='" + reader2[0].ToString() + "' AND ID <> '" + req.game_id + "'";  //根据搜索的TAG来搜索用户未购买的游戏，同时不包括当前游戏
                        OracleDataReader reader3 = cmd.ExecuteReader();
                        while (reader3.Read())
                        {
                            resp.game_list.Add(reader3[0].ToString());
                            num++;
                            if (num == 4)
                                break;
                        }
                        if (num == 4)
                            break;
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
    }
}
