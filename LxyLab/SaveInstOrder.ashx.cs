using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// SaveInstOrder 的摘要说明
    /// </summary>
    public class SaveInstOrder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            InstOrder lt = new InstOrder();
            lt.InstOrderID = Convert.ToInt32(context.Request.Params["InstOrderID"]); 
            lt.InstOrderLab = Convert.ToInt32(context.Request.Params["InstOrderLab"]);
            lt.InstOrderIns = Convert.ToInt32(context.Request.Params["InstOrderIns"]);
            lt.InstOrderAmount = Convert.ToInt32(context.Request.Params["InstOrderAmount"]);
            dm.SaveInstOrder(lt);
            dm.ReturnJsonMsg(context.Response, 1, "保存成功！", lt.InstOrderID); 
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