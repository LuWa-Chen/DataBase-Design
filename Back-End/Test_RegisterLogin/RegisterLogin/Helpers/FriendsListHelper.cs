using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using RegisterLogin.Models;

namespace RegisterLogin.Helpers
{
    public class FriendsListHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openConn(FriendsListResponse resp)
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

        public FriendsListResponse getFriendsList(FriendsListRequest req)
        {
            FriendsListResponse resp = new FriendsListResponse();
            resp.result = 0;
            openConn(resp);
            if (resp.result == -1)
                return resp;
            
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT FRIENDB_ID FROM FRIENDSHIP WHERE FRIENDA_ID='" + req.id + "'";
            OracleDataReader reader0 = cmd.ExecuteReader();
            cmd.CommandText = "SELECT FRIENDA_ID FROM FRIENDSHIP WHERE FRIENDB_ID='" + req.id + "'";
            OracleDataReader reader1 = cmd.ExecuteReader();

            try
            {
                //查找成功，赋值变量
                while (reader0.Read())
                {
                    get_user_friends friend = new get_user_friends();
                    friend.id = reader0[0].ToString();

                    cmd.CommandText = "SELECT NAME ,STATUS,PROFILE_PHOTO FROM GAME_USER WHERE ID='" + friend.id + "'";
                    OracleDataReader reader2 = cmd.ExecuteReader();
                    if (reader2.Read())
                    {
                        friend.name = reader2[0].ToString();
                        friend.status = int.Parse(reader2[1].ToString());
                        friend.profile_photo = reader2[2].ToString();
                    }
                    resp.friends_list.Add(friend);
                }
                while (reader1.Read())
                {
                    get_user_friends friend = new get_user_friends();
                    friend.id = reader1[0].ToString();

                    cmd.CommandText = "SELECT NAME ,STATUS,PROFILE_PHOTO FROM GAME_USER WHERE ID='" + friend.id + "'";
                    OracleDataReader reader3 = cmd.ExecuteReader();
                    if (reader3.Read())
                    {
                        friend.name = reader3[0].ToString();
                        friend.status = int.Parse(reader3[1].ToString());
                        friend.profile_photo = reader3[2].ToString();
                    }
                    resp.friends_list.Add(friend);
                }

                resp.result = 1;
                con.Close();
                return resp;
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
