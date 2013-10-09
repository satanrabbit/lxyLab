using LitJson;
using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// GetUserList 的摘要说明
    /// </summary>
    public class GetUserList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            List<LxyUser> lts = dm.GetUsers(); ;
            LxyUserWithTotal ltr = new LxyUserWithTotal();
            ltr.total = lts.Count;
            ltr.rows = lts;

            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(ltr));
            context.Response.End();
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