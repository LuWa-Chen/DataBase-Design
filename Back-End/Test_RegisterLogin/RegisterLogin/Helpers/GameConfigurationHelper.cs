using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameConfigurationHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(GameConfigurationResponse resp)
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
        
        public GameConfigurationResponse getGameConfiguration(GameConfigurationRequest req)
        {
            GameConfigurationResponse resp = new GameConfigurationResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;
            
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            try
            {
                /* get game tags */
                cmd.CommandText = $"SELECT TAG FROM GAME_TAGS WHERE ID='{req.game_id}'";
                reader = cmd.ExecuteReader();
                if (!reader.HasRows)    //ID not exist
                {
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                while (reader.Read())   //ID exists, get tags
                    resp.game_features.Add(reader[0].ToString());

                /* get game configurations */
                cmd.CommandText = $"SELECT MIN_MEMORY, MIN_CARD, MIN_DISK, MIN_PROCESSOR, MIN_OS, REC_MEMORY, REC_CARD, REC_DISK, REC_PROCESSOR, REC_OS " +
                    $"FROM GAME_CONFIGURATION WHERE GAME_ID='{req.game_id}'";
                reader = cmd.ExecuteReader();
                if (!reader.HasRows)    //ID not exist
                {
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                else if (reader.Read())  //ID exists, get tags
                {
                    for (int i = 0; i < 5; i++)     //get minimum config
                        resp.minimum_configuration.Add(reader[i].ToString());
                    for (int i = 5; i < 10; i++)
                        resp.recommended_configuration.Add(reader[i].ToString());
                }

                /* get game languages */
                cmd.CommandText = $"SELECT LANGUAGE FROM GAME_LANGUAGE WHERE GAME_ID='{req.game_id}'";
                reader = cmd.ExecuteReader();
                if (!reader.HasRows)    //ID not exist
                {
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                while (reader.Read())
                {
                    resp.ui_language.Add(reader[0].ToString());
                    resp.sound_language.Add(reader[0].ToString());
                    resp.text_language.Add(reader[0].ToString());
                }
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
