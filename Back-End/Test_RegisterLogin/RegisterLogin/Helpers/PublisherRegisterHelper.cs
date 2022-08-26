using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegisterLogin.Helpers
{
    public class PublisherRegisterHelper
    {
        public const string BASIC_NAME = "BASIC_PID";
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        Utils util = new Utils();
        public void openConn(ref PublisherRegisterResponse resp)
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
        
        public int SetBasicPID(string pid)
        {
            int result = 0;
            string new_id = (int.Parse(pid) + 1).ToString("d10");

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"UPDATE PUBLISHER SET ID='{new_id}' WHERE PUBLISHER_NAME='{BASIC_NAME}'";

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

        public string calcPID()
        {
            OracleCommand command = con.CreateCommand();
            command.CommandText = $"SELECT ID FROM PUBLISHER WHERE PUBLISHER_NAME='{BASIC_NAME}'";
            string pid = "";
            OracleDataReader reader;

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                    pid = reader[0].ToString();
                int rslt = SetBasicPID(pid);
                if (rslt == 0)
                {
                    command.CommandText = "ROLLBACK";       //update failed 
                    pid = "";
                }
                else
                    command.CommandText = "COMMIT";
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                pid = "";
            }

            return pid;
        }

        public async Task<string> SaveLogo(string pid, PublisherRegisterRequest req)
        {
            IFormFile file;
            //string filename, fileext;
            FileStream stream;

            file = req.files.Files["logo"];
            //filename = file.FileName;
            //fileext = Path.GetExtension(filename);
            string filepath = $"C:\\ExGame-Asset\\Publisher\\{pid}";
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            string logo_path = $"{filepath}\\LOGO.jpg";
            stream = new FileStream(logo_path, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            return logo_path;
        }

        public PublisherRegisterResponse publisherRegister(PublisherRegisterRequest req, HttpRequest hreq)
        {
            PublisherRegisterResponse resp = new PublisherRegisterResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -2)
                return resp;

            /* check repetition of email*/
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;
            cmd.CommandText = $"SELECT PUBLISHER_NAME FROM PUBLISHER WHERE EMAIL='{hreq.Form["email"]}'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    resp.result = 1;
                    resp.message = "该email已绑定账号！";
                    con.Close();
                    return resp;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -2;
                resp.message = "连接故障！"; 
                con.Close();
                return resp;
            }

            /* check repetition of username */
            cmd.CommandText = $"SELECT ID FROM PUBLISHER WHERE PUBLISHER_NAME='{hreq.Form["username"]}'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    resp.result = -1;
                    resp.message = "该用户名已存在！";
                    con.Close();
                    return resp;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = -2;
                resp.message = "连接故障！";
                con.Close();
                return resp;
            }

            /* try to create pid */
            string pid = calcPID();
            if (pid == "")
            {
                resp.result = -2;
                resp.message = "连接故障";
                con.Close();
                return resp;
            }

            /* create a publisher into the database */
            string logo_path = $"C:\\ExGame-Asset\\Publisher\\{pid}\\LOGO.jpg";
            cmd.CommandText = $"INSERT INTO PUBLISHER VALUES('{pid}', '{hreq.Form["password"]}', '{hreq.Form["username"]}', '{logo_path}', '{hreq.Form["intro"]}', '{hreq.Form["email"]}', to_date('{hreq.Form["time"]}','yyyy-mm-dd'), '{hreq.Form["area"]}', '{hreq.Form["phonenum"]}')";
            try
            {
                int rslt = cmd.ExecuteNonQuery();
                if (rslt == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cmd.ExecuteNonQuery();
                    resp.result = -2;
                    resp.message = "连接故障";
                }
                else
                {
                    string s = SaveLogo(pid, req).Result;
                    cmd.CommandText = "COMMIT";
                    cmd.ExecuteNonQuery();
                    resp.result = 0;
                    resp.message = "注册成功！";
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();
                resp.result = -2;
                resp.message = "连接故障";
            }

            con.Close();
            return resp;
        }
    }
}
