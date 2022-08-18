using System;
using Oracle.ManagedDataAccess.Client;
using UserCommentView.Models;

namespace UserCommentView.Helpers
{
    public class UserCommentViewHelper
    {
        public const int ID_LEN = 10;
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
        public string findCommentID(UserCommentViewRequest req, UserCommentViewResponse resp)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COMMENT_ID FROM COMMENTS WHERE USER_ID = '" + req.creator_id + "' AND GAME_ID = '" + req.game_id + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                resp.result = 0;
                resp.response = "找不到该帖子";
                return null;
            }
            else
            {
                try
                {
                    //查找成功
                    if (reader.Read())
                    {
                        return reader[0].ToString();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1; //数据库连接失败
                    return null;
                }
            }
            return null;//防止编译错误
        }
        public int hasUserComment(UserCommentViewRequest req, UserCommentViewResponse resp, string CommentID)
        {
            //返回值1表示赞，0表示未评论，-1表示踩，-2表示数据库连接失败
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select LIKE_CMT from COMMENT_LIKES WHERE USER_ID = '" + req.user_id + "' AND COMMENT_ID = '" + CommentID + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return 0;       //用户并未评论过该帖子
            else
            {
                try
                {
                    //查找成功
                    if (reader.Read())
                    {
                        return System.Convert.ToInt32(reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    resp.result = -1; //数据库连接失败
                    return -2;
                }
            }
            return -2;//防止编译错误
        }
        public UserCommentViewResponse postUserCommentView(UserCommentViewRequest req)
        {
            openConn();
            UserCommentViewResponse resp = new UserCommentViewResponse();

            string comment_id = findCommentID(req, resp);
            if (comment_id == null)//错误情况
            {
                con.Close();
                return resp;
            }                

            int like_cmt = hasUserComment(req, resp, comment_id);
            if (like_cmt == -2)
            {
                con.Close();
                return resp;//错误情况
            }                

            string newview = "";
            if (req.comment_view == 1)
                newview = "AGREE_NUM";
            else if (req.comment_view == -1)
                newview = "DISAGREE_NUM";
            else
            {
                resp.result = 0;
                resp.response = "输入了错误的comment_view！";
                con.Close();
                return resp;
            }

            int cen;
            OracleCommand cmd = con.CreateCommand();
            if (like_cmt == req.comment_view)//若赞/踩了还赞/踩，则是取消
            {
                cmd.CommandText = "DELETE FROM COMMENT_LIKES WHERE USER_ID = '" + req.user_id + "' AND comment_id = '" + comment_id + "'";
                try {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "取消操作失败1!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                cmd.CommandText = "UPDATE COMMENTS SET " + newview + "= " + newview + "-1 WHERE COMMENT_ID = '" + comment_id + "'";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "取消操作失败2!";
                        resp.result = 0;    
                        con.Close();
                        return resp;
                    }
                    else//取消操作成功
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                        resp.result = 3;
                        resp.response = "取消操作成功！";
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                con.Close();
                return resp;
            }
            else if (like_cmt == 0)//没有点过赞/踩，则新增数据
            {
                cmd.CommandText = "insert into COMMENT_LIKES values('" + req.user_id + "','" + comment_id + "'," + req.comment_view + ")";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "新增操作失败1!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                cmd.CommandText = "UPDATE COMMENTS SET " + newview + "= " + newview + "+1 WHERE COMMENT_ID = '" + comment_id + "'";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "新增操作失败2!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                    else//取消操作成功
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                        resp.result = 1;
                        resp.response = "新增操作成功！";
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                con.Close();
                return resp;
            }
            else//点过踩/赞，但此时点了另一个，则取消上一个，新增下一个
            {
                string lastview = "";
                if (like_cmt == -1)
                    lastview = "DISAGREE_NUM";
                else
                    lastview = "AGREE_NUM";
                cmd.CommandText = "DELETE FROM COMMENT_LIKES WHERE USER_ID = '" + req.user_id + "' AND comment_id = '" + comment_id + "'";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "修改操作失败1!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                cmd.CommandText = "insert into COMMENT_LIKES values('" + req.user_id + "','" + comment_id + "'," + req.comment_view + ")";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "修改操作失败2!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                cmd.CommandText = "UPDATE COMMENTS SET " + lastview + "= " + lastview + "-1 WHERE COMMENT_ID = '" + comment_id + "'";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "修改操作失败3!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                cmd.CommandText = "UPDATE COMMENTS SET " + newview + "= " + newview + "+1 WHERE COMMENT_ID = '" + comment_id + "'";
                try
                {
                    cen = cmd.ExecuteNonQuery();
                    if (cen == 0)
                    {
                        cmd.CommandText = "ROLLBACK";
                        cen = cmd.ExecuteNonQuery();
                        resp.response = "修改操作失败4!";
                        resp.result = 0;
                        con.Close();
                        return resp;
                    }
                    else//取消操作成功
                    {
                        cmd.CommandText = "COMMIT";
                        cen = cmd.ExecuteNonQuery();
                        resp.result = 2;
                        resp.response = "修改操作成功！";
                    }
                }
                catch (Exception e)
                {
                    cmd.CommandText = "ROLLBACK";
                    cen = cmd.ExecuteNonQuery();
                    resp.response = e.Message;
                    resp.result = 0;
                    con.Close();
                    return resp;
                }
                con.Close();
                return resp;
            }
        }
    }
}
