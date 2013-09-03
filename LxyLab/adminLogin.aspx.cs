using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class adminLogin : System.Web.UI.Page
    {
        protected string controlName = "";
        protected string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Panel1.Visible = false;
            if (IsPostBack)
            {
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
                        oledb.Cmd.CommandText = "select * from Admin_tb where AdminAccount=@account ";
                        oledb.Cmd.Parameters.AddWithValue("@account", account);
                        oledb.Cmd.Parameters.AddWithValue("@accountNum", account);
                        oledb.Dr = oledb.Cmd.ExecuteReader();
                        if (oledb.Dr.Read())
                        {
                            //账号存在，验证密码
                            if (oledb.Dr["AdminPWD"].ToString() ==  pwd)
                            {
                                //密码正确，设置session
                                Session["AdminID"] = oledb.Dr["AdminID"].ToString(); 
                                oledb.Conn.Close();
                                Response.Redirect("admin.aspx");
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