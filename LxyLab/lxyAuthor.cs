using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using LitJson;

namespace LxyLab
{
    public class lxyAuthor
    {

        public  static void validateAuthor(Page page, string sessionName,string loginUrl)
        {
            if (page.Session[sessionName] == null || page.Session[sessionName].ToString() == "")
            {
                //未通过
                page.Response.Redirect(loginUrl);
            }
        }
        public static void validateAuthor(Page page,string sessionName)
        {
            validateAuthor(page, sessionName, @"/Login.aspx");
        }
        public static void validateAuthor(Page page)
        {
            validateAuthor(page, "lxyLabUserID");
        }
        public static void validateAuthorHtml(Page page, string sessionName, string loginUrl)
        {
            if (page.Session[sessionName] == null || page.Session[sessionName].ToString() == "")
            {
                //未通过 
                page.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                page.Response.Write(@"您没有登录或登录超时，<a target='_top' href='" + loginUrl + @"'> 请登录!</a>");
                page.Response.End();
            }
        }
        public static void validateAuthorHtml(Page page, string sessionName)
        {
            validateAuthorHtml(page, sessionName, @"/Login.aspx");
        }
        public static void validateAuthorHtml(Page page)
        {
            validateAuthorHtml(page, "lxyLabUserID");
        }
        public static void validateAuthorJson(HttpContext context, string sessionName, string loginUrl)
        {
            if (context.Session[sessionName] == null || context.Session[sessionName].ToString() == "")
            {
                //未通过
                JsonData jd = new JsonData();

                jd["status"] = 0;
                jd["msg"] = @"<a target='_top' href='" + loginUrl + @"'> 请登录 !</a>";
                

                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                context.Response.Write(jd.ToJson());
                context.Response.End();
            }
        }
        public static void validateAuthorJson(HttpContext context, string sessionName)
        {
            validateAuthorJson(context, sessionName, @"/Login.aspx");
        }
        public static void validateAuthorJson(HttpContext context)
        {
            validateAuthorJson(context, "lxyLabUserID");
        }

    }
}