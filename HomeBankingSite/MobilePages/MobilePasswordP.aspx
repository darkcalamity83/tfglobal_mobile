<%@ Page Language="VB" AutoEventWireup="false" Inherits="HomeBankingSite.MobilePasswordP" Codebehind="MobilePasswordP.aspx.vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form id="Form1" runat="server" Font-Size="small" BackColor="#ccffff">
            <mobile:Image id="Img2" Runat="server" ImageUrl="~/imgs/logo.bmp"  NavigateUrl="~/MainTemplete.aspx"/>
            <mobile:label runat="server" id="Label7" Font-Bold="True">
                MOBILE BANK PASSWORD
            </mobile:label> 
            <mobile:label ID="Label8" runat="server" />
            <mobile:label runat="server" id="lbltest" Font-Bold="True">
                Please verify your image and key.
            </mobile:label> 
            <mobile:label ID="Label2" runat="server" />
             <mobile:image runat="server" id="imgHBK" />
             <mobile:label runat="server" id="lblHBK" />
            <mobile:label runat="server" />
            <mobile:label runat="server" id="Label1" Font-Bold="True">
                Please verify this was the last time you logged in.
            </mobile:label> 
            <mobile:label runat="server" id="lblLastLoggin" />
            <mobile:label ID="Label4" runat="server" />
            <mobile:label runat="server" id="Label9" Font-Bold="True" >
                Please answer the security question.
            </mobile:label>
             <mobile:label ID="lblQuestion" runat="server" />
            <mobile:textbox runat="server" id="txtAnswer" Password="true"/>
            <mobile:label ID="Label5" runat="server" />
            <mobile:label runat="server" id="Label3" Font-Bold="True" >
                Password:
            </mobile:label>
            <mobile:textbox runat="server" id="txtPass" Password="True" />
             <mobile:label runat="server" ID="lblerr" />
             <mobile:label ID="lblerr234" runat="server" />
            <mobile:command ID="btnLogin" runat="server" Text="Login" />
            <mobile:label ID="Label6" runat="server" />
            <mobile:label runat="server" id="Label10">
                Copyright 2010, Terra Firma Information Technology, LLC
            </mobile:label> 
    </mobile:Form>
</body>
</html>
