<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Requests.aspx.vb" Inherits="HomeBankingSite.Requests" %>

<html>
	<head>
		<title>Credit Union - Contact Us</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css" />
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css" />
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
		<form id="Frm" method="post" runat="server" action="">
		<table class="WhiteWin2" cellpadding="0" cellspacing="0" width="96%">
			<tr>
				<td>
					<div class="ToolBar" onmouseup="body.focus();">
						<a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a>
						<a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a>
						<a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a> 
						<a title="Withdrawal Money From Your Accounts" href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</a>
						<a title="Review Transactions that are set to post at a future date." href="PendingTransactions.aspx"><img src="TBar/Xfr.gif"> Pending Transactions</a>
						<a title="Stop payment on one or more checks." href="StopChecks.aspx"><img src="TBar/Stop.gif"> Stop Checks</a>
						<a title="Apply for a new loan." href="LoanApp.aspx"><img src="TBar/Loan.gif"> Loan App</a>
						<a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a>
						<!--a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a-->
						<a title="Make a request." href="Requests.aspx"><img src="TBar/Request.gif"> Contact Us</a>
						<a title="View Online Statements." href="OnlineStatements.aspx"><img src="TBar/Statement.gif"> E-Statements</a>
						<a title="Change Your Home Banking Logon." href="ChangeLogin.aspx"><img src="TBar/ChangePass.gif"> Change Info</a>
						<a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a>
						<a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');"> ?</a>
					</div>
				</td>
			</tr>
			<tr>
				<td class="Contents" align="center">
					<iframe id="wHelp" src="Help/Requests.htm"></iframe>
				</td>
			</tr>
			<tr>
				<td>
						<asp:panel id="wPnlErr" Runat="server" Visible="False" EnableViewState="False">
							<asp:Label id="lblErr" Runat="server">Error Message.</asp:Label>
						</asp:panel>
						<div class="RequestBar">
							<b>Request Options:</b><br>
							<table>
								<tr>
									<td width="20%">Request Type:</td>
									<td width="100%"><asp:DropDownList ID="lstRequestType" Runat="server" >
                                        <asp:ListItem Selected="True" Value="Link">Link Account</asp:ListItem>
                                        <asp:ListItem Value="GeneralIssues">General Issues</asp:ListItem>
                                        <asp:ListItem Value="SecurityIssues">Security Issues</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
								</tr>
								<tr>
									<td>From:</td>
									<td width="100%"><asp:Label ID="lblFrom" Runat="server" EnableViewState="False" /></td>
								</tr>
								<tr>
									<td>Directions:</td>
									<td width="100%"><asp:Label ID="lblDirections" Runat="server" EnableViewState="False" /></td>
								</tr>
							</table>
						</div>
						<div>
							<asp:TextBox ID="txtMessage" Runat="server" EnableViewState="False" 
                                Height="200px" Width="100%"
								TextMode="MultiLine" BorderStyle="Solid" MaxLength="500" />
						</div>
						<div class="ToolBar">
							<asp:Button ID="btnOk" Runat="server" EnableViewState="False" Text="Make Request" CssClass="btn" />
							<asp:Button ID="btnCancel" Runat="server" EnableViewState="False" Text="Cancel" CssClass="btn" />
						</div>
				</td>
			</tr>
		</table>
		</form>
	</body>
</html>
