Partial Class ProcessOSRequest
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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now)
        If (Not IsPostBack()) Then
            Dim ProcessRequest As String = Request.QueryString.Item("value")
            Dim Account As String = Request.QueryString("a")
            Dim User As Hbk.UserInfo

            If (ProcessRequest Is Nothing OrElse ProcessRequest = "" OrElse Account Is Nothing OrElse Account = "") Then
                Me.wPnlErr.Text = "This request is invalid."
            Else
                Try
                    Hbk.OnlineLogin(Account)
                    User = Hbk.getUserInfo(Account, "Acct")
                    Hbk.SetOnlineStatements(User.Email, Hbk.EOnlineStatements.SignedUp, ProcessRequest)
                    Me.wPnlErr.Text = "Your accounts have been setup for online statements."
                Catch ex As system.Exception
                    Me.wPnlErr.Text = ex.Message
                End Try
            End If
        End If
    End Sub

End Class
