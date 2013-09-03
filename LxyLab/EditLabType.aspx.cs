using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class EditLabType : System.Web.UI.Page
    {
        protected LabType lt = new LabType();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataModel dm = new DataModel();
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                int id = Convert.ToInt32(Request.Params["id"].Trim());
                lt = dm.GetLabType(id);
            }
        }
    }
}