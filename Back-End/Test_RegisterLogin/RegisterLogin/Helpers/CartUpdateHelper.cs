using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class CartUpdateHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(CartUpdateResponse resp)
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

        public int AddGame2Cart(CartUpdateRequest req)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"INSERT INTO CART VALUES('{req.user_id}', '{req.game_id}')";
            int cen = 0;
            int ret = 0;
            try
            {
                cen = cmd.ExecuteNonQuery();
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "COMMIT";
                    cmd.ExecuteNonQuery();
                    ret = 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();
            }

            return ret;
        }

        public int RemoveGameFromCart(CartUpdateRequest req)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"DELETE FROM CART WHERE USER_ID='{req.user_id}' AND GAME_ID='{req.game_id}'";
            int ret = 0;
            int cen = 0;

            try
            {
                cen = cmd.ExecuteNonQuery();
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cmd.ExecuteNonQuery();
                }                
                else
                {
                    cmd.CommandText = "COMMIT";
                    cmd.ExecuteNonQuery();
                    ret = 2;
                }
            }      
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();
            }
            
            return ret;
        }

        public CartUpdateResponse UpdateCartInfo(CartUpdateRequest req)
        {
            CartUpdateResponse resp = new CartUpdateResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;

            switch (req.op_type)
            {
                case 1:
                    resp.result = AddGame2Cart(req);
                    break;
                case 2:
                    resp.result = RemoveGameFromCart(req);
                    break;
                default:
                    break;
            }

            con.Close();
            return resp;
        }
    }
}
