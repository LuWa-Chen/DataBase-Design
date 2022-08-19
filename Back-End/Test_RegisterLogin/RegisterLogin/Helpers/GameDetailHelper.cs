using Oracle.ManagedDataAccess.Client;
using System.Collections;
using getGameInfo.Models;
using getGameName.Models;
using System;

namespace GameDetail.Helpers
{
    public class GameDetailHelper
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.222.196)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##ysjyyds;Password=DBprinciple2022;";
        OracleConnection con = new OracleConnection(connString);
        public void openGameNameConn(ref getGameNameResponse resp)
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
                resp.result = -2;
                con.Close();
            }
        }

        public void openGameInfoConn(ref getGameInfoResponse resp)
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
                resp.result = -2;
                con.Close();
            }
        }

        public getGameNameResponse findGameName(getGameNameRequest req)
        {
            getGameNameResponse resp = new getGameNameResponse();
            resp.result = 0;
            openGameNameConn(ref resp);
            if (resp.result == -2)
                return resp;
           
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
            con.Close();
            return resp;
        }
        public double findGameDiscount(string id, getGameInfoResponse resp)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT DISCOUNT_RATE FROM DISCOUNT WHERE GAME_ID = '"+ id +"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return 1;//无打折信息
            }
            else
            {
                try
                {
                    //查找成功，赋值变量
                    if (reader.Read())
                    {
                        return System.Convert.ToDouble(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    resp.result = -1;
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
            return 0;
        }

        public getGameInfoResponse findGameInfo(getGameInfoRequest req)
        {
            getGameInfoResponse resp = new getGameInfoResponse();
            resp.result = 0;
            openGameInfoConn(ref resp);
            if (resp.result == -2)
                return resp;

            OracleCommand cmd = con.CreateCommand();
            int count = req.game_id.Count;
            for (int i = 0; i < count; i++)
            {
                double discount = findGameDiscount(req.game_id[i], resp);
                if (discount == 0)
                {
                    Console.WriteLine("无法查找" + req.game_id[i] + "的相关折扣信息");
                    return resp;
                }
                cmd.CommandText = "SELECT NAME,PUBLISH_DATE,PRICE,GENERAL_INTRO FROM GAME WHERE ID = '" + req.game_id[i] + "'";
                OracleDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    resp.result = 0;       //查找游戏id不存在
                else
                {
                    try
                    {
                        //查找成功，赋值变量
                        if (reader.Read())
                        {
                            resp.game_name.Add(reader[0].ToString());
                            resp.publish_date.Add(reader[1].ToString());
                            resp.price.Add(System.Convert.ToDouble(reader[2].ToString()));
                            resp.general_intro.Add(reader[3].ToString());
                            resp.discount.Add(discount);
                            resp.result = 1;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        resp.result = -1;
                    }
                }
            }
            con.Close();
            return resp;
        }
    }
}
