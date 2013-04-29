<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Password.aspx.vb" Inherits="HomeBankingSite.Password"%>
<HTML>
	<HEAD>
		<title>Credit Union - Login</title>
		<LINK media="screen" href="Styles.css" type="text/css" rel="stylesheet"></LINK>
		<script src="Hbk.js"></script>
	</HEAD>
	<body>
		<form id="Frm" method="post" runat="server">
			<table class="WhiteWin" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td>
						<DIV class="Win" style="FLOAT: left; WIDTH: 300px; HEIGHT: 100%">
							<div class="Contents"><asp:panel id="pnlLogin" Runat="server" Height="96px">
		                            <label><b>Only enter your password if the picture and key is what you chose.</b></label>
									<TABLE style="MARGIN: 10px" cellSpacing="0" cellPadding="0">
										<TR>
											<TD>Password:</TD>
											<TD>
												<asp:TextBox id="txtPassword" Runat="server" TextMode="Password" Width="150px"></asp:TextBox></TD>
											<SCRIPT language="javascript">document.getElementById("txtPassword").focus();</SCRIPT>
										</TR>
									</TABLE>
									<TABLE cellSpacing="0" cellPadding="0" width="260px">
										<TR>
											<TD style="PADDING-RIGHT: 5px" align="right"><INPUT class="Btn" id="btnLogin" title="Login to home banking." type="submit" value="Login">
												<INPUT class="BtnHelp" id="btnHelp" title="Toggle Help." onclick="Toggle('wHelp');" type="button"
													value="?">
											</TD>
										</TR>
									</TABLE>
								</asp:panel>
								<div><asp:panel id="wPnlErr" Runat="server" EnableViewState="False" Visible="False">
										<asp:Label id="lblErr" Runat="server" EnableViewState="False">Error Message.</asp:Label>
									</asp:panel><iframe id="wHelp" src="Help/Login.htm"></iframe>
								</div>
							</div>
						</DIV>
						<div class="InfWrap" style="MARGIN: 5px">
							<DIV id="pnlHBKey" runat="server">
							<label><b>Before you continue verify this is the picture and key you chose.</b></label><br /><br />
								<asp:image id="imgHBKey" Runat="server"></asp:image><BR>
								<asp:label id="lblHBKey" Runat="server" Font-Bold="true"></asp:label><BR>
							</DIV>
							<DIV>
                                <label><b>Before you continue verify this was the last time you logged in.</b></label><br /><br />
								<asp:Label ID="lblLastLogin" runat="server" Font-Bold="true"></asp:Label>
							</DIV>
							<div style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BACKGROUND-IMAGE: url(Imgs/Security.gif); background-repeat: no-repeat; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px">This 
								site is <B>secured</B> using 128 bit SSL encryption and the latest password 
								encoding technology.
							</div>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
