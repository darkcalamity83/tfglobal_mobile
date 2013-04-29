<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MainTemplete.aspx.vb" Inherits="HomeBankingSite.MainTemplete" %>
<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <meta name="viewport" content="initial-scale=1, maximum-scale=1.0, width=device-width, user-scalable=0"/>
    <meta http-equiv="Content-Type" content="application/vnd.wap.xhtml+xml; charset=utf-8" />
        <meta name="HandheldFriendly" content="true" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>Homebanking Main </title>
    
   <!-- <script type="text/javascript">
        function SetSize() {
            //window.moveTo(0, 0);
            //window.resizeTo(screen.availWidth, screen.availHeight);
   var myWidth = 0, myHeight = 0, frameHeight = 0, frameWidth = 0;
  if( typeof( window.innerWidth ) == 'number' ) {
      //Non-IE
     myWidth = window.innerWidth;
    myHeight = window.innerHeight;
  } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
    //IE 6+ in 'standards compliant mode'
    myWidth = document.documentElement.clientWidth;
    myHeight = document.documentElement.clientHeight;
  } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
    //IE 4 compatible
    myWidth = document.body.clientWidth;
    myHeight = document.body.clientHeight;
  }
  frameHeight = myHeight - 125;
  frameWidth = myWidth - 25;
  //window.alert( 'FrameHeight = ' + frameHeight );
  //window.alert(document.getElementById('hbkFrame').getAttribute('height'))
  document.getElementById('hbkFrame').setAttribute('height', frameHeight + ' px');
  document.getElementById('hbkFrame').setAttribute('width', frameWidth + ' px');
}
    </script>-->

    <style type="text/css">
        .WinBar
        {
            background: #A0BFED url(Imgs/WinBar.gif) repeat-x top left;
            color: White;
            padding: 1px;
            padding-left: 0px;
            font-family: Arial;
            font-size: smaller;
            border-bottom: solid 1px #000;
        }
        .HeaderBar
        {
            background: #B10000 url(Imgs/WinBar.gif) repeat-x top left;
            color: White;
            padding: 1px;
            padding-left: 5px;
            font-family: Arial;
            font-size: smaller;
            border-bottom: solid 1px #000;
        }
        BODY
        {
            font-family: Arial;
            width: 100%;
        }
        h5:Hover
        {
            color: #FA4802;
        }
    </style>
    <!--<script type="text/javascript">

        window.onresize = resize;

        function resize() {
            SetSize();
        }
</script>-->

</head>
<!--<body onload="SetSize()" style="height:100%;width:100%;margin-left:0;margin-top:0;margin-right:0;margin-bottom:0;">-->
<body style="height:100%;width:100%;margin-left:0;margin-top:0;margin-right:0;margin-bottom:0;">
    <form id="form1" runat="server">
	<table cellpadding="0"  style="width:100%; height: 100%">
    	<tr>
    		<td class="HeaderBar" style="height:50px;">
    		<vs:Render ID="CU" runat="server" />
                <asp:Image ID="Image3" runat="server" Height="50px" 
                    ImageUrl="~/Imgs/Logo.bmp" Width="127px" />
                <asp:Image ID="Image4" runat="server" Height="50px" 
                    ImageUrl="~/Imgs/HbkTxt.gif" Width="167px" />
            </td>
    	</tr>
    	<!--<tr align="center" >-->
        <tr>
			<td>
                <iframe id="hbkFrame" src="LoginWebSafe.aspx" style="width:100%; height:400px" ></iframe>
                <!--<iframe id="Iframe1" width="100%" height="400px" src="LoginWebSafe.aspx" ></iframe>-->
			</td>
      	</tr>
        <tr>
        	<td>
				<div class="WinBar" style="height:20px;">
        			<label>Copyright 2010, Terra Firma Information Technology, LLC</label>
    			</div>            
        	</td>
     	</tr>
    </table>
    </form>
</body>
</html>