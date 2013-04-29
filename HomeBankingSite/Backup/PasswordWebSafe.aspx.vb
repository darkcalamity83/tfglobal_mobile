Public Partial Class PasswordWebSafe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            If (Me.IsPostBack) Then
                Try
                    Dim UI As Hbk.UserInfo = Hbk.getUserInfo(Session.Item("Username"), "UserName")
                    'Hbk.AcctNumber = UI.Acct
                Catch ex1 As system.Exception
                End Try
                Dim ErrMsg As String = Hbk.Login(Session.Item("Username"), txtUser.Text)
                If (Not ErrMsg Is Nothing) Then
                    If (ErrMsg = "ACTIVATE") Then
                        Response.Redirect("Activation.aspx?code=" & txtUser.Text & "&acct=" & Session("Username"))
                    Else
                        wPnlErr.Visible = True
                        wPnlErr.BackColor = Color.Red
                        wPnlErr.BorderStyle = BorderStyle.Double
                        wPnlErr.BorderWidth = 3
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
                    wPnlErr.Visible = True
                    lblErr.Text = Key.Password
                Else
                    If (Key.HBKey = "") Then

                    Else
                        Me.imgHBKey.ImageUrl = Key.HBKey
                        Me.lblHBKey.Text = Key.HBKeyName
                        If (Not Key.LastLogin Is Nothing AndAlso Key.LastLogin <> "") Then
                            Me.lblLastLogin.Text = "Last Login: " & Key.LastLogin
                        End If
                    End If
                End If
            End If
        Catch ex As system.Exception

        End Try


    End Sub

End Class