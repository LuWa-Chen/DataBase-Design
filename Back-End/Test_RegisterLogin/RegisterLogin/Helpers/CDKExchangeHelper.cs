using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class CDKExchangeHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(CDKExchangeResponse resp)
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

        public int ExpireCDK(string value)
        {
            int ret = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"UPDATE CDKS SET HAVE_USED=1 WHERE CDK_VALUE='{value}'";
            try
            {
                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                    cmd.CommandText = "COMMIT";
                else
                    cmd.CommandText = "ROLLBACK";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
            }

            cmd.ExecuteNonQuery();
            return ret;
        }

        public CDKExchangeResponse ExchangeCDK(CDKExchangeRequest req)
        {
            CDKExchangeResponse resp = new CDKExchangeResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;

            /* First check if cdk is valid */
            Utils utils = new Utils();
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;
            string gid = "";        //game id corresponds to the cdk
            cmd.CommandText = $"SELECT HAVE_USED, GAME_ID FROM CDKS WHERE CDK_VALUE='{req.cdk_value}'";
            try
            {
                reader = cmd.ExecuteReader();
                if (!reader.HasRows)        //cdk not exists
                {
                    resp.result = 0;
                    con.Close();
                    return resp;
                }

                if (reader.Read())
                {
                    int used = int.Parse(reader[0].ToString());

                    //check if cdk is already used 
                    if (used == 1)    
                    {
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }

                    /* check whether user already owns the game */
                    gid = reader[1].ToString();
                    int has_game = utils.isGameInUserLib(gid, req.user_id, con);
                    if (has_game == 1)
                    {
                        resp.result = -1;
                        con.Close();
                        return resp;
                    }
                    else if (has_game == -1)
                    {
                        resp.result = -2;   //connection failed
                        con.Close();
                        return resp;
                    }

                    /* set cdk invalid and add game to user lib */
                    if (ExpireCDK(req.cdk_value) == 1 && utils.AddGame(gid, req.user_id, 1, con) == 1)
                        resp.result = 1;
                    else
                        resp.result = -2;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -2;
            }

            con.Close();
            return resp;
        }
    }
}
