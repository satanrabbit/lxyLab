<%@LANGUAGE="VBSCRIPT" CODEPAGE="936"%><%Session.CodePage=936%> 
<!--#include file="conn1.asp"-->
<html>
<head>
<title>�������Գɼ���ѯ</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"> 

</head> 
<body bgcolor="#FFFFFF" text="#000000" background="�½��ļ���/����/fushitongzhi1/images/bg.gif" topmargin="0">
    <%
if request.Form("kh4")="" then 
        %>
        <script  type="text/javascript">
            alert("��������Ҫ��ѯ�Ŀ��Ա�ţ�");
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
    <td height="72"><img src="�½��ļ���/����/fushitongzhi1/images/yjsc1.gif" width="775" height="120"></td>
  </tr>
  <tr> 
    <td height="18"><img src="�½��ļ���/����/fushitongzhi1/images/fgx006.gif" width="775" height="8"></td>
  </tr>
  <tr> 
    <td height="144">
        <table width="750" border="0" align="center" cellspacing="0">
          <tr> 
            <td><strong><font color="#3300CC" size="2"><%=year(now)%>��˶ʿ�о��������ʸ��ѯ</font></strong></td>
          </tr>
        </table>
        <p><font size="2"></font></p>
        <table  border="1" width="70%" cellpadding="1" cellspacing="0" bordercolordark="#FFFFFF" bordercolorlight="#999999" align="center">
          <tr bgcolor="#33CCFF"> 
            <td colspan="3" height="20"> <div align="center"><font color="#FF0000">�����ʸ��ѯ</font></div></td>
          </tr>
          <tr>
            <td height="20" colspan="3"><div align="center">������ </div></td>
          </tr>
          <tr>
            <td height="20" colspan="3"><div align="center">������ţ�</div> </td>
          </tr>
<tr> 
            <td width="54%" height="20"> <div align="center">����רҵ��</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr> 
		  <tr> 
            <td width="54%" height="20"> <div align="center">ѧԺ��</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr>         
          <tr>
            <td height="20"><div align="center">�������:</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr>
<tr>
            <td height="20"><div align="center">����רҵ���룺</div></td>
            <td height="20" colspan="2"><div align="center"> </div></td>
          </tr>
          <tr>
            <td height="20" colspan="3"><div align="left">
              <p>���б��θ����ʸ�Ŀ����������ز�<font color="#FF0000">���ղ�ѯ��Ϣ��д</font>�����غ��뽫�ļ���׺����Ϊ.doc����</p>
              <p><a href="http://yjs.tust.edu.cn/down.asp?id=704">2012����֪ͨ��.doc</a></p>
              <p><a href="http://yjs.tust.edu.cn/down.asp?id=705">˼�����ο��˱�.doc</a></p>
              <p>�����б��θ����ʸ�Ŀ����뾡��������ϵ����ѧУ��</p>
              <p>&nbsp;</p>
              </div></td>
		  </tr>
      </table>
        <p align="center">&nbsp;</p>
        <p align="right"><font color="#FF0000" face="����_GB2312">���Ƽ���ѧ�о��������칫��&nbsp;&nbsp;&nbsp;</font></p>
       <p align="center">&nbsp; </p>      </td>
  </tr>
</table>
  


</body>
</html>
