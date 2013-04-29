Partial Class Password
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
        Response.ExpiresAbsolute = #1/1/1980#
        Response.AddHeader("cache-control", "no-store, must-revalidate, private")
        Response.AddHeader("Pragma", "no-cache")
        Response.CacheControl = "no-cache"
        Response.Expires = -1
        If (Me.IsPostBack) Then
            Try
                Dim UI As Hbk.UserInfo = Hbk.getUserInfo(Session.Item("Username"), "UserName")
                Hbk.AcctNumber = UI.Acct
            Catch ex As Exception
            End Try
            Dim ErrMsg As String = Hbk.Login(Session.Item("Username"), txtPassword.Text)
            If (Not ErrMsg Is Nothing) Then
                If (ErrMsg = "ACTIVATE") Then
                    Response.Redirect("Activation.aspx?code=" & txtPassword.Text & "&acct=" & Session("Username"))
                Else
                    wPnlErr.Visible = True
                    lblErr.Text = ErrMsg
                    If ErrMsg = "Your login attempt failed." Then
                        Hbk.LogFailedAttempt(Session.Item("Username"))
                    End If
                End If
            End If
        Else
            Dim loName As String
            loName = Session.Item("Username")
            Dim Key As Hbk.UserInfo = Hbk.GetHBKey(Session.Item("Username"))
            If (Key.UserName = "##ERROR##") Then
                Me.pnlHBKey.Visible = False
                wPnlErr.Visible = True
                lblErr.Text = Key.Password
            Else
                If (Key.HBKey = "") Then
                    Me.pnlHBKey.Visible = False
                Else
                    Me.pnlHBKey.Visible = True
                    Me.imgHBKey.ImageUrl = Key.HBKey
                    Me.lblHBKey.Text = Key.HBKeyName
                    If (Not Key.LastLogin Is Nothing AndAlso Key.LastLogin <> "") Then
                        Me.lblLastLogin.Text = "Last Login: " & Key.LastLogin
                    End If
                End If
            End If
        End If
    End Sub

End Class
