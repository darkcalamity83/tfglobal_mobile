<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProcessOSRequest.aspx.vb" Inherits="HomeBankingSite.ProcessOSRequest"%>
<HTML>
	<HEAD>
		<title>Online Statements Conformation</title><link rel="stylesheet" type="text/css" media="screen" href="Styles.css"></link>
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
		<table cellpadding="0" cellspacing="0" width="100%" height="100%">
			<tr>
				<td valign="middle" align="center">
					<table class="Win" cellSpacing="0" cellPadding="0" width="350">
						<tr>
							<td class="WinBar">Online Statements Conformation</td>
						</tr>
						<tr>
							<td class="TtlBar"><IMG src="Imgs/wicuLogo.bmp" align="left"><IMG src="Imgs/HbkTxt.gif"></td>
						</tr>
						<tr>
							<td style="PADDING-RIGHT:5px;PADDING-LEFT:5px;PADDING-BOTTOM:5px;PADDING-TOP:5px" align="right"><form runat="server" ID="Form1">
									<asp:Label ID="wPnlErr" Runat="server" Width="100%"></asp:Label></form>
								<a href="MainTemplete.aspx" class="BtnHelp">OK</a>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
