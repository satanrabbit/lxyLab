using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using LitJson;

namespace LxyLab
{
    /// <summary>
    /// UserEditInfoSave 的摘要说明
    /// </summary>
    public class UserEditInfoSave : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            lxyAuthor.validateAuthorJson(context);
            DataModel dm = new DataModel();
            int userID = Convert.ToInt32(context.Session["lxyLabUserID"]);
            LxyUser lxyUser = new LxyUser();
            lxyUser = dm.GetUser(userID);
            lxyUser.UserAccount = context.Request["UserAccount"];
            lxyUser.UserCollege = context.Request["UserCollege"];
            lxyUser.UserName = context.Request["UserName"];
            lxyUser.UserTel = context.Request["UserTel"];

            dm.SaveLxyUser(lxyUser);
            JsonData jd = new JsonData();
            jd["status"] = 1;
            jd["msg"] = "修改个人信息成功！";
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(jd.ToJson());
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