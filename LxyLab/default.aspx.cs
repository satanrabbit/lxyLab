using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LxyLab
{
    public partial class _default : System.Web.UI.Page
    {
        
        protected int defaultLab = 0;
        protected int currentWeek = 1;
        protected int weeks = 23;
        protected LxyUser us = new LxyUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            lxyAuthor.validateAuthor(this);
            //lxyAuthor.validateAuthorJson(this);
            
            DataModel dm = new DataModel();
            defaultLab = dm.GetLab().LabID;
            Term term = dm.GetCurrntTerm();
            weeks = term.TermWeeks;
            currentWeek = (DateTime.Now - term.TermStartDay).Days / 7 + 1;
            us = dm.GetUser(Convert.ToInt32(Session["lxyLabUserID"]));
        }
    }
}