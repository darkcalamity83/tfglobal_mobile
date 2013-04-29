<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ChangeLogin.aspx.vb" Inherits="HomeBankingSite.ChangeLogin"%>
<HTML>
	<HEAD>
		<title>Credit Union - Change Information</title>
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
	</HEAD>
	<body>
		<table class="WhiteWin2" cellpadding="0" cellspacing="0" width="96%">
		
			<tr>
				<td height="30" onmouseup="body.focus();">
					<div class="ToolBar">
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
						<Span class="Disabled" title="Change Your Home Banking Logon."><img src="TBar/ChangePass.gif"> Change Info</Span>&nbsp; 
						<a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a>
						<a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');">?</a>
					</div>
				</td>
			</tr>
			<tr>
				<td valign="middle" align="center">
					<form id="Frm" method="post" runat="server">
						<table class="Win" cellSpacing="0" cellPadding="0" width="350">
							<tr>
								<td class="WinBar">Change Information</td>
							</tr>
							<tr>
								<td class="Contents" align="center"><asp:panel id="pnlLogin" Runat="server">
										<asp:Label id="wInfo" Runat="server" EnableViewState="False" CssClass="Info" Visible="False"></asp:Label>
										<TABLE cellSpacing="0" cellPadding="0">
										    <tr>
										        <td colspan="2" style="color:White;"><b>Enter the information you wish to change and click ok.</b></td>
										    </tr>
											<TR>
												<TD>Current Username:</TD>
												<TD>
													<asp:TextBox id="txtUser" Runat="server" Width="150px"></asp:TextBox>
													<SCRIPT language="javascript">document.getElementById("txtUser").focus();</SCRIPT>
												</TD>
											</TR>
											<TR>
												<TD>Current Password:</TD>
												<TD>
													<asp:TextBox id="txtPassword" Runat="server" Width="150px" TextMode="Password"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>New Username:</TD>
												<TD>
													<asp:TextBox id="txtNewUser" Runat="server" Width="150px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>New Password:</TD>
												<TD>
													<asp:TextBox id="txtNewPassword" Runat="server" Width="150px" TextMode="Password"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Confirm New Password:</TD>
												<TD>
													<asp:TextBox id="txtNewPassword2" Runat="server" Width="150px" TextMode="Password"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Email:</TD>
												<TD>
													<asp:TextBox id="txtEmail" Runat="server" Width="150px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>HBKey Name:</TD>
												<TD>
													<asp:TextBox id="txtHBKeyName" Runat="server" Width="150px"></asp:TextBox></TD>
											</TR>
										</TABLE>
										<TABLE onmouseup="body.focus();" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD></TD>
												<TD align="right"><INPUT class="Btn" id="btnOK" title="Click to change login." type="submit" value="OK">
													<INPUT class="BtnHelp" id="btnHelp2" title="Toggle Help." onclick="Toggle('wHelp');" type="button"
														value="?">
												</TD>
											</TR>
										</TABLE>
									</asp:panel></td>
							</tr>
							<tr>
								<td class="Contents">
									<iframe id="wHelp" src="Help/ChangeLogin.htm"></iframe>
									<asp:panel id="wPnlErr" Runat="server" Visible="False" EnableViewState="False">
										<asp:Label id="lblErr" Runat="server">Error Message.</asp:Label>
									</asp:panel>
								</td>
							</tr>
						</table>
					</form>
				</td>
			</tr>
		</table>
	</body>
</HTML>
