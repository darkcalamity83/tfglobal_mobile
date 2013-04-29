Partial Class ChangeLogin
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            Dim OS As String = Request.QueryString("OS")
            If (IsPostBack) Then
                Dim ErrMsg As String = Hbk.ChangeLogin(txtUser.Text, txtPassword.Text, txtNewUser.Text, txtNewPassword.Text, txtNewPassword2.Text, txtEmail.Text, txtHBKeyName.Text, False)

                If (Not ErrMsg Is Nothing AndAlso ErrMsg <> "") Then
                    wPnlErr.Visible = True
                    lblErr.Text = ErrMsg
                Else
                    wInfo.Text = "Your information has been successfully changed."
                    wInfo.Visible = True 'Response.Redirect("Balances.aspx")
                    If (Not OS Is Nothing AndAlso (Not DirectCast(Hbk.MyAccts().Item(0), Hbk.Member).Email Is Nothing OrElse DirectCast(Hbk.MyAccts().Item(0), Hbk.Member).Email <> "")) Then
                        Response.Redirect("~/OnlineStatements.aspx")
                    ElseIf (Not OS Is Nothing) Then
                        wInfo.Text = "Please update your email address so we know where to send statement notifications."
                        wInfo.Visible = True
                    End If
                End If
            Else
                Me.txtUser.Text = Session.Item("Username")
                Me.txtEmail.Text = DirectCast(Hbk.MyAccts().Item(0), Hbk.Member).Email
                If (Not OS Is Nothing) Then
                    wInfo.Text = "Please update your email address so we know where to send statement notifications."
                    wInfo.Visible = True
                End If
            End If
        Catch ex As System.Exception
            Me.lblErr.Text = ex.Message
            Me.wPnlErr.Visible = True
        End Try
    End Sub

End Class
