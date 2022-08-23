using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class CartGetHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(ref CartGetResponse resp)
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
        public CartGetResponse GetCartInfo(CartGetRequest req)
        {
            CartGetResponse resp = new CartGetResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;

            OracleCommand main_cmd = con.CreateCommand();
            OracleDataReader reader;

            try
            {
                main_cmd.CommandText = $"SELECT GAME_ID FROM CART WHERE USER_ID='{req.user_id}'";
                reader = main_cmd.ExecuteReader();
                string gid = "";
                OracleCommand sub_cmd = con.CreateCommand();
                OracleDataReader sub_reader;
                Dictionary<string, dynamic> game;

                while (reader.Read())
                {
                    gid = reader[0].ToString();
                    sub_cmd.CommandText = $"SELECT NAME, COVER, PRICE, FUN_GET_CURRENT_DISCOUNT(ID), IS_DLC FROM GAME WHERE ID='{gid}'";
                    sub_reader = sub_cmd.ExecuteReader();
                    while (sub_reader.Read())
                    {
                        game = new Dictionary<string, dynamic>();

                        game["id"] = gid;
                        game["name"] = sub_reader[0].ToString();
                        game["poster"] = sub_reader[1].ToString();
                        game["price"] = double.Parse(sub_reader[2].ToString());
                        game["discount"] = double.Parse(sub_reader[3].ToString()) * 100;
                        game["is_dlc"] = int.Parse(sub_reader[4].ToString()) == 1 ? true : false;

                        resp.game_list.Add(game);
                    }
                }

                resp.result = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = 0;
            }

            con.Close();
            return resp;
        }
    }
}
