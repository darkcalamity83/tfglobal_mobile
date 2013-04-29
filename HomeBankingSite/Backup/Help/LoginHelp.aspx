<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginHelp.aspx.vb" Inherits="HomeBankingSite.Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style4
        {
        	font-size: medium;
            color: black;
            padding-left: 5px;
            height: 314px;
            width: 95%;
            float: left;
        }
   </style>
</head>
<body>
    <form id="LoginHelp" runat="server">
    <div class=style4>
        If you are having problems logging onto Home Banking, read the following.
        <ol>
            <li>You cannot log onto Home Banking until you have <b>activated your account</b>. To
                do this, you can go to your <b>nearest branch</b> to obtain an <b>Activation Code</b>.<br /><br /></li>
            <li> If you have an activaion code 
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Activation.aspx">Click Here.</asp:HyperLink><br /><br /></li>
            <li>If you are logging on <b>between 11:45pm and 1:30am</b>, the Home Banking site is
                unavailable due to nightly maintentance.<br /><br /></li>
            <li>If you have <b>forgotten your username or password</b>, you can have the credit
                union assign you new ones.</li>
        </ol>
    </div>
    </form>
</body>
</html>
