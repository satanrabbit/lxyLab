using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using SRLib;
namespace LxyLab
{
    class LxyOledb
    {
        public LxyOledb()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["connLxyLab"].ConnectionString;
            Conn = new OleDbConnection(ConnStr);
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Da = new OleDbDataAdapter(Cmd);
            Ds = new DataSet();
        }

        private OleDbConnection conn;
        public OleDbConnection Conn { get { return conn; } set { conn = value; } }
        private string connStr;
        public string ConnStr { get { return connStr; } set { connStr = value; } }
        private OleDbCommand cmd;
        public OleDbCommand Cmd { get { return cmd; } set { cmd = value; } }
        private DataSet ds;
        public DataSet Ds { get { return ds; } set { ds = value; } }
        private OleDbDataReader dr;
        public OleDbDataReader Dr { get { return dr; } set { dr = value; } }
        private OleDbDataAdapter da;
        public OleDbDataAdapter Da { get { return da; } set { da = value; } }
    }
}
