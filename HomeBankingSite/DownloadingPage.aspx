<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DownloadingPage.aspx.vb" Inherits="HomeBankingSite.DownloadingPage" %>

<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Download Page</title>
    <style type="text/css">
        #wDownPage
        {
            background: #FDFAF0;
            border: solid 1px #D4CAA5;
            padding: 5px;
            padding-left: 25px;
            color: #000;
            margin: 10px;
            text-align: left;
        }
        .WinBar
        {
            background: #A0BFED url(Imgs/WinBar.gif) repeat-x top left;
            color: White;
            padding: 1px;
            padding-left: 5px;
            font-family: Arial;
            font-size: smaller;
            border-bottom: solid 1px #000;
            height: 300px;
            width: 350px;
        }
    </style>
</head>
<body>
    
    <form class="WinBar" runat="server">
   
    <asp:Label runat="server" Font-Bold="true" Text="Download File" />
   
    <br />
    <br />
    <br />
    <br />
    <br />
   
    <asp:Label ID="lblDown" runat="server" Font-Bold="true" Text="To download or view the file please click the link below:" />
    
    <br />
    <br />
    <br />
    
    <vs:Render ID="DownLink" runat="server" />

    </form>
</body>
</html>
