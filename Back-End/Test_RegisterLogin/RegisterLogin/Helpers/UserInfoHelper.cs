using System;
using Oracle.ManagedDataAccess.Client;
using UserInfo.Models;

namespace UserInfo.Helpers
{
    public class UserInfoHelper
    {
        public const int ID_LEN = 10;
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
        public UserInfoResponse getUserInfo(UserInfoRequest req)
        {
            openConn();
            UserInfoResponse resp = new UserInfoResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "select PASSWORD,NAME,STATUS,GAME_NUM,EMAIL,BIRTHDAY,INTRO,PROFILE_PHOTO,AREA from GAME_USER where ID = '" + req.id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                resp.result = 0;       //查找id不存在
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        resp.password = reader[0].ToString();
                        resp.name = reader[1].ToString();
                        resp.status = System.Convert.ToInt32(reader[2].ToString());
                        resp.game_num = System.Convert.ToInt32(reader[3].ToString());
                        resp.email = reader[4].ToString();
                        resp.birthday = (reader[5].ToString()).Split(" ")[0];
                        resp.intro = reader[6].ToString();
                        resp.profile_photo = reader[7].ToString();
                        resp.area = reader[8].ToString();
                        resp.result = 1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                }
            }
            return resp;
        }
    }
}
