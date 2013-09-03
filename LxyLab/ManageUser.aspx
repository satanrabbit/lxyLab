<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="LxyLab.ManageUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link  type="text/css" rel="stylesheet" href="source/easyui/themes/bootstrap/easyui.css" />
    <link type="text/css" rel="stylesheet" href="source/easyui/themes/icon.css" />
    <link type="text/css" rel="stylesheet" href="source/Alice/one-full.css" />
    <link href="source/qTip2/tips/jquery.qtip.nightly.min.css" rel="stylesheet" /> 
    <script type="text/javascript" src="source/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="source/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="source/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script  type="text/javascript" src="source/qTip2/tips/jquery.qtip.nightly.min.js"></script>     
    <script type="text/javascript" src="source/easyui/easyui_ex.js"></script>
     <script src="source/easyui/sr_easyui_dialog_form_20130602.js"></script>
    <script src="Scripts/json2.js"></script>
</head>
<body> 
    <div id="userOrder-dg">
    
    </div>
    <script type="text/javascript">
        $(function () {
            $("#userOrder-dg").datagrid({
                url: 'GetUserList.ashx', pagination: true,
                columns: [[
                           { field: 'UserID', title: '编号', checkbox: true },
                           { field: 'UserName', title: '姓名' },
                           { field: 'UserAccount', title: '帐号' },
                           { field: 'UserNumber', title: '卡号' },
                           { field: 'UserTel', title: '电话' },
                           {
                               field: 'UserIdentity', title: '身份', formatter: function (value, rowData, rowIndex) {
                                   if (value === 1) {
                                       return "教师";
                                   } else {
                                       return "学生";
                                   }

                               }
                           },
                           { field: 'UserCollege', title: '学院或部门' },
                           {
                               field: '_LabTypeID', title: '操作', align: 'center', formatter: function (value, rowData, rowIndex) {
                                   var formatterhtml =
                                     '<a herf="javascript:void(0)" class="easyui-linkbutton delete-dl-btn"  data-options="iconCls:\'icon-delete\',plain:true" data-id="' + rowData.UserID
                                   + '" >删除</a>'
                                   return formatterhtml;
                               }
                           }

                ]],
                onLoadSuccess: function (data) {
                    $.parser.parse();
                  
                    /*
                    ** 删除 
                    */
                    $(".delete-dl-btn").linkbutton().on("click", function () {
                        var $this = $(this);
                        $.messager.confirm("请确认", "您确定要删除？", function (b) {
                            if (b) {
                                $.post("DeleteUser.ashx", { id: parseInt($this.attr("data-id")) }, function (data) {
                                    $.messager.show({ title: "提示", msg: "删除成功", timeout: 2000 });
                                    $("#userOrder-dg").datagrid("reload");
                                });
                            }
                        });
                    });
                }

            });
        });
        </script>
</body>
</html>
