using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace LxyLab
{
    /// <summary>
    /// BookLabSave 的摘要说明
    /// </summary>
    public class BookLabSave : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            LabOrder lo = new LabOrder();
            if (context.Request.Params["OrderLab"] == null || context.Request.Params["OrderLab"] == "")
            {
                dm.ReturnJsonMsg(context.Response, 0, "未知实验室！");
            }
            if (context.Request.Params["OrderWeek"] == null || context.Request.Params["OrderWeek"] == "")
            {
                dm.ReturnJsonMsg(context.Response, 0, "未知 周次！");
            }
            if (context.Request.Params["OrderWeekday"] == null || context.Request.Params["OrderWeekday"] == "")
            {
                dm.ReturnJsonMsg(context.Response, 0, "未知 工作日！");
            }
            if (context.Request.Params["OrderCls"] == null || context.Request.Params["OrderCls"] == "")
            {
                dm.ReturnJsonMsg(context.Response, 0, "未知 节次！");
            }
            if (context.Request.Params["OrderTitle"] == null || context.Request.Params["OrderTitle"] == "")
            {
                dm.ReturnJsonMsg(context.Response, 0, "未填写预约实验课题！");
            }
            if (context.Request.Params["OrderAmount"] == null || context.Request.Params["OrderAmount"] == "")
            {
                dm.ReturnJsonMsg(context.Response, 0, "未填写预约人数！");
            }

            //用户
            lo.OrderUser = Convert.ToInt32(context.Session["lxyLabUserID"]);
            //实验室
            lo.OrderLab = Convert.ToInt32(context.Request.Params["OrderLab"]);
            lo.OrderAmount = Convert.ToInt32(context.Request.Params["OrderAmount"]);
            lo.OrderCls = Convert.ToInt32(context.Request.Params["OrderCls"]);
            lo.OrderIntro = context.Request.Params["OrderIntro"];
            lo.OrderTerm = dm.GetCurrntTerm().TermID;
            lo.OrderTitle = context.Request.Params["OrderTitle"];
            lo.OrderPostTime = DateTime.Now;
            lo.OrderWeek = Convert.ToInt32(context.Request.Params["OrderWeek"]);
            lo.OrderWeekday = Convert.ToInt32(context.Request.Params["OrderWeekday"]);
            dm.SaveLabOrder(lo);
            dm.ReturnJsonMsg(context.Response,1,"预约成功！",lo.OrderID);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}