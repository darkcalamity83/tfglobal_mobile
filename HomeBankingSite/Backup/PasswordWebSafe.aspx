<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PasswordWebSafe.aspx.vb" Inherits="HomeBankingSite.PasswordWebSafe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PasswordWebSafe Page</title>
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
</head>
<body>
  <form id="frm" runat="server" style="height: 100%">
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
                    <iframe width="100%" height="100%" src="Help/Password.htm"></iframe>
               </div>
            </td>
            <td class="InfWrap3" width="78%">
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

</body>
</html>
