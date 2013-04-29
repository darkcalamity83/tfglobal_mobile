Imports tf.AllV2

Partial Public Class LoginWebSafe
    Inherits System.Web.UI.Page

    Private Sub Msgs_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Msgs.OnRender
        Try
            Hbk.RenderMsgs(Writer)
        Catch ex As System.Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            TfLog.Log.MyLog.Write("Page_Load PostBack=" & Me.IsPostBack.ToString, "Login.Load")
            If (Me.IsPostBack) Then
                If txtUser.Text.Trim.Length = 0 Then
                    Exit Sub
                End If
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
                            Key.Password = "Your Homebanking site is currently down for maintenance, please wait a few minutes and try again. Sorry for the inconvenience."
                        ElseIf Key.Password.Contains("No connection could be made because the target machine actively refused") Then
                            Key.Password = "Your Homebanking site is currently down for maintenance, please wait a few minutes and try again. Sorry for the inconvenience."
                        ElseIf Key.Password.Contains("An established connection was aborted by the software") Then
                            Key.Password = "There was an issue accessing your account or your account may be lock for a small amount of time due to excessive failed login attempts. please try again later, Thank you"
                        End If
                        lblErr.Text = Key.Password
                    End If
                Else
                    If Key.IP <> Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress) Then
                        Response.BufferOutput = True
                        Server.Transfer("SignIn.aspx")
                    Else
                        Server.Transfer("PasswordWebSafe.aspx")
                    End If
                End If
            End If
        Catch ex As System.Exception
            wPnlErr.Visible = True
            wPnlErr.BackColor = Color.Red
            wPnlErr.BorderStyle = BorderStyle.Double
            wPnlErr.BorderWidth = 3
            lblErr.Text = "Error with the login rutine."
        End Try
    End Sub

End Class