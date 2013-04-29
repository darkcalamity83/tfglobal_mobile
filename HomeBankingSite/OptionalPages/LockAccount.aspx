<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LockAccount.aspx.vb" Inherits="HomeBankingSite.LockAccount"%>
<HTML>
	<HEAD>
		<title>Credit Union - Lock Account</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"></link>
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css"></link>
		<script src="Hbk.js"></script>
	</HEAD>
	<body>
		<form id="Frm" runat="server">
			<table class="WhiteWin" cellpadding="0" cellspacing="0" width="100%">
			
				<tr>
					<td>
						<div class="ToolBar" onmouseup="body.focus();">
							<a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a>
							<a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a>
							<a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a> 
							<a title="Stop payment on one or more checks." href="StopChecks.aspx"><img src="TBar/Stop.gif"> Stop Checks</a> 
							<a title="Cut a check and have it mailed to you." href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</a>
							<a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a> 
							<a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');">?</a>
						</div>
						<div class="ToolBar" onmouseup="body.focus();">
							<a title="Apply for a new loan." href="LoanApp.aspx"><img src="TBar/Loan.gif"> New Loan</a> 
							<Span class="Disabled" title="Lock your account."><img src="TBar/LockAccount.gif"> Lock Account</Span>
							<a title="Pay Your Bills Online." href="BillPay.aspx"><img src="TBar/BillPay.gif"> Bill Pay</a>
							<a title="Make a request." href="Requests.aspx"><img src="TBar/Request.gif"> Requests</a>
							<a title="View Online Statements." href="OnlineStatements.aspx"><img src="TBar/Statement.gif"> Online Statements</a>
							<a title="Change Your Home Banking Logon." href="ChangeLogin.aspx"><img src="TBar/ChangePass.gif"> Change Information</a>
						</div>
						<vs:Render ID="LockAcctInfo" runat="server" />
						<div class="ToolBar">
							<b>Lock Account Options:</b><br>
							Account To Lock:
							<asp:DropDownList ID="lstLockAcct" Runat="server" EnableViewState="False" />
							<input type="submit" ID="btnOK" value="OK" Class="btn">
						</div>
					</td>
				</tr>
				<tr>
					<td class="Contents" align="center">
						<iframe id="wHelp" src="Help/LockAccount.htm"></iframe>
					</td>
				</tr>
				<tr>
					<td onmouseup="body.focus();">
						<vs:Render ID="Bals" runat="server" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
