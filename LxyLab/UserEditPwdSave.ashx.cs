using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using LitJson;
namespace LxyLab
{
    /// <summary>
    /// UserEditPwdSave 的摘要说明
    /// </summary>
    public class UserEditPwdSave : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            lxyAuthor.validateAuthorJson(context);
            int status = 0;
            string msg = "未知错误";

            if (context.Request["UserNewPwd"] != context.Request["UserComPwd"])
            {
                msg = "两次密码不一致！";
            }
            else
            {
                DataModel dm = new DataModel();
                int userID = Convert.ToInt32(context.Session["lxyLabUserID"]);
                LxyUser lxyUser = new LxyUser();
                lxyUser = dm.GetUser(userID);

                string oldPwd = context.Request.Params["UserOldPwd"];
                string newPwd = context.Request.Params["UserNewPwd"];
                if (lxyUser.UserPwd == SRLib.Des.EncryptDES(oldPwd, "SatanRabbit"))
                {

                    lxyUser.UserPwd = SRLib.Des.EncryptDES(newPwd, "SatanRabbit");
                    dm.SaveLxyUser(lxyUser);
                    status = 1;
                    msg = "修改密码成功！";
                }
                else
                {
                    msg = "原密码错误！";
                } 
            }
            JsonData jd = new JsonData();
            jd["status"] = status;
            jd["msg"] = msg;
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