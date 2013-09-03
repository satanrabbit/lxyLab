<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLabType.aspx.cs" Inherits="LxyLab.EditLabType" %>
 
<form id="EditForm" name="EditForm" action="SaveLabType.ashx" method="post" onkeypress="if(event.keyCode==13||event.which==13){return false;}" >
    <table>
        <tr><td>名称：</td>
            <td>
                <input type="hidden" name="LabTypeID" value="<%=lt.LabTypeID %>" />
                <input type="text" class="easyui-validatebox" name="LabTypeName" value="<%=lt.LabTypeName %>" />
            </td></tr>
    </table>
</form>