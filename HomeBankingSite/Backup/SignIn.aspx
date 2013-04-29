<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignIn.aspx.vb" Inherits="HomeBankingSite.SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Credit Union Sign-in </title>

    <script src="Hbk.js" type="text/javascript"></script>
    		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>

    <style type="text/css">
        .styleInfo
        {
            background: #B10000;
            padding-left: 0;
            border: solid 2px #0000ff;
            width: 100%;
            color: #FFFFFF;
        }
        BODY
        {
            font-family: Arial;
            padding: 10px;
        }
        A
        {
            text-decoration: none;
            font-size: smaller;
            color: #A0BFED;
        }
        A:Hover
        {
            color: #FA4802;
        }
        .BtnHelp, .ToolBar .BtnHelp, .ToolBar .BtnHelp:Hover, .BtnHelp:Hover
        {
            background: #FF723B url(Imgs/BtnHelp.gif) repeat-x bottom left;
            color: #FFF;
            font-weight: bold;
            padding-left: 3px;
            padding-right: 3px;
            text-decoration: none;
            border: solid 1px #596275;
        }
        Input
        {
            border: solid 1px #596275;
            margin-left: 0px;
        }
        #wHelp, .Help
        {
            display: none;
            margin: 10px;
            height: 100%;
            width: 97%;
            border: solid 1px #D4CAA5;
            float: none;
        }
        #wPnlErr, .Err
        {
            background: #FDFAF0 url(Imgs/Error.gif) no-repeat center left;
            border: solid 1px #D4CAA5;
            padding: 5px;
            padding-left: 25px;
            color: #000;
            margin: 10px;
            text-align: left;
            height: 60px;
            width: 97%;
        }
    </style>
</head>
<body style="background: #DFD5B0 url(Imgs/DtlBarTan.gif) repeat-x top left;">
    <form method="post" runat="server" style="border: 3px #0000FF solid; width: 97%;
    height: 97%;">
    <div class="styleInfo">
        <br />
        The information you entered does not match our records or if this is the first time
        logging into your homebanking account. Please enter your username and answer the
        security question you are asked with the answer you choose when you activated your
        account. If you have forgot your username or security question please contact the
        credit union.
        <br />
        <br />
    </div>
    <div>
        <br />
        <table style="border: 3px #0000FF solid; background: #B10000; color: #FFFFFF;">
            <tr>
                <td align="right">
                    <b>Username:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server" Width="113px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Security question:</b>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>
                        <asp:Label ID="lblQ" runat="server" Text="Label"></asp:Label></b>
                </td>
                <td>
                    <asp:TextBox ID="txtA" runat="server" Width="113px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSignIn" runat="server" Text="Sign In" Width="70%" BackColor="#33CCFF"
                        BorderColor="#3333CC" BorderWidth="2px" ForeColor="White"
                        Height="25px" />
                    <input class="BtnHelp" id="btnHelp" title="Toggle Help." onclick="Toggle('wHelp');" type="button"
                        value="?" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <br />
        <asp:Panel ID="wPnlErr" runat="server" Visible="False" EnableViewState="False">
            <asp:Label ID="lblErr" runat="server" EnableViewState="False">Error Message:</asp:Label>
        </asp:Panel>
    </div>
    <iframe id="wHelp" src="help/Signin.htm" style="width: 97%; height: 300px"></iframe>
    </form>
</body>
</html>
