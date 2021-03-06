﻿ 
var sr = $.extend({}, sr);/* 定义全局对象 */ 
sr.dialog = $.extend({}, sr.dialog);

sr.dialog.dialog = function (data) {
    $(".sr_dialog").dialog('destroy').remove();
    $("body").append('<div class="sr_dialog"></div>');
    $(".sr_dialog").dialog(data);
}
    sr.dialog.form = function (url, data, fn) {
        var u, d, f;
        if (arguments.length == 0) {
            $.messager.alert("错误", "错误:未填写发送请求地址！", "error");
        }
        else {

            if (arguments.length == 1) {
                u = url;
                d = {};
                f = function () { }
            }
            if (arguments.length == 2) {
                u = url;
                if ($.isFunction(data)) {
                    d = {};
                    f = data;
                } else {
                    d = data;
                    f = function () { }
                }
            }
            if (arguments.length == 3) {
                u = url;
                d = data;
                f = fn;
            }

            $(".sr_edit_dialog").dialog('destroy').remove();
            $("body").append('<div class="sr_edit_dialog"></div>');
            $.messager.progress({ msg: "正在加载......" });

            $('.sr_edit_dialog').load(u, d, function () { 
                $.messager.progress('close');
                var dg = $('.sr_edit_dialog');
                var fm;
                //if (d.title != null) {
                //    dg.dialog({ title: d.title });
                //}
                //表单
                if (d.form == null) {
                    fm = $('.sr_edit_dialog').find("form:first");
                } else {
                    fm = $('.sr_edit_dialog').find("form#" + d.form);
                }
                if (fm.length == 0) {
                    $.messager.alert("错误", "错误:<br /> 表单名-" + d.form + "不存在！", "error");
                    $(".sr_edit_dialog").dialog('destroy').remove();
                } else {
                   

                    d = $.extend(d, {
                       
                        buttons: [{
                            text: "保存",
                            iconCls: 'icon-disk',
                            handler: function () {
                                //提交表单
                                fm.form('submit', {
                                    url: fm.attr("action"),
                                    onSubmit: function () {
                                        return fm.form("validate")
                                    },
                                    success: function (data) {
                                        data = $.parseJSON(data);
                                        if (data.status === 1) {
                                            dg.dialog('destroy');
                                            $.messager.show({ title: '提示', msg: data.msg, timeout: 2000 });
                                        } else {
                                            $.messager.alert("错误", data.msg, "error");
                                        }
                                        //console.info(data);
                                        f(data);
                                    }
                                });
                            }
                        }, {
                            text: "取消",
                            iconCls: 'icon-cross',
                            handler: function () {
                                dg.dialog('destroy');
                            }
                        }]
                    });

                    dg.dialog(d);
                }
            });
        }
    }; 
