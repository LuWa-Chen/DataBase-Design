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
    public class uploadColumnContentHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        ColumnContentResponse resp = new ColumnContentResponse();
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

        public async Task<ColPicPath> FileSave(ColumnContentRequest req, ColumnContentResponse resp)
        {
            int filecount = req.files.Count;
            string filename, fileext, filepath;
            IFormFile file;
            FileStream stream;
            ColPicPath ap = new ColPicPath();

            //存P1
            if (req.photo_num >= 1)
            {
                file = req.files.Files["Pic1Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic1Src = "Column/" + resp.column_id + "/image1" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image1" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P2
            if (req.photo_num >= 2)
            {
                file = req.files.Files["Pic2Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic2Src = "Column/" + resp.column_id + "/image2" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image2" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P3
            if (req.photo_num >= 3)
            {
                file = req.files.Files["Pic3Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic3Src = "Column/" + resp.column_id + "/image3" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image3" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P4
            if (req.photo_num >= 4)
            {
                file = req.files.Files["Pic4Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic4Src = "Column/" + resp.column_id + "/image4" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image4" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P5
            if (req.photo_num >= 5)
            {
                file = req.files.Files["Pic5Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic5Src = "Column/" + resp.column_id + "/image5" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image5" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P6
            if (req.photo_num >= 6)
            {
                file = req.files.Files["Pic6Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic6Src = "Column/" + resp.column_id + "/image6" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image6" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P7
            if (req.photo_num >= 7)
            {
                file = req.files.Files["Pic7Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic7Src = "Column/" + resp.column_id + "/image7" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image7" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P8
            if (req.photo_num >= 8)
            {
                file = req.files.Files["Pic8Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic8Src = "Column/" + resp.column_id + "/image8" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image8" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            //存P9
            if (req.photo_num == 9)
            {
                file = req.files.Files["Pic9Src"];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Column\\" + resp.column_id;
                ap.Pic9Src = "Column/" + resp.column_id + "/image9" + fileext;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\image9" + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            OkObjectResult re = new OkObjectResult(new { count = req.files.Count });
            return ap;
        }

        public ColumnContentResponse uploadColumnContent(ColumnContentRequest req, ColumnContentResponse resp,HttpRequest hreq)
        {
            openConn();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM GAME WHERE ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 999;       //游戏不存在
            }
            else
            {
                cmd.CommandText = "SELECT COLUMN_ID FROM COLUMNS";
                OracleDataReader reader1 = cmd.ExecuteReader();
                if (!reader1.HasRows)
                {
                    resp.column_id = "0000000001";
                }
                else
                {
                    while (reader1.Read())
                    {
                        resp.column_id = (int.Parse(reader1[0].ToString()) + 1).ToString("d10");
                    }
                }

                ColPicPath ap = FileSave(req, resp).Result;

                try
                {
                    cmd.CommandText = "INSERT INTO COLUMNS VALUES('" + resp.column_id + "','" + req.user_id + "','" + req.game_id + "','" + req.title + "','" + req.content + "',SYSDATE,'0')";    //初始评论为0
                    cmd.ExecuteNonQuery();

                    //查找成功，赋值变量

                    if (req.photo_num >= 1)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic1Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 2)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic2Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 3)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic3Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 4)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic4Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 5)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic5Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 6)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic6Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 7)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic7Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num >= 8)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic8Src + "')";
                        cmd.ExecuteNonQuery();
                    }

                    if (req.photo_num == 9)
                    {
                        cmd.CommandText = "INSERT INTO COLUMN_PHOTOS VALUES('" + resp.column_id + "','" + ap.Pic9Src + "')";
                        cmd.ExecuteNonQuery();
                    }
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

