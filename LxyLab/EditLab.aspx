<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLab.aspx.cs" Inherits="LxyLab.EditLab" %>

<form id="EditForm" name="EditForm" action="SaveLab.ashx" method="post" onkeypress="if(event.keyCode==13||event.which==13){return false;}" >
    <table>

         <tr>
             <td>名称 
                <input type="hidden" name="LabID" value="<%=lab.LabID %>" /></td>
            <td ><input type="text" name="LabName" value="<%=lab.LabName %>" class="easyui-validatebox"  data-options="required:true"/></td>
        </tr> 


        <tr><td>所属一级分类：</td>
            <td>
                <input type="text" class="easyui-combobox" data-options="valueField:'LabTypeID',textField:'LabTypeName',url:'GetTypeList.ashx',required:true" name="LabSupType" value="<%=lab.LabSupType %>" />
            </td></tr>
        <tr>
             <tr><td>所属二级分类：</td>
            <td>
                <input type="text" class="easyui-combobox" data-options="valueField:'LabChID',textField:'LabChName',url:'GetChTypeList.ashx',required:true" name="LabType" value="<%=lab.LabType %>" />
            </td></tr>
        <tr>
            <td>地址：</td>
            <td>
                <input type="text" class="easyui-validatebox" name="LabAddr" value="<%=lab.LabAddr %>" />
            </td></tr> <tr>
            <td>容纳人数：</td>
            <td>
                <input type="text" class="easyui-numberbox" data-options="required:true" name="LabAmount" value="<%=lab.LabAmount %>" />
            </td></tr><tr>
            <td>简介：</td>
            <td>
                 <textarea name="LabInfo" style="width:98%;height:48px;"><%=lab.LabInfo %></textarea>
            </td></tr>
    </table>
    <script type="text/javascript">
        $(function () {
            $.parser.parse();
        });
    </script>
</form>