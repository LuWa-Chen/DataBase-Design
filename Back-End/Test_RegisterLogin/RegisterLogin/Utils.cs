using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace RegisterLogin
{
    public class Utils
    {
        public string convert2date(string date)
        {
            /*
             * convert yyyy-mm-dd to format oracle supports
             * @param date: yyyy-mm-dd
             * return: Oracle Date Format 
            */
            string[] dateArr = date.Split('-');
            string oracle_date = dateArr[2] + "-" + int.Parse(dateArr[1]).ToString() + "月-" + dateArr[0].Substring(2);

            return oracle_date;
        }

        public string calcID(string sum)
        {
            /* calculate id with current sum of ids */
            string id = (int.Parse(sum) + 1).ToString().PadLeft(10, '0');
            return id;
        }

        public int isUserInDB(string uid, OracleConnection conn)
        {
            /*
             * find whether user uid is in database
             * @retrun: 1  -- uid in db
             *          0  -- uid not in db
             *          -1 -- connection failed
             */
            int ret;
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT NAME FROM GAME_USER WHERE ID='{uid}'";
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

        public int isGameInDB(string gid, OracleConnection conn)
        {
            /*
             * find whether game gid is in database
             * @retrun: 1  -- gid in db
             *          0  -- gid not in db
             *          -1 -- connection failed
             */
            int ret;
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT NAME FROM GAME WHERE ID='{gid}'";
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
        public int isGameInUserLib(string gid, string uid, OracleConnection con)
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

        public int AddGame(string gid, string uid, int via_cdk, OracleConnection con)
        {
            int ret = 0;
            OracleCommand cmd = con.CreateCommand();

            /* add game to ownership */
            cmd.CommandText = $"INSERT INTO OWNERSHIP VALUES('{uid}', '{gid}', {via_cdk})";
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

    }
}
