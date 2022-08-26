using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;


namespace RegisterLogin.Helpers
{
    public class UserIDGetHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void OpenConn(UserIDGetResponse resp)
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

        public UserIDGetResponse GetUID(UserIDGetRequest req)
        {
            UserIDGetResponse resp = new UserIDGetResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT ID FROM GAME_USER WHERE NAME='{req.user_name}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    resp.user_id = reader[0].ToString();
                    resp.result = 1;
                }
                else
                    resp.result = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -1;
            }

            con.Close();
            return resp;
        }
    }
}
