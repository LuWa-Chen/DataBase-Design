using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class DiscountPublishHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        public OracleConnection con = new OracleConnection(connString);
        public void OpenConn(DiscountPublishResponse resp)
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
                resp.result = -1;
                resp.message = e.Message;
            }
        }

        public string GetGID(string name)
        {
            string gid = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT ID FROM GAME WHERE NAME='{name}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    gid = reader[0].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                gid = "ERROR";
            }
            return gid;
        }
        
        public DiscountPublishResponse PublishDiscount(DiscountPublishRequest req)
        {
            DiscountPublishResponse resp = new DiscountPublishResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
            {
                resp.message = "连接失败！";
                return resp;
            }

            if (req.discount >= 1 || req.discount < 0)
            {
                resp.message = "折扣不合法！";
                return resp;
            }

            string gid = GetGID(req.name);
            if (gid == "")
            {
                resp.message = "游戏不存在！";
                return resp;
            }
            else if (gid == "ERROR")
            {
                resp.message = "连接失败！";
                return resp;
            }

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"INSERT INTO DISCOUNT VALUES('{gid}', to_date('{req.start_date}', 'yyyy-mm-dd'), to_date('{req.end_date}', 'yyyy-mm-dd'), {req.discount})";
            try
            {
                cmd.ExecuteNonQuery();
                resp.message = "发布成功！";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.message = "连接失败！";
            }

            return resp;
        }
    }
}
