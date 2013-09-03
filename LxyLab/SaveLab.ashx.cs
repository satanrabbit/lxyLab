using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// SaveLab 的摘要说明
    /// </summary>
    public class SaveLab : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            Lab  lt = new Lab ();
            lt.LabID = Convert.ToInt32(context.Request.Params["LabID"]);
            lt.LabAddr = context.Request.Params["LabAddr"];
            lt.LabName = context.Request.Params["LabName"];
            lt.LabInfo = context.Request.Params["LabInfo"];
            lt.LabAmount = Convert.ToInt32( context.Request.Params["LabAmount"]);
            lt.LabType = Convert.ToInt32(context.Request.Params["LabType"]);
            lt.LabDefault =false;
            lt.LabAdmin = 1; 
            dm.SaveLab(lt);
            dm.ReturnJsonMsg(context.Response, 1, "保存成功！", lt.LabID); 
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