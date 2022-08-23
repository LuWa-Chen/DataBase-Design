using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class DBHelper
    {
        public const string BASIC_NAME = "BASIC_USER";
        public const int ID_LEN = 10;

        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openLoginConn(ref LoginResponse resp)
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

        public void openRegisterConn(ref RegisterResponse resp)
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

        public int SetBasicUID(string uid)
        {
            int result = 0;
            string new_id = (int.Parse(uid) + 1).ToString("d10");

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"UPDATE GAME_USER SET ID='{new_id}' WHERE NAME='{BASIC_NAME}'";

            try
            {
                result = cmd.ExecuteNonQuery();
                cmd.CommandText = result == 0 ? "ROLLBACK" : "COMMIT";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();
            }
            return result;
        }

        public string calcID()
        {
            OracleCommand command = con.CreateCommand();
            command.CommandText = $"SELECT ID FROM GAME_USER WHERE NAME='{BASIC_NAME}'";
            string uid = "";
            OracleDataReader reader;

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                    uid = reader[0].ToString();
                int rslt = SetBasicUID(uid);
                if (rslt == 0)
                {
                    command.CommandText = "ROLLBACK";       //update failed 
                    uid = "";
                }
                else
                    command.CommandText = "COMMIT";
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                uid = "";
            }

            return uid;
        }

        public LoginResponse checkLogin(LoginRequest req)
        {
            LoginResponse resp = new LoginResponse();
            resp.result = 0;
            openLoginConn(ref resp);
            if (resp.result == -2)      //connection fail, return 
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT PASSWORD, NAME, ID FROM GAME_USER WHERE EMAIL = '" + req.email + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            
            if (!reader.HasRows)
                resp.result = -1;       //邮箱不存在
            else
            {
                if (reader.Read())
                {
                    string pwd = reader[0].ToString();
                    if (pwd == req.password)
                    {
                        resp.result = 0;    //成功登录                        
                        resp.name = reader[1].ToString();
                        resp.id = reader[2].ToString();
                    }            
                    else
                        resp.result = 1;    //密码错误

                }
            }
            con.Close();
            return resp;
        }

        public RegisterResponse register(RegisterRequest req)
        {
            RegisterResponse resp = new RegisterResponse();
            resp.result = 0;
            openRegisterConn(ref resp);
            if (resp.result == -2)      //connection fail, return 
                return resp;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT NAME FROM GAME_USER WHERE EMAIL = '" + req.email + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                resp.result = 1;
                resp.message = "该email已绑定账号！";
                con.Close();
                return resp;
            }

            try
            {
                cmd.CommandText = string.Format("SELECT ID FROM GAME_USER WHERE NAME = '{0}'", req.username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    resp.result = -1;
                    resp.message = "用户名已存在";
                }
                else
                {
                    string id = calcID();
                    if (id == "")
                    {
                        resp.result = -2;
                        resp.message = "连接故障";
                        con.Close();
                        return resp;
                    }
                    cmd.CommandText = $"INSERT INTO GAME_USER VALUES('{id}', '{req.password}', '{req.username}', 1, 0, '{req.email}', '', '', '', '{req.area}', '{req.time}')";
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

            con.Close();
            return resp;
        }
    }
}
