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
    public class removeFriendsHelper
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
                resp.result = 0;
                con.Close();
            }
        }

        public FriendResponse removeFriends(FriendRequest req)
        {
            FriendResponse resp = new FriendResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM FRIENDSHIP WHERE FRIENDA_ID='" + req.id_A + "' AND FRIENDB_ID='" + req.id_B + "'";
            OracleDataReader reader0 = cmd.ExecuteReader();
            cmd.CommandText = "SELECT * FROM FRIENDSHIP WHERE FRIENDB_ID='" + req.id_A + "' AND FRIENDA_ID='" + req.id_B + "'";
            OracleDataReader reader1 = cmd.ExecuteReader();

            if (!reader0.HasRows && !reader1.HasRows)
                resp.result = 0;       //用户不存在
            else
            {
                if (reader0.Read())
                {
                    try
                    {
                        //查找成功，删除
                        cmd.CommandText = "DELETE FROM FRIENDSHIP WHERE FRIENDA_ID='" + req.id_A + "' AND FRIENDB_ID='" + req.id_B + "'";
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
                else if (reader1.Read())
                {
                    try
                    {
                        //查找成功，删除
                        cmd.CommandText = "DELETE FROM FRIENDSHIP WHERE FRIENDB_ID='" + req.id_A + "' AND FRIENDA_ID='" + req.id_B + "'";
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
            }
            con.Close();
            return resp;
        }
    }
}