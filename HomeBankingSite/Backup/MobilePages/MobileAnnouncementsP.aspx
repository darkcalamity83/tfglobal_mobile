<%@ Page Language="VB" AutoEventWireup="false" Inherits="HomeBankingSite.MobileAnnouncementsP" Codebehind="MobileAnnouncementsP.aspx.vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form ID="Form1" Runat="server" Font-Size="small" BackColor="#ccffff">
        <mobile:Image ID="Img2" Runat="server" ImageUrl="~/imgs/logo.bmp" NavigateUrl="~/MainTemplete.aspx" />
        <mobile:Label ID="Label1" Runat="server" Font-Bold="True">
               MOBILE BANK ANNOUNCEMENTS
        </mobile:Label>
        <mobile:Label ID="lblerr" Runat='server' Font-Bold='True' />
        <mobile:List ID="announce" Runat="server" >
        </mobile:List>
        <mobile:Label ID="Label8" Runat='server' Font-Bold='True' />
        <mobile:Link ID="lnkBal" Runat='server'>Balances</mobile:Link>
        <mobile:Link ID="lnkTran" Runat='server'>Transfers</mobile:Link>
        <mobile:Link ID="lnkAnnounce" Runat='server'>Announcements</mobile:Link>
        <mobile:Link ID="lnkMain" Runat='server' NavigateUrl="~/MainTemplete.aspx">Return to HomeBanking</mobile:Link>
        <mobile:Link ID="lnkChange" Runat='server' Visible="False">Change Info</mobile:Link>
        <mobile:Label Runat="server" ID="Label10">
                Copyright 2010, Terra Firma Information Technology, LLC
        </mobile:Label>
    </mobile:Form>
</body>
</html>
