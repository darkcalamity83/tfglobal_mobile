<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Balances.aspx.vb" Inherits="HomeBankingSite.Balances"%>
<html>
	<head>
		<title>Credit Union - Accounts and balances</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css" />
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css" />
		<script src="Hbk.js"></script>
		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
  </head>
	<body onmouseup="body.focus();">
		<table class="WhiteWin2" cellpadding="0" cellspacing="0" width="96%">
			<tr>
				<td>
					<div class="ToolBar" onmouseup="body.focus();">
						<Span class="Disabled" title="View Account Balances."><img src="TBar/Bal.gif"> Balances</Span>
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
					<iframe id="wHelp" src="Help/Balances.htm"></iframe>
				</td>
			</tr>
			<tr>
				<td>
					<vs:Render ID="Bals" runat="server" />
				</td>
			</tr>
		</table>
	</body>
</html>
