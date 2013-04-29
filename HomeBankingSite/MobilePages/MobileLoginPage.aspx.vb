
Partial Class MobileLoginPage
    Inherits System.Web.UI.MobileControls.MobilePage

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Dim username As String = txtUser.Text
            Dim Key As Hbk.UserInfo = Hbk.GetHBKey(username)
            If (Key.UserName = "##ERROR##") Then
                If Key.ErrNum = Hbk.EErrors.InvalidUser Then
                    lblerr.BackColor = Color.Red
                    Key.Password = "Invalid username."
                    lblerr.Text = Key.Password
                ElseIf Key.ErrNum = Hbk.EErrors.TimeToActivate Then
                    lblerr.BackColor = Color.Red
                    Key.Password = "Please activate your account and try again."
                    lblerr.Text = Key.Password
                ElseIf Key.ErrNum = Hbk.EErrors.PartialActivate Then
                    lblerr.BackColor = Color.Red
                    Key.Password = "Please finish activating your account."
                    lblerr.Text = Key.Password
                Else
                    lblerr.BackColor = Color.Red
                    If Key.Password.Contains("The socket disconnected") Then
                        Key.Password = "Your Homebanking site is currently down for maintenance, please wait a few minutes and try again. Sorry for the inconvenience."
                    ElseIf Key.Password.Contains("No connection could be made because the target machine actively refused") Then
                        Key.Password = "Your Homebanking site is currently down for maintenance, please wait a few minutes and try again. Sorry for the inconvenience."
                    ElseIf Key.Password.Contains("An established connection was aborted by the software") Then
                        Key.Password = "There was an issue accessing your account or your account may be lock for a small amount of time due to excessive failed login attempts. please try again later, Thank you"
                    End If
                    lblerr.Text = Key.Password
                End If
            Else
                Response.Redirect("MobilePasswordP.aspx?username=" & username)
            End If
        Catch ex As system.Exception
            lblerr.Text = "Server has had a issue has occurred logging in."
        End Try
    End Sub

End Class
