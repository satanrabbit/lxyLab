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
            LxyOledb oledb = new LxyOledb();
            oledb.Conn.Open();
            oledb.Cmd.CommandText = "select * from Lab_tb LEFT JOIN LabType_tb on Lab_tb.LabType=LabType_tb.LabTypeID order by Lab_tb.LabType ,Lab_tb.LabID";
            oledb.Dr = oledb.Cmd.ExecuteReader();
            JsonData jd = new JsonData();
            int labTypeFlag = 0;
            while (oledb.Dr.Read())
            {
                JsonData jLab = new JsonData();
                jLab["id"] = Convert.ToInt32(oledb.Dr["LabID"]);
                jLab["text"] = Convert.ToString(oledb.Dr["LabName"]);
                //"iconCls":"icon-search" 
                jLab["iconCls"] = "icon-application_home";
               
                if (labTypeFlag == Convert.ToInt32(oledb.Dr["LabType"]))
                {
                    //该实验室的类型已经存在 ，遍历添加
                    jd[jd.Count - 1]["children"].Add(jLab); 
                }
                else
                {
                    //该实验室的类型不存在，新建实验室类型
                    labTypeFlag = Convert.ToInt32(oledb.Dr["LabType"]);
                    JsonData jLabType = new JsonData();
                    jLabType["id"] ="t"+ Convert.ToString(oledb.Dr["LabTypeID"]);
                    jLabType["text"] = Convert.ToString(oledb.Dr["LabTypeName"]);
                    jLabType["children"]=new JsonData();
                    jLabType["children"].Add(jLab);
                    jLabType["iconCls"] = "icon-application_cascade";
                    jd.Add(jLabType);
                }
            }
            string labs=jd.ToJson();
            oledb.Conn.Close();
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(labs);
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