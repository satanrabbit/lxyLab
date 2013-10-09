using System;
using System.Collections.Generic;
using System.Web;
using LitJson;

namespace LxyLab
{
    /// <summary>
    /// GetWeeks 获取指定学期的教学周的树形菜单json数据
    /// </summary>
    public class GetTerms : IHttpHandler
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
            LxyOledb oledb = new LxyOledb();

            oledb.Conn.Open();
            oledb.Cmd.CommandText = "select * from Term_tb order by TermStartDay desc";
            oledb.Dr = oledb.Cmd.ExecuteReader();
            JsonData jd = new JsonData();
            while(oledb.Dr.Read()){
                JsonData jdWeek = new JsonData();
                jdWeek["id"] = Convert.ToInt32(oledb.Dr["TermID"]);
                jdWeek["text"] = oledb.Dr["TermName"].ToString();
                jdWeek["TermStartDay"] = ((DateTime)oledb.Dr["TermStartDay"]).ToString("yyyy-MM-dd");
                jdWeek["TermWeeks"] = Convert.ToInt32(oledb.Dr["TermWeeks"]);
                jdWeek["iconCls"] = "icon-date";
                jdWeek["isCurrent"] = oledb.Dr["TermIsCurrent"].ToString();
                jd.Add(jdWeek);
            }
            string jdString = jd.ToJson();
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(jdString);
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