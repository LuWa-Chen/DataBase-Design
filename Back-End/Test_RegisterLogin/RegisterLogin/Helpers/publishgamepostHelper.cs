using System.IO;
using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using publishgamepost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace publishgamepost.Helpers
{
    public class publishgamepostHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        public OracleConnection con = new OracleConnection(connString);
        publishgamepostResponse resp = new publishgamepostResponse(); 
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
        public int getNextIndex(string gameID)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT MAX(POST_INDEX) FROM GAME_POST WHERE GAME_ID = '" + gameID + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader[0].ToString() == "")
                return 0;
            int NextIndex = System.Convert.ToInt32(reader[0].ToString()) + 1;
            return NextIndex;
        }
        public async Task<string> FileSave(string NextID,int NextIndex , publishgamepostRequest req)
        {
            string filename, fileext, filepath;
            IFormFile file;
            FileStream stream;
            string tdp = "";
            //存主封面
            file = req.files.Files["PostCover"];
            filename = file.FileName;
            fileext = Path.GetExtension(filename);
            filepath = "C:\\ExGame-Asset\\Game\\" + NextID + "\\News";
            tdp = "Game/" + NextID + "/News/"+NextIndex + fileext;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            stream = new FileStream(filepath + "\\"+NextIndex + fileext, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            return tdp;
        }
            public publishgamepostResponse PublishGamePost(publishgamepostRequest req, HttpRequest hreq)
        {
            openConn();

            int NextIndex = getNextIndex(hreq.Form["GameID"]);
            string tdp = FileSave(hreq.Form["GameID"], NextIndex, req).Result;

            int cen = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO GAME_POST VALUES(" + NextIndex + ",'" + hreq.Form["GameID"] + "','" + tdp + "','" + hreq.Form["PostTitle"] + "',to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'),'" + hreq.Form["Content"] + "')";
            Console.WriteLine(cmd.CommandText);
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
                resp.message = "添加新推送失败";
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
            con.Close();
            return resp;
        }
    }
}
