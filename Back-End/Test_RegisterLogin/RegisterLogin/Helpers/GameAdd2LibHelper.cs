using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameAdd2LibHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(GameAdd2LibResponse resp)
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
        
        public int isGameInUserLib(string gid, string uid)
        {
            int ret = 0;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT VIA_CDK FROM OWNERSHIP WHERE USER_ID='{uid}' AND GAME_ID='{gid}'";
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                ret = reader.HasRows ? 1 : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ret = -1;
            }

            return ret;
        }

        public int AddGame(string gid, string uid)
        {
            int ret = 0;
            OracleCommand cmd = con.CreateCommand();

            /* add game to ownership */
            cmd.CommandText = $"INSERT INTO OWNERSHIP VALUES('{uid}', '{gid}', 0)";
            try
            {
                ret = cmd.ExecuteNonQuery();
                if (ret == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cmd.ExecuteNonQuery();
                    return ret;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();
                return ret;
            }

            /* update number of games user owns */
            cmd.CommandText = $"UPDATE GAME_USER SET GAME_NUM=GAME_NUM+1 WHERE ID='{uid}'";
            try
            {
                ret = cmd.ExecuteNonQuery();
                if (ret == 0)
                {
                    cmd.CommandText = "ROLLBACK";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "COMMIT";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ret = 0;
                Console.WriteLine(e);
                cmd.CommandText = "ROLLBACK";
                cmd.ExecuteNonQuery();
            }

            return ret;
        }

        public GameAdd2LibResponse Add2Lib(GameAdd2LibRequest req)
        {
            GameAdd2LibResponse resp = new GameAdd2LibResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;

            /* check if user and game exists and user hasn't owned the game yet */
            Utils utils = new Utils();

            //check if user exists
            int user_exists = utils.isUserInDB(req.user_id, con);
            if (user_exists != 1)
            {
                resp.result = user_exists;
                con.Close();
                return resp;
            }

            //check if game exists
            int game_exists = utils.isGameInDB(req.game_id, con);
            if (game_exists != 1)
            {
                resp.result = game_exists;
                con.Close();
                return resp;
            }

            //check if the user has already owns the game
            int owns = isGameInUserLib(req.game_id, req.user_id);
            if (owns != 0)
            {
                resp.result = owns == 1 ? 0 : -1;
                con.Close();
                return resp;
            }

            /* add game to user library */
            int ret = AddGame(req.game_id, req.user_id);
            if (ret != 1)
                resp.result = -1;
            else
                resp.result = 1;

            con.Close();
            return resp;
        }
    }
}
