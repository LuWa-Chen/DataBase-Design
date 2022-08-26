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
        public const string BASIC_UID = "BASICORDER";
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

        public int SetBasicOID(string oid)
        {
            int result = 0;
            string new_id = (int.Parse(oid) + 1).ToString("d20");
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"UPDATE GAME_ORDER SET ID='{new_id}' WHERE USER_ID='{BASIC_UID}'";

            try
            {
                result = cmd.ExecuteNonQuery();
                cmd.CommandText = result == 0 ? "ROLLBACK" : "COMMIT";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
            }
            cmd.ExecuteNonQuery();

            return result;            
        }

        public string CalcOrderID()
        {
            OracleCommand command = con.CreateCommand();
            command.CommandText = $"SELECT ID FROM GAME_ORDER WHERE USER_ID='{BASIC_UID}'";
            string oid = "";
            OracleDataReader reader;

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                    oid = reader[0].ToString();

                if (SetBasicOID(oid) == 0)
                    oid = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                oid = "";
            }

            return oid;
        }

        public int CreateOrder(string uid, string gid, string time, int method, double amount, string rid)
        {
            int ret = 0;
            OracleCommand cmd = con.CreateCommand();
            string oid = CalcOrderID();

            if (oid == "")
                return ret;     //create failed

            cmd.CommandText = $"INSERT INTO GAME_ORDER VALUES('{oid}', '{uid}', '{gid}', to_date('{time}', 'yyyy-mm-dd hh24:mi:ss'), {method}, {amount}, '{rid}')";
            try
            {
                ret = cmd.ExecuteNonQuery();
                cmd.CommandText = ret == 0 ? "ROLLBACK" : "COMMIT";
            }
            catch (Exception e)
            {
                ret = 0;
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
            }
            cmd.ExecuteNonQuery();

            return ret;
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
                    if (ExpireCDK(req.cdk_value) == 1 && CreateOrder(req.user_id, gid, req.order_time, 0, 0, req.user_id) == 1 && utils.AddGame(gid, req.user_id, 1, con) == 1)
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
