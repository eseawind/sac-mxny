<HTML>
  <HEAD>
  <%
'if session("LOGGEDIN")=false then
'   Response.Write "<script>alert('您没有登录或者会话已过期，请重新登录!!!')</script>"
'   response.end
'end if
%>
    <SCRIPT LANGUAGE="javascript"> 
	 //  self.moveTo(0,0)
	//   self.resizeTo(screen.availWidth,screen.availHeight)
    </script>

    <SCRIPT LANGUAGE = "VBScript">
      <!--
      Sub window_onLoad()
      'Pbd1.DisplayURL ="<%=request("PicName")%>"
      end sub
      -->
    </script>

    <META HTTP-EQUIV="Content-Type" content="text/html; charset=gb2312">
  </head>

  <body topmargin=0 leftmargin=0>
    <P>
	<object CLASSID="clsid:4F26B906-2854-11D1-9597-00A0C931BFC8" ID="Pbd1" WIDTH="100%" HEIGHT="100%" BORDER="0" VSPACE="0" HSPACE="0" codebase="..\include\ActiveView_3_0_15_3_.exe#version=3,0,15,2" >
	  <param name="ServerIniURL" value="C:\Program Files\PIPC\DAT\pilogin.ini">
	  <param name="DIsplayURL" value="http://localhost/PDI/扬州热力系统图6.pdi">
	</object>

    </p>


  </body>
</html>
