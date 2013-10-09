using LitJson;
using System;
using System.Collections.Generic;
using System.Web;

namespace LxyLab
{
    /// <summary>
    /// GetInstrumentsForCombo 的摘要说明
    /// </summary>
    public class GetInstrumentsForCombo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataModel dm = new DataModel();
            List<Instrument> lts = dm.GetInstruments();

            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(lts));
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