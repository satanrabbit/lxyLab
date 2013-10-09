using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// DeleteUser 的摘要说明
    /// </summary>
    public class DeleteUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();

            int id = Convert.ToInt32(context.Request.Params["id"]);
            dm.DeleteUser(id);
            dm.ReturnJsonMsg(context.Response, 1, "删除成功！"); 
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