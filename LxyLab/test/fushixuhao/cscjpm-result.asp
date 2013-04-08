<%@LANGUAGE="VBSCRIPT" CODEPAGE="936"%><%Session.CodePage=936%> 
<!--#include file="conn1.asp"-->
<html>
<head>
<title>考生复试成绩查询</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"> 

</head> 
<body bgcolor="#FFFFFF" text="#000000" background="新建文件夹/再试/fushitongzhi1/images/bg.gif" topmargin="0">
    <%
if request.Form("kh4")="" then 
        %>
        <script  type="text/javascript">
            alert("请输入您要查询的考试编号！");
            window.history.go(-1);
        </script>
        <%
else
    num=request.Form("kh4")
   dim rsArt,sqlArt
		set rsArt=server.CreateObject("Adodb.Recordset")
			sqlArt="select * from articles where id = "&num&" "
			rsArt.open sqlArt,conn,1,3
end if
%>
  
<table width="750" border="0" align="center" cellpadding="0" cellspacing="0" style="BORDER-BOTTOM: #677aa2 1px solid; BORDER-LEFT: #677aa2 1px solid; BORDER-RIGHT: #677aa2 1px solid; BORDER-TOP: #677aa2 0px solid">
  <tr> 
    <td height="72"><img src="新建文件夹/再试/fushitongzhi1/images/yjsc1.gif" width="775" height="120"></td>
  </tr>
  <tr> 
    <td height="18"><img src="新建文件夹/再试/fushitongzhi1/images/fgx006.gif" width="775" height="8"></td>
  </tr>
  <tr> 
    <td height="144">
        <table width="750" border="0" align="center" cellspacing="0">
          <tr> 
            <td><strong><font color="#3300CC" size="2"><%=year(now)%>年硕士研究生复试资格查询</font></strong></td>
          </tr>
        </table>
        <p><font size="2"></font></p>
        <table  border="1" width="70%" cellpadding="1" cellspacing="0" bordercolordark="#FFFFFF" bordercolorlight="#999999" align="center">
          <tr bgcolor="#33CCFF"> 
            <td colspan="3" height="20"> <div align="center"><font color="#FF0000">复试资格查询</font></div></td>
          </tr>
          <tr>
            <td height="20" colspan="3"><div align="center">姓名： </div></td>
          </tr>
          <tr>
            <td height="20" colspan="3"><div align="center">考生编号：</div> </td>
          </tr>
<tr> 
            <td width="54%" height="20"> <div align="center">调剂专业：</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr> 
		  <tr> 
            <td width="54%" height="20"> <div align="center">学院：</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr>         
          <tr>
            <td height="20"><div align="center">复试序号:</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr>
<tr>
            <td height="20"><div align="center">调剂专业代码：</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr>
          <tr>
            <td height="20" colspan="3"><div align="left">
              <p>具有本次复试资格的考生请点击下载并<font color="#FF0000">按照查询信息填写</font>（下载后请将文件后缀名改为.doc）：</p>
              <p><a href="http://yjs.tust.edu.cn/down.asp?id=704">2012复试通知书.doc</a></p>
              <p><a href="http://yjs.tust.edu.cn/down.asp?id=705">思想政治考核表.doc</a></p>
              <p>不具有本次复试资格的考生请尽快自行联系调剂学校。</p>
              <p>&nbsp;</p>
              </div></td>
		  </tr>
      </table>
        <p align="center">&nbsp;</p>
        <p align="right"><font color="#FF0000" face="楷体_GB2312">天津科技大学研究生招生办公室&nbsp;&nbsp;&nbsp;</font></p>
       <p align="center">&nbsp; </p>      </td>
  </tr>
</table>
  


</body>
</html>
