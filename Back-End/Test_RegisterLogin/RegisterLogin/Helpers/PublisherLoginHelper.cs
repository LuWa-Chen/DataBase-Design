using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using Oracle.ManagedDataAccess.Client;

namespace RegisterLogin.Helpers
{
    public class PublisherLoginHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
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
        public PublisherLoginResponse publisherLogin(PublisherLoginRequest req)
        {
            openConn();
            PublisherLoginResponse resp = new PublisherLoginResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = $"SELECT PASSWORD FROM PUBLISHER WHERE EMAIL='{req.email}'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                resp.result = -1;       //邮箱不存在
            else
            {
                if (reader.Read())
                {
                    if (reader[0].ToString() == req.password)
                        resp.result = 0;    //成功登录
                    else
                        resp.result = 1;    //密码错误
                }
            }
            
            return resp;
        }
    }
}
