<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PasswordWebSafe.aspx.vb" Inherits="HomeBankingSite.PasswordWebSafe" %>
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
<link href="boilerplate.css" rel="stylesheet" type="text/css">
<link href="mobile-3.css" rel="stylesheet" type="text/css">
<link href="mobile-1.css" rel="stylesheet" type="text/css" media="all">
<meta http-equiv="Content-Type" content="application/vnd.wap.xhtml+xml; charset=utf-8" />
        <meta name="HandheldFriendly" content="true" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
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
  <form id="frm" runat="server" style="height: 100%; width: 100%">
    <table cellpadding="0" cellspacing="0" style="height: 100%; background-color:#000066;" class="WinBlueB">
        <tr>
            <td width="22%" style="vertical-align:top;">
                <div style="color: black; background:#DFD5B0 url(Imgs/DtlBarTan.gif) repeat-x top left;	font-size: smaller; border-left:solid 3px #C0B9A1; padding:1px; margin:0;">
                    <b>Password:</b>
                    <br />
                    <asp:TextBox ID="txtUser" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                    <script language="javascript" type="text/javascript">document.getElementById("txtUser").focus();</script>
                    <br />
                    <br />
                    <input class="Btn" id="btnLogin" title="Login to home banking." type="submit" value="Login" />
                    <input class="BtnHelp" id="btnHelp" title="Toggle Help." onclick="Toggle('wHelp2');" type="button" value="?" />
                    <br />
                    <br />
                    <asp:Panel ID="wPnlErr" runat="server" Height="120px" Width="67%" Visible="False" EnableViewState="False">
                        <asp:Label ID="lblErr" runat="server" Height="95%" Width="95%" EnableViewState="False">Error Message.</asp:Label>
                    </asp:Panel> 
                     <br />
               </div>
               <div id="wHelp2" style="display: none; height:300px;">
                    <iframe width="auto" style="height: 100%" src="Help/Password.htm" seamless="seamless"></iframe>
               </div>
            </td>
            <!--<td class="InfWrap3" width="78%">-->
            <td class="InfWrap3" width="auto">
                <h1><b>Before you continue</b></h1>
                <div style="background:#FFF url(Imgs/Info.gif) center left no-repeat; background-color: #EEEEFF;">  
                    <h3>Please verify this is the picture and key you chose during activation.</h3>
                    <asp:image id="imgHBKey" Runat="server"></asp:image><br />
					<asp:label id="lblHBKey" Runat="server" Font-Bold="true"></asp:label><br />
                </div>
                <div style="background:#FFF url(Imgs/Info.gif) center left no-repeat; background-color: #FFFAF0;">
                    <h3>Please verify this was the last time you logged in.</h3>
                    <asp:Label ID="lblLastLogin" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div style="background:#FFF url(Imgs/Security.gif) center left no-repeat; background-color: #EEEEFF;">
                    <h4>This site is secured using 128 bit SSL encryption and the latest password encoding technology.</h4>
                </div>
            </td>
        </tr>
      </table>
    </form>

  </div>
</div>
</body>
</html>
