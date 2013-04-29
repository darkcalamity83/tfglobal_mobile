<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="History.aspx.vb" Inherits="HomeBankingSite.History"%>
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
<title>Credit Union - Account history</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"/>
		<link rel="stylesheet" type="text/css" media="screen" href="Hist.css"/>
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
<style>
    .desc {
        height: 15%;
    }

    .alttd {
        background-color: ghostwhite;
    }
</style>
<script src="respond.min.js"></script>
</head>
<body>
<div class="gridContainer clearfix">
  <div id="LayoutDiv1">
  <form id="Frm" runat="server">
			<table class="WhiteWin2" style="width: auto">
			    <tr>
					<td>
						
						<div class="ToolBar">
							<b>History Options:</b><br>
							Jump To Date:
							<asp:TextBox ID="txtDate" Runat="server" EnableViewState="True" />
							On Account:
							<asp:DropDownList ID="lstAcct" Runat="server" EnableViewState="False" />
							View Amount:
							<asp:DropDownList ID="lstViewAmount" Runat="server" EnableViewState="False" Width="88px">
								<asp:ListItem Value="10">10</asp:ListItem>
								<asp:ListItem Value="20">20</asp:ListItem>
								<asp:ListItem Value="30">30</asp:ListItem>
								<asp:ListItem Value="1mo">1 Month</asp:ListItem>
								<asp:ListItem Value="2mo">2 Months</asp:ListItem>
								<asp:ListItem Value="3mo">3 Months</asp:ListItem>
							</asp:DropDownList>
							<input type="submit" ID="btnView" value="View" Class="btn"> </div>
                            
                            
					</td>
				</tr>
				<tr>
					<td class="Contents" align="center">
						<iframe id="wHelp" src="Help/History.htm" style="width: auto"></iframe>
					</td>
				</tr>
				<tr>
					<td>
						<vs:Render ID="Hist" runat="server" />
					</td>
				</tr>
                <tr><td>
                	<div class="ToolBar" onMouseUp="body.focus();">
						
						<Span class="Disabled" title="View Account History."><img src="TBar/Hist.gif"> History</Span>
						<div class="menu_list"><a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a></div>
						<div class="menu_list"><a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a></div>
						<div class="menu_list"><a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a> </div>
						<!--<div class="menu_list"><a title="Withdrawal Money From Your Accounts" href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</a></div>-->
						<div class="menu_list"><a title="Review Transactions that are set to post at a future date." href="PendingTransactions.aspx"><img src="TBar/Xfr.gif"> Pending Transactions</a></div>
						<!--<div class="menu_list"><a title="Stop payment on one or more checks." href="StopChecks.aspx"><img src="TBar/Stop.gif"> Stop Checks</a></div>-->
						<div class="menu_list"><a title="Apply for a new loan." href="LoanApp.aspx"><img src="TBar/Loan.gif"> Loan App</a></div>
						<div class="menu_list"><a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a></div>
						<!--a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a-->
						<div class="menu_list"><a title="Make a request." href="Requests.aspx"><img src="TBar/Request.gif"> Contact Us</a></div>
						<!--<div class="menu_list"><a title="View Online Statements." href="OnlineStatements.aspx"><img src="TBar/Statement.gif"> E-Statements</a></div>-->
						<!--<div class="menu_list"><a title="Change Your Home Banking Logon." href="ChangeLogin.aspx"><img src="TBar/ChangePass.gif"> Change Info</a></div>-->
						<div class="menu_list"><a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a></div>
						<div class="menu_list"><a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');"> ?</a></div>
                    </div>
                </td></tr>
			</table>
		</form>
  </div>
</div>
</body>
</html>
