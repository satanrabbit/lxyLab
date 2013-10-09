using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class UserEditInfo : System.Web.UI.Page
    {
        protected LxyUser lxyUser = new LxyUser();
        protected string userIdt = "学生";
        protected string userNumLabel = "学号";
        protected void Page_Load(object sender, EventArgs e)
        {
            lxyAuthor.validateAuthorHtml(this);
            int userID =Convert.ToInt32( Session["lxyLabUserID"]);
            DataModel dm = new DataModel();

            lxyUser= dm.GetUser(userID);

            lxyUser = dm.GetUser(lxyUser.UserNumber);
            if (lxyUser.UserIdentity == 1)
            {
                //teacher
                userIdt = "教师";
                userNumLabel = "教工号";
            }
        }
    }
}