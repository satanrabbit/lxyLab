using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class _default : System.Web.UI.Page
    {
        protected int defaultLab = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( Session["lxyLabUserName"] == null||Session["lxyLabUserName"].ToString() == "" )
            {
                Response.Redirect("Login.aspx"); 
            }
            LxyOledb oledb = new LxyOledb();
            oledb.Conn.Open();
            oledb.Cmd.CommandText = "select LabID from Lab_tb where LabDefault = true";
            oledb.Dr = oledb.Cmd.ExecuteReader();
            if(oledb.Dr.Read()){
                defaultLab=Convert.ToInt32(oledb.Dr["LabID"]);
            }
            oledb.Conn.Close();
        }
    }
}