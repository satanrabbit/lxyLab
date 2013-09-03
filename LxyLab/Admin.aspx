<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="LxyLab.Admin" %>

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
    <style type="text/css"
        >
        .op-memu {
            text-align:center;
        }
            .op-memu p {
                margin:8px auto;
            }
    </style>
    
    <script type="text/javascript"> 
        
        $(function () {
            //增加tab页
            $(".menuLink").click(function () {
                var $this = $(this);
                var nd = { text: $this.text(), attributes: { url: $this.attr("href") } };
                $.addTab(nd, centerTabs);
                return false;
            });
            var centerTabs = $('#centerTabs').tabs({
                fit: true,
                border: false
            });
        });
        </script>
</head>
<body  class="easyui-layout"  >
		<div region="north"  style="height:60px;background-color:#f0f0f0;">
            <div style="position:relative; height:60px;">
                <div style="position:absolute; right:30px;bottom:5px;">
                      <p><a class="easyui-linkbutton" href="logout.ashx" data-options="iconCls:'icon-door_out'">退出</a></p>
                </div>
            </div>
            
		</div> 
		<div region="east" data-options="iconCls:'icon-reload',title:'关于',split:true,collapsed:true" style="width:180px;"></div>
		<div region="west" split="true" title="控制面板" style="width:190px;">
            <div id="p" class="easyui-panel"   style="  padding:10px;background:#fafafa;" data-options="noheader: true">
		        <p>管理员：夏千祥</p>
		        <p>身份：教师</p>
                <p>教工号：98812701</p> 
                <p>电&emsp;话：98812701</p> 
                <p>计算机科学与信息工程学院</p>
                <p><a class="easyui-linkbutton" id="UserEditInfoBtn" href="javascript:;" data-options="iconCls:'icon-user_edit'">修改资料</a></p>
	        </div>
            <div   class="easyui-panel" title="操作" style=" padding:10px;background:#fbfbfb;" data-options="iconCls:'icon-cog'">
		        <div class="op-memu" >
                    <p ><a class="easyui-linkbutton menuLink" href="ManageUser.aspx" data-options="iconCls:'icon-report_edit'" > 用户管理</a></p>       
                    <p ><a class="easyui-linkbutton menuLink"  href="UserOrder.aspx"  data-options="iconCls:'icon-report_edit'" >实验室预约管理</a></p>                    
                    <p ><a class="easyui-linkbutton menuLink" href="LabType.aspx"  data-options="iconCls:'icon-chart_bar'" >一级分类管理</a></p>           
                    <p ><a class="easyui-linkbutton menuLink" href="LabCHType.aspx"  data-options="iconCls:'icon-chart_bar'" >二级分类管理</a></p>
                    <p ><a class="easyui-linkbutton menuLink" href="Lab.aspx" data-options="iconCls:'icon-chart_bar'" >实验室信息管理</a></p>
                    <p ><a class="easyui-linkbutton menuLink" href="Instrument.aspx"  data-options="iconCls:'icon-note'">仪器信息管理</a></p>
		        </div>
                     
	        </div>
           <%-- <div  class="easyui-panel" title="实验室规则管理" style=" padding:10px;background:#fbfbfb;" data-options="iconCls:'icon-book'">
                <ul class="ui-list ui-list-gray">
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何申请认证？</a></li>
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何提现？</a></li> 
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何申请认q证？</a></li>
                </ul>--%>
	        </div>
		</div>
		<div region="center" >
            <div id="centerTabs">
                <div title="预约" style="padding:10px; ">                   
                     
		        </div>                
            </div>
		</div>
    
</body>
</html>
