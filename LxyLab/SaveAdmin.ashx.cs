using System;
using System.Collections.Generic;
using System.Web;
using LitJson;

namespace LxyLab
{
    /// <summary>
    /// SaveAdmin 的摘要说明
    /// </summary>
    public class SaveAdmin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string adminNewPWD = context.Request.Params["AdminNewPwd"].ToString();
            string adminCfPwd = context.Request.Params["AdminCfPwd"].ToString();
            JsonData jd = new JsonData();
            int status = 0;
            string msg = "";
            if (adminCfPwd != adminNewPWD)
            {
                msg = "新密码不一致，请确认！";
                //context.Response.Write();
            }
            else
            {
                LxyAdmin admin = new LxyAdmin();
                admin.AdminID = Convert.ToInt32(context.Request.Params["AdminID"]);
                admin.AdminAccount = context.Request.Params["AdminAccount"].ToString();
                admin.AdminPwd = context.Request.Params["AdminPwd"].ToString();
                admin.AdminName = context.Request.Params["AdminName"].ToString();
                admin.AdminLevel = "1";
                DataModel dm =new DataModel();

                if (admin.AdminID != 0)
                {
                    //修改
                    LxyAdmin _admin = dm.GetAdmin(admin.AdminID);
                    if (admin.AdminPwd != _admin.AdminPwd)
                    {
                        msg = "原密码错误！";
                    }
                    else
                    {
                        admin.AdminID=dm.SaveAdmin(admin);
                        status = 1;
                        msg = "修改成功！";
                    }
                }
            }
            jd["status"] = status;
            jd["msg"] = msg;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(jd));
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