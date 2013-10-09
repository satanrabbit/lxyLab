<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabCHType.aspx.cs" Inherits="LxyLab.LabCHType" %>

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
                url: 'GetLabChTypeList.ashx', pagination: true,
                columns: [[
                           { field: 'LabChID', title: '编号', checkbox: true },
                           { field: 'LabChName', title: '二级分类名' },
                           { field: 'LabSupName', title: '所属一级分类' },
                           {
                               field: '_LabTypeID', title: '操作', align: 'center', formatter: function (value, rowData, rowIndex) {
                                   var formatterhtml = //'<a herf="javascript:void(0)" class="easyui-linkbutton view-dl-btn"  data-options="iconCls:\'icon-table\' ,plain:true" data-id="' + rowData.OrderID
                                   //+ '" >详细</a>'
                                   '<a herf="javascript:void(0)" class="easyui-linkbutton edit-dl-btn"  data-options="iconCls:\'icon-page_edit\' ,plain:true" data-id="' + rowData.LabChID
                                   + '" >修改</a>' +
                                   '<a herf="javascript:void(0)" class="easyui-linkbutton delete-dl-btn"  data-options="iconCls:\'icon-delete\',plain:true" data-id="' + rowData.LabChID
                                   + '" >删除</a>'
                                   return formatterhtml;
                               }
                           }

                ]], toolbar: [
                   {
                       text: "增加",
                       iconCls: "icon-add",
                       handler: function () { 
                           sr.dialog.form("EditLabChType.aspx",
                           { title: '增加', form: 'EditForm', modal: true, width: 400 },
                           function (data) {
                               if (data.status === 1) {
                                   $("#userOrder-dg").datagrid("reload");
                               }
                           });
                       }
                   }, "-"
                ],
                onLoadSuccess: function (data) {
                    $.parser.parse();
                    //查看预约详细
                    //$(".view-dl-btn").on("click", function () {
                    //    var $this = $(this);
                    //    sr.dialog.dialog({ title: "预约详细信息", modal: true, width: 800, href: "OrderDetail.aspx?id=" + $this.attr("data-id") });
                    //    $("#userOrder-dg").datagrid("clearSelections");
                    //});
                    //修改 
                    $(".edit-dl-btn").on("click", function () {
                        $this = $(this);
                        sr.dialog.form("EditLabChType.aspx",
                        { title: '修改', form: 'EditForm', modal: true, width: 400, id: $this.attr("data-id") },
                        function (data) {
                            if (data.status === 1) {
                                $("#userOrder-dg").datagrid("reload");
                            }
                        });
                    });
                    /*
                    ** 删除 
                    */
                    $(".delete-dl-btn").linkbutton().on("click", function () {
                        var $this = $(this);
                        $.messager.confirm("请确认", "您确定要删除？", function (b) {
                            if (b) {
                                $.post("DeleteLabChType.ashx", { id: parseInt($this.attr("data-id")) }, function (data) {
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
