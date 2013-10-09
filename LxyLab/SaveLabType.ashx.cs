using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// SaveLabType 的摘要说明
    /// </summary>
    public class SaveLabType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            LabType lt = new LabType();
            lt.LabTypeID = Convert.ToInt32(context.Request.Params["LabTypeID"]);
            lt.LabTypeName = context.Request.Params["LabTypeName"];
            lt.LabTypeInfo = "";
            dm.SaveLabType(lt);
            dm.ReturnJsonMsg(context.Response, 1, "保存成功！", lt.LabTypeID); 
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