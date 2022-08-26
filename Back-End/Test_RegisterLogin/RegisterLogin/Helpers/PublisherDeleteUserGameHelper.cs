using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class PublisherDeleteUserGameHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);

        public void openConn(ref UserGameResponse resp)
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

        public UserGameResponse deleteUserGame(UserGameRequest req)
        {
            UserGameResponse resp = new UserGameResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT USER_ID,GAME_ID FROM OWNERSHIP WHERE USER_ID = '" + req.user_id + "' AND GAME_ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                resp.result = 0;       //查找记录不存在
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    cmd.CommandText = "DELETE FROM OWNERSHIP WHERE USER_ID = '" + req.user_id + "' AND GAME_ID = '" + req.game_id + "'";
                    cmd.ExecuteNonQuery();
                    resp.result = 1;
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
    }
}
