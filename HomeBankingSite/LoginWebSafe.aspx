<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginWebSafe.aspx.vb" Inherits="HomeBankingSite.LoginWebSafe" %>
<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<!doctype html>
<!--[if lt IE 7]> <html class="ie6 oldie"> <![endif]-->
<!--[if IE 7]>    <html class="ie7 oldie"> <![endif]-->
<!--[if IE 8]>    <html class="ie8 oldie"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="">
<!--<![endif]-->

<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Untitled Document</title>
<link media="screen" href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Hbk.js" type="text/javascript"></script>
    		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
<link href="boilerplate.css" rel="stylesheet" type="text/css">
<link href="mobile-1.css" rel="stylesheet" type="text/css" media="all">
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
<body onload="parent.alertsize(document.body.scrollHeight);" style="overflow: hidden">
<div class="gridContainer clearfix">
  <div id="LayoutDiv1">
  <form id="frm" runat="server" style="height: auto; width:auto">
    <table cellpadding="0" cellspacing="0" style="height: auto; width: auto; background-color:#000066;" class="WinBlueB">
        <tr>
            <td style="width:22%; height: 100%" style="vertical-align: top">
               <!-- <div style="color: black; background:#DFD5B0 url(Imgs/DtlBarTan.gif) repeat-x top left;	font-size: smaller; border-left:solid 3px #C0B9A1; padding:1px; margin:0;">-->
                <div style="color: black; background:#DFD5B0 url(Imgs/DtlBarTan.gif) repeat-x top left;	font-size: smaller; border-left:solid 1px #C0B9A1; padding:1px; margin:0;">
                    <br />
                    <b>
                    <asp:Label ID="Label1" runat="server" 
                        
                        Text="The first time you log back into your Homebanking account you will be prompted to enter some information that will add a new layer in securing your Account."></asp:Label>
                    <br />
                     <br />
                        <asp:Label ID="Label2" runat="server" 
                        Text="Username:"></asp:Label></b>

                    <br />
                    <asp:TextBox ID="txtUser" runat="server" Width="150px"></asp:TextBox>
                    <script language="javascript" type="text/javascript">document.getElementById("txtUser").focus();</script>
                    <br />
                    <br />
                    <input class="Btn" id="btnLogin" title="Login to home banking." type="submit" value="Login" />
                    <input class="Btn" onclick="javascript:window.location='Activation.aspx';return false;" type="button" value="New User?" />
                    <input class="BtnHelp" id="btnHelp" title="Toggle Help." onclick="Toggle('wHelp2');" type="button" value="?" />
                    <br />
                    <br />
                    <asp:Panel ID="wPnlErr" runat="server" Height="120px" Width="67%" Visible="False" EnableViewState="False">
                        <asp:Label ID="lblErr" runat="server" Height="95%" Width="95%" EnableViewState="False">Error Message.</asp:Label>
                    </asp:Panel> 
                     <br />
                    <br />
               </div>
               <div id="wHelp2" style="display: none; height:auto; width:auto">
                    <iframe style="width:auto; height:100%; overflow: visible" src="Help/Login.htm" seamless="seamless"></iframe>
               </div>
            </td>
            <td class="InfWrap2" width="78%" style="height: 100%">
              <h1><b>Credit Union Announcements and information</b></h1>
                <vs:Render ID="Msgs" runat="server" />
            </td>
        </tr>
      </table>
    </form>
  </div>
</div>
</body>
</html>
