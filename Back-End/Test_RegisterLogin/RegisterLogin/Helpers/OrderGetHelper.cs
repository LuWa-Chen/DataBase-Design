using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class OrderGetHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void OpenConn(OrderGetResponse resp)
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

        public OrderGetResponse GetOrder(OrderGetRequest req)
        {
            OrderGetResponse resp = new OrderGetResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
                return resp;

            Dictionary<string, dynamic> order;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT FUN_GET_GAME_NAME(GAME_ID), ID, ORDER_TIME, METHOD, AMOUNT FROM GAME_ORDER WHERE USER_ID='{req.id}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    order = new Dictionary<string, dynamic>();
                    order["game_name"] = reader[0].ToString();
                    order["order_id"] = reader[1].ToString();
                    order["order_time"] = reader[2].ToString();
                    order["method"] = int.Parse(reader[3].ToString());
                    order["amount"] = double.Parse(reader[4].ToString());
                    resp.order_list.Add(order);
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
