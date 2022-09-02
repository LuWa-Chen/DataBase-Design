using System.IO;
using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using PublisherProfile.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PublisherProfile.Helpers
{
    public class PublisherProfileHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        public OracleConnection con = new OracleConnection(connString);
        PublisherProfileResponse resp = new PublisherProfileResponse();
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
        public async Task<int> FileSave(string ID, PublisherProfileRequest req)
        {
            string filename, fileext, filepath;
            IFormFile file;
            FileStream stream;

            //存主封面
            file = req.files.Files["logo"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Publisher\\" + ID + "\\LOGO";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            return 1;
        }
        public int Update(string id, string New,int choice)//0表示更新名字，1更新电话
        {
            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            if (choice == 0)
                cmd.CommandText = "UPDATE PUBLISHER SET PUBLISHER_NAME = '" + New + "' WHERE ID = '" + id + "'";
            else
                cmd.CommandText = "UPDATE PUBLISHER SET TELEPHONE = '" + New + "' WHERE ID = '" + id + "'";
            try
            {
                cen = cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.result = 0;
                resp.message = e.Message;
                return 0;
            }

            return 1;
        }
        public PublisherProfileResponse PublishGame(PublisherProfileRequest req, HttpRequest hreq)
        {
            openConn();
            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            string filere = "", namere = "", numberre = "";

            if(req.files.Files.Count != 0)
            {
                if (FileSave(hreq.Form["id"], req).Result == 0)
                    return resp;
                else
                    filere = "(头像)";
            }

            if (hreq.Form["name"] != "")
            {
                if (Update(hreq.Form["id"], hreq.Form["name"], 0) == 0)
                    return resp;
                else
                    namere = "(名称)";
            }

            if (hreq.Form["phone_number"] != "")
            {
                if (Update(hreq.Form["id"], hreq.Form["phone_number"], 1) == 0)
                    return resp;
                else
                    numberre = "(电话)";
            }

            try
            {
                cmd.CommandText = "COMMIT";
                cen = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.result = 0;
                resp.message = e.Message;
            }

            resp.message = "更新成功，本次更新了" + filere + namere + numberre;
            resp.result = 1;
            return resp;
        }
    }
}
