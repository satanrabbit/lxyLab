using System;
using System.Collections.Generic;
using System.Web;
using LitJson;

using System.Web.SessionState;
 

namespace LxyLab
{
    /// <summary>
    /// GetBookInfo 的摘要说明
    /// </summary>
    public class GetBookInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //page ,rows,sort,order,pager是否分页获取
            int page = 0;
            int pageSize = 20;
            int term = 0;
            int lab = 0;
            int week = 0;
            int weekday = 0;
            int cls = 0;
            string userNum="";
            int labAmount = 0;
            DataModel dm = new DataModel();
            if(term==0){
                term=dm.GetCurrntTerm().TermID;
            }
            if(context.Request.Params["labID"]!=null&&context.Request.Params["labID"].Trim()!=""){
                lab=Convert.ToInt32(context.Request.Params["labID"].Trim());
            }
            if(context.Request.Params["page"]!=null&&context.Request.Params["page"].Trim()!=""){
                page=Convert.ToInt32(context.Request.Params["page"].Trim());
            } if(context.Request.Params["pageSize"]!=null&&context.Request.Params["pageSize"].Trim()!=""){
                pageSize=Convert.ToInt32(context.Request.Params["pageSize"].Trim());
            }
            if(context.Request.Params["week"]!=null&&context.Request.Params["week"].Trim()!=""){
                week=Convert.ToInt32(context.Request.Params["week"].Trim());
            }
            if(context.Request.Params["weekday"]!=null&&context.Request.Params["weekday"].Trim()!=""){
                weekday=Convert.ToInt32(context.Request.Params["weekday"].Trim());
            }
            if(context.Request.Params["cls"]!=null&&context.Request.Params["cls"].Trim()!=""){
                cls=Convert.ToInt32(context.Request.Params["cls"].Trim());
            }
            if(context.Request.Params["userNum"]!=null&&context.Request.Params["userNum"].Trim()!=""){
                userNum = context.Request.Params["userNum"].Trim();
            }

                //int page,int pageSize, string sort,string sortOrder,int lab,int term,string userNum,int week,int weekday,int cls
            string wst = JsonMapper.ToJson(dm.GetBookLabs(page,pageSize,"OrderPostTime","desc",lab,term,userNum,week,weekday,cls));
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(wst);
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