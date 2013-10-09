<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabDetail.aspx.cs" Inherits="LxyLab.LabDetail" %>
<div>
    <table style="text-align:center;line-height:2em;">
        <tr>
            <td colspan="2"><%=lab.LabName %></td>
        </tr>
         <tr>
             <td>地址：</td>
            <td><%=lab.LabAddr %></td>
        </tr>
         <tr>
             <td>所属：</td>
            <td><%=lab.LabTypeName %>--<%=lab.LabChName %></td>
        </tr>
         <tr>
             <td>容纳人数：</td>
            <td><%=lab.LabAmount %></td>
        </tr>
         <tr>
             <td>简介：</td>
            <td><%=lab.LabInfo %></td>
        </tr>
    </table>
</div>