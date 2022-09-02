using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameModifyHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        public OracleConnection con = new OracleConnection(connString);
        public void OpenConn(GameModifyResponse resp)
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

        public string GetGID(string g_name, string p_name)
        {
            /*
             * gid == "ERROR": Error
             * gid == "": game not found
             */
            string gid = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT ID FROM GAME WHERE NAME='{g_name}' AND PUBLISHER_ID=FUN_GET_PUBLISHER_ID('{p_name}')";

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    gid = reader[0].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                gid = "ERROR";
            }
            
            return gid;
        }

        public int JudgeString(string temp)
        {
            /* 
             * return 0: error
             * return 1: ok
             */
            string[] templist = temp.Split("/");
            for (int i = 0; i < temp.Length; i++)
                if (temp[i] == ',' || temp[i] == ';')
                    return 0;
            for (int i = 0; i < templist.Length; i++)
                if (templist[i].Length > 10)
                    return 0;
            return 1;
        }

        public async Task<SrcPath> FileSave(string gid, GameModifyRequest req)
        {
            int filecount = req.files.Count;
            string filename, fileext, filepath;
            IFormFile file;
            FileStream stream;
            SrcPath srcs = new SrcPath();

            //存主封面
            file = req.files.Files["MainPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + gid + "\\Cover";
            srcs.MainPicSrc = "Game/" + gid + "/Cover/Cover" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\Cover" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //存副封面
            file = req.files.Files["AssPicSrc"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + gid + "\\Cover";
            srcs.AssPicSrc = "Game/" + gid + "/Cover/anCover" + fileext;
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
            filepath = "C:\\ExGame-Asset\\Game\\" + gid + "\\Cover";
            srcs.BacPicSrc = "Game/" + gid + "/Cover/background" + fileext;
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
            filepath = "C:\\ExGame-Asset\\Game\\" + gid + "\\Cover";
            srcs.ConPicSrc = "Game/" + gid + "/Cover/Intro" + fileext;
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
            filepath = "C:\\ExGame-Asset\\Game\\" + gid + "\\Cover";
            srcs.LogoPicSrc = "Game/" + gid + "/Cover/LOGO" + fileext;
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
            filepath = "C:\\ExGame-Asset\\Game\\" + gid + "\\Source";
            double size = System.Convert.ToDouble(file.Length) / 1024 / 1024;
            srcs.size = System.Convert.ToDouble(size.ToString("0000000.00"));
            srcs.ProgramSrc = "Game/" + gid + "/Source/Source" + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\Source" + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            //OkObjectResult re = new OkObjectResult(new { count = req.files.Count });
            return srcs;
        }

        public int ModifyGameBasicInfo(GameModifyRequest req, HttpRequest hreq, string gid)
        {
            /*
             * return -3: subject not found
             * return -2: Name Exists
             * return -1: Connection Error
             * return 0: Modification Failed
             * return 1: Modificaiton Successed 
             */
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            //Check if name exists
            if (hreq.Form["ori_name"] != hreq.Form["name"])
            {
                cmd.CommandText = $"SELECT ID FROM GAME WHERE NAME='{hreq.Form["name"]}'";
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return -2;   //name exists
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -1;
                }
            }            

            //Check if subject exists
            if (hreq.Form["isDLC"] == 1)
            {
                cmd.CommandText = $"SELECT NAME FROM GAME WHERE ID='{hreq.Form["SourceGameID"]}'";
                try
                {
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                        return -3;   //subject not found
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -1;
                }
            }

            //save files
            SrcPath srcs = FileSave(gid, req).Result;

            //Modify basic info
            cmd.CommandText = $"UPDATE GAME SET NAME='{hreq.Form["name"]}', GENERAL_INTRO='{hreq.Form["basicalinfo"]}', SPECIFIC_INTRO='{hreq.Form["normalinfo"]}', PUBLISH_DATE=to_date('{hreq.Form["date"]}', 'yyyy-mm-dd'), PRICE={double.Parse(hreq.Form["price"])}, AGE_LEVEL='{int.Parse(hreq.Form["AGE"])}', IS_DLC={int.Parse(hreq.Form["isDLC"])}, INTRO_PHOTO='{srcs.ConPicSrc}', COVER='{srcs.MainPicSrc}', SRC='{srcs.ProgramSrc}', ANOCOVER='{srcs.AssPicSrc}', BACK='{srcs.BacPicSrc}', LOGO='{srcs.LogoPicSrc}' WHERE ID={gid}";
            try
            {
                int rslt = cmd.ExecuteNonQuery();
                if (rslt == 0)
                    return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
            
            return 1;
        }

        public int ModifySubject(HttpRequest hreq, string gid)
        {
            /*
             * return -2: Subject not exists
             * return -1: Connection Failed
             * return 0: Modify Failed
             * return 1: Modify Successfully
             */
            OracleCommand cmd = con.CreateCommand();
            bool has_subject = false;

            //check if gid has a subject before
            cmd.CommandText = $"SELECT SUBJECT_ID FROM SUBJECT_DLC WHERE DLC_ID='{gid}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                has_subject = reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            //check if subject exists
            cmd.CommandText = $"SELECT NAME FROM GAME WHERE ID='{hreq.Form["SourceGameID"]}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    return -2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            //update or insert
            if (has_subject)
                cmd.CommandText = $"UPDATE SUBJECT_DLC SET SUBJECT_ID={hreq.Form["SourceGameID"]} WHERE DLC_ID='{gid}'";
            else
                cmd.CommandText = $"INSERT INTO SUBJECT_DLC VALUES('{hreq.Form["SourceGameID"]}', '{gid}')";
            try
            {
                int rslt = cmd.ExecuteNonQuery();
                return rslt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int RemoveSubject(string gid)
        {
            /*
             * return -1: Connection Failed
             * return 1: Modify Successfully
             */
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"DELETE FROM SUBJECT_DLC WHERE DLC_ID='{gid}'";
            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int ModifyConfig(string gid, HttpRequest hreq)
        {
            /*
             * return -1: Connection Failed
             * return 0: Modify Failed
             * return 1: Modify Successfully
             */
            OracleCommand cmd = con.CreateCommand();
            bool has_config;

            //check if config exists
            cmd.CommandText = $"SELECT GAME_ID FROM GAME_CONFIGURATION WHERE GAME_ID='{gid}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                has_config = reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            //insert or update
            if (has_config)
                cmd.CommandText = $"UPDATE GAME_CONFIGURATION SET REC_MEMORY='{hreq.Form["MIN_MEMORY"]}', MIN_CARD='{hreq.Form["MIN_CARD"]}', MIN_DISK='{hreq.Form["MIN_DISK"]}', MIN_PROCESSOR='{hreq.Form["MIN_PROCESSOR"]}', MIN_OS='{hreq.Form["MIN_OS"]}', MIN_MEMORY='{hreq.Form["REC_MEMORY"]}', REC_CARD='{hreq.Form["REC_CARD"]}', REC_DISK='{hreq.Form["REC_DISK"]}', REC_PROCESSOR='{hreq.Form["REC_PROCESSOR"]}', REC_OS='{hreq.Form["REC_OS"]}' WHERE GAME_ID='{gid}'";
            else
                cmd.CommandText = $"INSERT INTO GAME_CONFIGURATION VALUES('{gid}','{hreq.Form["MIN_MEMORY"]}','{hreq.Form["MIN_CARD"]}','{hreq.Form["MIN_DISK"]}','{hreq.Form["MIN_PROCESSOR"]}','{hreq.Form["MIN_OS"]}','{hreq.Form["REC_MEMORY"]}','{hreq.Form["REC_CARD"]}','{hreq.Form["REC_DISK"]}','{hreq.Form["REC_PROCESSOR"]}','{hreq.Form["REC_OS"]}')";
            try
            {
                int rslt = cmd.ExecuteNonQuery();
                return rslt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int ModifyLanguage(HttpRequest hreq, string gid)
        {
            /*
             * return -2: language illegal
             * return -1: Connection Failed
             * retrun 0: Modify Failed
             * return 1: Modify Successfully
             */
            // check if languages legal
            string language = hreq.Form["language"];
            if (JudgeString(language) == 0)
                return -2;

            string[] languagelist = language.Split("/");
            int count = languagelist.Length;
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            //delete existing languages that not in languagelist
            cmd.CommandText = $"DELETE FROM GAME_LANGUAGE WHERE GAME_ID='{gid}' ";
            for (int i = 0; i < count; i++)
                cmd.CommandText += $"AND LANGUAGE <> '{languagelist[i]}' ";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            //add new languages
            for (int i = 0; i < count; i++)
            {
                cmd.CommandText = $"SELECT LANGUAGE FROM GAME_LANGUAGE WHERE GAME_ID='{gid}' AND LANGUAGE='{languagelist[i]}'";
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        continue;
                    cmd.CommandText = $"INSERT INTO GAME_LANGUAGE VALUES('{gid}', '{languagelist[i]}')";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -1;
                }
            }

            return 1;
        }

        public int ModifyTags(string gid, HttpRequest hreq)
        {
            /*
             * return -2: tag illegal
             * return -1: Connection Failed
             * retrun 0: Modify Failed
             * return 1: Modify Successfully
             */
            // check if tags legal
            string tags = hreq.Form["tag"];
            if (JudgeString(tags) == 0)
                return -2;

            string[] taglist = tags.Split("/");
            int count = taglist.Length;
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            //delete existing tags that not in taglist
            cmd.CommandText = $"DELETE FROM GAME_TAGS WHERE ID='{gid}' ";
            for (int i = 0; i < count; i++)
                cmd.CommandText += $"AND TAG <> '{taglist[i]}'";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            //add new tags
            for (int i = 0; i < count; i++)
            {
                cmd.CommandText = $"SELECT TAG FROM GAME_TAGS WHERE ID='{gid}' AND TAG='{taglist[i]}'";
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        continue;
                    cmd.CommandText = $"INSERT INTO GAME_TAGS VALUES('{gid}', '{taglist[i]}')";
                    try
                    {
                        int rslt = cmd.ExecuteNonQuery();
                        if (rslt == 0)
                            return 0;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -1;
                }
            }

            return 1;
        }

        public void Rollback()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "ROLLBACK";
            try
            {
                int rslt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public GameModifyResponse ModifyGame(GameModifyRequest req, HttpRequest hreq)
        {
            GameModifyResponse resp = new GameModifyResponse();
            resp.result = 0;
            OpenConn(resp);
            if (resp.result == -1)
            {
                con.Close();
                return resp;
            }

            // get gameid
            string gid = GetGID(hreq.Form["ori_name"], hreq.Form["publisher"]);
            if (gid == "")
            {
                resp.result = 0;
                resp.message = "游戏不存在！";
                con.Close();
                return resp;
            }    
            else if (gid == "ERROR")
            {
                resp.result = -1;
                resp.message = "连接失败！";
                con.Close();
                return resp;
            }

            //modify basic info
            int rslt = ModifyGameBasicInfo(req, hreq, gid);
            if (rslt != 1)
            {
                switch (rslt)
                {
                    case -3:
                        resp.result = 0;
                        resp.message = "游戏主体不存在！";
                        break;
                    case -2:
                        resp.result = 0;
                        resp.message = "游戏名已存在！";
                        break;
                    case -1:
                        resp.result = -1;
                        resp.message = "连接失败！";
                        break;
                    case 0:
                        resp.result = 0;
                        resp.message = "修改失败1！";
                        break;                    
                }
                
                Rollback();
                con.Close();
                return resp;                
            }

            //modify subject
            if (int.Parse(hreq.Form["isDLC"]) == 1)
            {
                rslt = ModifySubject(hreq, gid);
                if (rslt == -2)
                {
                    resp.result = 0;
                    resp.message = "主体不存在！";
                    Rollback();
                    con.Close();
                    return resp;
                }
                else if (rslt == -1)
                {
                    resp.result = -1;
                    resp.message = "连接失败！";
                    Rollback();
                    con.Close();
                    return resp;
                }
                else if (rslt == 0)
                {
                    resp.result = 0;
                    resp.message = "修改失败2！";
                    Rollback();
                    con.Close();
                    return resp;
                }
            }    
            else
            {
                rslt = RemoveSubject(gid);
                if (rslt == -1)
                {
                    resp.result = -1;
                    resp.message = "连接失败！";
                    Rollback();
                    con.Close();
                    return resp;
                }
            }

            //modify config
            rslt = ModifyConfig(gid, hreq);
            if (rslt == -1)
            {
                resp.result = -1;
                resp.message = "连接失败！";
                Rollback();
                con.Close();
                return resp;
            }
            else if (rslt == 0)
            {
                resp.result = 0;
                resp.message = "修改失败3！";
                Rollback();
                con.Close();
                return resp;
            }

            //modify languages
            rslt = ModifyLanguage(hreq, gid);
            if (rslt != 1)
            {
                switch (rslt)
                {
                    case -2:
                        resp.result = 0;
                        resp.message = "语言不合法！";
                        break;
                    case -1:
                        resp.result = -1;
                        resp.message = "连接失败！";
                        break;
                    case 0:
                        resp.result = 0;
                        resp.message = "修改失败4！";
                        break;
                }

                Rollback();
                con.Close();
                return resp;
            }

            //modify tags
            rslt = ModifyTags(gid, hreq);
            if (rslt != 1)
            {
                switch (rslt)
                {
                    case -2:
                        resp.result = 0;
                        resp.message = "标签不合法！";
                        break;
                    case -1:
                        resp.result = -1;
                        resp.message = "连接失败！";
                        break;
                    case 0:
                        resp.result = 0;
                        resp.message = "修改失败5！";
                        break;
                }

                Rollback();
                con.Close();
                return resp;
            }

            //commit
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "COMMIT";
            try
            {
                rslt = cmd.ExecuteNonQuery();
                resp.result = 1;
                resp.message = "修改成功!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.message = "连接失败！";
                resp.result = -1;
                Rollback();
            }

            con.Close();
            return resp;
        }
    }
}
