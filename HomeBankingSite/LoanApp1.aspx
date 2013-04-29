<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LoanApp.aspx.vb" Inherits="HomeBankingSite.LoanApp"%>
<html>
	<head>
		<title>Credit Union - Loan application</title>
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
		<table class="WhiteWin2" height="100%" cellSpacing="0" cellPadding="0" width="96%">
		
			<tr>
				<td onMouseUp="body.focus();" height="30">
					<div class="ToolBar" onMouseUp="body.focus();">
						<a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a>
						<a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a>
						<a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a>
						<a title="Withdrawal Money From Your Accounts" href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</a>
						<a title="Review Transactions that are set to post at a future date." href="PendingTransactions.aspx"><img src="TBar/Xfr.gif"> Pending Transactions</a>
						<a title="Stop payment on one or more checks." href="StopChecks.aspx"><img src="TBar/Stop.gif"> Stop Checks</a>
						<Span class="Disabled" title="Apply for a new loan."><img src="TBar/Loan.gif"> Loan App</Span> 
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
				<td vAlign="middle" align="center">
					<form id="Frm" method="post" runat="server">
						<table class="Win" cellSpacing="0" cellPadding="0">
							<tr>
								<td class="WinBar">Loan application</td>
							</tr>
							<tr>
								<td class="Contents"><iframe id="wHelp" src="Help/LoanApp.htm"></iframe><asp:panel id="wPnlErr" Runat="server" Visible="False" EnableViewState="False">
										<asp:Label id="lblErr" Runat="server">Error Message.</asp:Label>
									</asp:panel></td>
							</tr>
							<tr>
								<td class="Contents" align="center"><asp:label id="wInfo" Runat="server" Visible="False" CssClass="Info" EnableViewState="False"></asp:label><asp:panel id="pnlApp" Runat="server">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD colSpan="4" class="Dots"><B>Loan Information:</B></TD>
											</TR>
											<TR>
												<TD>Loan amount:</TD>
												<TD>
													<asp:TextBox id="txtAmount" Runat="server"></asp:TextBox>
													<SCRIPT language="javascript">document.getElementById("txtAmount").focus();</SCRIPT>
												</TD>
												<TD>Loan Type:</TD>
												<TD>
													<asp:DropDownList id="lstType" Runat="server"></asp:DropDownList></TD>
											</TR>
											<TR>
												<TD>Term (in months):</TD>
												<TD colSpan="3">
													<asp:TextBox id="txtTerm" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD colSpan="4" class="Dots"><B>Personal Information:</B></TD>
											</TR>
											<TR>
												<TD>Drivers License #:</TD>
												<TD>
													<asp:TextBox id="txtLicense" Runat="server"></asp:TextBox></TD>
												<TD>Monthly mortgage (or rent):</TD>
												<TD>
													<asp:TextBox id="txtRent" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Contact Phone:</TD>
												<TD>
													<asp:TextBox id="txtPhone" Runat="server"></asp:TextBox></TD>
												<TD>E-mail Address:</TD>
												<TD>
													<asp:TextBox id="txtEmail" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD colSpan="4" class="Dots"><B>Employment Information:</B></TD>
											</TR>
											<TR>
												<TD>Annual Income:</TD>
												<TD>
													<asp:TextBox id="txtIncome" Runat="server"></asp:TextBox></TD>
												<TD>Employer Name:</TD>
												<TD>
													<asp:TextBox id="txtEmployer" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Employer Address:</TD>
												<TD>
													<asp:TextBox id="txtEmpAddr" Runat="server"></asp:TextBox></TD>
												<TD>Employer City:</TD>
												<TD>
													<asp:TextBox id="txtEmpCity" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Employer State:</TD>
												<TD>
													<asp:TextBox id="txtEmpState" Runat="server"></asp:TextBox></TD>
												<TD>Employer Zip:</TD>
												<TD>
													<asp:TextBox id="txtEmpZIP" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD colSpan="4" class="Dots"><B>Coapplicant (optional)</B></TD>
											</TR>
											<TR>
												<TD>First Name:</TD>
												<TD>
													<asp:TextBox id="txtFirstName" Runat="server"></asp:TextBox></TD>
												<TD>Last Name:</TD>
												<TD>
													<asp:TextBox id="txtLastName" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>SSN:</TD>
												<TD colSpan="3">
													<asp:TextBox id="txtSSN" Runat="server"></asp:TextBox></TD>
											</TR>
										</TABLE>
										<TABLE onMouseUp="body.focus();" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD></TD>
												<TD align="right"><INPUT class="Btn" id="btnOK" title="Click to submit the application." type="submit" value="Apply">
													<INPUT class="BtnHelp" id="btnHelp2" title="Toggle Help." onClick="Toggle('wHelp');" type="button"
														value="?">
												</TD>
											</TR>
										</TABLE>
									</asp:panel></td>
							</tr>
						</table>
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
