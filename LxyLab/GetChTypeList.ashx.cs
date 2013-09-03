using LitJson;
using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// GetChTypeList 的摘要说明
    /// </summary>
    public class GetChTypeList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            List<LabChType> lts = dm.GetLabChTypes();
            List<LabChType> _lts = new List<LabChType>();
            if (context.Request.Params["id"] != null && context.Request.Params["id"].Trim() != "")
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                _lts = lts.FindAll(delegate(LabChType lct) { return lct.LabSupType == id; });
            }
            else
            {
                _lts = lts;
            }
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(_lts));
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