﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class GameProfileHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(ref GameProfileResponse resp)
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

        public GameProfileResponse getGameProfile(GameProfileRequest req)
        {
            GameProfileResponse resp = new GameProfileResponse();
            resp.result = 0;
            openConn(ref resp);
            if (resp.result == -1)
                return resp;
            
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader reader;

            try
            {
                /* get profile of subject */
                cmd.CommandText = $"SELECT COVER, GENERAL_INTRO, SPECIFIC_INTRO FROM GAME WHERE ID='{req.game_id}'";
                reader = cmd.ExecuteReader();
/*                if (!reader.HasRows)
                {
                    resp.result = 0;
                    return resp;
                }*/
                Dictionary<string, string> profile = new Dictionary<string, string>();
                if (reader.Read())
                {
                    profile["poster"] = reader[0].ToString();
                    profile["title"] = reader[1].ToString();
                    profile["content"] = reader[2].ToString();
                }
                resp.about_game.Add(profile);

                /* get dlcs */
                cmd.CommandText = $"SELECT DLC_ID FROM SUBJECT_DLC WHERE SUBJECT_ID='{req.game_id}'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    resp.dlc_list.Add(reader[0].ToString());

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
