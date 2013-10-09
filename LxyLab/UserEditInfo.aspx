<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEditInfo.aspx.cs" Inherits="LxyLab.UserEditInfo" %>

<!--用户修改信息，密码页面-->
<div style="width:450px;margin:5px auto;">
    <table style="line-height:2.5em;">
        <tr>
            <td style="width:250px;">
                <form action="UserEditInfoSave.ashx" method="post" name="UserEditInfoForm" id="UserEditInfoForm"> 
                <table><tr>
                        <td style="width:80px;">身份</td>
                        <td><input name="userIdentity" type="text" disabled="disabled"  class="easyui-validatebox" data-options="required:true" value="<%=userIdt %>" /></td>
                    </tr>
                    <tr>
                        <td style="width:80px;"><%=userNumLabel %></td>
                        <td><input name="UserNumber" type="text" disabled="disabled" class="easyui-validatebox" data-options="required:true" value="<%=lxyUser.UserNumber %>" /></td>
                    </tr><tr>
                        <td style="width:80px;">姓名</td>
                        <td><input name="UserName" type="text" class="easyui-validatebox" data-options="required:true" value="<%=lxyUser.UserName %>" /></td>
                    </tr><tr>
                        <td style="width:80px;">学院或部门</td>
                        <td><input name="UserCollege" type="text" class="easyui-validatebox" data-options="required:true" value="<%=lxyUser.UserCollege %>" /></td>
                    </tr><tr>
                        <td style="width:80px;">邮箱</td>
                        <td><input name="UserAccount" type="text" class="easyui-validatebox" data-options="required:true,validType:'email'" value="<%=lxyUser.UserAccount %>" /></td>
                    </tr><tr>
                        <td style="width:80px;">联系电话</td>
                        <td><input name="UserTel" type="text" class="easyui-validatebox" data-options="required:true" value="<%=lxyUser.UserTel %>" /></td>
                    </tr><tr>
                         <td colspan="2" style="text-align:center">
                             <a href="JavaScript:;" id="UserEditInfoSubmit" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true">保存信息</a>
                         </td> 
                    </tr>
                </table></form>
            </td>
            <td style="width:200px;">
                <form action="UserEditPwdSave.ashx" method="post" name="UserEditPwdForm" id="UserEditPwdForm">
                <table> <tr>
                        <td style="width:80px;">原密码</td>
                        <td><input name="UserOldPwd" type="password" class="easyui-validatebox" data-options="required:true" /></td>
                    </tr><tr>
                        <td style="width:80px;">新密码</td>
                        <td><input name="UserNewPwd" type="password" class="easyui-validatebox" data-options="required:true" /></td>
                    </tr><tr>
                        <td style="width:80px;">确认密码</td>
                        <td><input name="UserComPwd" type="password" class="easyui-validatebox" data-options="required:true" /></td>
                    </tr><tr>
                        <td colspan="2" style="text-align:center"><a href="javascript:;" id="UserEditPwdSubmit" class="easyui-linkbutton" data-options="iconCls:'icon-save' ,plain:true">修改密码</a></td>
                    </tr>
                </table>
                    </form>
            </td>
        </tr>
        <tr>
            <td colspan="2"> 
                <div style="color:#f00;font-size:14px;font-weight:600;" id="editInfoTip"></div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function () {
            $.parser.parse();
            //Save User Information
            $("#UserEditInfoSubmit").click(function () {
                var $form = $("#UserEditInfoForm");
                $form.form('submit', {
                    url:$form.attr("action"),
                    onSubmit: function () {
                        return $form.form('validate');
                    },
                    success: function (data) {
                        data = $.parseJSON(data);
                        if (data.status === 1) {
                            //保存成功
                            $("#editInfoTip").text("个人信息修改成功！");
                            $.messager.show({ title: '提示', msg: data.msg, timeout: 2500 });
                        } else {
                            $.messager.alert('错误', data.msg, 'error');
                        }
                    }
                });
            });
            //Save User Password
            $("#UserEditPwdSubmit").click(function () {
                var $form = $("#UserEditPwdForm");
                $form.form('submit', {
                    url: $form.attr("action"),
                    onSubmit: function () {
                        return $form.form('validate');
                    },
                    success: function (data) {
                        data = $.parseJSON(data);
                        if (data.status === 1) {
                            //保存成功
                            $("#editInfoTip").text("密码修改成功！");
                            $.messager.show({ title: '提示', msg: data.msg, timeout: 2500 });
                        } else {
                            $.messager.alert('错误', data.msg, 'error');
                        }
                    }
                });
            });
        });
    </script>
</div>