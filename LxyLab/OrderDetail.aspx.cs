using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected LabOrder lo;
        protected bool hasOrder=true;
        protected LxyUser luser;
        protected List<InstOrder> inos = new List<InstOrder>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] == null || Request.Params["id"].Trim() == "")
            {
                hasOrder = false;
            }
            else
            {
                DataModel dm=new DataModel();
                int id = Convert.ToInt32(Request.Params["id"].Trim());
                lo = dm.GetLabOrder(id);
                if (lo != null)
                {
                    luser = dm.GetUser(lo.OrderUser);
                    inos = dm.GetLabOrderInst(lo.OrderID);
                }
                else
                {
                    hasOrder = false;
                }

            }
        }
    }
}