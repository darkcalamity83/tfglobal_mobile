<%@ Page Language="VB" AutoEventWireup="false" Inherits="HomeBankingSite.MobileTransfersP" Codebehind="MobileTransfersP.aspx.vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form id="Form1" runat="server" Font-Size="small" BackColor="#ccffff">
        <mobile:Image id="Img2" Runat="server" ImageUrl="~/imgs/logo.bmp" NavigateUrl="~/MainTemplete.aspx"/>
        <mobile:label ID="Label1" runat="server" Font-Bold="True">
               MOBILE BANK TRANSFERS
        </mobile:label> 
        <mobile:label ID="Label2" runat='server' Font-Bold='True' />
           <mobile:label runat="server" id="response1">
           </mobile:label> 
           <mobile:label runat="server" id="Label3">
                Transfer From:
            </mobile:label> 
            <mobile:SelectionList ID="tranfrom" Runat="server">
            </mobile:SelectionList>
              <mobile:label runat="server" id="Label4">
                Transfer To:
            </mobile:label> 
              <mobile:SelectionList ID="tranto" Runat="server">
            </mobile:SelectionList>
              <mobile:label runat="server" id="Label5">
                Amount:
            </mobile:label> 
             <mobile:textbox runat="server" id="txtamt" />
              <mobile:label runat="server" id="Label6">
                Description:
            </mobile:label> 
              <mobile:textbox runat="server" id="txtdesc" />
              <mobile:label ID="Label7" runat='server' Font-Bold='True' />
              <mobile:command ID="btntransfer" runat="server" Text="Transfer" />
              <mobile:label ID="Label8" runat='server' Font-Bold='True' />
              <mobile:Link ID="lnkBal" Runat='server' >Balances</mobile:Link>
              <mobile:Link ID="lnkTran" Runat='server' >Transfers</mobile:Link>
              <mobile:Link ID="lnkAnnounce" Runat='server'>Announcements</mobile:Link>
              <mobile:Link ID="lnkMain" Runat='server' NavigateUrl="~/MainTemplete.aspx" >Return to HomeBanking</mobile:Link>
              <mobile:Link ID="lnkChange" Runat='server' Visible="False" >Change Info</mobile:Link>
              <mobile:label runat="server" id="Label10">
                Copyright 2010, Terra Firma Information Technology, LLC
              </mobile:label> 
        </mobile:Form>
</body>
</html>
