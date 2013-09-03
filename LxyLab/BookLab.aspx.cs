using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class BookLab : System.Web.UI.Page
    {
        protected LabOrder lo;
        protected Lab lab;
        protected LxyUser luser;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataModel dm = new DataModel();
            lo = new LabOrder();
            if (Request.Params["week"] == null || Request.Params["week"] == "")
            {
                dm.ReturnJsonMsg(Response, 0, "未知周次！");
            }
            if (Request.Params["cls"] == null || Request.Params["cls"] == "")
            {
                dm.ReturnJsonMsg(Response, 0, "未知节次！");
            }
            if (Request.Params["wd"] == null || Request.Params["wd"] == "")
            {
                dm.ReturnJsonMsg(Response, 0, "未知工作日！");
            }
            if (Request.Params["lab"] == null || Request.Params["lab"] == "")
            {
                dm.ReturnJsonMsg(Response, 0, "未知实验室！");
            }
            lo.OrderCls = Convert.ToInt32(Request.Params["cls"]);
            lo.OrderWeek = Convert.ToInt32(Request.Params["week"]);
            lo.OrderWeekday = Convert.ToInt32(Request.Params["wd"]);
            //用户信息
            luser = dm.GetUser(Convert.ToInt32(Session["lxyLabUserID"]));
            //实验室信息
            lab = dm.GetLab(Convert.ToInt32(Request.Params["lab"]));
        }
    }
}