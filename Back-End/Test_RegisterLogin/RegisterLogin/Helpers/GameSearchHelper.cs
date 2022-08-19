using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameSearchHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        const int GAME_NUM_PER_PAGE = 6;

        public void openConn(GameSearchResponse resp)
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

        public string[] GetCommands(GameSearchRequest req)
        {
            /* get rank_condition */
            string rank = "";
            switch (req.rank_condition)
            {
                case 0:
                    rank = "FUN_GET_GAME_PRICE(ID) ASC";
                    break;
                case 1:
                    rank = "TOT_DEAL_NUM DESC";
                    break;
                case 2:
                    rank = "PUBLISH_DATE DESC";
                    break;
                default:
                    break;
            }

            string cmd = $"SELECT ID, ROW_NUMBER() OVER (ORDER BY {rank}) R FROM GAME WHERE ";

            /* set cmd text according to name */
            switch (req.game_or_publisher)
            {
                case 0:
                    cmd += $"NAME LIKE '%{req.name}%' ";
                    break;
                case 1:
                    cmd += $"PUBLISHER_ID=FUN_GET_PUBLISHER_ID('{req.name}') ";
                    break;
                default:
                    break;
            }

            /* filter1: on sale or not */
            switch (req.is_on_sale) 
            {
                case 0:
                    cmd += "AND PRICE=FUN_GET_GAME_PRICE(id) ";
                    break;
                case 1:
                    cmd += "AND PRICE<>FUN_GET_GAME_PRICE(id) ";
                    break;
                default:
                    break;
            }

            /* filter2: is dlc or not */
            switch (req.is_DLC)
            {
                case 0:
                    cmd += "AND IS_DLC=0 ";
                    break;
                case 1:
                    cmd += "AND IS_DLC=1 ";
                    break;
                default:
                    break;
            }

            string command1 = $"SELECT ID FROM ({cmd}) WHERE R BETWEEN {GAME_NUM_PER_PAGE * (req.page - 1) + 1} AND {GAME_NUM_PER_PAGE * req.page} ";
            string command2 = $"SELECT CEIL(COUNT(ID)/{GAME_NUM_PER_PAGE}) FROM ({cmd})";
            string[] sqls = { command1, command2 };
            return sqls;
        }

        public GameSearchResponse SearchGames(GameSearchRequest req)
        {
            GameSearchResponse resp = new GameSearchResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            try
            {
                string[] commands = GetCommands(req);

                /* execute command1: get gane_ids corresponds */
                cmd.CommandText = commands[0];
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    resp.id_list.Add(reader[0].ToString());

                /* execute command2: get number of pages */
                cmd.CommandText = commands[1];
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    resp.all_page = int.Parse(reader[0].ToString());

                resp.result = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resp.result = 0;
            }

            con.Close();
            return resp;
        }
    }
}
