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
<title>Mobile Banking Home</title>
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
 <style type="text/css">
        .WinBar
        {
            background: #A0BFED url(Imgs/WinBar.gif) repeat-x top left;
            color: White;
            padding: 0px;
            padding-left: 0px;
            font-family: Arial;
            font-size: medium;
            border-bottom: solid 1px #000;
        }
        .HeaderBar
        {
            background: white url(Imgs/WinBar.gif) repeat-x top left;
            color: White;
            padding: 1px;
            /*padding-left: 1px;*/
            font-family: Arial;
            font-size: smaller;
            border-bottom: solid 1px #000;
        }
        BODY
        {
            font-family: Arial;
            width: 100%;
            height: 100%;
        }
        h5:Hover
        {
            color: #FA4802;
        }
    </style>
<script src="respond.min.js"></script>
    <script type="text/javascript">
<!--//
   function sizeFrame() {
        var F = document.getElementById("hbkFrame");
        if (F.contentDocument) {
            F.height = F.contentDocument.documentElement.scrollHeight + 30; //FF 3.0.11, Opera 9.63, and Chrome
        } else {



            F.height = F.contentWindow.document.body.scrollHeight + 30; //IE6, IE7 and Chrome

        }

    }

    //window.onload = sizeFrame;

    //-->
</script>
<script>
    function alertsize(pixels) {
        pixels += 32;
        document.getElementById('hbkFrame').style.height = pixels + "px";
    }
</script>
</head>
<body>
<div class="gridContainer clearfix">
  <div id="LayoutDiv1">
  	<form id="form1" runat="server">
	<table style="width:100%; height: 100%">
    	<tr>
    		<td class="HeaderBar" style="height:50px;">
    		<vs:Render ID="CU" runat="server" />
                <asp:Image ID="Image3" runat="server" Height="50px" 
                    ImageUrl="~/Imgs/Logo.bmp" Width="127px" />
                <asp:Image ID="Image4" runat="server" Height="50px" 
                    ImageUrl="~/Imgs/TF_mobilelogo2.png" Width="167px" />
            </td>
    	</tr>
    	<!--<tr align="center" >-->
        <tr style="align-content:center">
			<td >
                <div class="scroller">
                <iframe id="hbkFrame" class="scroller" src="LoginWebSafe.aspx" style="width:100%; height: 100%" seamless="seamless"></iframe>
                </div>
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
