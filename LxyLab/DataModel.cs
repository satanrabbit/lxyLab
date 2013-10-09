using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using LitJson;

namespace LxyLab
{
    public class DataModel
    {
        public DataModel()
        {
            connStr = ConfigurationManager.ConnectionStrings["connLxyLab"].ConnectionString;
            conn = new OleDbConnection(connStr);
            ds = new DataSet();
        }

        private OleDbConnection conn { get; set; }
        public string connStr {get;set;}
        public OleDbCommand cmd { get; set; }
        private DataSet ds { get; set; }
        private OleDbDataReader dr { get ; set;}
        private OleDbDataAdapter da{ get ;set ; }

        private void InitCommand()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            da = new OleDbDataAdapter(cmd);
        }


        #region 获取指定id 的记录的预约信息
        public LabOrder GetLabOrder(int id)
        {
            string sqlStr = "select *  from ((Order_tb left join User_tb on Order_tb.OrderUser = User_tb.UserID)left join Lab_tb on Order_tb.OrderLab = Lab_tb.LabID) where OrderID = @id";
            InitCommand();
            cmd.CommandText = sqlStr;
            cmd.Parameters.AddWithValue("@id",id);
            dr = cmd.ExecuteReader();
            LabOrder lo = new LabOrder();
            if (dr.Read())
            {
                lo.OrderID = Convert.ToInt32(dr["OrderID"]);
                lo.OrderAmount = Convert.ToInt32(dr["OrderAmount"]);
                lo.OrderCls = Convert.ToInt32(dr["OrderCls"]);

                lo.OrderIntro = dr["OrderIntro"].ToString();
                lo.OrderLab = Convert.ToInt32(dr["OrderLab"]);
                lo.OrderLabName = dr["LabName"].ToString();

                lo.OrderReplay = dr["OrderReplay"].ToString();
                lo.OrderStatus = Convert.ToInt32(dr["OrderStatus"]);
                lo.OrderTerm = Convert.ToInt32(dr["OrderTerm"]);

                lo.OrderTitle = dr["OrderTitle"].ToString();
                lo.OrderUser = Convert.ToInt32(dr["OrderUser"]);
                lo.OrderUserIdentity = Convert.ToInt32(dr["UserIdentity"]);

                lo.OrderUserName = dr["UserName"].ToString();
                lo.OrderWeek = Convert.ToInt32(dr["OrderWeek"]);
                lo.OrderWeekday = Convert.ToInt32(dr["OrderWeekday"]);

                
            }

            dr.Close(); 
            conn.Close();
            return lo;
        }
        #endregion
        #region 获取实验室预约信息
        /// <summary>
        /// 获取实验室预约信息
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="sort">排序元</param>
        /// <param name="sortOrder">desc或asc</param>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userNum">查询用户</param>
        /// <param name="week">查询周</param>
        /// <param name="weekday">查询</param>
        /// <param name="cls">查询节次</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs (int page,int pageSize, string sort,string sortOrder,
            int lab,int term,string userNum,int week,int weekday,int cls){

            string lab_s = lab == 0 ? " " : " and OrderLab = " + lab.ToString() + " ";
            string term_s = term == 0 ? "  " :" and OrderTerm= "+ term.ToString();
            string week_s = week==0? "  ":" and OrderWeek = "+ week.ToString();
            string weekday_s = weekday==0?"  ":" and OrderWeekday= "+ weekday.ToString();
            string cls_s = cls == 0 ? "  " : " and OrderCls = " + cls.ToString();
            string user_s = userNum == "" ? "  " :" and OrderUser = "+userNum.ToString()+"";

            string where_s = " where 1=1 " + user_s  + lab_s +  term_s +  week_s  + weekday_s  + cls_s + "  ";

            string order_s = " order by  "+sort+ " "+ sortOrder+" ";

            string from_s=" from ((Order_tb left join User_tb on Order_tb.OrderUser = User_tb.UserID)left join Lab_tb on Order_tb.OrderLab = Lab_tb.LabID)  ";
            string cmdText;
            if (page>0)
            {
                //分页
                if (page == 1)
                {
                    //取第一页
                    cmdText = "select top " + pageSize + " * " +from_s 
                        + where_s + order_s;
                }
                else
                {
                    //取大于第一页的数据
                    cmdText = "select top " + pageSize + " *  " + from_s
                        + where_s + " and OrderID not in ( select top "
                                            + pageSize * (page - 1) + " OrderID from Order_tb " + where_s +  order_s +" ) " + order_s;
                }
            }
            else
            {
                cmdText = "select * " + from_s + where_s + order_s;
            }

            LabOrdersWithTotal lbot = new LabOrdersWithTotal();

            InitCommand();
            cmd.CommandText = cmdText;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LabOrder lo = new LabOrder();

                lo.OrderID =Convert.ToInt32( dr["OrderID"]);
                lo.OrderAmount = Convert.ToInt32(dr["OrderAmount"]);
                lo.OrderCls = Convert.ToInt32(dr["OrderCls"]);

                lo.OrderIntro = dr["OrderIntro"].ToString();
                lo.OrderLab =Convert.ToInt32( dr["OrderLab"]);
                lo.OrderLabName = dr["LabName"].ToString();

                lo.OrderReplay = dr["OrderReplay"].ToString();
                lo.OrderStatus = Convert.ToInt32(dr["OrderStatus"]);
                lo.OrderTerm = Convert.ToInt32(dr["OrderTerm"]);

                lo.OrderTitle = dr["OrderTitle"].ToString();
                lo.OrderUser = Convert.ToInt32(dr["OrderUser"]);
                if (lo.OrderUser != 0)
                {
                    lo.OrderUserIdentity = Convert.ToInt32(dr["UserIdentity"]);

                    lo.OrderUserName = dr["UserName"].ToString();
                }
                lo.OrderWeek = Convert.ToInt32(dr["OrderWeek"]);
                lo.OrderWeekday = Convert.ToInt32(dr["OrderWeekday"]);

                lbot.rows.Add(lo);
            }
            dr.Close();
            cmd.CommandText = "select count(OrderID) from Order_tb " +where_s;
            lbot.total = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return lbot;
        }


        /// <summary>
        /// 获取实验室预约信息,默认页数20
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="sort">排序元</param>
        /// <param name="sortOrder">desc或asc</param>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <param name="week">查询周</param>
        /// <param name="weekday">查询</param>
        /// <param name="cls">查询节次</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>        
        public LabOrdersWithTotal GetBookLabs(int page, string sort, string sortOrder,
            int lab, int term, string userNum, int week, int weekday, int cls)
        {
            return GetBookLabs(page, 20, sort, sortOrder,
             lab, term, userNum, week, weekday, cls);
        }

        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认不分页
        /// </summary>
        /// <param name="sort">排序元</param>
        /// <param name="sortOrder">desc或asc</param>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <param name="week">查询周</param>
        /// <param name="weekday">查询</param>
        /// <param name="cls">查询节次</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>        
        public LabOrdersWithTotal GetBookLabs(string sort, string sortOrder,
            int lab, int term, string userNum, int week, int weekday, int cls)
        {
            return GetBookLabs(0, sort,  sortOrder,
             lab,  term,  userNum,  week,  weekday,  cls);
        }

        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序
        /// </summary>
        /// <param name="sort">排序元</param>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <param name="week">查询周</param>
        /// <param name="weekday">查询</param>
        /// <param name="cls">查询节次</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>        
        public LabOrdersWithTotal GetBookLabs(string sort, 
            int lab, int term, string userNum, int week, int weekday, int cls)
        {
            return GetBookLabs( sort, " desc ",
             lab, term, userNum, week, weekday, cls);
        }
        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序
        /// </summary>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <param name="week">查询周</param>
        /// <param name="weekday">查询</param>
        /// <param name="cls">查询节次</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>        
        public LabOrdersWithTotal GetBookLabs(int lab, int term, string userNum, int week, int weekday, int cls)
        {
            return GetBookLabs("OrderPostTime",lab, term, userNum, week, weekday, cls);
        }
        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序，默认全部课节
        /// </summary>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <param name="week">查询周</param>
        /// <param name="weekday">查询</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs(int lab, int term, string userNum, int week, int weekday)
        {
            return GetBookLabs(lab, term,  userNum, week, weekday,0);
        }

        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序，默认全部课节，默认全部工作日
        /// </summary>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <param name="week">查询周</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs(int lab, int term, string userNum, int week)
        {
            return GetBookLabs(lab, term, userNum, week, 0);
        }

        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序，默认全部课节，默认全部工作日，默认全部周
        /// </summary>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <param name="userID">查询用户</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs(int lab, int term, string userNum)
        {
            return GetBookLabs(lab, term,  userNum, 0);
        }
        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序，默认全部课节，默认全部工作日，默认全部周，默认全部用户
        /// </summary>
        /// <param name="lab">查询实验室</param>
        /// <param name="term">查询学期</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs(int lab, int term)
        {
            return GetBookLabs(lab, term,"");
        }
        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序，默认全部课节，默认全部工作日，默认全部周，默认全部用户，默认全部学期
        /// </summary>
        /// <param name="lab">查询实验室</param>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs(int lab)
        {
            return GetBookLabs(lab,0);
        }

        /// <summary>
        /// 获取实验室预约信息,默认页数20，默认desc排序，默认OrderPostTime排序，默认全部课节，默认全部工作日，默认全部周，默认全部用户，默认全部学期，默认全部实验室
        /// </summary>
        /// <returns>带有total（总记录条数）和各个记录的信息</returns>
        public LabOrdersWithTotal GetBookLabs()
        {
            return GetBookLabs(0);
        }
        #endregion

        #region 获取实验室一级分类
        public List<LabType> GetLabTypes()
        {
            List<LabType> lts = new List<LabType>();
            InitCommand();
            cmd.CommandText = "select * from LabType_tb";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LabType lt = new LabType();
                lt.LabTypeID = Convert.ToInt32(dr["LabTypeID"]);
                lt.LabTypeInfo = dr["LabTypeInfo"].ToString();
                lt.LabTypeName = dr["LabTypeName"].ToString();
                lts.Add(lt);
            }
            dr.Close();
            conn.Close();
           
            return lts;
        }

        #endregion
        #region 获取实验室一级分类
        public LabType GetLabType(int id)
        {
            LabType lt = new LabType();
            InitCommand();
            cmd.CommandText = "select * from LabType_tb where LabTypeID = @id";
            cmd.Parameters.AddWithValue("@id",id);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lt.LabTypeID = Convert.ToInt32(dr["LabTypeID"]);
                lt.LabTypeInfo = dr["LabTypeInfo"].ToString();
                lt.LabTypeName = dr["LabTypeName"].ToString();
                
            }
            dr.Close();
            conn.Close();
            return lt;
        }

        #endregion
        #region 保存实验室一级分类
        public int SaveLabType(LabType lt)
        {
            InitCommand();
            if (lt.LabTypeID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO LabType_tb (LabTypeName,LabTypeInfo) VALUES (@LabTypeName,@LabTypeInfo)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LabTypeName",lt.LabTypeName);
                cmd.Parameters.AddWithValue("@LabTypeInfo",lt.LabTypeInfo);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                lt.LabTypeID = (int)cmd.ExecuteScalar();
            }
            else
            { 
                cmd.CommandText = "UPDATE LabType_tb SET  " +
                " LabTypeName=@LabTypeName,LabTypeInfo=@LabTypeInfo where LabTypeID=@LabTypeID";
                cmd.Parameters.Clear(); 
                cmd.Parameters.AddWithValue("@LabTypeName", lt.LabTypeName);
                cmd.Parameters.AddWithValue("@LabTypeInfo", lt.LabTypeInfo);
                cmd.Parameters.AddWithValue("@LabTypeID", lt.LabTypeID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return lt.LabTypeID;
        }
        #endregion
        #region 删除实验室一级分类
        public void DeleteLabType(int id)
        {
            InitCommand();
            cmd.CommandText = "delete from  LabType_tb where LabTypeID=@id";
            cmd.Parameters.AddWithValue("@id",id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        #endregion
        #region 获取实验室二级分类
        public List<LabChType> GetLabChTypes()
        {
            List<LabChType> lts = new List<LabChType>();
            InitCommand();
            cmd.CommandText = "select * from ( LabChType_tb left join LabType_tb on LabChType_tb.LabSupType= LabType_tb.LabTypeID )";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LabChType lt = new LabChType();
                lt.LabChID = Convert.ToInt32(dr["LabChID"]);
                lt.LabChName = dr["LabChName"].ToString();
                lt.LabSupType = Convert.ToInt32(dr["LabSupType"]);
                lt.LabSupName = dr["LabTypeName"].ToString();
                lts.Add(lt);
            }
           // lts.Find(delegate(LabChType lct) { return lct.LabChID == 1; });
            dr.Close();
            conn.Close();
            return lts;
        }

        #endregion
        #region 保存实验室二级分类
        public int SaveLabChType(LabChType lt)
        {
            InitCommand();
            if (lt.LabChID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO LabChType_tb (LabChName,LabSupType) VALUES (@LabChName,@LabSupType)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LabTypeName", lt.LabChName);
                cmd.Parameters.AddWithValue("@LabTypeInfo", lt.LabSupType);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                lt.LabChID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd.CommandText = "UPDATE LabChType_tb SET  " +
                " LabChName=@LabChName,LabSupType=@LabSupType where LabChID=@LabChID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LabChName", lt.LabChName);
                cmd.Parameters.AddWithValue("@LabSupType", lt.LabSupType);
                cmd.Parameters.AddWithValue("@LabChID", lt.LabChID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return lt.LabChID;
        }
        #endregion
        #region 删除实验室二级分类
        public void DeleteLabChType(int id)
        {
            InitCommand();
            cmd.CommandText = "delete from  LabChType_tb where LabChID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        #endregion
        #region 获取实验室二级分类详细
        public LabChType GetLabChType(int id)
        {
            LabChType lt = new LabChType();
            InitCommand();
            cmd.CommandText = "select * from ( LabChType_tb left join LabType_tb on LabChType_tb.LabSupType= LabType_tb.LabTypeID ) where LabChType_tb.LabChID = @id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lt.LabChID = Convert.ToInt32(dr["LabChID"]);
                lt.LabChName = dr["LabChName"].ToString();
                lt.LabSupType = Convert.ToInt32(dr["LabSupType"]);
                lt.LabSupName = dr["LabTypeName"].ToString();
            }
            dr.Close();
            conn.Close();
            return lt;
        }
        #endregion

        #region 获取全部的实验室信息
        public List<Lab> GetLabs()
        {
            List<Lab> labs = new List<Lab>();
            InitCommand();
            cmd.CommandText = "select * from (( Lab_tb left join  LabChType_tb on Lab_tb.LabType = LabChType_tb.LabChID ) left join  LabType_tb on LabChType_tb.LabSupType= LabType_tb.LabTypeID ) ";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Lab lab = new Lab();
                lab.LabID = Convert.ToInt32(dr["LabID"]);
                lab.LabAddr = dr["LabAddr"].ToString();
                lab.LabAdmin = Convert.ToInt32(dr["LabAdmin"]);
                lab.LabAmount = Convert.ToInt32(dr["LabAmount"]);
                lab.LabDefault = Convert.ToBoolean(dr["LabDefault"]);
                lab.LabInfo = dr["LabInfo"].ToString();
                lab.LabName = dr["LabName"].ToString();
                lab.LabType = Convert.ToInt32(dr["LabType"]);
                lab.LabChName = dr["LabChName"].ToString();
                lab.LabTypeName = dr["LabTypeName"].ToString();
                labs.Add(lab);
            } 
            return labs;
        }

        #endregion 
        #region 获取指定实验室的信息
        /// <summary>
        /// 获取指定实验室的信息
        /// </summary>
        /// <param name="id">指定实验室的ID</param>
        /// <returns>实验室信息</returns>
        public Lab GetLab(int id)
        {
            Lab lab = new Lab();
            InitCommand();
            string cmdText = "select * from ((( Lab_tb left join LabChType_tb on Lab_tb.LabType=LabChType_tb.LabChID) left join LabType_tb on LabChType_tb.LabSupType=LabType_tb.LabTypeID) left join Admin_tb on Lab_tb.LabAdmin=Admin_tb.AdminID)  where Lab_tb.LabID=" 
                + id.ToString();
            cmd.CommandText = cmdText;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                lab.LabAddr = dr["LabAddr"].ToString();
                lab.LabAdmin =Convert.ToInt32(dr["LabAdmin"]); 

                lab.LabAmount = Convert.ToInt32(dr["LabAmount"]);
                lab.LabDefault = Convert.ToBoolean(dr["LabDefault"]);
                lab.LabID = Convert.ToInt32(dr["LabID"]);

                lab.LabInfo = dr["LabInfo"].ToString();
                lab.LabType = Convert.ToInt32(dr["LabType"]);

                lab.LabName = dr["LabName"].ToString();
                lab.LabChName = dr["LabChName"].ToString();
                lab.LabTypeName = dr["LabTypeName"].ToString();
                lab.LabSupType = Convert.ToInt32(dr["LabTypeID"]);
            }
            conn.Close();
            return lab;
        }
        /// <summary>
        /// 获取默认的实验室
        /// </summary>
        /// <returns></returns>
        public Lab GetLab()
        {
            Lab lab = new Lab();
            InitCommand();
            string cmdText = "select * from ((( Lab_tb left join LabChType_tb on Lab_tb.LabType=LabChType_tb.LabChID) left join LabType_tb on LabChType_tb.LabSupType=LabType_tb.LabTypeID) left join Admin_tb on Lab_tb.LabAdmin=Admin_tb.AdminID) where LabDefault";
            cmd.CommandText = cmdText;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                lab.LabAddr = dr["LabAddr"].ToString();
                lab.LabAdmin = Convert.ToInt32(dr["LabAdmin"]); 

                lab.LabAmount = Convert.ToInt32(dr["LabAmount"]);
                lab.LabDefault = Convert.ToBoolean(dr["LabDefault"]);
                lab.LabID = Convert.ToInt32(dr["LabID"]);

                lab.LabInfo = dr["LabInfo"].ToString();
                lab.LabType = Convert.ToInt32(dr["LabType"]);

                lab.LabName = dr["LabName"].ToString();
                lab.LabChName = dr["LabChName"].ToString();
                lab.LabTypeName = dr["LabTypeName"].ToString();
                lab.LabSupType = Convert.ToInt32(dr["LabTypeID"]);
            }
            conn.Close();
            return lab;
        }
        #endregion
        #region 保存实验室信息
        public int SaveLab(Lab lab)
        {
            InitCommand();
            if (lab.LabID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO Lab_tb (LabName,LabAdmin,LabAddr,LabInfo,LabAmount,LabType,LabDefault) VALUES (@LabName,@LabAdmin,@LabAddr,@LabInfo,@LabAmount,@LabType,@LabDefault)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LabName", lab.LabName);
                cmd.Parameters.AddWithValue("@LabAdmin", lab.LabAdmin);
                cmd.Parameters.AddWithValue("@LabAddr", lab.LabAddr);
                cmd.Parameters.AddWithValue("@LabInfo", lab.LabInfo);
                cmd.Parameters.AddWithValue("@LabAmount", lab.LabAmount);
                cmd.Parameters.AddWithValue("@LabType", lab.LabType);
                cmd.Parameters.AddWithValue("@LabDefault", lab.LabDefault); 
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                lab.LabID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd.CommandText = "UPDATE Lab_tb SET  " +
                " LabName=@LabName,LabAdmin=@LabAdmin ,LabAddr=@LabAddr,LabInfo=@LabInfo,LabAmount=@LabAmount,LabType=@LabType,LabDefault=@LabDefault  where LabID=@LabID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LabName", lab.LabName);
                cmd.Parameters.AddWithValue("@LabAdmin", lab.LabAdmin);
                cmd.Parameters.AddWithValue("@LabAddr", lab.LabAddr);
                cmd.Parameters.AddWithValue("@LabInfo", lab.LabInfo);
                cmd.Parameters.AddWithValue("@LabAmount", lab.LabAmount);
                cmd.Parameters.AddWithValue("@LabType", lab.LabType);
                cmd.Parameters.AddWithValue("@LabDefault", lab.LabDefault);
                cmd.Parameters.AddWithValue("@LabID", lab.LabID); 
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            
            return lab.LabID;

        }
        #endregion
        #region 删除实验室信息
        public void DeleteLab(int id)
        {
            InitCommand();
            cmd.CommandText = "delete from  Lab_tb where LabID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion
        #region 保存预约信息
        public int SaveLabOrder(LabOrder lo)
        {

            InitCommand();
            if (lo.OrderID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO Order_tb (OrderUser,OrderLab,OrderAmount,OrderTitle,OrderTerm,OrderWeek,OrderWeekday,OrderCls,OrderIntro,OrderPostTime)"
                    +" VALUES (@OrderUser,@OrderLab,@OrderAmount,@OrderTitle,@OrderTerm,@OrderWeek,@OrderWeekday,@OrderCls,@OrderIntro,@OrderPostTime)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrderUser", lo.OrderUser);
                cmd.Parameters.AddWithValue("@OrderLab",lo.OrderLab);
                cmd.Parameters.AddWithValue("@OrderAmount", lo.OrderAmount);
                cmd.Parameters.AddWithValue("@OrderTitle", lo.OrderTitle);
                cmd.Parameters.AddWithValue("@OrderTerm",lo.OrderTerm);
                cmd.Parameters.AddWithValue("@OrderWeek", lo.OrderWeek);
                cmd.Parameters.AddWithValue("@OrderWeekday", lo.OrderWeekday);
                cmd.Parameters.AddWithValue("@OrderCls", lo.OrderCls);
                cmd.Parameters.AddWithValue("@OrderIntro", lo.OrderIntro);
                OleDbParameter parameter = new OleDbParameter();
                parameter.OleDbType = OleDbType.DBDate;
                parameter.Value = lo.OrderPostTime;
                cmd.Parameters.Add(parameter);   
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                lo.OrderID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Order_tb SET  " +
                " OrderUser=@OrderUser,OrderLab=@OrderLab,OrderAmount=@OrderAmount,OrderTitle=@OrderTitle,OrderTerm=@OrderTerm," +
               " OrderWeek=@OrderWeek,OrderWeekday=@OrderWeekday ,OrderCls=@OrderCls,OrderIntro=@OrderIntro where UserID=@userID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrderUser", lo.OrderUser);
                cmd.Parameters.AddWithValue("@OrderLab", lo.OrderLab);
                cmd.Parameters.AddWithValue("@OrderAmount", lo.OrderAmount);
                cmd.Parameters.AddWithValue("@OrderTitle", lo.OrderTitle);
                cmd.Parameters.AddWithValue("@OrderTerm", lo.OrderTerm);
                cmd.Parameters.AddWithValue("@OrderWeek", lo.OrderWeek);
                cmd.Parameters.AddWithValue("@OrderWeekday", lo.OrderWeekday);
                cmd.Parameters.AddWithValue("@OrderCls", lo.OrderCls);
                cmd.Parameters.AddWithValue("@OrderIntro", lo.OrderIntro);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return lo.OrderID;
        }
        #endregion
        #region 获取当前学期
        public Term GetCurrntTerm()
        {
            Term term = new Term();
            InitCommand();
            string cmdText = "select * from Term_tb where TermIsCurrent";
            cmd.CommandText = cmdText;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                term.TermID = Convert.ToInt32(dr["TermID"]);
                term.TermIsCurrent = Convert.ToBoolean(dr["TermIsCurrent"]);
                term.TermName = dr["TermName"].ToString();

                term.TermStartDay = Convert.ToDateTime(dr["TermStartDay"]);
                term.TermWeeks = Convert.ToInt32(dr["TermWeeks"]);
            }
            conn.Close();
            return term;
        }
        #endregion
        #region 获取用户信息
        public List<LxyUser> GetUsers()
        {
            List<LxyUser> users = new List<LxyUser>();
            InitCommand();
            cmd.CommandText = "select * from User_tb";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LxyUser lt = new LxyUser();
                lt.UserAccount = dr["UserAccount"].ToString();
                lt.UserCollege = dr["UserCollege"].ToString();
                lt.UserName = dr["UserName"].ToString();
                lt.UserNumber = dr["UserNumber"].ToString();
                lt.UserTel = dr["UserTel"].ToString();
                lt.UserID = Convert.ToInt32(dr["UserID"]);
                lt.UserIdentity = Convert.ToInt32(dr["UserIdentity"]);
                users.Add(lt);
            }
            // lts.Find(delegate(LabChType lct) { return lct.LabChID == 1; });
            dr.Close();
            conn.Close();
            return users;
        }
        #endregion
        #region  获取指定学号的用户
        public LxyUser GetUser(string userNumber)
        {
            LxyUser lxyUser = new LxyUser();
            InitCommand();
            string cmdText = "select * from User_tb where UserNumber = @userNumber";
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("@userNumber", userNumber);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lxyUser.UserAccount = dr["UserAccount"].ToString();
                lxyUser.UserCollege = dr["UserCollege"].ToString();
                lxyUser.UserIdentity =Convert.ToInt32( dr["UserIdentity"]);
                lxyUser.UserName = dr["UserName"].ToString();
                lxyUser.UserNumber = userNumber;
                lxyUser.UserPwd = dr["UserPwd"].ToString();
                lxyUser.UserTel = dr["UserTel"].ToString(); 
                lxyUser.UserID = Convert.ToInt32(dr["UserID"]);
            }
            conn.Close();
            return lxyUser;
        }
        #endregion
        #region  获取指定账号邮箱的用户
        public LxyUser GetUser(string userAccount,bool flag)
        {
            LxyUser lxyUser = new LxyUser();
            InitCommand();
            string cmdText = "select * from User_tb where UserAccount = @userAccount";
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("@userAccount", userAccount);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lxyUser.UserAccount = dr["UserAccount"].ToString();
                lxyUser.UserCollege = dr["UserCollege"].ToString();
                lxyUser.UserIdentity = Convert.ToInt32(dr["UserIdentity"]);
                lxyUser.UserName = dr["UserName"].ToString();
                lxyUser.UserNumber = dr["UserNumber"].ToString(); ;
                lxyUser.UserPwd = dr["UserPwd"].ToString();
                lxyUser.UserTel = dr["UserTel"].ToString();
                lxyUser.UserID =Convert.ToInt32( dr["UserID"]);
            }
            conn.Close();
            return lxyUser;
        }
        #endregion

        #region 获取指定ID 的用户
        public LxyUser GetUser(int id){
            LxyUser lxyUser=new LxyUser();
            InitCommand();
            string cmdText = "select * from User_tb where UserID = @UserID";
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("@UserID", id);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lxyUser.UserAccount = dr["UserAccount"].ToString();
                lxyUser.UserCollege = dr["UserCollege"].ToString();
                lxyUser.UserIdentity = Convert.ToInt32(dr["UserIdentity"]);
                lxyUser.UserName = dr["UserName"].ToString();
                lxyUser.UserNumber = dr["UserNumber"].ToString(); ;
                lxyUser.UserPwd = dr["UserPwd"].ToString();
                lxyUser.UserTel = dr["UserTel"].ToString();
                lxyUser.UserID = Convert.ToInt32(dr["UserID"]);
            }
            conn.Close();
            return lxyUser;
        }
        #endregion
        #region 保存用户
        public int SaveLxyUser(LxyUser user)
        {
            InitCommand();
            if (user.UserID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO User_tb (UserName,UserPwd,UserAccount,UserNumber,UserTel,UserIdentity,UserCollege) VALUES (@userName,@userPwd,@userAccount,@userNumber,@userTel,@userIdentity,@userCollege)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@userPwd", user.UserPwd);
                cmd.Parameters.AddWithValue("@userAccount", user.UserAccount);
                cmd.Parameters.AddWithValue("@userNumber", user.UserNumber);
                cmd.Parameters.AddWithValue("@userTel", user.UserTel);
                cmd.Parameters.AddWithValue("@userIdentity", user.UserIdentity);
                cmd.Parameters.AddWithValue("@userCollege", user.UserCollege);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                user.UserID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE User_tb SET  " +
                " UserName=@userName,UserPwd=@userPwd,UserAccount=@userAccount,UserNumber=@userNumber,UserTel=@userTel,"+
               " UserIdentity=@userIdentity,UserCollege=@userCollege where UserID=@userID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@userPwd", user.UserPwd);
                cmd.Parameters.AddWithValue("@userAccount", user.UserAccount);
                cmd.Parameters.AddWithValue("@userNumber", user.UserNumber);
                cmd.Parameters.AddWithValue("@userTel", user.UserTel);
                cmd.Parameters.AddWithValue("@userIdentity", user.UserIdentity);
                cmd.Parameters.AddWithValue("@userCollege", user.UserCollege);
                cmd.Parameters.AddWithValue("@userID", user.UserID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return user.UserID;
        }
        #endregion
        #region 删除用户
        public void DeleteUser(int id)
        {
            InitCommand();
            cmd.CommandText = "delete from  User_tb where UserID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion
        #region 仪器
        #region 获取全部仪器
        public List<Instrument> GetInstruments()
        {
            List<Instrument> lts = new List<Instrument>();
            InitCommand();
            cmd.CommandText = "select * from Instrument_tb ";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Instrument lt = new Instrument();
                lt.InstrumentID = Convert.ToInt32(dr["InstrumentID"]);
                lt.InstrumentName = dr["InstrumentName"].ToString();
                lt.InstrumentAmount = Convert.ToInt32(dr["InstrumentAmount"]);
                lt.InstrumentPer = Convert.ToInt32(dr["InstrumentPer"]);
                lt.InstrumentIntro ="";
                lts.Add(lt);
            } 
            dr.Close();
            conn.Close();
            return lts;
        }
        #endregion
        #region 获取指定的仪器详细
        public Instrument GetInstrument(int id)
        {
            Instrument lt = new Instrument();
            InitCommand();
            cmd.CommandText = "select * from Instrument_tb ";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lt.InstrumentID = Convert.ToInt32(dr["InstrumentID"]);
                lt.InstrumentName = dr["InstrumentName"].ToString();
                lt.InstrumentAmount = Convert.ToInt32(dr["InstrumentAmount"]);
                lt.InstrumentPer = Convert.ToInt32(dr["InstrumentPer"]);
                lt.InstrumentIntro = dr["InstrumentIntro"].ToString(); 
            }
            dr.Close();
            conn.Close();
            return lt;
        }
        #endregion
        #region 保存仪器
        public int SaveInstrument(Instrument inst)
        {

            InitCommand();
            if (inst.InstrumentID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO Instrument_tb (InstrumentName,InstrumentAmount,InstrumentIntro,InstrumentPer) VALUES (@InstrumentName,@InstrumentAmount,@InstrumentIntro,@InstrumentPer)";
                cmd.Parameters.Clear(); 
                cmd.Parameters.AddWithValue("@InstrumentName", inst.InstrumentName);
                cmd.Parameters.AddWithValue("@InstrumentAmount", inst.InstrumentAmount); 
                cmd.Parameters.AddWithValue("@InstrumentIntro", inst.InstrumentIntro);
                cmd.Parameters.AddWithValue("@InstrumentPer", inst.InstrumentPer); 
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                inst.InstrumentID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd.CommandText = "UPDATE Instrument_tb SET  " +
                " InstrumentName=@InstrumentName,InstrumentAmount=@InstrumentAmount ,InstrumentIntro=@InstrumentIntro ,InstrumentPer=@InstrumentPer where InstrumentID=@InstrumentID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@InstrumentName", inst.InstrumentName);
                cmd.Parameters.AddWithValue("@InstrumentAmount", inst.InstrumentAmount);
                cmd.Parameters.AddWithValue("@InstrumentIntro", inst.InstrumentIntro);
                cmd.Parameters.AddWithValue("@InstrumentPer", inst.InstrumentPer); 
                cmd.Parameters.AddWithValue("@InstrumentID", inst.InstrumentID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            
            return inst.InstrumentID;
        }
        #endregion 
        #region 删除仪器
        public void DeleteInstrument(int id)
        {
            InitCommand();
            cmd.CommandText = "delete from  Instrument_tb where InstrumentID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion
        #endregion

        #region 获取指定至节次的仪器预约
        public List<InstOrder> GetInstOrders(int term,int week,int weekday,int cls,int instID )
        {
            List<InstOrder> orders = new List<InstOrder>();
            InitCommand();
            cmd.CommandText = "select * from (InstOrder_tb left join Order_tb on InstOrder_tb.InstOrderLab=Order_tb.OrderID) where  InstOrder_tb.InstOrderIns=@instID and InstOrderLab in "
                + " (select OrderID form Order_tb where OrderTerm = @term and OrderWeek = @week and  OrderWeekday=@weekday and   OrderCls=@cls  )";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                InstOrder lt = new InstOrder();
                lt.InstOrderID = Convert.ToInt32(dr["InstOrderID"]);
                lt.InstOrderLab = Convert.ToInt32(dr["InstOrderLab"]);
                lt.InstOrderIns = Convert.ToInt32(dr["InstOrderIns"]);
                lt.InstOrderAmount = Convert.ToInt32(dr["InstOrderAmount"]);
                orders.Add(lt);
            }

            dr.Close();
            conn.Close();
            return orders;
        }
        #endregion
        #region 保存预约信息
        public int SaveInstOrder(InstOrder ino)
        {
            InitCommand();
            if (ino.InstOrderID == 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO InstOrder_tb (InstOrderLab,InstOrderIns,InstOrderAmount) VALUES (@InstOrderLab,@InstOrderIns,@InstOrderAmount)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@InstOrderLab", ino.InstOrderLab);
                cmd.Parameters.AddWithValue("@InstOrderIns", ino.InstOrderIns);
                cmd.Parameters.AddWithValue("@InstOrderAmount", ino.InstOrderAmount); 
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                ino.InstOrderID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE User_tb SET  " +
                " InstOrderLab=@InstOrderLab,InstOrderIns=@InstOrderIns,InstOrderAmount=@InstOrderAmount where InstOrderID=@InstOrderID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@InstOrderLab", ino.InstOrderLab);
                cmd.Parameters.AddWithValue("@InstOrderIns", ino.InstOrderIns);
                cmd.Parameters.AddWithValue("@InstOrderAmount", ino.InstOrderAmount);
                cmd.Parameters.AddWithValue("@InstOrderID", ino.InstOrderID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return ino.InstOrderID;
        }
        #endregion
        #region 获取指定预约实验室的仪器预约
        public List<InstOrder> GetLabOrderInst(int id)
        {
            List<InstOrder> ods = new List<InstOrder>();
            InitCommand();
            cmd.CommandText = "select * from ( InstOrder_tb left join Instrument_tb on InstOrder_tb.InstOrderIns= Instrument_tb.InstrumentID ) where InstOrder_tb.InstOrderLab = @id";
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                InstOrder lt = new InstOrder();
                lt.InstOrderID = Convert.ToInt32(dr["InstOrderID"]);
                lt.InstOrderLab = Convert.ToInt32(dr["InstOrderLab"]);
                lt.InstOrderIns = Convert.ToInt32(dr["InstOrderIns"]);
                lt.InstOrderAmount = Convert.ToInt32(dr["InstOrderAmount"]);
                lt.InstName = dr["InstrumentName"].ToString();
                ods.Add(lt);
            }
            dr.Close();
            conn.Close();
            return ods;
        }
        #endregion
        #region 工具方法
        #region 返回json信息
        public void ReturnJsonMsg(HttpResponse response, int status, string msg)
        {
            JsonData jd = new JsonData();
            jd["status"] = status;
            jd["msg"] = msg;
            response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            response.Write(jd.ToJson());
            response.End();
        }
        public void ReturnJsonMsg(HttpResponse response, int status, string msg,int id)
        {
            JsonData jd = new JsonData();
            jd["status"] = status;
            jd["msg"] = msg;
            jd["id"] = id;
            response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            response.Write(jd.ToJson());
            response.End();
        }
        #endregion
        #endregion

        #region 获取管理员
        public LxyAdmin GetAdmin(string adminAccount)
        {
            InitCommand();
            LxyAdmin admin = new LxyAdmin();
            cmd.CommandText = "select * from Admin_tb where AdminAccount = @adminAccount";
            cmd.Parameters.AddWithValue("@AdminName", adminAccount);
            dr = cmd.ExecuteReader();
            if(dr.Read()){
                admin.AdminID = Convert.ToInt32(dr["adminID"]);
                admin.AdminLevel = dr["adminLevel"].ToString();
                admin.AdminAccount = adminAccount;
                admin.AdminPwd = dr["adminPWD"].ToString();
                admin.AdminName = dr["adminName"].ToString();
            }
            return admin;
        }
        public LxyAdmin GetAdmin(int adminID)
        {
            InitCommand();
            LxyAdmin admin = new LxyAdmin();
            cmd.CommandText = "select * from Admin_tb where AdminID = @adminID";
            cmd.Parameters.AddWithValue("@adminID", adminID);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                admin.AdminID = adminID;
                admin.AdminLevel = dr["adminLevel"].ToString();
                admin.AdminAccount = dr["AdminAccount"].ToString(); ;
                admin.AdminPwd = dr["adminPWD"].ToString();
                admin.AdminName = dr["adminName"].ToString();
            }
            return admin;
        }
        #endregion
        #region 保存管理员
        public int SaveAdmin(LxyAdmin admin)
        {
            InitCommand();
            if (admin.AdminID== 0)
            {
                //Insert new user 
                cmd.CommandText = "INSERT INTO Admin_tb (AdminName,AdminAccount,AdminPwd,AdminLevel) VALUES (@AdminName,@AdminAccount,@AdminPwd,@AdminLevel)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                cmd.Parameters.AddWithValue("@AdminAccount", admin.AdminAccount);
                cmd.Parameters.AddWithValue("@AdminLevel", admin.AdminLevel);
                cmd.Parameters.AddWithValue("@AdminPwd", admin.AdminPwd);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity ";
                admin.AdminID = (int)cmd.ExecuteScalar();
            }
            else
            {
                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Admin_tb SET  " +
                " AdminName=@AdminName,AdminAccount=@AdminAccount,AdminPwd=@AdminPwd,AdminLevel=@AdminLevel where AdminID =@AdminID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                cmd.Parameters.AddWithValue("@AdminAccount", admin.AdminAccount);
                cmd.Parameters.AddWithValue("@AdminPwd", admin.AdminPwd);
                cmd.Parameters.AddWithValue("@AdminLevel", admin.AdminLevel);
                cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return admin.AdminID;
        }
        #endregion

    }

    #region 元数据
    #region 实验室预约信息集合类
    /// <summary>
    /// 实验室预约信息集合类
    /// </summary>
    public class LabOrdersWithTotal {
        public LabOrdersWithTotal() {
            total = 0;
            rows = new List<LabOrder>();
        }
        /// <summary>
        /// 总的预约记录数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 记录集合
        /// </summary>
        public List<LabOrder> rows { get; set; }
        
    }
    #endregion
    #region 实验室预约信息类
    /// <summary>
    /// 实验室预约信息
    /// </summary>
    public class LabOrder
    {
        public int OrderID { get; set; }
        public int OrderUser { get; set; }
        public int OrderLab { get; set; }

        public int OrderAmount { get; set; }
        public string OrderTitle { get; set; }
        public int OrderTerm { get; set; }

        public int OrderWeek { get; set; }
        public int OrderWeekday { get; set; }
        public int OrderCls { get; set; }

        public string OrderIntro { get; set; }
        public string OrderUserName { get; set; }
        public int OrderUserIdentity { get; set; }

        public string OrderLabName { get; set; }
        public int OrderStatus { get; set; }
        public string OrderReplay { get; set; }

        public DateTime OrderPostTime { get; set; }
    }
    #endregion
    #region 实验使一级分类信息
    public class LabType
    {
        public int LabTypeID { get; set; }
        public string LabTypeName { get; set; }
        public string LabTypeInfo { get; set; }
    }
    #endregion
    #region LabTypeWithTotal
    public class LabTypeWithTotal
    {
        public int total { get; set; }
        public List<LabType> rows { get; set; }
    }
    #endregion
    #region 实验使二级分类信息
    public class LabChType
    {
        public int LabChID { get; set; }
        public string LabChName { get; set; }
        public int LabSupType { get; set; }
        public string LabSupName { get; set; }
    }
    #endregion
    #region LabChTypeWithTotal
    public class LabChTypeWithTotal
    {
        public int total { get; set; }
        public List<LabChType> rows { get; set; }
    }
    #endregion
    #region 实验室信息
    public class Lab
    {

        public int LabID { get; set; }
        public string LabName { get; set; }

        public int LabAdmin { get; set; }
        public string LabAddr { get; set; }
        public int LabAmount { get; set; }
        public string LabInfo { get; set; }
        public int LabType { get; set; }
        public bool LabDefault { get; set; }

        public string LabChName { get; set; }
        public string LabTypeName { get; set; }
        public int LabSupType { get; set; }


    }
    #endregion
    #region LabWithTotal
    public class LabWithTotal
    {
        public int total { get; set; }
        public List<Lab> rows { get; set; }
    }
    #endregion
    #region 学期信息
    public class Term
    {
        public int TermID { get; set; }
        public string TermName { get; set; }
        public DateTime TermStartDay { get; set; }

        public int TermWeeks { get; set; }
        public bool TermIsCurrent { get; set; }
    }
    #endregion

    #region 用户信息
    public class LxyUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserAccount { get; set; }
        public string UserNumber { get; set; }
        public string UserTel { get; set; }
        public int UserIdentity { get; set; }
        public string UserCollege { get; set; }
    }
    public class LxyUserWithTotal
    {
        public LxyUserWithTotal()
        {
            total = 0;
            rows = new List<LxyUser>();
        }
        public int total { get; set; }
        public List<LxyUser> rows { get; set; }
    }
    #endregion

    #region 仪器信息
    public class Instrument
    {
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int InstrumentAmount { get; set; }
        public string InstrumentIntro { get; set; }
        public int InstrumentPer { get; set; }
    }

    public class InstrumentWithTotal
    {
        public int total { get; set; }
        public List<Instrument> rows { get; set; }
    }
    #endregion
    #region 仪器预约信息
    public class InstOrder
    {
        public int InstOrderID { get; set; }
        public int InstOrderLab { get; set; }
        public int InstOrderIns { get; set; }
        public int InstOrderAmount { get; set; }
        public string InstName { get; set; }

    }
    #endregion

    #region 管理员信息
    public class LxyAdmin
    {
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string AdminPwd { get; set; }
        public string AdminLevel { get; set; }
        public string AdminAccount { get; set; }
    }
    public class LxyAdminWithTotal
    {
        public int total { get; set; }
        public List<LxyAdmin> rows { get; set; }
    }
    #endregion
    #endregion
}