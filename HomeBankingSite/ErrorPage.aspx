<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErrorPage.aspx.vb" Inherits="HomeBankingSite.ErrorPage"%>
<HTML>
	<HEAD>
		<title>Unknown Error</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"></link>
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
							<td style="padding:5px;" align="right"><form runat="server">
									<asp:Label ID="wPnlErr" Runat="server">An unknown error has occurred</asp:Label></form>
									<a href="Logout.aspx" class="BtnHelp">OK</a>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
