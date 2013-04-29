<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrintHistoryFreindly.aspx.vb" Inherits="HomeBankingSite.PrintHistoryFreindly" %>

<%@ Register src="Render.ascx" tagname="Render" tagprefix="vs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<link rel="stylesheet" type="text/css" media="screen" href="Styles.css" />
		<link rel="stylesheet" type="text/css" media="screen" href="Bals.css" />
    <title>Untitled Page</title>
    		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
</head>
<body>
    <form id="form1" runat="server">
		<vs:Render ID="Hist" runat="server" />
    </form>
</body>
</html>
