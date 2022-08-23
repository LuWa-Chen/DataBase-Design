using System.IO;
using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class uploadAvatarHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        UserAvatarResponse resp = new UserAvatarResponse();
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
                resp.result = -1;
                con.Close();
            }
        }

        public async Task<AvatarPath> FileSave(UserAvatarRequest req)
        {
            int filecount = req.files.Count;
            string filename, fileext, filepath;
            IFormFile file;
            FileStream stream;
            AvatarPath ap = new AvatarPath();

            //存头像
            file = req.files.Files["AvatarPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\User\\" + req.id + "\\ProfilePhoto";
            ap.AvatarPicSrc = "User/" + req.id + "/ProfilePhoto" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\ProfilePhoto" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            OkObjectResult re = new OkObjectResult(new { count = req.files.Count });
            return ap;
        }
       
        public UserAvatarResponse uploadAvatar(UserAvatarRequest req, HttpRequest hreq)
        {
            openConn();

            AvatarPath ap = FileSave(req).Result;

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT PROFILE_PHOTO FROM GAME_USER WHERE ID = '" + req.id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;       //查找id不存在
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    cmd.CommandText = "UPDATE GAME_USER SET PROFILE_PHOTO='" + ap.AvatarPicSrc + "' WHERE ID ='" + req.id + "'";
                    cmd.ExecuteNonQuery();
                    resp.result = 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = 0;
                }
            }

            con.Close();
            return resp;
        }
    }
}
