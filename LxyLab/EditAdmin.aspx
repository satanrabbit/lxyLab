<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAdmin.aspx.cs" Inherits="LxyLab.EditAdmin" %>

<form id="EditForm" name="EditForm" action="SaveAdmin.ashx" method="post" onkeypress="if(event.keyCode==13||event.which==13){return false;}" >
    <table style="width:98%;line-height:2em;">
        <tr>
            <td style="width:65px;">账号：</td>
            <td>
                <input type="hidden" name="AdminID" value="<%=admin.AdminID%>" />
                <input type="text" class="easyui-validatebox" name="AdminAccount"  data-options="required:true,missingMessage:'请填写账号！'"  value="<%=admin.AdminAccount %>" />
            </td></tr>
          <tr><td>姓名：</td>
            <td> 
                <input type="text" class="easyui-validatebox"  data-options="required:true,missingMessage:'请填写姓名！'"  name="AdminName" value="<%=admin.AdminName %>" />
            </td></tr>
        <tr> <td>原始密码：</td>
            <td> 
                <input type="password" class="easyui-validatebox"  data-options="required:true,missingMessage:'请填写原密码！'"  name="AdminPwd" value="" />
            </td></tr>
        <tr> <td>新密码：</td>
            <td> 
                 <input type="password" class="easyui-validatebox"  data-options="required:true,missingMessage:'请填写新密码！'"  name="AdminNewPwd" value="" />
            </td></tr>
        <tr> <td>确认密码：</td>
            <td>
                 <input type="password" class="easyui-validatebox"  data-options="required:true,missingMessage:'请确认密码！'"  name="AdminCfPwd" value="" />
            </td></tr>
    </table>
    <script type="text/javascript">
        $(function () {
            $.parser.parse();
        });
    </script>
</form>