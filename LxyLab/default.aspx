<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="LxyLab._default" %>

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
    <script type="text/javascript" src="source/sea-modules/seajs/seajs/2.0.0/sea.js"></script>
    <script  type="text/javascript" src="source/qTip2/tips/jquery.qtip.nightly.min.js"></script>     
    <script type="text/javascript" src="source/easyui/easyui_ex.js"></script>
    <script type="text/javascript" src="source/easyui/jquery_easyUI_dialog_form.js"></script>
    <style >
        .book-table {

        }
        .book-table td div.class-cell{
            text-align:center;
            height:55px;
            margin:2px;

            width:95px;
            background-color:#eee;
            display:block;
            padding-top:5px;
        }
        a.book-red,a.book-blue,a.book-orenge,a.book-green,a.book-gray {
            padding-top:10px;
            display:block;
            width:80%;
            height:40px;
            margin:0px auto;
            color:#f0f0f0;
        }
        a.book-red {
            background-color: #f1684f;
        }
         a.book-red:hover {
            background-color: #ff2700;
        }
        a.book-blue {
            background-color: #6cbef8;
        }
        a.book-blue:hover {
            background-color: #0094ff;
        }
        a.book-orenge {
            background-color: #f2a357;
        }
        a.book-orenge:hover {
            background-color: #f66c0a;
        }
        a.book-green {
            background-color: #69b04b;
        } 
        a.book-green:hover {
            background-color: #38ad07;
        } 
        a.book-gray {
            background-color: #9d9a9a;
        } 
        a.book-gray:hover {
            background-color: #6d6a6a;
        }
        .op-memu {
            text-align:center;
        }
            .op-memu p {
                margin:8px auto;
            }
    </style>  
    <script type="text/javascript"> 
        $(function () {

            /*
             *  初始化本学期周
            */
            var weekJson = [];
            var currentWeek=1;
            $.post("GetTErms.ashx", function (data) {
                data = $.parseJSON(data);
                $.each(data, function (i, item) {
                    if (item.isCurrent == "True") {
                        //当前学期
                        //判断当前周次
                        var _dt1 = new Date();
                        var _dt2 = new Date(item.TermStartDay);
                        var dt1 = _dt1.getTime();
                        var dt2 = _dt2.getTime();
                        currentWeek = parseInt((Math.abs(dt1 - dt2) / 1000 / 60 / 60 / 24 / 7) + 1); 
                        //循环直总周数
                        for (var i = 1; i <= parseInt(item.TermWeeks) ; i++) {
                            var week = {};
                            week["id"] = i;
                            week["text"] = "第" + i + "周";
                            weekJson.push(week);
                        } 
                    }
                });
                //教学周选择 
                $('#weeks').combotree({
                    value:currentWeek,
                    data: weekJson,
                    onBeforeSelect: function (node) {
                        if (!$(this).tree('isLeaf', node.target)) {
                            return false;
                        }
                    } 
                    , onSelect: function () {
                        //异步加载数据
                    }
                });
            });
            //提示
            $('.book-red').each(function () { // Notice the .each() loop, discussed below
                $(this).qtip({
                    content: {
                        text: function () {
                            return "name";
                            },
                        title: {
                            text: "不可预约" 
                            //,button: true
                        }
                    },  
                    hide: {  
                        //event: 'unfocus'  ,
                        //inactive: 3000  ,
                        delay: 500  ,
                        fixed: true  
                    }  ,
                    position: {
                        my: "bottom right ", // Use the corner...
                        at: "top center" // ...and opposite corner
                    }, 
                    style: {
                        classes: 'qtip-shadow  qtip-red'
                    },
                    events: {
                        show: function (event, api) {   },
                        render: function (event, api) {
                            
                            // Grab the tooltip element from the API
                            var tooltip = api.elements.tooltip

                            // ...and here's the extra event binds
                            tooltip.bind('tooltipshow', function (event, api) {
                                $('.selector').removeClass('active');
                            })
                        }
                    }
                });
            });
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
            //实验室选择
            $('#labs').combotree({
                url: 'GetLabTree.ashx',
                onBeforeSelect: function (node) {
                    if (!$(this).tree('isLeaf', node.target)) {
                        $(this).tree('toggle', node.target);//node.toggle();
                        return false;
                    }  
                },
                onClick: function (node) {
                    if (!$(this).tree('isLeaf', node.target)) {
                        $('#labs').combo('showPanel');
                    }
                }
                , onSelect: function () {
                    //异步加载数据
                }
            });
            //初始化选择界面
            function initBook(lab,wk){
                $.post("GetBookInfo.ashx", { labID: lab, week: wk }, function (data) {
                    data = $.parseJSON(data);

                });
            }
            //预约界面
            $(".book-blue").click(function () {
                $.sr_edit_dialog("BookLab.aspx", { title: '预约', form: 'form1', maximizable: true, modal: true, resizable:true,maximized: false, width: 400 }, function (data) {
                    $.messager.show("OK");
                });
            });

        });
       
    </script> 
</head>
<body  class="easyui-layout"  >
		<div region="north"  style="height:70px;background-color:#f0f0f0;">
            <div style="position:relative; height:70px;">
                <div style="position:absolute; right:30px;bottom:5px;">
                      <p><a class="easyui-linkbutton" href="#" data-options="iconCls:'icon-door_out'">退出</a></p>
                </div>
            </div>
            
		</div>
		<div region="south"  style="height:40px;" ></div>
		<div region="east" data-options="iconCls:'icon-reload',title:'关于',split:true,collapsed:true" style="width:180px;"></div>
		<div region="west" split="true" title="控制面板" style="width:190px;">
            <div id="p" class="easyui-panel"   style="  padding:10px;background:#fafafa;" data-options="noheader: true">
		        <p>用户：夏千祥</p>
		        <p>身份：教师</p>
                <p>教工号：98812701</p> 
                <p>电&emsp;话：98812701</p> 
                <p>计算机科学与信息工程学院</p>
                <p><a class="easyui-linkbutton menuLink" href="#" data-options="iconCls:'icon-user_edit'">修改资料</a></p>
	        </div>
            <div   class="easyui-panel" title="操作" style=" padding:10px;background:#fbfbfb;" data-options="iconCls:'icon-cog'">
		        <div class="op-memu" >
                    <p ><a class="easyui-linkbutton menuLink" data-options="iconCls:'icon-report_edit'" >预约</a></p>
                    <p ><a class="easyui-linkbutton menuLink"  data-options="iconCls:'icon-note'">我的预约</a></p>
                    <p ><a class="easyui-linkbutton menuLink"  data-options="iconCls:'icon-chart_bar'" >实验室信息</a></p>
		        </div>
                     
	        </div>
            <div  class="easyui-panel" title="实验室规则" style=" padding:10px;background:#fbfbfb;" data-options="iconCls:'icon-book'">
                <ul class="ui-list ui-list-gray">
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何申请认证？</a></li>
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何提现？</a></li> 
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何申请认q证？</a></li>
                </ul>
	        </div>
		</div>
		<div region="center" >
            <div id="centerTabs">
                <div title="预约" style="padding:10px; ">                   
                    <table width="80%" border="0" cellspacing="2" class="book-table">
                      <caption>
                        实验室预约
                      </caption>
                      <tr>
                        <th colspan="8" scope="col"  style="text-align:center;">
                            <div style="margin:20px;">实验室： 
                            <input id="labs" value="<%= defaultLab %>" style="width:200px;" />
                            &emsp;&emsp;教学周：<input id="weeks" style="width:100px;" />
                            </div>
                            </th>
                      </tr>
                      <tr>
                        <td><div class="class-cell" align="center"></div></td>
                        <td><div class="class-cell" align="center">周一</div></td>
                        <td><div class="class-cell"  align="center">周二</div></td>
                        <td><div class="class-cell"  align="center">周三</div></td>
                        <td><div class="class-cell"  align="center">周四</div></td>
                        <td><div class="class-cell"  align="center">周五</div></td>
                        <td><div class="class-cell"  align="center">周六</div></td>
                        <td><div class="class-cell"  align="center">周日</div></td>
                      </tr>
                       <tr cls="1">
                        <td><div class="class-cell"  align="center">第一节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="7">预约</a></div></td>
                      </tr>
                      <tr cls="2">
                        <td><div class="class-cell"  align="center">第二节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="7">预约</a></div></td>
                      </tr>
                      <tr>
                        <td colspan="8"><div align="center" style="height:20px; background-color:#f0f0f0; margin:5px 50px;">中午</div> </td>
                      </tr>
                      <tr cls="3">
                        <td><div class="class-cell"  align="center">第三节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="7">预约</a></div></td>
                      </tr>
                      <tr cls="4">
                        <td><div class="class-cell"  align="center">第四节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="7">预约</a></div></td>
                      </tr>
                         <tr>
                        <td colspan="8"><div align="center" style="height:20px; background-color:#f0f0f0;margin:5px 50px;">晚间</div> </td>
                      </tr>
                      <tr cls="5">
                        <td><div class="class-cell"  align="center">第五节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="7">预约</a></div></td>
                      </tr>
                        <tr cls="6">
                        <td><div class="class-cell"  align="center">第六节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="1" >已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="2" >已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="3" >已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="4">已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="5">已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="6">不开放</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" wd="7">不开放</a></div></td>
                      </tr>
                    </table>
		        </div>                
            </div>
		</div>
    
</body>
</html>
