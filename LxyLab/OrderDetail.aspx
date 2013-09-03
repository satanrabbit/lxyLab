<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="LxyLab.OrderDetail" %>
 
<style type="text/css">
    .order-detail {
        line-height:2em; width:460px; 
    }
    .order-detail td { 
        border:1px #000 solid;
    }
</style>
<div style="width:480px;margin:5px auto;">
    <table class="order-detail"  >
       <tr>
            <td style="width:80px;">用户信息：</td>
            <td>
                姓名:<%=luser.UserName %>&emsp;<br />
                身份:<%=(luser.UserIdentity==1?"教师":"学生") %>&emsp;<br />
                学院或部门:<%=luser.UserCollege %>&emsp; 
                <br />
                电话:<%=luser.UserTel %>&emsp;
                <br />
                邮箱:<%=luser.UserAccount %>&emsp;
                <br />
            </td>
        </tr>
        <tr>
            <td style="width:80px;">预约信息：</td>
            <td>
                第<%=lo.OrderWeek %>教学周，周<%=lo.OrderWeekday %>第<%=lo.OrderCls %>节课<br />
                实验室：<%=lo.OrderLabName %>
            </td>
        </tr><tr>
            <td style="width:80px;">预约课题：</td>
            <td> <%=lo.OrderTitle %></td>
        </tr><tr>
            <td style="width:80px;">预约人数：</td>
            <td><%=lo.OrderAmount %></td>
        </tr><tr>
            <td style="width:80px;" valign="top">备注信息：</td>
            <td><%=lo.OrderIntro %> </td>
        </tr><tr>
            <td style="width:80px;" valign="top">预约仪器：</td>
            <td> 
                <%foreach (LxyLab.InstOrder ins in inos)
                  { 
                      %>
                <%=ins.InstName %> &emsp; <%=ins.InstOrderAmount %>台(件)<br />
                <%
                  } %>

            </td>
        </tr> 
    </table>
    </div>
     <script type="text/javascript">
         $(function () {
             $.parser.parse();
         });
    </script>