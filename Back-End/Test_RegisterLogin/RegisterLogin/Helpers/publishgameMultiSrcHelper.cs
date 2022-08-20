using System.IO;
using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using testDataForm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace publishgameMultiSrc.Helpers
{
    public class publishgameMultiSrcHelper
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
        public string getGameID(HttpRequest hreq)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID FROM GAME WHERE NAME = '" + hreq.Form["name"] + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;
                resp.message = "未查询到该游戏ID!";
                return "";       
            }
            else
            {
                reader.Read();
                string gameID = reader[0].ToString();
                return gameID;
            }
        }
        public async Task<MultiSrc> FileSave(string NextID, HttpRequest hreq, testDataFormRequest req)
        {
            string filename, fileext, filepath, filetype;
            int filecount = System.Convert.ToInt32(hreq.Form["num"]);
            IFormFile file;
            FileStream stream;
            MultiSrc tdp = new MultiSrc();
            if (System.Convert.ToInt32(hreq.Form["category"]) == 0)
                filetype = "Photo";
            else
                filetype = "Video";

            for(int i = 1; i <= filecount; i++)
            {
                file = req.files.Files["Src" + i.ToString()];
                filename = file.FileName;
                fileext = Path.GetExtension(filename);
                filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\Exhibition\\" + filetype;
                tdp.Src.Add("Game/" + NextID + "/Exhibition/" + filetype + "/" + i.ToString() + fileext);
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                stream = new FileStream(filepath + "\\" + i.ToString() + fileext, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
            }

            return tdp;
        }
        public testDataFormResponse PublishGameSrc(testDataFormRequest req, HttpRequest hreq)
        {
            openConn();

            string NextID = getGameID(hreq);
            if (NextID == "")
                return resp;

            MultiSrc tdp = FileSave(NextID, hreq, req).Result;

            string filetype;
            if (System.Convert.ToInt32(hreq.Form["category"]) == 0)
                filetype = "GAME_PHOTOS";
            else
                filetype = "GAME_VIDEOS";

            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            
            for(int i=0;i< System.Convert.ToInt32(hreq.Form["num"]);i++)
            {
                cmd.CommandText = "INSERT INTO "+ filetype + " VALUES('"+ NextID +"',"+ (i+1).ToString() + ",'" + tdp.Src[i] +"')";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
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
                    resp.message = "添加轮播资源失败";
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
            }
            con.Close();
            return resp;
        }
    }
}
