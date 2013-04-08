<%
 Set conn=Server.CreateObject("ADODB.Connection")
	path=server.mappath("fenshu.mdb")
    connstr="provider=microsoft.jet.oledb.4.0; data source="&path&""
    conn.open connstr
 %>