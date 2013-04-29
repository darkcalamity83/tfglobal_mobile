<%@ Page Language="VB" AutoEventWireup="false" Inherits="HomeBankingSite.MobileBalancesP" Codebehind="MobileBalancesP.aspx.vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ Register TagPrefix="vs" TagName="Render" Src="../Render.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form ID="Form1" Runat="server" Font-Size="Small" BackColor="#ccffff">
        <mobile:Image id="Image1" Runat="server" ImageUrl="~/imgs/logo.bmp"  NavigateUrl="~/MainTemplete.aspx"/>
        <mobile:label ID="Label1" runat="server" Font-Bold="True">
               MOBILE BANK BALANCES
        </mobile:label> 
         <mobile:label ID="lblerr" runat='server' Font-Bold='True' />
         <mobile:Image id="Img1" Runat="server" ImageUrl="~/TBar/Mbr.gif" Visible="false" />
         <mobile:label ID="user1" runat='server' Font-Bold='True'>
         </mobile:label>
         <mobile:List ID="balances1" Runat="server" >
        </mobile:List>
        <mobile:label ID="Label3" runat='server' Font-Bold='True' />
        <mobile:Image id="Img2" Runat="server" ImageUrl="~/TBar/Mbr.gif" Visible="false" />
        <mobile:label ID="user2" runat='server' Font-Bold='True'>
         </mobile:label>
         <mobile:List ID="balances2" Runat="server" >
        </mobile:List>
        <mobile:label ID="Label4" runat='server' Font-Bold='True' />
        <mobile:Image id="Img3" Runat="server" ImageUrl="~/TBar/Mbr.gif" Visible="false" />
        <mobile:label ID="user3" runat='server' Font-Bold='True'>
         </mobile:label>
        <mobile:List ID="balances3" Runat="server" >
        </mobile:List>
        <mobile:label ID="Label5" runat='server' Font-Bold='True' />
        <mobile:Image id="Img4" Runat="server" ImageUrl="~/TBar/Mbr.gif" Visible="false" />
        <mobile:label ID="user4" runat='server' Font-Bold='True'>
         </mobile:label>
        <mobile:List ID="balances4"  Runat="server" >
        </mobile:List>
        <mobile:label ID="Label7" runat='server' Font-Bold='True' />
        <mobile:Link ID="lnkBal" Runat='server' >Balances</mobile:Link>
        <mobile:Link ID="lnkTran" Runat='server' >Transfers</mobile:Link>
        <mobile:Link ID="lnkAnnounce" Runat='server'>Announcements</mobile:Link>
        <mobile:Link ID="Link1" Runat='server' NavigateUrl="~/MainTemplete.aspx" >Return to HomeBanking</mobile:Link>
        <mobile:Link ID="lnkChange" Runat='server' Visible="False" >Change Info</mobile:Link>
        <mobile:label runat="server" id="Label10">
          Copyright 2010, Terra Firma Information Technology, LLC
        </mobile:label> 
    </mobile:Form>
 </body>
</html>
