using Oracle.ManagedDataAccess.Client;
using getGameName.Models;
using System;

namespace GameDetail
{
    public class GameDetailHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
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
            }
        }

        public getGameNameResponse findGameName(getGameNameRequest req)
        {
            openConn();
            getGameNameResponse resp = new getGameNameResponse();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT NAME FROM GAME WHERE ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                resp.result = 0;       //查找游戏id不存在
            else
            {
                try
                {
                    reader.Read();
                    string name = reader[0].ToString();
                    resp.game_name = name;
                    resp.result = 1;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1;
                }
            }

            return resp;
        }
    }
}
