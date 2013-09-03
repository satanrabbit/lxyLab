using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class EditLab : System.Web.UI.Page
    {
        protected Lab lab =new Lab();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                DataModel dm = new DataModel();
                int id = Convert.ToInt32(Request.Params["id"]);
                lab = dm.GetLab(id);
            }
        }
    }
}