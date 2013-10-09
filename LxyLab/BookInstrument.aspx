<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookInstrument.aspx.cs" Inherits="LxyLab.BookInstrument" %>

   
<div style="width:380px;margin:5px auto;">
    <table style="line-height:2.5em;">
        <tr>
            <td style="width:300px;">
                <form action="SaveInstOrder.ashx" method="post" name="EditForm" id="EditForm"> 
                <table><tr>
                        <td style="width:80px;">预约仪器：<input type="hidden" value="<%=ino.InstOrderLab %>" name="InstOrderLab" id="InstOrderLab" /></td>
                        <td> 
                            <input type="text" name="InstOrderIns" id="InstOrderIns" value="<%=ino.InstOrderIns %>"  />
                            <input type="hidden" name="InstOrderID" value="<%=ino.InstOrderID %>"
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width:80px;">预约数量：</td>

                        <td>
                          <input type="text" name="InstOrderAmount" class="easyui-numberbox" data-options="required:true" id="InstOrderAmount" />
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="width:80px;">仪器数量：</td>
                        <td style="text-align:left;">共<i id="InstAmount"></i>台（件），当前<i id="usableAmount"></i></td>
                    </tr>--%>

                </table></form>
            </td>
           
        </tr>
    </table>
    </div>
     <script type="text/javascript">
         $(function () {
             $.parser.parse();
             $("#InstOrderIns").combobox({
                 valueField: 'InstrumentID',required:true,
                 textField: 'InstrumentName',
                 url: 'GetInstrumentsForCombo.ashx'
                
                 //GetInstrumentsForCombo
             });
         });
    </script>