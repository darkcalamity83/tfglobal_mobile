<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Transfer.aspx.vb" Inherits="HomeBankingSite.Transfer"%>
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
<script src="respond.min.js"></script>
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
<div class="gridContainer clearfix">
  <div id="LayoutDiv1">
  		<form id="Frm" runat="server">
			<table class="WhiteWin2" style="width: 100%">			
				<tr>
					<td>
						
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
                <tr>
                    <td>
                    <div class="ToolBar" onMouseUp="body.focus();">
						<div class="menu_list"><a title="View Account Balances." href="Balances.aspx"><img src="TBar/Bal.gif"> Balances</a></div>
						<div class="menu_list"><a title="View Account History." href="History.aspx"><img src="TBar/Hist.gif"> History</a></div>
						<div class="menu_list"><a title="Transfer Money Between Your Accounts." href="Transfer.aspx"><img src="TBar/Xfr.gif"> Transfer</a> </div>
						<!--<div class="menu_list"><a title="Withdrawal Money From Your Accounts" href="Withdrawal.aspx"><img src="TBar/Wtd.gif"> Withdrawal</a></div>-->
						<div class="menu_list"><a title="Review Transactions that are set to post at a future date." href="PendingTransactions.aspx"><img src="TBar/Xfr.gif"> Pending Transactions</a></div>
						<!--<div class="menu_list"><a title="Stop payment on one or more checks." href="StopChecks.aspx"><img src="TBar/Stop.gif"> Stop Checks</a></div>-->
                        <div class="menu_list"><a title="Apply for a new loan." href="LoanApp.aspx"><img src="TBar/Loan.gif"> Loan App</Span> </div>
						<div class="menu_list"><a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a></div>
						<!--a title="Pay Your Bills Online." href="https://billscenter.paytrust.com/csp/CSPServlet/Login?brId=3004"><img src="TBar/BillPay.gif"> Bill Pay</a>-->
						<div class="menu_list"><a title="Make a request." href="Requests.aspx"><img src="TBar/Request.gif"> Contact Us</a></div>
						<!--<div class="menu_list"><a title="View Online Statements." href="OnlineStatements.aspx"><img src="TBar/Statement.gif"> E-Statements</a></div>-->
						<!--<div class="menu_list"><a title="Change Your Home Banking Logon." href="ChangeLogin.aspx"><img src="TBar/ChangePass.gif"> Change Info</a></div>-->
						<div class="menu_list"><a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a></div>
						<div class="menu_list"><a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');"> ?</a></div>	
					</div>
                    </td>
                </tr>
			</table>
		</form>
  </div>
</div>
</body>
</html>
