﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LitJson;

namespace LxyLab
{
    public partial class Admin : System.Web.UI.Page
    {
        protected LxyAdmin admin;
        protected void Page_Load(object sender, EventArgs e)
        {
            lxyAuthor.validateAuthor(this,"AdminID");
            DataModel dm = new DataModel();
            admin=dm.GetAdmin(Convert.ToInt32(Session["AdminID"]));
            if (admin == null)
            {
                Response.Redirect("adminLogin.aspx");
            }
        }
    }
}