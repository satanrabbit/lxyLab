using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// SaveLabChType 的摘要说明
    /// </summary>
    public class SaveLabChType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            LabChType lt = new LabChType();
            lt.LabChID = Convert.ToInt32(context.Request.Params["LabChID"]);
            lt.LabChName = context.Request.Params["LabChName"];
            lt.LabSupType = Convert.ToInt32(context.Request.Params["LabSupType"]);
            dm.SaveLabChType(lt);
            dm.ReturnJsonMsg(context.Response, 1, "保存成功！", lt.LabChID); 
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