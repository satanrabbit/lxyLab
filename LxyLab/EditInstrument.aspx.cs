using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class EditInstrument : System.Web.UI.Page
    {
        protected Instrument inst = new Instrument();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                int id = Convert.ToInt32(Request.Params["id"]);
                DataModel dm = new DataModel();
                inst = dm.GetInstrument(id);
            }
        }
    }
}