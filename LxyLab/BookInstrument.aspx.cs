using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class BookInstrument : System.Web.UI.Page
    {
        protected InstOrder ino = new InstOrder();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(Request.Params["id"]);
             
            ino.InstOrderLab = id;
            
            
        }
    }
}