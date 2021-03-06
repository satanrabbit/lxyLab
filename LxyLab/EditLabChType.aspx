﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLabChType.aspx.cs" Inherits="LxyLab.EditLabChType" %>

<form id="EditForm" name="EditForm" action="SaveLabChType.ashx" method="post" onkeypress="if(event.keyCode==13||event.which==13){return false;}" >
    <table>
        <tr><td>所属一级分类：</td>
            <td>
                <input type="text" class="easyui-combobox" data-options="valueField:'LabTypeID',textField:'LabTypeName',url:'GetTypeList.ashx',required:true" name="LabSupType" value="<%= lc.LabSupType.ToString()=="0"?"": lc.LabSupType.ToString()%>" />
            </td></tr>
        <tr><td>名称：</td>
            <td> 
                <input type="hidden" name="LabChID" value="<%= lc.LabChID.ToString()=="0"?"": lc.LabChID.ToString()%>" />
                <input type="text" class="easyui-validatebox"  data-options="required:true,missingMessage:'请填写名称！'"
                     name="LabChName" value="<%=lc.LabChName %>" />
            </td></tr>
    </table>
    <script type="text/javascript">
        $(function () {
            $.parser.parse();
        });
    </script>
</form>