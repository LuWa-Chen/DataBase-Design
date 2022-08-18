using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class UserGameInfoHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(ref UserGameInfoResponse resp)
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

        public UserGameInfoResponse getUserGameInfo(UserGameInfoRequest req)
        {
            UserGameInfoResponse resp = new UserGameInfoResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -2)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT GAME_ID FROM OWNERSHIP WHERE USER_ID='" + req.id + "'";
            OracleDataReader reader0 = cmd.ExecuteReader();
            if (!reader0.HasRows)
                resp.result = 0;       //查找用户id不存在
            else
            {
                try
                {
                    int num = 0;
                    
                    //查找成功，赋值变量
                    while (reader0.Read())
                    {
                        get_user_game_info info = new get_user_game_info();
                        info.game_id = reader0[0].ToString();

                        cmd.CommandText = "SELECT NAME ,COVER FROM GAME WHERE ID='" + info.game_id + "'";
                        OracleDataReader reader1 = cmd.ExecuteReader();
                        if (reader1.Read())
                        {
                            info.game_name = reader1[0].ToString();
                            info.cover = reader1[1].ToString();
                        }
                        resp.game_info_set.Add(info);
                        num++;
                    }
                    resp.game_num= num;
                    if (num != 0)          
                        resp.result = 1;
                    else                    
                    {
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
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
