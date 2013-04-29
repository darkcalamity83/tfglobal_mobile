<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewStatement.aspx.vb" Inherits="HomeBankingSite.ViewStatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Statement</title>

		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
</head>
<body >
    <form id="form1" runat="server">
    <div id="lblMessage" runat="server">
    <p>hi this is a test</p>
        
    
    </div>
    </form>
</body>
</html>
