using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class PublisherRegisterHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        Utils util = new Utils();
        public void openConn()
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
            }
        }

        public PublisherRegisterResponse publisherRegister(PublisherRegisterRequest req)
        {
            openConn();
            PublisherRegisterResponse resp = new PublisherRegisterResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = $"SELECT PUBLISHER_NAME FROM PUBLISHER WHERE EMAIL='{req.email}'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                resp.result = 1; 
                resp.message = "该email已绑定账号！";
                return resp;
            }

            try
            {
                cmd.CommandText = "SELECT COUNT(ID) FROM PUBLISHER";
                reader = cmd.ExecuteReader();
                string id = "";
                if (reader.Read())
                    id = util.calcID(reader[0].ToString());

                cmd.CommandText = $"SELECT ID FROM PUBLISHER WHERE PUBLISHER_NAME='{req.username}'";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    resp.result = -1;
                    resp.message = "用户名已存在";
                }
                else
                {
                    cmd.CommandText = $"INSERT INTO PUBLISHER VALUES('{id}', '{req.password}', '{req.username}', '', '', '{req.email}', '{util.convert2date(req.time)}', '{req.area}')";
                    cmd.ExecuteNonQuery();
                    resp.result = 0;
                    resp.message = "注册成功！";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = 2;
                resp.message = "注册失败！";
            }

            return resp;
        }
    }
}
