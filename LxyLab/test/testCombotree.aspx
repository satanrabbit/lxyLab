<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testCombotree.aspx.cs" Inherits="LxyLab.test.testCombotree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link  type="text/css" rel="stylesheet" href="../source/easyui/themes/bootstrap/easyui.css" />
    <link type="text/css" rel="stylesheet" href="../source/easyui/themes/icon.css" />
    <link type="text/css" rel="stylesheet" href="../source/Alice/one-full.css" />
    <link href="../source/qTip2/tips/jquery.qtip.nightly.min.css" rel="stylesheet" /> 
    <script type="text/javascript" src="../source/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../source/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../source/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script  type="text/javascript" src="../source/qTip2/tips/jquery.qtip.nightly.min.js"></script>     
    <script type="text/javascript" src="../source/easyui/easyui_ex.js"></script>
    <script type="text/javascript" src="../source/easyui/jquery_easyUI_dialog_form.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input id="labs" class="easyui-combotree" data-options="url: '../GetLabTree.ashx'" style="width:200px;" />
    </div>
    </form>
</body>
</html>
