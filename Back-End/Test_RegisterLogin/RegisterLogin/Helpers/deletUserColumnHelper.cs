using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class deletUserColumnHelper
    {
        public const int ID_LEN = 10;
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);

        public void openConn(ref deletUserColumnResponse resp)
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

        public deletUserColumnResponse deletUserColumn(deletUserColumnRequest req)
        {
            deletUserColumnResponse resp = new deletUserColumnResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            if (req.type == 1)
            {
                cmd.CommandText = "SELECT USER_ID,COLUMN_ID FROM COLUMNS WHERE USER_ID = '" + req.user_id + "' AND COLUMN_ID = '" + req.column_id + "'";
                OracleDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    resp.result = 0;       //查找记录不存在
                }
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        cmd.CommandText = "DELETE FROM COLUMN_PHOTOS WHERE COLUMN_ID = '" + req.column_id + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "DELETE FROM COLUMNS WHERE USER_ID = '" + req.user_id + "' AND COLUMN_ID = '" + req.column_id + "'";
                        cmd.ExecuteNonQuery();
                        resp.result = 1;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        resp.result = 0;
                    }
                }
            }
            else if(req.type == 2)
            {
                cmd.CommandText = "SELECT USER_ID,COLUMN_ID FROM REPLY_COLUMN WHERE USER_ID = '" + req.user_id + "' AND COLUMN_ID = '" + req.column_id + "'";
                OracleDataReader reader1 = cmd.ExecuteReader();

                if (!reader1.HasRows)
                {
                    resp.result = 0;       //查找记录不存在
                }
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        cmd.CommandText = "DELETE FROM REPLY_COLUMN WHERE USER_ID = '" + req.user_id + "' AND COLUMN_ID = '" + req.column_id + "'";
                        cmd.ExecuteNonQuery();
                        resp.result = 2;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        resp.result = 0;
                    }
                }
            }
            con.Close();
            return resp;
        }
    }
}
