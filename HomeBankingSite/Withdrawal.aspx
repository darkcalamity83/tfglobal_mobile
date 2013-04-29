<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Withdrawal.aspx.vb" Inherits="HomeBankingSite.Withdrawal"%>
<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<HTML>
	<HEAD>
		<title>Credit Union - Withdrawal funds</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"></link>
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css"></link>
		<script src="Hbk.js"></script>
		<script language="javascript">
			function Verify()
			{
				var From = document.getElementById("lstFromAcct");
				var Amt = document.getElementById("txtAmount");
				var Amount = FormatCurrency(Amt.value);
				if(confirm("Do you want to withdrawal " + Amount + " from " + From.value + "?"))
					document.forms[0].submit();
				else
					return false;
			}
		</script>
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
		<form id="Frm" runat="server" method="post" action="">
			<table class="WhiteWin2" cellpadding="0" cellspacing="0" width="96%">
			
				<tr>
					<td>
					    <div class="ToolBar" onmouseup="body.focus();">
						    <a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a>
						    <a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a>
						    <a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a>
						    <Span class="Disabled"  title="Withdrawal Money From Your Accounts" href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</Span>
						    <a title="Review Transactions that are set to post at a future date." href="PendingTransactions.aspx"><img src="TBar/Xfr.gif"> Pending Transactions</a>
						    <a title="Stop payment on one or more checks." href="StopChecks.aspx"><img src="TBar/Stop.gif"> Stop Checks</a>
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
					<td>
						<vs:Render ID="XfrInfo" runat="server" />
						<div class="ToolBar">
							<b>Withdrawal Options:</b><br>
							From Account:
							<asp:DropDownList ID="lstFromAcct" Runat="server" EnableViewState="False" />
							Amount:
							<asp:TextBox Width="100px" ID="txtAmount" Runat="server" />
							Memo:
							<asp:TextBox ID="txtMemo" Runat="server" />
							<input type="button" ID="btnOK" value="OK" Class="btn" onclick="Verify();">
						</div>
					</td>
				</tr>
				<tr>
					<td class="Contents" align="center">
						<iframe id="wHelp" src="Help/Withdrawal.htm"></iframe>
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
