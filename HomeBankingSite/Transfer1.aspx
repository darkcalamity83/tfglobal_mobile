<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Transfer.aspx.vb" Inherits="HomeBankingSite.Transfer"%>
<html>
	<head>
		<title>Credit Union - Transfer funds</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"></link>
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css"></link>
		<script src="Hbk.js"></script>
		<script language="javascript">
			function Verify()
			{
				var XferFrom = document.getElementById("lstFromAcct");
				var XferTo = document.getElementById("lstToAcct");
				var XferAmt = document.getElementById("txtAmount");
				var Amount = FormatCurrency(XferAmt.value);
				if(confirm("Do you want to transfer " + Amount + " from " + XferFrom.value + " to " + XferTo.value + "?"))
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
	</head>
	<body>
		<form id="Frm" runat="server">
			<table class="WhiteWin2" cellpadding="0" cellspacing="0" width="96%">			
				<tr>
					<td>
						<div class="ToolBar" onMouseUp="body.focus();">
							<a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a>
							<a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a>
							<Span class="Disabled" title="Transfer Money Between Your Accounts."><img src="TBar/Xfr.gif"> Transfer</Span>
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
							<a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');">?</a>
						</div>
						<vs:Render ID="XfrInfo" runat="server" />
						<div class="ToolBar">
							<b>Transfer Options:</b><br>
							From Account:
							<asp:DropDownList ID="lstFromAcct" Runat="server" EnableViewState="False" />
							To Account:
							<asp:DropDownList ID="lstToAcct" Runat="server" EnableViewState="False" />
							Amount:
							<asp:TextBox Width="100px" ID="txtAmount" Runat="server" />
							Date:
							<asp:TextBox ID="txtFDate" Runat="server" />
							Time:
							<asp:DropDownList ID="lstHours" Runat="server" EnableViewState="False" >
                                <asp:ListItem Value="AnyTime">AnyTime</asp:ListItem>
                                <asp:ListItem Value="4:00 AM">4:00 AM</asp:ListItem>
                                <asp:ListItem Value="5:00 AM">5:00 AM</asp:ListItem>
                                <asp:ListItem Value="6:00 AM">6:00 AM</asp:ListItem>
                                <asp:ListItem Value="7:00 AM">7:00 AM</asp:ListItem>
                                <asp:ListItem Value="8:00 AM">8:00 AM</asp:ListItem>
                                <asp:ListItem Value="9:00 AM">9:00 AM</asp:ListItem>
                                <asp:ListItem Value="10:00 AM">10:00 AM</asp:ListItem>
                                <asp:ListItem Value="11:00 AM">11:00 AM</asp:ListItem>
                                <asp:ListItem Value="12:00 PM">12:00 PM</asp:ListItem>
                                <asp:ListItem Value="1:00 PM">1:00 PM</asp:ListItem>
                                <asp:ListItem Value="2:00 PM">2:00 PM</asp:ListItem>
                                <asp:ListItem Value="3:00 PM">3:00 PM</asp:ListItem>
                                <asp:ListItem Value="4:00 PM">4:00 PM</asp:ListItem>
                                <asp:ListItem Value="5:00 PM">5:00 PM</asp:ListItem>
                                <asp:ListItem Value="6:00 PM">6:00 PM</asp:ListItem>
                                <asp:ListItem Value="7:00 PM">7:00 PM</asp:ListItem>
                                <asp:ListItem Value="8:00 PM">8:00 PM</asp:ListItem>
                                <asp:ListItem Value="9:00 PM">9:00 PM</asp:ListItem>
                                <asp:ListItem Value="10:00 PM">10:00 PM</asp:ListItem>
                                <asp:ListItem Value="11:00 PM">11:00 PM</asp:ListItem>
                            </asp:DropDownList>
                            Memo:
							<asp:TextBox ID="txtMemo" Runat="server" />
							<input type="button" ID="btnOK" value="OK" Class="btn" name="btnOK" onClick="Verify();">
						</div>
					</td>
				</tr>
				<tr>
					<td class="Contents" align="center">
						<iframe id="wHelp" src="Help/Transfer.htm"></iframe>
					</td>
				</tr>
				<tr>
					<td onMouseUp="body.focus();">
						<vs:Render ID="Bals" runat="server" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
