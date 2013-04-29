<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="HomeBankingSite.Login" %>

<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<html>
<head>
    <meta name="viewport" content="initial-scale=1, maximum-scale=1.0, width=device-width, user-scalable=0"/>

    <title>
        Credit Union Log-in
    </title>
    <link media="screen" href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Hbk.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 90%;
        }
        .style2
        {
            height: 186px;
        }
        </style>
</head>
<body style="height: 80%" >
    <form id="Frm" method="post" runat="server">
    <table class="WhiteWin" cellpadding="0" cellspacing="0" width="100%" style="height: 90%" >
        <tr>
            <td class="style1">
                <div class="Win" style="float: left; width: 30%; height: 100%">
                    <div class="Contents">
                        <asp:Panel ID="pnlLogin" runat="server">
                            <table cellspacing="0" cellpadding="0" style="float: left; width: 30%; height: 100%">
                                <tr style="height: 20%">
                                    <td>
                                        Username:
                                    </td>
                                    <td>
                                    
                                        <asp:TextBox ID="txtUser" runat="server" Width="150px"></asp:TextBox>

                                        <script language="javascript" type="text/javascript">document.getElementById("txtUser").focus();</script>

                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" style="float: right; width: 70%; height: 100%">
                                <tr>
                                    <td align="right">
                                        <input class="Btn" id="btnLogin" title="Login to home banking." type="submit" value="Login" />
                                        <input class="Btn" onclick="javascript:window.location='Activation.aspx';return false;"
                                            type="button" value="New User?" />
                                        <input class="BtnHelp" id="btnHelp" title="Toggle Help." onclick="Toggle('wHelp');" type="button" value="?" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div>
                            <asp:Panel ID="wPnlErr" runat="server" Visible="False" EnableViewState="False">
                                <asp:Label ID="lblErr" runat="server" EnableViewState="False">Error Message.</asp:Label>
                            </asp:Panel>
                            <iframe id="wHelp" src="Help/Login.htm" ></iframe>
                        </div>
                    </div>
                </div>
                <div class="InfWrap" style="margin: 5px">
                    <h1>
                        About <b></b> <b>Credit Union</b>'s Home Banking Site</h1>
                        <vs:Render ID="Msgs" runat="server" />
                </div>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
