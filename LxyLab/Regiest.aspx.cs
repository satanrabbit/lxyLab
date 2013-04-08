using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRLib;
using LitJson;
namespace LxyLab
{
    public partial class Regiest : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int status = 2;
                string msg = "未知错误，请重试";

                //处理提交的数据

                string userName = Request.Form["userName"];
                if (userName == null || userName == "")
                {
                    status = 1;//有未填写项
                    msg = "请填写您的姓名！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                string userPwd = Request.Form["userPwd"];
                if (userPwd == null || userPwd == "")
                {

                    status = 1;//有未填写项
                    msg = "请填写密码！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                string userPwdCompare = Request.Form["userPwdCpmpare"];
                if (userPwdCompare == null || userPwdCompare == "")
                {
                    status = 1;
                    msg = "请确认密码！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                if (userPwd != userPwdCompare)
                {
                    //出错，提示密码不一致！
                    status = 2;
                    msg = "密码不一致，请确认密码！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                else
                {
                    userPwd = SRLib.Des.EncryptDES(userPwd, "SatanRabbit");
                }
                string userAccount = Request.Form["userAccount"];
                if (userAccount == null || userAccount == "")
                {
                    status = 1;
                    msg = "请填写邮箱！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                userAccount = userAccount.ToLower();
                string userNumber = Request.Form["userNumber"];
                if (userNumber == "" || userNumber == null)
                {
                    status = 1;
                    msg = "请填写教工卡号或学号！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                string userTel = Request.Form["userTel"];
                if (userTel == null || userTel == "")
                {
                    status = 1;
                    msg = "请填写您的联系电话！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                string userCollege = Request.Form["userCollege"];
                if (userCollege == null || userCollege == "")
                {
                    status = 1;
                    msg = "请填写您所在学院或部门！";
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status, msg));
                    Response.End();
                }
                int userIdentity = 3;//未知身份
                try
                {
                    userIdentity = Convert.ToInt32(Request.Form["userIdentity"]);
                }
                catch (Exception ex)
                {
                    status = 2;
                    msg = "你的身份填写有误！请核对！"; 
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(status,  msg));
                    Response.End();
                }

                //保存数据
                LxyOledb oledb = new LxyOledb();
                oledb.Conn.Open();
                //检查邮箱是否存在
                oledb.Cmd.CommandText = "select  * from User_tb where  UserNumber=@userNumber or UserAccount=@userAccount";
                oledb.Cmd.Parameters.Clear();
                oledb.Cmd.Parameters.AddWithValue("@userNumber", userNumber);
                oledb.Cmd.Parameters.AddWithValue("@userAccount", userAccount);
                oledb.Dr = oledb.Cmd.ExecuteReader();
                if (oledb.Dr.Read())
                {
                    if (oledb.Dr["UserAccount"].ToString() == userAccount)
                    {
                        oledb.Conn.Close();
                        
                        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                        Response.Write(ReturnMsg(2, "该邮箱账号已经存在，请更换邮箱或直接用此邮箱登录！"));
                        Response.End();
                    }
                    else
                    {
                        if (oledb.Dr["UserNumber"].ToString() == userNumber)
                        {
                            oledb.Conn.Close(); 
                            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                            Response.Write(ReturnMsg(2,"该教工卡号或学号已经被注册！请更换或直接用此账号登录！"));
                            Response.End();
                        }
                    }
                }
                else
                {
                    oledb.Dr.Dispose();
                    //保存账号
                    oledb.Cmd.CommandText = "INSERT INTO User_tb (UserName,UserPwd,UserAccount,UserNumber,UserTel,UserIdentity,UserCollege) VALUES (@userName,@userPwd,@userAccount,@userNumber,@userTel,@userIdentity,@userCollege)";
                    oledb.Cmd.Parameters.Clear();
                    oledb.Cmd.Parameters.AddWithValue("@userName", userName);
                    oledb.Cmd.Parameters.AddWithValue("@userPwd", userPwd);
                    oledb.Cmd.Parameters.AddWithValue("@userAccount", userAccount);
                    oledb.Cmd.Parameters.AddWithValue("@userNumber", userNumber);
                    oledb.Cmd.Parameters.AddWithValue("@userTel", userTel);
                    oledb.Cmd.Parameters.AddWithValue("@userIdentity", userIdentity);
                    oledb.Cmd.Parameters.AddWithValue("@userCollege", userCollege);
                    try
                    {
                        oledb.Cmd.ExecuteNonQuery();
                        Session["lxyLabUserName"] = userName;
                        Session["lxyLabUserNumber"] = userNumber;
                        oledb.Conn.Close();
                    }
                    catch (Exception ex)
                    {
                        oledb.Conn.Close();  
                        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                        Response.Write(ReturnMsg(2, ex.Message));
                        Response.End();
                    }


                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(ReturnMsg(0, "注册成功！"));
                    Response.End();

                }
                oledb.Conn.Close();
            }

        }
        private string ReturnMsg(int status, string msg)
        {
            JsonData jsonData = new JsonData();
            jsonData["status"] = status;
            jsonData["msg"] = msg;
            string jsonString = jsonData.ToJson();
            return jsonString;
        }
    }
}