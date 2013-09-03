<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInstrument.aspx.cs" Inherits="LxyLab.EditInstrument" %>
 
<form id="EditForm" name="EditForm" action="SaveInstrument.ashx" method="post" onkeypress="if(event.keyCode==13||event.which==13){return false;}" >
    <table style="width:98%;line-height:2em;">
        <tr><td style="width:65px;">仪器名称：</td>
            <td>
                <input type="hidden" name="InstrumentID" value="<%=inst.InstrumentID %>" />
                <input type="text" class="easyui-validatebox" name="InstrumentName" value="<%=inst.InstrumentName %>" />
            </td></tr>
          <tr><td>仪器数量：</td>
            <td> 
                <input type="text" class="easyui-numberbox" name="InstrumentAmount" value="<%=inst.InstrumentAmount %>" />
            </td></tr>
        <tr> <td>共用人数：</td>
            <td> 
                <input type="text" class="easyui-numberbox" name="InstrumentPer" value="<%=inst.InstrumentPer %>" />
            </td></tr>
        <tr> <td>仪器介绍：</td>
            <td> 
                 <textarea name="InstrumentIntro" style="width:98%;height:65px;"><%=inst.InstrumentIntro %></textarea>
            </td></tr>
    </table>
</form>