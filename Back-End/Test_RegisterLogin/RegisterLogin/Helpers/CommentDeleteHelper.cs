using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class CommentDeleteHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void OpenConn(CommentDeleteResponse resp)
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

        public CommentDeleteResponse DeleteComment(CommentDeleteRequest req)
        {
            CommentDeleteResponse resp = new CommentDeleteResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"DELETE FROM COMMENTS WHERE USER_ID='{req.user_id}' AND GAME_ID='{req.game_id}'";
            try
            {
                int rslt = cmd.ExecuteNonQuery();
                cmd.CommandText = rslt == 1 ? "COMMIT" : "ROLLBACK";
                resp.result = rslt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                resp.result = -1;
            }

            cmd.ExecuteNonQuery();
            con.Close();
            return resp;
        }
    }
}
