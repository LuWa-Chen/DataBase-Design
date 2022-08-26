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
                cmd.CommandText = "SELECT ID FROM GAME_ORDER";
                OracleDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    resp.order_id = "0000000001";
                }
                else
                {
                    while (reader.Read())
                    {
                        resp.order_id = (int.Parse(reader[0].ToString()) + 1).ToString("d10");
                    }
                }

                cmd.CommandText = "INSERT INTO GAME_ORDER VALUES('" + resp.order_id + "','" + req.user_id + "','" + req.game_id + "',SYSDATE,'" + req.via_cdk + "','" + req.pay_amount + "','" + req.recept_id + "')";
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
