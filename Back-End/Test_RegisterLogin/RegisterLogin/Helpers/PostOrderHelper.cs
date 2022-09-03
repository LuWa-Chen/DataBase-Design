using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class PostOrderHelper
    {
        public const string BASIC_UID = "BASICORDER";
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void OpenConn(PostOrderResponse resp)
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

        public int CreateOrder(string uid, string gid, int method, double amount, string rid,ref string order_id)
        {
            int ret = 0;
            OracleCommand cmd = con.CreateCommand();
            string oid = CalcOrderID();

            if (oid == "")
                return ret;     //create failed

            cmd.CommandText = $"INSERT INTO GAME_ORDER VALUES('{oid}', '{uid}', '{gid}',SYSDATE, {method}, {amount}, '{rid}')";
            try
            {
                ret = cmd.ExecuteNonQuery();
                cmd.CommandText = ret == 0 ? "ROLLBACK" : "COMMIT";
                order_id = oid;
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

        public PostOrderResponse PostOrder(PostOrderRequest req)
        {
            PostOrderResponse resp = new PostOrderResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
                return resp;

            try
            {
                OracleCommand cmd = con.CreateCommand();

                string oid="";

                CreateOrder(req.user_id, req.game_id, req.via_cdk, req.pay_amount, req.recept_id,ref oid);

                resp.order_id=oid;

                cmd.CommandText = "UPDATE GAME SET TOT_DEAL_NUM=TOT_DEAL_NUM+1 WHERE ID='" + req.game_id + "'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT ORDER_TIME FROM GAME_ORDER WHERE ID='" + resp.order_id + "'";
                OracleDataReader reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    resp.order_time = reader1[0].ToString();
                }

                resp.result = 1;
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
