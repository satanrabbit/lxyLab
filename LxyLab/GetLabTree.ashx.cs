using System;
using System.Collections.Generic;
using System.Web;
using LitJson;

namespace LxyLab
{
    /// <summary>
    /// GetLabTree 获取实验室列表的树形菜单json数据
    /// </summary>
    public class GetLabTree : IHttpHandler
    {
        /* 返回json格式
         *  id：节点id，对载入远程数据很重要。
            text：显示在节点的文本。
            state：节点状态，'open' or 'closed'，默认为'open'。当设置为'closed'时，拥有子节点的节点将会从远程站点载入它们。
            checked：表明节点是否被选择。
            attributes：可以为节点添加的自定义属性。
            children：子节点，必须用数组定义。
         */
        public void ProcessRequest(HttpContext context)
        {
            //查询数据库获取实验室列表
            DataModel dm=new DataModel();
            JsonData jd = new JsonData();
            List<LabType> lts = dm.GetLabTypes();
            List<LabChType> lcts = dm.GetLabChTypes();
            List<Lab> labs = dm.GetLabs();
            foreach (var t in lts)
            {
                JsonData jt = new JsonData();
                jt["id"] = t.LabTypeID;
                jt["text"] = t.LabTypeName;
                jt["iconCls"] = "icon-application_cascade";
                jt["children"] = new JsonData();
                foreach (var ct in lcts.FindAll(delegate(LabChType lct) { return lct.LabSupType == t.LabTypeID; }))
                {
                    JsonData jct = new JsonData();
                    jct["id"] = ct.LabChID;
                    jct["text"] = ct.LabChName;
                    jct["iconCls"] = "icon-application_cascade";
                    jct["children"] = new JsonData();
                    foreach (var lb in labs.FindAll(delegate(Lab _lb) { return _lb.LabType == ct.LabChID; }))
                    {
                        JsonData jLab = new JsonData();
                        jLab["id"] = lb.LabID;
                        jLab["text"] =lb.LabName;
                        //"iconCls":"icon-search" 
                        jLab["iconCls"] = "icon-application_home";
                        jLab["checked"] =  lb.LabDefault;
                        jct["children"].Add(jLab);
                    }
                    jt["children"].Add(jct);
                }
                
                jd.Add(jt);
            }
            string labStr=jd.ToJson();
           
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(labStr);
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