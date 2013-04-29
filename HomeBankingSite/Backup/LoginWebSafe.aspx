<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginWebSafe.aspx.vb" Inherits="HomeBankingSite.LoginWebSafe" %>
<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Homebanking Login Page</title>
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
            <td width="22%" style="vertical-align: top;">
                <div style="color: black; background:#DFD5B0 url(Imgs/DtlBarTan.gif) repeat-x top left;	font-size: smaller; border-left:solid 3px #C0B9A1; padding:1px; margin:0;">
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
               <div id="wHelp2" style="display: none; height:250px;">
                    <iframe width="100%" height="100%" src="Help/Login.htm"></iframe>
               </div>
            </td>
            <td class="InfWrap2" width="78%">
                <h1><b>Credit Union Announcements and information</b></h1>
                <vs:Render ID="Msgs" runat="server" />
            </td>
        </tr>
      </table>
    </form>
</body>
</html>
