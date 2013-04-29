Imports Tf.All

Partial Class Login
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Msgs_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Msgs.OnRender
        Hbk.RenderMsgs(Writer)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Sys.IO.MyLog.Write("Page_Load PostBack=" & Me.IsPostBack.ToString, "Login.Load")
 
        Response.Expires = 5
        If (Me.IsPostBack) Then
            Me.Session.Add("Username", txtUser.Text)
            Dim Key As Hbk.UserInfo = Hbk.GetHBKey(Session.Item("Username"))
            If (Key.UserName = "##ERROR##") Then
                If Key.ErrNum = Hbk.EErrors.InvalidUser Then
                    Response.BufferOutput = True
                    Server.Transfer("SignIn.aspx")
                ElseIf Key.ErrNum = Hbk.EErrors.TimeToActivate Then
                    Server.Transfer("Activation.aspx")
                ElseIf Key.ErrNum = Hbk.EErrors.PartialActivate Then
                    Server.Transfer("Activation.aspx?hbkey=true")
                Else
                    wPnlErr.Visible = True
                    wPnlErr.BackColor = Color.Red
                    wPnlErr.BorderStyle = BorderStyle.Double
                    wPnlErr.BorderWidth = 3
                    If Key.Password.Contains("The socket disconnected") Then
                        Key.Password = "Your Homebanking site is currently down for maintenance, please wait try again later. Sorry for the inconvenience."
                    End If
                    lblErr.Text = Key.Password
                End If
            Else
                If Key.IP <> HttpContext.Current.Request.UserHostAddress Then
                    Response.BufferOutput = True
                    Server.Transfer("SignIn.aspx")
                Else
                    Server.Transfer("Password.aspx")
                End If
            End If
        Else

        End If
    End Sub

End Class
