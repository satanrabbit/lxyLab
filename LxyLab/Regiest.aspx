<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regiest.aspx.cs" Inherits="LxyLab.Regiest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link  type="text/css" rel="stylesheet" href="source/easyui/themes/bootstrap/easyui.css" />
    <link type="text/css" rel="stylesheet" href="source/easyui/themes/icon.css" />
    <link type="text/css" rel="stylesheet" href="source/Alice/one-full.css" />
    <script type="text/javascript" src="source/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="source/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="source/easyui/locale/easyui-lang-zh_CN.js"></script>    
    <script type="text/javascript" src="source/easyui/easyui_ex.js"></script>
</head>
<body >   
    <div  style="background-color:#f0f0f0; height:80px;  ">
        <div  style="  width:990px;   margin:0 auto;">
           <div class="ui-grid-row" >
                <div class="ui-grid-7" ></div>
                <div class="ui-grid-18" ><a style="font-size:24px;  color:#0094ff;" href="#" >天津科技大学-理学院-实验室预约系统</a></div>
            </div>
        </div>
    </div> 
    <div style="width:990px; margin:10px auto;" class="fn-clear" >
        <div  style="width:600px;" class="fn-left">
            <div class="ui-box">
                <div class="ui-box-head">
                    <div class="ui-box-head-border">
                        <h3 class="ui-box-head-title">注册</h3>
                    </div>
                </div>
                <div class="ui-box-container">
                    <div class="ui-box-content">
                        <form class="ui-form" name="regiestForm" method="post" action="Regiest.aspx" id="ff" runat="server">
                            <fieldset>
                                <legend>表单标题</legend> 
                                <div class="ui-form-item">
                                    <label for="" class="ui-label"> 电子邮箱
                                    </label>
                                    <input class="ui-input easyui-validatebox"  type="text" name="userAccount" data-options="required:true,validType:'email'"  /> 
                                    
                                </div>
                                 <div class="ui-form-item">
                                    <label for="" class="ui-label">身份</label>
                                    <select id="userIdentity" name="userIdentity"> 
                                        <option value="1">
                                        学生
                                        </option> 
                                        <option value="0">
                                        教师
                                        </option>
                                    </select>
                                    <p class="ui-form-other"><a href="javascript:void(0);">非本校师生请联系实验室，Tel：60601089</a></p>
                                </div>
                                <div class="ui-form-item ">
                                    <label for="" class="ui-label">教工卡号</label>
                                    <input class="ui-input easyui-validatebox" type="text" name="userNumber" data-options="required:true,validType:'equalLength[8]'"/>  
                                    
                                </div>
                                <div class="ui-form-item  ">
                                    <label for="" class="ui-label">姓名</label>
                                    <input class="ui-input easyui-validatebox" type="text" name="userName" data-options="required:true" />
                                    
                                </div>
                                <div class="ui-form-item  ">
                                    <label for="" class="ui-label">学院或部门</label>
                                    <input class="ui-input easyui-validatebox" type="text" name="userCollege"  data-options="required:true" />
                                    
                                </div>
                                 <div class="ui-form-item  ">
                                    <label for="" class="ui-label">联系电话</label>
                                    <input class="ui-input easyui-validatebox" type="text" name="userTel"  data-options="required:true"/>
                                    
                                </div>
                                <div class="ui-form-item">
                                    <label for="" class="ui-label">密码</label>
                                    <input class="ui-input easyui-validatebox " type="password" name="userPwd" id="userPwd"  data-options="required:true,validType:'length[6,15]',invalidMessage:'请输入6-15位的密码'"  />
                                </div>
                                 <div class="ui-form-item">
                                    <label for="" class="ui-label">确认密码</label>
                                    <input class="ui-input  easyui-validatebox" type="password"  name="userPwdCpmpare"  id="userPwdCompare" data-options="required:true,validType:'eqPassword[userPwd]'"  />
                                </div>
                                <!--div class="ui-form-item">
                                    <label for="" class="ui-label">验证码</label>
                                    <input class="ui-input ui-input-checkcode easyui-validatebox" type="text" data-explain="请输入右图中字符，不区分大小写。" data-options="required:true ,invalidMessage:'验证码错误'" maxlength="4" autocomplete="off" value="" name="fourcode">
                                    <img class="ui-checkcode-imgcode-img" align="absMiddle" alt="请输入您看到的内容" src="https://omeo.alipay.com/service/checkcode?sessionID=82012ab6b1f4ed9e675fb9092a25cb3b&r=0.9321065918075809" title="点击刷新图片校验码">
                                    <a href="#">看不清，换一张</a>
                                    <div class="ui-form-explain">请输入右图中字符，不区分大小写。</div>
                                </div !--> 
                                <div class="ui-form-item">
                                    <input type="submit" id="submit_form" class="ui-button ui-button-mblue" value="注册" />
                                    <input type="button" class="ui-button ui-button-mwhite" value="取消">
                                </div>   
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div  style="width:360px;" class="fn-right">
            <div class="ui-box" > 
                <div class="ui-box-container">
                    <div class="ui-box-content">
                        <div class="ui-tipbox ui-tipbox-message">
                            <div class="ui-tipbox-icon">
                                <i class="iconfont" title="提示"></i>
                            </div>
                            <div class="ui-tipbox-content">
                                <h3 class="ui-tipbox-title">提示标题</h3>
                                <p class="ui-tipbox-explain">完成的说明完成的说明。</p>
                                <p class="ui-tipbox-explain"><a href="#">查看实验室规范守则</a> | <a href="#">其他资料</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            //调用表单插件的'submit'方法提交
            $("#submit_form").click(function () {
                $('#ff').form('submit', {
                    url: $(this).attr('action'),
                    onSubmit: function () { 
                        return $(this).form('validate');
                    },
                    success: function (data) { 
                        data = $.parseJSON(data);
                        console.info(data);
                        if (data.status === 0) {
                            //注册成功，跳转至用户首页
                            $.messager.show({ title: "注册成功", msg: data.msg, timeout: 1000 });
                            $("#submit_form").fadeTo(1000, 0.25, function () {
                                window.location.href = "Default.aspx";
                            });
                           
                        } else {
                            if (data.status === 1) {
                                $.messager.alert("注意", data.msg, "info");
                            }else {
                                $.messager.alert("错误", data.msg, "error");
                            }
                        }
                    }
                });
                return false;
            });
        });
    </script>
</body>
</html>