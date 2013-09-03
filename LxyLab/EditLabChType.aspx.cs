using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class EditLabChType : System.Web.UI.Page
    {
        protected LabChType lc = new LabChType();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataModel dm = new DataModel();
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                int id = Convert.ToInt32(Request.Params["id"].Trim());
                lc = dm.GetLabChType(id);
            }
        }
    }
}