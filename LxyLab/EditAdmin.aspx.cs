using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class EditAdmin : System.Web.UI.Page
    {
        protected LxyAdmin admin;
        protected void Page_Load(object sender, EventArgs e)
        {
            lxyAuthor.validateAuthor(this, "AdminID");
            DataModel dm = new DataModel();
            admin = dm.GetAdmin(Convert.ToInt32(Session["AdminID"]));
            if (admin == null)
            {
                Response.Redirect("您还未登录，请刷新后登录！");
            }
        }
    }
}