<%@ Page Language="VB" AutoEventWireup="false" Inherits="HomeBankingSite.MobileLoginPage" Codebehind="MobileLoginPage.aspx.vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
    
<body>
    <mobile:Form id="Form1" runat="server" Font-Size="Small" BackColor="#ccffff">
        <mobile:Image id="Img2" Runat="server" ImageUrl="~/imgs/logo.bmp" NavigateUrl="~/MainTemplete.aspx" />
            <mobile:label ID="Label1" runat="server" Font-Bold="True">
                WELCOME TO YOUR MOBILE BANK
            </mobile:label> 
           <mobile:label ID="Label4" runat="server" Font-Bold="True" />
            <mobile:label runat="server" id="Label2">
                Please Login to your account.
            </mobile:label> 
            <mobile:label ID="Label5" runat="server" Font-Bold="True" />
            <mobile:label runat="server" id="Label3">
                Username:
            </mobile:label>
            <mobile:textbox runat="server" id="txtUser" />
            <mobile:label ID="Label6" runat="server" Font-Bold="True" />
            <mobile:command ID="btnLogin" runat="server" Text="Login" />
            <mobile:label ID="Label7" runat="server" Font-Bold="True" />
            <mobile:label runat="server" id="lblerr">
            </mobile:label> 
            <mobile:Link ID="Link1" Runat='server' NavigateUrl="~/MainTemplete.aspx" >Return to HomeBanking</mobile:Link>
            <mobile:label runat="server" id="Label10">
                Copyright 2010, Terra Firma Information Technology, LLC
            </mobile:label> 
    </mobile:Form>
</body>
</html>