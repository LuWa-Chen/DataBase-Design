﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class UserEmailHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);

        public void openConn(ref UserEmailResponse resp)
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

        public UserEmailResponse editUserEmail(UserEmailRequest req)
        {
            UserEmailResponse resp = new UserEmailResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT EMAIL FROM GAME_USER WHERE ID = '" + req.id + "'";
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
                    cmd.CommandText = "SELECT PASSWORD FROM GAME_USER WHERE ID = '" + req.id + "'";
                    OracleDataReader reader1 = cmd.ExecuteReader();

                    if (reader1.Read())
                    {
                        string pwd = reader1[0].ToString();

                        if (pwd == req.password)
                        {
                            cmd.CommandText = "UPDATE GAME_USER SET EMAIL='" + req.email + "' WHERE ID ='" + req.id + "'";
                            cmd.ExecuteNonQuery();
                            resp.result = 1;
                            resp.message = "修改成功！";
                        }
                        else
                        {
                            resp.result = -2;
                            resp.message = "密码错误。";
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = 0;
                    resp.message = "修改失败。";
                }
            }

            con.Close();
            return resp;
        }
    }
}
