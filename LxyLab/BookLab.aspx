<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookLab.aspx.cs" Inherits="LxyLab.BookLab" %>

 
   
<div style="width:480px;margin:5px auto;">
    <table style="line-height:2.5em;">
        <tr>
            <td style="width:460px;">
                <form action="BookLabSave.ashx" method="post" name="EditForm" id="EditForm"> 
                <table><tr>
                        <td style="width:80px;">用户信息：</td>
                        <td>
                            <input type="hidden" name="OrderLab" value="<%=lab.LabID %>" />
                            <input type="hidden" name="OrderWeek" value="<%=lo.OrderWeek %>" />
                            <input type="hidden" name="OrderWeekday" value="<%=lo.OrderWeekday %>" />
                            <input type="hidden" name="OrderCls" value="<%=lo.OrderCls %>" />
                            姓名:<%=luser.UserName %>&emsp;
                            身份:<%=(luser.UserIdentity==1?"教师":"学生") %>&emsp;
                            学院或部门:<%=luser.UserCollege %>&emsp;<br />
                            电话:<%=luser.UserTel %>&emsp;
                            邮箱:<%=luser.UserAccount %>&emsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:80px;">预约信息：</td>
                        <td>
                            第<%=lo.OrderWeek %>教学周，周<%=lo.OrderWeekday %>第<%=lo.OrderCls %>节课<br />
                            实验室：<%=lab.LabName %>
                        </td>
                    </tr><tr>
                        <td style="width:80px;">预约课题：</td>
                        <td><input name="OrderTitle" type="text" style="width:250px;" class="easyui-validatebox" data-options="required:true,missingMessage:'请填写预约实验课题名称！'" value="<%= ""%>" /></td>
                    </tr><tr>
                        <td style="width:80px;">预约人数：</td>
                        <td><input name="OrderAmount" type="text" style="width:60px;" class="easyui-numberbox" data-options="required:true,min:0,missingMessage:'请填写整数的人数！'" value="<%=""%>" /></td>
                    </tr><tr>
                        <td style="width:80px;" valign="top">备注信息：</td>
                        <td><textarea name="OrderIntro" style="width:250px;height:55px;"></textarea><br /><p style="line-height:14px;">请说明实验多所需的药品及实验器材，如果需要特殊仪器，请在完成预约实验室后预约仪器！</p></td>
                    </tr> 
                </table></form>
            </td>
        </tr>
    </table>
    </div>
     <script type="text/javascript">
         $(function () {
             $.parser.parse();
         });
    </script>