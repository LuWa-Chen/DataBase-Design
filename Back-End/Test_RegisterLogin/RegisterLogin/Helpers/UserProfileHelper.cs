using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class UserProfileHelper
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

        public UserProfileResponse editUserProflie(UserProfileRequest req)
        {
            openConn();
            UserProfileResponse resp = new UserProfileResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT NAME,AREA,BIRTHDAY,INTRO FROM GAME_USER WHERE ID = '" + req.id + "'AND ID <> '" + req.id +"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //查找id不存在
                resp.message = "修改失败。";
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    cmd.CommandText = string.Format("SELECT ID FROM GAME_USER WHERE NAME = '{0}'", req.name);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        resp.result = 0;
                        resp.message = "用户名已存在";
                    }
                    else
                    {
                        //注意数据库date格式
                        string[] birthdayArr = req.birthday.Split('-');
                        string temp = birthdayArr[2] + "-" + int.Parse(birthdayArr[1]).ToString() + "月-" + birthdayArr[0].Substring(2);

                        cmd.CommandText = "UPDATE GAME_USER SET NAME='" + req.name + "',AREA='" + req.area + "',BIRTHDAY='" + temp + "',INTRO='" + req.intro + "' WHERE ID ='" + req.id + "'";
                        cmd.ExecuteNonQuery();
                        resp.result = 1;
                        resp.message = "修改成功！";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = 0;
                    resp.message = "修改失败。";
                }
            }
            return resp;
        }
    }
}
