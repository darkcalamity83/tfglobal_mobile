<%@ Page Language="VB" AutoEventWireup="false" Inherits="HomeBankingSite.MobileHistoryP" Codebehind="MobileHistoryP.aspx.vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <mobile:Form ID="Form1" Runat="server" Font-Size="Small" BackColor="#ccffff">
       <mobile:Image id="Img2" Runat="server" ImageUrl="~/imgs/logo.bmp"  NavigateUrl="~/MainTemplete.aspx"/>
        <mobile:label ID="Label1" Runat="server" Font-Bold="True">
                MOBILE BANK HISTORY
        </mobile:label>
        <mobile:label ID="lblerr" Runat="server" Font-Bold="True" />
        <mobile:Image ID="Img1" Runat="server" ImageUrl="~/TBar/Hist.gif" />
        <mobile:label ID="user1" Runat="server" Font-Bold="True" />
        <mobile:List ID="history1" Runat="server">
        </mobile:List>
        <mobile:label ID="label19" Runat="server" Font-Bold="True" />
        <mobile:command ID="reset1" Runat="server" Text="Reset" />
        <mobile:command ID="next1" Runat="server" Text="Next" />
        <mobile:label ID="Label4" Runat="server" Font-Bold="True" />
        <mobile:Link ID="lnkBal" Runat="server">Balances</mobile:Link>
        <mobile:Link ID="lnkTran" Runat="server">Transfers</mobile:Link>
        <mobile:Link ID="lnkAnnounce" Runat="server">Announcements</mobile:Link>
        <mobile:Link ID="Link1" Runat="server" NavigateUrl="~/MainTemplete.aspx" >Return to HomeBanking</mobile:Link>
        <mobile:Link ID="lnkChange" Runat="server" Visible="False">Change Info</mobile:Link>
        <mobile:Label Runat="server" ID="Label10">
                Copyright 2010, Terra Firma Information Technology, LLC
        </mobile:Label>
    </mobile:Form>
</body>
</html>
