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
            bool pager = true;
            if(context.Request.Params["pager"]!=null){
                pager = Convert.ToBoolean(context.Request.Params["pager"]);
            }

            int term = 0;
            int lab = 0;
            int week = 0;
            int weekday = 0;
            int cls = 0;
            string userNum=null;
            int labAmount = 0;
            LxyOledb oledb = new LxyOledb();
            oledb.Conn.Open();
            if (context.Request.Params["userID"] == null || context.Request.Params["userID"] == "")
            {
                //未指定查询用户
                if (context.Session["adminID"] != null && context.Session["adminID"] != "")
                {
                    //查询全部用户的
                }
                else
                {
                    //查询当前用户的
                    userNum = context.Session["userNumber"].ToString();
                }
            }
            else
            {
                userNum = context.Request.Params["userID"];
            }
            //如是管理员登录的则查询不限用户的记录
         
            //学期
            if (context.Request.Params["term"] == null || context.Request.Params["term"] == "")
            {
                //学期为空，默认查询当前学期
                oledb.Cmd.CommandText = "select TermID form Term_tb where TermIsCurren= true";
                oledb.Dr = oledb.Cmd.ExecuteReader();
                if (oledb.Dr.Read())
                {
                    term = Convert.ToInt32(oledb.Dr["TermID"]);
                }
            }else{
                term=Convert.ToInt32(context.Request.Params["term"]);
            }
            //实验室
            if (context.Request.Params["lab"] == null || context.Request.Params["lab"] == "")
            {
                //不指定查询全部的实验室的记录
            }
            else
            {
                lab = Convert.ToInt32(context.Request.Params["lab"]);
                //查询实验室最多数
                oledb.Cmd.CommandText = "select * from Lab_tb where LabDefault=true ";
                oledb.Dr=oledb.Cmd.ExecuteReader();
                if (oledb.Dr.Read())
                {
                    labAmount = Convert.ToInt32(oledb.Dr["LabAmount"]);
                }
            }
            //周次
            if (context.Request.Params["week"] == null || context.Request.Params["week"] == "")
            {
                //未指定查询周，查询全部周的
            }
            else
            {
                week = Convert.ToInt32(context.Request.Params["week"]);
            }
            //工作日

            if (context.Request.Params["weekday"] == null || context.Request.Params["weekday"] == "")
            {
                //未指定查询工作日，查询全部工作日
            }
            else
            {
                weekday = Convert.ToInt32(context.Request.Params["weekday"]);
            }
            //课节
            if (context.Request.Params["cls"] == null || context.Request.Params["cls"] == "")
            {
                //未指定查询课节，查询全部课节
            }
            else
            {
                weekday = Convert.ToInt32(context.Request.Params["cls"]);
            }
            string cmdText = "select * from (Order_tb left join User_tb on Order_tb.OrderUser=User_tb.UserNumber) left join Lab_tb on Order_tb.OrderLab=Lab_tb.LabID  where Order_tb.OrderTerm = "+term;
            if (userNum != null)
            {
                cmdText = cmdText + " and Order_tb.OrderUser = "+userNum;
            }
            if (lab != 0)
            {
                cmdText = cmdText + " and Order_tb.OrderTab = " + lab;
            }

            if (week != 0)
            {
                cmdText = cmdText + " and Order_tb.OrderWeek = " + week;
            }

            if (weekday != 0)
            {
                cmdText = cmdText + " and  Order_tb.OrderWeekday = " + weekday;
            }

            if (cls != 0)
            {
                cmdText = cmdText + " and Order_tb.OrderCls = " + cls;
            }

            if (pager)
            {
                //分页
            }

            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write("");
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