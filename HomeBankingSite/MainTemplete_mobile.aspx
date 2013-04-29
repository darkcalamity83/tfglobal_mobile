<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MainTemplete.aspx.vb" Inherits="HomeBankingSite.MainTemplete" %>
<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<!doctype html>
<!--[if lt IE 7]> <html class="ie6 oldie"> <![endif]-->
<!--[if IE 7]>    <html class="ie7 oldie"> <![endif]-->
<!--[if IE 8]>    <html class="ie8 oldie"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="">
<!--<![endif]-->
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Homebanking Main</title>
<link href="boilerplate.css" rel="stylesheet" type="text/css">
<link href="mobile-1.css" rel="stylesheet" type="text/css">
<link href="Styles.css" rel="stylesheet" type="text/css" media="all">
<!-- 
To learn more about the conditional comments around the html tags at the top of the file:
paulirish.com/2008/conditional-stylesheets-vs-css-hacks-answer-neither/

Do the following if you're using your customized build of modernizr (http://www.modernizr.com/):
* insert the link to your js here
* remove the link below to the html5shiv
* add the "no-js" class to the html tags at the top
* you can also remove the link to respond.min.js if you included the MQ Polyfill in your modernizr build 
-->
<!--[if lt IE 9]>
<script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<script src="respond.min.js"></script>
</head>
<body>
<div class="gridContainer clearfix">
  <div id="LayoutDiv1">
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
  </div>
</div>
</body>
</html>
