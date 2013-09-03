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
    <script  type="text/javascript" src="source/qTip2/tips/jquery.qtip.nightly.min.js"></script>     
    <script type="text/javascript" src="source/easyui/easyui_ex.js"></script>
    <script type="text/javascript" src="source/easyui/jquery_easyUI_dialog_form.js"></script> 
    <script src="Scripts/json2.js"></script>
  
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
            var currentWeek=<%=currentWeek%> ;
            var isInitLab=false;
            var isInitWeeks=false;
            //循环直总周数
            for (var i = 1; i <=<%=weeks%> ; i++) {
                var week = {};
                week["id"] = i;
                week["text"] = "第" + i + "周";
                weekJson.push(week);
            } 
                //教学周选择 
                
                $('#weeks').combotree({
                    data: weekJson,
                    value: currentWeek,
                    onBeforeSelect: function (node) {
                        isInitWeeks=true;
                        if (!$(this).tree('isLeaf', node.target)) {
                            return false;
                        }
                    }
                    , onSelect: function (record) {
                        //异步加载数据
                        if(isInitLab){
                            var slab = $('#labs').combotree("getValue");
                            initBook(slab, record.id);
                        }
                    }
                });
            //提示
            function initTip() {
                $('.book-red').each(function () { // Notice the .each() loop, discussed below
                   var  $this=$(this);
                    $this.qtip({
                        content: {
                            text: function () {
                                var orders=JSON.parse( $this.attr('data-order')); 
                                return "共"+orders.rows.length+"组"+orders.total+"人预约";
                            },
                            title: {
                                text: "预约已满"
                                //,button: true
                            }
                        },
                        hide: {
                            //event: 'unfocus'  ,
                            //inactive: 3000  ,
                            delay: 500,
                            fixed: true
                        },
                        position: {
                            my: "bottom right ", // Use the corner...
                            at: "top center" // ...and opposite corner
                        },
                        style: {
                            classes: 'qtip-red',
                            width: 150 
                        },
                        events: {
                            show: function (event, api) { },
                            render: function (event, api) { 
                            }
                        }
                    });
                });
                $('.book-blue').each(function () { // Notice the .each() loop, discussed below
                   var  $this=$(this);
                    $this.qtip({
                        content: {
                            text: function () {
                                var orders=JSON.parse( $this.attr('data-order')); 
                                return "共"+orders.rows.length+"组"+orders.total+"人预约";
                            },
                            title: {
                                text: "可预约" 
                            }
                        },
                        hide: { 
                            delay: 500,
                            fixed: true
                        },
                        position: {
                            my: "bottom right ", // Use the corner...
                            at: "top center" // ...and opposite corner
                        },
                        style: {
                            classes: 'qtip-blue',
                            width: 150 
                        },
                        events: {
                            show: function (event, api) { },
                            render: function (event, api) { 
                            }
                        }
                    });
                });
            }
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
            //实验室选择 value
            $('#labs').combotree({
                url: 'GetLabTree.ashx',value:<%= defaultLab%>,
                onBeforeSelect: function (node) {
                    isInitLab=true;
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
                , onSelect: function (record) {
                    //console.info(record);
                    if(isInitWeeks){
                        var sweek = $('#weeks').combotree("getValue");
                        initBook(record.id, sweek);
                        //异步加载数据
                    }
                }
            });
            initBook($('#labs').combotree('getValue'), $('#weeks').combotree('getValue'));
            //初始化选择界面
            function initBook(lab, wk) {
                $.messager.progress({text:'正在加载实验室信息'});
                
                //初始化预约数据
                $(".book-blue,.book-red").unbind();
                $(".book-red").removeClass('book-red').addClass('book-blue').text("预约");  
                
                $(".book-blue").attr('data-order', '{"total":"0","rows":[]}');
                
                $.post("GetLabInfo.ashx", { lid: lab }, function (lab_data) {

                    lab_data = $.parseJSON(lab_data);
                    //console.info(lab_data);
                    var max = lab_data.LabAmount;
                    //显示实验室信息
                    //*********************************
                    //初始化界面
                    $.messager.progress({text:'正在加载预约信息'});
                    $.post("GetBookInfo.ashx", { labID: lab, week: wk }, function (data) {
                        data = $.parseJSON(data);
                        $("#total_tip").empty().html("共" + data.total + "条预约");
                        $.each(data.rows, function (i, item) {
                            var $target = $("[data-cls=" + item.OrderCls + "]").find("[data-wd=" + item.OrderWeekday + "]");
                            var data_str = $target.attr("data-order");
                            var data_json = JSON.parse(data_str);
                            data_json.total= parseInt(data_json.total)+parseInt(item.OrderAmount);
                            var row={};
                            row.title=item.OrderTitle;
                            row.status=item.OrderStatus;
                            row.user=item.OrderUser;
                            row.amount=item.OrderAmount;
                            data_json.rows.push(row); 
                            data_str=JSON.stringify(data_json); 
                            $target.attr("data-order",data_str);
                            if(data_json.total>=max){
                                $target.removeClass('book-blue').addClass('book-red').text("预约已满");                            
                            }
                        });
                        initTip();
                        
                        //单击预约
                        if(wk<currentWeek){
                            $(".book-blue").click(function(){return false;}).removeClass('book-blue').addClass('book-red').text("不可预约");
                        }else{
                            $(".book-blue").click(function () {
                                $this=$(this);
                                //节次
                                var cls=$this.parent().parent().parent("tr").attr("data-cls");
                                //周几
                                var wd=$this.attr("data-wd");
                                $.sr_edit_dialog("BookLab.aspx", 
                                    { title: '预约', form: 'EditForm',
                                        maximizable: true, modal: true, resizable: true, maximized: false, width: 500,
                                        week:$('#weeks').combotree("getValue"),cls:cls, wd:wd,lab:$('#labs').combotree("getValue")}, 
                                    function (save_data) {
                                        save_data=$.parseJSON(save_data);
                                        if(save_data.status===1){
                                            initBook($('#labs').combotree("getValue"), $('#weeks').combotree("getValue"));
                                            $.messager.confirm("预约成功","您已经成功预约实验室，是否需要预约相关仪器？",function(b){
                                                if(b){
                                                    BookInst(save_data.id);
                                                }
                                            });
                                        }else{
                                            $.messager.alert("错误",save_data.msg,"error");
                                        }
                                    });
                            });
                        }
                        $.messager.progress('close');
                    });
                });
            }

            function BookInst(id){
                $.sr_edit_dialog("BookInstrument.aspx", 
                               { title: '预约仪器', form: 'EditForm',
                                   maximizable: true, modal: true, resizable: true, maximized: false, width: 400,
                                   id:id}, 
                               function (data) {
                                   data=$.parseJSON(data);
                                   if(data.status===1){                                     
                                       $.messager.confirm("预约成功","继续预约？",function(b){
                                           if(b){
                                               BookInst(id)
                                           }
                                       });
                                   }else{
                                       $.messager.alert("错误",data.msg,"error");
                                   }
                               });
            }

            //修改信息
            $("#UserEditInfoBtn").click(function(){ 
                $(".user-edit-info-dialog").dialog('destroy');
                $('body').append('<div class="user-edit-info-dialog"></div>');
                $(".user-edit-info-dialog").load('UserEditInfo.aspx',function(){
                    $('.user-edit-info-dialog').dialog({title:'修改信息',width:580,modal:true,
                        buttons:[{text:'关闭',iconCls:'icon-ok' ,
                            handler:function(){
                                $(".user-edit-info-dialog").dialog('destroy');
                            }
                        }]
                       });
                });
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
		        <p>用户：<%=us.UserName %></p>
		        <p>身份：<%=(us.UserIdentity==1?"教师":"学生") %></p>
                <p>卡号：<%=us.UserNumber %></p> 
                <p>电&emsp;话：<%=us.UserTel %></p> 
                <p><%=us.UserCollege %></p>
                <p><a class="easyui-linkbutton" id="UserEditInfoBtn" href="javascript:;" data-options="iconCls:'icon-user_edit'">修改资料</a></p>
	        </div>
            <div   class="easyui-panel" title="操作" style=" padding:10px;background:#fbfbfb;" data-options="iconCls:'icon-cog'">
		        <div class="op-memu" >
                    <p ><a class="easyui-linkbutton menuLink" data-options="iconCls:'icon-report_edit'" >预约</a></p>
                    
                    <p ><a class="easyui-linkbutton menuLink" href="UserOrder.aspx"  data-options="iconCls:'icon-note'">我的预约</a></p>
                    <p ><a class="easyui-linkbutton menuLink"  href="Lab.aspx" data-options="iconCls:'icon-chart_bar'" >实验室信息</a></p>
		        </div>
                     
	        </div>
           <%-- <div  class="easyui-panel" title="实验室规则" style=" padding:10px;background:#fbfbfb;" data-options="iconCls:'icon-book'">
                <ul class="ui-list ui-list-gray">
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何申请认证？</a></li>
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何提现？</a></li> 
                    <li class="ui-list-item"><a href="#"  class="menuLink" >如何申请认q证？</a></li>
                </ul>
	        </div>--%>
		</div>
		<div region="center" >
            <div id="centerTabs">
                <div title="预约" style="padding:10px; ">                   
                    <table width="80%" border="0" cellspacing="2" class="book-table">
                      <tr>
                        <th colspan="8" scope="col"  style="text-align:center;">
                            <span style="margin:20px;">实验室： 
                            <input id="labs"  />
                            &emsp;&emsp;教学周：<input id="weeks" style="width:100px;" />
                            </span>
                            </th>
                      </tr>
                      <tr>
                        <td><div class="class-cell" align="center" id="total_tip"></div></td>
                        <td><div class="class-cell" align="center">周一</div></td>
                        <td><div class="class-cell"  align="center">周二</div></td>
                        <td><div class="class-cell"  align="center">周三</div></td>
                        <td><div class="class-cell"  align="center">周四</div></td>
                        <td><div class="class-cell"  align="center">周五</div></td>
                        <td><div class="class-cell"  align="center">周六</div></td>
                        <td><div class="class-cell"  align="center">周日</div></td>
                      </tr>
                       <tr data-cls="1">
                        <td><div class="class-cell"  align="center">第一节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;"  data-wd="7">预约</a></div></td>
                      </tr>
                      <tr data-cls="2">
                        <td><div class="class-cell"  align="center">第二节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="7">预约</a></div></td>
                      </tr>
                      <tr>
                        <td colspan="8"><div align="center" style="height:20px; background-color:#f0f0f0; margin:5px 50px;">中午</div> </td>
                      </tr>
                      <tr data-cls="3">
                        <td><div class="class-cell"  align="center">第三节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="7">预约</a></div></td>
                      </tr>
                      <tr data-cls="4">
                        <td><div class="class-cell"  align="center">第四节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="7">预约</a></div></td>
                      </tr>
                         <tr>
                        <td colspan="8"><div align="center" style="height:20px; background-color:#f0f0f0;margin:5px 50px;">晚间</div> </td>
                      </tr>
                      <tr data-cls="5">
                        <td><div class="class-cell"  align="center">第五节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="1" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="2" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="3" >预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="4">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="5">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="6">预约</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="7">预约</a></div></td>
                      </tr>
                        <tr data-cls="6">
                        <td><div class="class-cell"  align="center">第六节</div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="1" >已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="2" >已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="3" >已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="4">已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="5">已满</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="6">不开放</a></div></td>
                        <td><div class="class-cell"  align="center"><a class="book-blue" href="javascript:;" data-wd="7">不开放</a></div></td>
                      </tr>
                    </table>
		        </div>                
            </div>
		</div>
    
</body>
</html>
