using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class addFriendsHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(FriendResponse resp)
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

        public FriendResponse addFriends(FriendRequest req)
        {
            FriendResponse resp = new FriendResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM GAME_USER WHERE ID='" + req.id_A + "'";
            OracleDataReader reader0 = cmd.ExecuteReader();
            cmd.CommandText = "SELECT * FROM GAME_USER WHERE ID='" + req.id_B + "'";
            OracleDataReader reader1 = cmd.ExecuteReader();

            if (!reader0.HasRows|| !reader1.HasRows)
                resp.result = 0;       //用户不存在
            else
            {
                try
                {
                    //查找成功，赋值变量
                    cmd.CommandText = "INSERT INTO FRIENDSHIP VALUES ('"+ req.id_A+"','"+ req.id_B+"')";
                    cmd.ExecuteNonQuery();

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
    }
}
