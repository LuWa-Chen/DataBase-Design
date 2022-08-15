using System;
using Oracle.ManagedDataAccess.Client;
using logout.Models;
namespace logout.Helpers
{
    public class logoutHelper
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
        public logoutResponse logout(logoutRequest req)
        {
            openConn();
            logoutResponse resp = new logoutResponse();
            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE GAME_USER SET STATUS = 0 WHERE ID = '"+req.id+"'";
            cen = cmd.ExecuteNonQuery();
            if (cen == 0)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.result = 0;  //修改失败
            }
            else
            {
                try
                {
                    cmd.CommandText = "COMMIT";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 1;
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    Console.WriteLine(e);
                    resp.result = -1;
                }
            }
            return resp;
        }
    }
}
