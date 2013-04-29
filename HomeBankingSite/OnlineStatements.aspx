<%@ Register TagPrefix="vs" TagName="Render" Src="Render.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OnlineStatements.aspx.vb" Inherits="HomeBankingSite.OnlineStatements"%>
<html>
	<head>
		<title>Credit Union - Online Statements</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css" />
        <link rel="stylesheet" type="text/css" media="screen" href="Bals.css" />
		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
        <script src="Hbk.js"></script>

        <script type="text/JavaScript" language="JavaScript">
            var mine = window.open('','','width=1,height=1,left=0,top=0,scrollbars=no');
            if(mine)
                var popUpsBlocked = false;
            else
                var popUpsBlocked = true;
                mine.close();
        </script>

        <style type="text/css">
            .style1
            {
                width: 262px;
            }
        </style>
	</head>
	<body>
		<form id="Frm" runat="server">
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
						<Span class="Disabled"  title="View Online Statements." ><img src="TBar/Statement.gif"> E-Statements</Span>
							<a title="Change Your Home Banking Logon." href="ChangeLogin.aspx">	<img src="TBar/ChangePass.gif"> Change Info</a>
							<a title="Log Out Of Home Banking." href="Logout.aspx"><img src="TBar/Logout.gif"> Logout</a>
							<a class="BtnHelp" id="btnHelp" title="Toggle Help." href="javascript:Toggle('wHelp');"> ?</a>
						</div>
						<tr>
					<td class="Contents" style="padding:5 5 5 5;">
					    <div style="margin:auto;font-family:Arial;font-size:10pt;width:800px" runat="server" id="pnlSetup">
					        <span style="color:#003365;font-weight:bold;">Stop receiving paper statements and gain immediate access of up to 6 years of online statement history<sup>1</sup>.</span><br />
					        <ul style="color:black;">
					            <li>Receive e-mail notification when your new statement is ready</li>
					            <li>Decrease exposure to mail fraud</li>
					            <li>Never worry about forgetting your payment</li>
					        </ul>
					        <span style="color:black;">Go Paperless to see your statement history online and stop receiving mailed statements.  To start the process enter or validate your email address and click "I Consent".  You will be emailed a confirmation with a link to complete the process.  If you do not receive the email return to this page and verify your email address and click the "I Consent" button to resend the confirmation email.</span><br /><br />
                            <span style="color:black;">Note:  We'll e-mail you when your statements are available online. If you choose to go paperless, it may take up to two months before you stop receiving paper statements in the mail, depending on when your statement cycle ends.</span><br /><br />
                            <span style="font-size:8pt; color:Black;">By clicking "I Consent," you hereby give us your affirmative consent to provide communications to you and conduct business regarding the accounts selected above in an electronic form, as described in the <a href="Help/eDisclosure.htm" target="_blank" style="color:white ">Paperless Statement e-Sign Disclosure</a>. You further agree that your computer satisfies the minimum hardware and software requirements specified in the Disclosure, you can access and retain the sample PDF file provided <a href="Help/Test.pdf" target="_blank" style="color:white">here</a> using Adobe&#169; Acrobat&#169; Reader&#169;  4.0 or higher (excluding 6.0), and that you have provided us with a current e-mail address to which we may send electronic communications to you.</span><br /><br />
					        <span style="font-size:12pt; color:Black;">Email:</span> <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnConsent" runat="server" Text="I Consent" />&nbsp;<asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red">(Please click "I Consent" only once.)</asp:Label>
                        </div>
                        <div style="margin:auto;font-family:Arial;width:800px" id="pnlStatements" runat="server">
                            <table class="style1" >
                                <tr>
                                    <td>Account: </td>
                                    <td>
                                        <asp:DropDownList ID="cboAccount" runat="server" DataSourceID="dsAccts" DataTextField="Number" DataValueField="Number">
                                        </asp:DropDownList>
                                    </td>
                            
                                </tr>
                                <tr>
                                    <td>Year: </td>
                                    <td>
                                        <asp:DropDownList ID="cboYear" runat="server" DataSourceID="dsYears" DataTextField="Year" DataValueField="Year" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                         <asp:ListBox ID="lbStatements" AutoPostBack="true" runat="server" 
                                             Height="148px"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                               <br />
                           
                                <asp:Image ID="ImgBlocker" runat="server" ImageUrl="~/Imgs/popupblocker.bmp" />
                                <br />
                                <br />
                                <asp:Label ID="LblBlocker" runat="server" Style="color: Black;" Text="Label">Some browsers may require users to allow Pop-ups. If using Internet Explorer, click on the yellow bar at the top of the page and then select 'Always Allow Pop-ups From This Site...' 
                                        If using another browser with Pop-ups disabled, you will need to permit them from this site. 
                                </asp:Label>
                        
                         <script type="text/JavaScript" language="JavaScript">
                                if(popUpsBlocked)
                                {
                                        var control1 = document.getElementById("ImgBlocker");
                                        var control2 = document.getElementById("LblBlocker");
                                        control1.style.visibility = "visible";
                                        control2.style.visibility = "visible";
                                }
                                else
                                {
                                        var control1 = document.getElementById("ImgBlocker");
                                        var control2 = document.getElementById("LblBlocker");
                                        control1.style.visibility = "hidden";
                                        control2.style.visibility = "hidden";
                                }
                         </script>
                            <!-- 
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="dsStatements" ShowHeader="False" AutoGenerateColumns="False" BorderStyle="None" GridLines="None">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnStatement" runat="server" ForeColor="Black" OnClientClick='<%# GetUrl(Eval("Statement"), Eval("Desc")) %>'
                                                Text='<%# Eval("Desc") %>' Font-Size="12pt"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <iframe id="frmLoadStatement" src="javascript:false;" style="display:none;" ></iframe>
                             -->
                            
                        </div>
                        <asp:ObjectDataSource ID="dsAccts" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetAccounts" TypeName="HomeBankingSite.StatementAccounts"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="dsStatements" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetStatements" TypeName="HomeBankingSite.Statements">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="cboYear" DefaultValue="0" Name="Year" PropertyName="SelectedValue"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="dsYears" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetStatementYears" TypeName="HomeBankingSite.Statements"></asp:ObjectDataSource>
 					</td>
				</tr>
			</table>
			<script language="javascript" type="text/javascript">
			function ViewStatement(Statment, FileName, acct)
			{
				document.getElementById('frmLoadStatement').src = "ViewStatement.aspx?a=" + acct + "&s=" + Statment + "&f=" + FileName ;
				//window.open("https://ibank.nyteamfcu.org/ViewStatement.aspx?a=" + document.getElementById("<%=cboAccount.ClientID %>").value + "&s=" + Statment + "&f=" + FileName);
			    return false;
			}
		    </script>
		</form>
	</body>
</html>
