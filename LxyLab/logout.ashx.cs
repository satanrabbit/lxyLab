using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace LxyLab
{
    /// <summary>
    /// logout 的摘要说明
    /// </summary>
    public class logout : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Session.Abandon();
            context.Response.Redirect("/");
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