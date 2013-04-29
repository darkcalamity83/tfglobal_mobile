<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LoanApp2.aspx.vb" Inherits="HomeBankingSite.LoanApp"%>
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
<title>Credit Union - Loan application</title>
		<link rel="stylesheet" type="text/css" media="screen" href="Styles.css"/>
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css"/>
		<script src="Hbk.js"></script>
		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
<link href="boilerplate.css" rel="stylesheet" type="text/css">
<link href="mobile-3.css" rel="stylesheet" type="text/css">
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
</head>
<body>
<div class="gridContainer clearfix">

  <div id="LayoutDiv1">
  <table class="WhiteWin2" style="width:auto; height: auto">
		
			
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
										<TABLE>
											<TR>
												<TD colSpan="2" class="Dots"><B>Loan Information:</B></TD>
											</TR>
											<TR>
												<TD>Loan amount:</TD>
												<TD>
													<asp:TextBox id="txtAmount" Runat="server"></asp:TextBox>
													<SCRIPT language="javascript">document.getElementById("txtAmount").focus();</SCRIPT>
												</TD>
                                            </tr>
                                            <tr>
												<TD>Loan Type:</TD>
												<TD>
													<asp:DropDownList id="lstType" Runat="server"></asp:DropDownList></TD>
											</TR>
											<TR>
												<TD>Term (in months):</TD>
												<TD>
													<asp:TextBox id="txtTerm" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD colSpan="2" class="Dots"><B>Personal Information:</B></TD>
											</TR>
											<TR>
												<TD>Drivers License #:</TD>
												<TD>
													<asp:TextBox id="txtLicense" Runat="server"></asp:TextBox></TD>
                                            </TR>
                                            <tr>
												<TD>Monthly mortgage (or rent):</TD>
												<TD>
													<asp:TextBox id="txtRent" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Contact Phone:</TD>
												<TD>
													<asp:TextBox id="txtPhone" Runat="server"></asp:TextBox></TD>
                                            </TR>
                                            <TR>
												<TD>E-mail Address:</TD>
												<TD>
													<asp:TextBox id="txtEmail" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD colSpan="2" class="Dots"><B>Employment Information:</B></TD>
											</TR>
											<TR>
												<TD>Annual Income:</TD>
												<TD>
													<asp:TextBox id="txtIncome" Runat="server"></asp:TextBox></TD>
                                            </TR>
                                            <TR>
												<TD>Employer Name:</TD>
												<TD>
													<asp:TextBox id="txtEmployer" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Employer Address:</TD>
												<TD>
													<asp:TextBox id="txtEmpAddr" Runat="server"></asp:TextBox></TD>
                                            </TR>
                                            <TR>
												<TD>Employer City:</TD>
												<TD>
													<asp:TextBox id="txtEmpCity" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>Employer State:</TD>
												<TD>
													<asp:TextBox id="txtEmpState" Runat="server"></asp:TextBox></TD>
                                            </TR>
                                            <TR>
												<TD>Employer Zip:</TD>
												<TD>
													<asp:TextBox id="txtEmpZIP" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD colSpan="2" class="Dots"><B>Coapplicant (optional)</B></TD>
											</TR>
											<TR>
												<TD>First Name:</TD>
												<TD>
													<asp:TextBox id="txtFirstName" Runat="server"></asp:TextBox></TD>
                                            </TR>
                                            <TR>
												<TD>Last Name:</TD>
												<TD>
													<asp:TextBox id="txtLastName" Runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>SSN:</TD>
												<TD>
													<asp:TextBox id="txtSSN" Runat="server"></asp:TextBox></TD>
											</TR>
										</TABLE>
										<TABLE onMouseUp="body.focus();" cellSpacing="0" cellPadding="0" width="auto">
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
					
				</td>
			</tr>
    
  </div>
</div>
</body>
</html>
