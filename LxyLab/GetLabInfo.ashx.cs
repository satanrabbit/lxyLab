using System;
using System.Collections.Generic;
using System.Web;
using LitJson;
namespace LxyLab
{
    /// <summary>
    /// GetLabInfo 的摘要说明
    /// </summary>
    public class GetLabInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Params["lid"] == null || context.Request.Params["lid"] == "")
            {
                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                context.Response.Write("");
                context.Response.End();
            }
            else
            {
                int labID = Convert.ToInt32(context.Request.Params["lid"]);
                DataModel dm = new DataModel();
                Lab lab = dm.GetLab(labID);
                string wst = JsonMapper.ToJson(lab);
                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                context.Response.Write(wst);
                context.Response.End();
            }
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