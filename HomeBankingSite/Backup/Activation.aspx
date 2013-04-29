<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Activation.aspx.vb" Inherits="HomeBankingSite.Activation" %>

<html>
<head>
    <title>Credit Union - Activate account</title>
    <link media="screen" href="Styles.css" type="text/css" rel="stylesheet">
    <script src="Hbk.js"></script>
    		<script type="text/javascript" language="javascript">
		    function disableBackButton() {
		        window.history.forward()
		    }
		    disableBackButton();
		    window.onpageshow = function(evt) { if (evt.persisted) disableBackButton() }
		    window.onunload = function() { void (0) }  
        </script>
</head>
<body style="background: #DFD5B0 url(Imgs/DtlBarTan.gif) repeat-x top left;">
    <form id="frmActivate" method="post" runat="server" style="width: 99%">
    <div>
        <asp:Panel ID="pnlAllInfo" runat="server" Width="97%" Style="margin-top: 0px; width: 97%;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; border: 5px #0000FF solid;">
                <tr>
                    <td>
                        <b>Account Number:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAcct" runat="server" Width="125px" MaxLength="50"></asp:TextBox>

                        <script language="javascript">
                            document.getElementById("txtAcct").focus();
                        </script>

                        &lt;--(Please enter your account number.)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Activation Code:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtActivation" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                        &lt;--(This is the code that the credit union assigned you.)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Desired Username:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                        &lt;--(Please choose a unique username.)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Password:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="125px" MaxLength="50"></asp:TextBox>
                        &lt;--(Password must be at least 6 chars long, must contain atleast 1 letter and 1 
                        number.)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Confirm Password:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="125px" MaxLength="50"></asp:TextBox>
                        &lt;--(Please retype your password.)
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Panel runat="server" Width="97%" Style="margin-top: 0px; width: 97%;">
            <table cellspacing="0" cellpadding="0" width="100%" style="border: 5px #0000FF solid;">
                <tr>
                    <td>
                        <b>What is your first pets name?</b>
                    </td>
                    <td>
                        <asp:TextBox ID="Q1" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                        &lt;-- (Please avoid using simple answers.)<br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>What is your favorite sport?</b>
                    </td>
                    <td>
                        <asp:TextBox ID="Q2" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                        &lt;-- (Please avoid using simple answers.)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>What is your favorite color?</b>
                    </td>
                    <td>
                        <asp:TextBox ID="Q3" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                        &lt;-- (Please avoid using simple answers.)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Please enter a valid email:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Please choose a keyword:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHBKeyName" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                        &lt;--This is a word you will see each time you log in.
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
     <br />
    <asp:Panel ID="pnlLogin" runat="server" Width="97%">
        <table id="MyImages" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td>
                    <b>Please select a picture that you will remember each time you log in.</b> &nbsp;<b><input
                        id="btnHelp" class="BtnHelp" onclick="Toggle('wHelp');" title="Toggle Help."
                        type="button" value="?" /></b>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td style="border: 5px #0000FF solid;">
                    <asp:LinkButton ID="Apples" runat="server"><img id="Img1" style="width: 64px; height: 64px" src="HBKey/Apples.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Arch" runat="server"><img id="Image2" style="width: 64px; height: 64px" src="HBKey/Arch.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Boat" runat="server"><img id="Image3" style="width: 64px; height: 64px" src="HBKey/Boat.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Bridge" runat="server"><img id="Img2" style="width: 64px; height: 64px" src="HBKey/Bridge.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Bridge1" runat="server"><img id="Img3" style="width: 64px; height: 64px" src="HBKey/Bridge1.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Bus" runat="server"><img id="Img4" style="width: 64px; height: 64px" src="HBKey/Bus.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Butterfly" runat="server"><img id="Img5" style="width: 64px; height: 64px" src="HBKey/Butterfly.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Cone" runat="server"><img id="Img6" style="width: 64px; height: 64px" src="HBKey/Cone.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Eggs" runat="server"><img id="Img7" style="width: 64px; height: 64px" src="HBKey/Eggs.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Hammer" runat="server"><img id="Img8" style="width: 64px; height: 64px" src="HBKey/Hammer.jpg" border="0"></asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="LightHouse" runat="server"><img id="Img9" style="width: 64px; height: 64px" src="HBKey/LightHouse.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Lightning" runat="server"><img id="Img10" style="width: 64px; height: 64px" src="HBKey/Lightning.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Monitor" runat="server"><img id="Img11" style="width: 64px; height: 64px" src="HBKey/Monitor.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Peach" runat="server"><img id="Img12" style="width: 64px; height: 64px" src="HBKey/Peach.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Phone" runat="server"><img id="Img13" style="width: 64px; height: 64px" src="HBKey/Phone.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Printer" runat="server"><img id="Img14" style="width: 64px; height: 64px" src="HBKey/Printer.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Tiger" runat="server"><img id="Img15" style="width: 64px; height: 64px" src="HBKey/Tiger.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Tornado" runat="server"><img id="Img16" style="width: 64px; height: 64px" src="HBKey/Tornado.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Truck" runat="server"><img id="Img17" style="width: 64px; height: 64px" src="HBKey/Truck.jpg" border="0"></asp:LinkButton>
                    <asp:LinkButton ID="Watermelon" runat="server"><img id="Img18" style="width: 64px; height: 64px" src="HBKey/Watermelon.jpg" border="0"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div>
        <iframe id="wHelp" src="Help/Activation.htm" style="width: 97%; height: 300px"></iframe>
    </div>
    <table id="tblImgName">
        <tr>
            <td class="Contents">
                <asp:Panel ID="wPnlErr" runat="server" Visible="False" Width="97%">
                    <asp:Label ID="lblErr" runat="server" Width="97%">Error Message.</asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
