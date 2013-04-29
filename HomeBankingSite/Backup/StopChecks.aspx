<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StopChecks.aspx.vb" Inherits="HomeBankingSite.StopChecks"%>
<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<html>
	<head>
		<title>Credit Union - Stop Checks</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"></link>
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css"></link>
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
	<body>
	<form id="Frm" method="post" runat="server">
		<table class="WhiteWin2" cellSpacing="0" cellPadding="0" height="100%" width="100%">
		    <tr>
				<td>							
					<div class="ToolBar" onmouseup="body.focus();">
							<a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a>
							<a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a>
							<a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a> 
							<a title="Withdrawal Money From Your Accounts" href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</a>
							<a title="Review Transactions that are set to post at a future date." href="PendingTransactions.aspx"><img src="TBar/Xfr.gif"> Pending Transactions</a>
							<span class="Disabled" title="Stop payment on one or more checks."><img src="TBar/Stop.gif"> Stop Checks</span>
							<a title="Apply for a new loan." href="LoanApp.aspx"><img src="TBar/Loan.gif"> Loan App</a> 
						<a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a>
						<!--a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a-->
						<a title="Make a request." href="Requests.aspx"><img src="TBar/Request.gif"> Contact Us</a>
						<a title="View Online Statements." href="OnlineStatements.aspx"><img src="TBar/Statement.gif"> E-Statements</a>
							<a title="Change Your Home Banking Logon." href="ChangeLogin.aspx"><img src="TBar/ChangePass.gif"> Change Info</a>
							<a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a> 
							<a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');">?</a>
					</div>
				</td>
			</tr>
			<tr>
				<td align="center">
					    <table class="Win" cellSpacing="0" cellPadding="0" width="60%">
							<tr>
								<td class="WinBar">Stop one or more checks.</td>
							</tr>
							<tr>
								<td class="Contents"><iframe id="wHelp" src="Help/StopChecks.htm"></iframe>
									<asp:Label ID="wPnlErr" Runat="server" Visible="False" />
								</td>
							</tr>
							<tr>
								<td class="Contents" align="center"><asp:panel id="pnlLogin" Runat="server">
										<asp:Label id="wInfo" Runat="server" EnableViewState="False" CssClass="Info" Visible="False">Your request has been saved.  <b>
												Note: </b> this is only a request.  The credit union reserves the right to ignore your request.</asp:Label>
										<table cellSpacing="0" cellPadding="0">
											<tr>
												<td>Select the account on which you wish to stop a check payment:</td>
												<td valign="top">
													<asp:DropDownList id="lstAccts" Runat="server"></asp:DropDownList></td>
											</tr>
											<tr>
												<td>To place a hold on a check (or range of checks), you may enter the check number 
													(or starting check number) here:</td>
												<td valign="top">
													<asp:TextBox id="txtBegNum" Runat="server"></asp:TextBox>
													<script type="text/javascript" language="javascript">document.getElementById("txtBegNum").focus();</script>
												</td>
											</tr>
											<tr>
												<td>then enter the end check number here:</td>
												<td>
													<asp:TextBox id="txtEndNum" Runat="server"></asp:TextBox></td>
											</tr>
											<tr>
												<td>or you can enter the amount of the check here:</td>
												<td>
													<asp:TextBox id="txtAmount" Runat="server" Width="150px"></asp:TextBox></td>
											</tr>
										</table>
										<table onmouseup="body.focus();" cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td></td>
												<td align="right"><input class="Btn" id="btnOK" title="Click to request check hold." type="submit" value="OK">
													<input class="BtnHelp" id="btnHelp2" title="Toggle Help." onclick="Toggle('wHelp');" type="button"
														value="?">
												</td>
											</tr>
										</table>
									</asp:panel></td>
							</tr>
						</table>
					
				</td>
			</tr>
		</table>
		</form>
	</body>
</html>
