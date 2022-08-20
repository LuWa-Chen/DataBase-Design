using System.IO;
using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using testDataForm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace publishgame.Helpers
{
    public class publishgameHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        testDataFormResponse resp = new testDataFormResponse();
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
        public int HasNoPublished(HttpRequest hreq)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM GAME WHERE NAME = '"+hreq.Form["name"]+"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return 1;       //说明无重名
            else
            {
                resp.result = 0;
                resp.message = "该游戏有重名!";
                return 0;
            }
        }
        public string getNextID()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM GAME WHERE NAME = 'testNext'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;
                resp.message = "无法查询到下一个game_id";
                return "";
            }
            else
            {
                reader.Read();
                string NextID = reader[0].ToString();
                int outid = System.Convert.ToInt32(NextID);
                outid += 1;
                string AddID = outid.ToString("0000000000");

                int cen = 0;
                cmd.CommandText = "UPDATE GAME SET ID = '" + AddID + "' WHERE NAME = 'testNext'";
                cen = cmd.ExecuteNonQuery();
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 0;  //修改失败
                    resp.message = "修改GAME记录ID失败";
                    NextID = "";
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.message = e.Message.ToString();
                        resp.result = 0;
                        NextID = "";
                    }
                }
                return NextID;
            }
        }
        public string getPublishedID(HttpRequest hreq)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM PUBLISHER WHERE PUBLISHER_NAME = '" + hreq.Form["publisher"] + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;
                resp.message = "该发行商未注册!";
                return "";       //发行商没注册
            }
            else
            {
                reader.Read();
                string publisherID = reader[0].ToString();
                return publisherID;
            }
        }
        public async Task<testDataPath> FileSave(string NextID, testDataFormRequest req)
        {
            int filecount = req.files.Count;
            string filename,fileext,filepath;
            IFormFile file;
            FileStream stream;
            testDataPath tdp = new testDataPath();

            //存主封面
            file = req.files.Files["MainPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Cover";
            tdp.MainPicSrc = "Game/" + NextID + "/Cover/Cover" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath+ "\\Cover" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //存副封面
            file = req.files.Files["AssPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Cover";
            tdp.AssPicSrc = "Game/" + NextID + "/Cover/anCover" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\anCover" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //存背景图片
            file = req.files.Files["BacPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Cover";
            tdp.BacPicSrc = "Game/" + NextID + "/Cover/background" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\background" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //存介绍图片
            file = req.files.Files["ConPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Cover";
            tdp.ConPicSrc = "Game/" + NextID + "/Cover/Intro" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\Intro" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //存logo资源
            file = req.files.Files["LogoPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Cover";
            tdp.LogoPicSrc = "Game/" + NextID + "/Cover/LOGO" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\LOGO" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //存程序资源
            file = req.files.Files["ProgramSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Source";
            double size = System.Convert.ToDouble(file.Length) / 1024 / 1024;
            tdp.size = System.Convert.ToDouble(size.ToString("0000000.00"));
            tdp.ProgramSrc = "Game/" + NextID + "/Source/Source" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\Source" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            OkObjectResult re = new OkObjectResult(new { count = req.files.Count });
            return tdp;
        }
        public int addTages(string NextID, HttpRequest hreq)
        {
            string tags = hreq.Form["tag"];
            string[] taglist = tags.Split("/");
            int count = taglist.Length;
            int i = 0;
            while (i < count)
            {
                int cen = 0;
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO GAME_TAGS VALUES('" + NextID + "','" + taglist[i] +"')";
                cen = cmd.ExecuteNonQuery();
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 0;  //修改失败
                    resp.message = "添加新tag失败";
                    return 0;
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.message = e.Message.ToString();
                        resp.result = 0;
                        return 0;
                    }
                }
                i++;
            }
            return 1;
        }
        public int addLanguage(string NextID, HttpRequest hreq)
        {
            string languages = hreq.Form["language"];
            string[] languagelist = languages.Split("/");
            int count = languagelist.Length;
            int i = 0;
            while (i < count)
            {
                int cen = 0;
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO GAME_LANGUAGE VALUES('" + NextID + "','" + languagelist[i] + "')";
                cen = cmd.ExecuteNonQuery();
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 0;  //修改失败
                    resp.message = "添加语言失败";
                    return 0;
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.message = e.Message.ToString();
                        resp.result = 0;
                        return 0;
                    }
                }
                i++;
            }
            return 1;
        }
        public void addConfiguration(string NextID, HttpRequest hreq)
        {
            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO GAME_CONFIGURATION VALUES('" + NextID + "','" + hreq.Form["MIN_MEMORY"] + "','" + hreq.Form["MIN_CARD"] + "','" + hreq.Form["MIN_DISK"] + "','" + hreq.Form["MIN_PROCESSOR"] + "','" + hreq.Form["MIN_OS"] + "','" + hreq.Form["REC_MEMORY"] + "','" + hreq.Form["REC_CARD"] + "','" + hreq.Form["REC_DISK"] + "','" + hreq.Form["REC_PROCESSOR"] + "','" + hreq.Form["REC_OS"] +"')";
            cen = cmd.ExecuteNonQuery();
            if (cen == 0)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.result = 0;  //修改失败
                resp.message = "添加游戏配置失败";
            }
            else
            {
                try
                {
                    cmd.CommandText = "COMMIT";
                    cen = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.message = e.Message.ToString();
                    resp.result = 0;
                }
            }
        }
        public int existSource(HttpRequest hreq)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT NAME FROM GAME WHERE ID = '" + hreq.Form["SourceGameID"] + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;
                resp.message = "查不到该游戏的本体！";
                return 0;       //查询不到本体
            }
            else
            {
                return 1;
            }
        }
        public testDataFormResponse PublishGame(testDataFormRequest req, HttpRequest hreq)
        {
            openConn();

            if (HasNoPublished(hreq) == 0)
                return resp;

            string NextID = getNextID();
            if (NextID == "")
                return resp;

            string publichserID = getPublishedID(hreq);
            if (publichserID == "")
                return resp;

            if (System.Convert.ToInt32(hreq.Form["isDLC"]) == 1)//判断Source是否存在
            {
                if (existSource(hreq) == 0)
                    return resp;
            }

            testDataPath tdp = FileSave(NextID, req).Result;

            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO GAME VALUES('"+ NextID + "'," + hreq.Form["isDLC"] + ",to_date('" + hreq.Form["date"] + "','yyyy-mm-dd')," + hreq.Form["price"] +","+ tdp.size+ ",'" + hreq.Form["basicalinfo"] +"','"+tdp.ConPicSrc+"','"+ hreq.Form["normalinfo"] + "',"+ hreq.Form["AGE"] + ",'" + tdp.MainPicSrc + "',0,0,0,'" + tdp.ProgramSrc + "','" + tdp.AssPicSrc + "','" + hreq.Form["name"] +"','"+publichserID+"','"+tdp.BacPicSrc+"','"+tdp.LogoPicSrc+"')";
            try 
            {
                cen = cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.message = e.Message.ToString();
                resp.result = 0;
                return resp;
            }
            if (cen == 0)
            {
                cmd.CommandText = "ROLLBACK";
                cen = cmd.ExecuteNonQuery();
                resp.result = 0;  //修改失败
                resp.message = "添加新游戏失败";
                return resp;
            }
            else
            {
                try
                {
                    cmd.CommandText = "COMMIT";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 1;
                    resp.message = "添加成功";
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.message = e.Message.ToString();
                    resp.result = 0;
                    return resp;
                }
            }
            if (System.Convert.ToInt32(hreq.Form["isDLC"]) == 1)//添加dlc从属关系
            {
                cmd.CommandText = "INSERT INTO SUBJECT_DLC VALUES('" + hreq.Form["SourceGameID"] + "','" + NextID + "')";
                try 
                {
                    cen = cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    resp.message = e.Message.ToString();
                    resp.result = 0;
                    return resp;
                }
                if (cen == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.result = 0;  //修改失败
                    resp.message = "添加dlc从属关系失败";
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                        resp.result = 1;
                        resp.message = "添加成功";
                    }
                    catch (Exception e)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.message = e.Message.ToString();
                        resp.result = 0;
                    }
                }
            }//end of if(hreq.Form["isDLC"] == 1)

            if (addTages(NextID, hreq) == 0)
                return resp;
            if (addLanguage(NextID, hreq) == 0)
                return resp;
            addConfiguration(NextID, hreq);
            con.Close();
            return resp;
        }
    }
}
