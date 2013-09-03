using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRLib;

namespace LxyLab
{
    public partial class Login : System.Web.UI.Page
    {
        protected string controlName = "";
        protected string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Panel1.Visible = false;
            if(IsPostBack){
                string account = Request.Form["userAccount"];
                if (account == "" || account == null)
                {
                    this.Panel1.Visible = true;
                    controlName = "userAccount";
                    msg = "账号不能为空！";
                }
                else
                {
                    string pwd = Request.Form["userPwd"];
                    if (pwd == null || pwd == "")
                    {
                        this.Panel1.Visible = true;
                        controlName = "userPwd";
                        msg = "密码不能为空！";
                    }
                    else
                    {
                        LxyOledb oledb = new LxyOledb();
                        oledb.Conn.Open();
                        oledb.Cmd.CommandText = "select * from User_tb where UserAccount=@account or UserNumber=@acountNum";
                        oledb.Cmd.Parameters.AddWithValue("@account" ,account);
                        oledb.Cmd.Parameters.AddWithValue("@accountNum", account);
                        oledb.Dr = oledb.Cmd.ExecuteReader();
                        if (oledb.Dr.Read())
                        {
                            //账号存在，验证密码
                            if (oledb.Dr["UserPwd"].ToString() == SRLib.Des.EncryptDES(pwd, "SatanRabbit"))
                            {
                                //密码正确，设置session
                                Session["lxyLabUserName"] = oledb.Dr["UserName"].ToString();
                                Session["lxyLabUserNumber"] = oledb.Dr["UserNumber"].ToString();
                                Session["lxyLabUserID"] = oledb.Dr["UserID"].ToString();
                                oledb.Conn.Close();
                                Response.Redirect("Default.aspx");
                            }
                            else
                            {
                                this.Panel1.Visible = true;
                                controlName = "userPwd";
                                msg = "密码错误！";
                            }
                        }
                        else
                        {
                            this.Panel1.Visible = true;
                            controlName = "userAccount";
                            msg = "账号不存在！！";
                        }
                        oledb.Conn.Close();


                    }
                } 
                
            }
        }
    }
}