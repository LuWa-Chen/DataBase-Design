using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using PublisherInfo.Models;
using System;

namespace PublisherInfo.Helpers
{
    public class PublisherInfoHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        public OracleConnection con = new OracleConnection(connString);
        PublisherInfoResponse resp = new PublisherInfoResponse();
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
                resp.result = -1;
                resp.message = e.Message;
            }
        }
        public PublisherInfoResponse getPublisherInfo(PublisherInfoRequest req)
        {
            openConn();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT TELEPHONE,INTRO_TEXT,EMAIL,AREA FROM PUBLISHER WHERE ID = '" + req.id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //查找游戏id不存在
                resp.message = "查找发行商id不存在";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        resp.phone_number = reader[0].ToString();
                        resp.intro = reader[1].ToString();
                        resp.email = reader[2].ToString();
                        resp.area = reader[3].ToString();
                        resp.result = 1;
                    }
                }
                catch (Exception e)
                {
                    resp.message = e.Message;
                    resp.result = -1;
                }
            }
            con.Close();
            return resp;
        }
    }
}
