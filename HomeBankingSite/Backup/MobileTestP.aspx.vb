Imports Tf.AllV2

Partial Class MobileTestP
    Inherits System.Web.UI.MobileControls.MobilePage

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Mobile") = "0" Then
                Response.Redirect("MainTemplete.aspx")
                Exit Sub
            End If
            If Request.Browser("IsMobileDevice") = "true" Then
                Response.Redirect("MobilePages/MobileLoginPage.aspx")
            Else
                If Request.Browser.IsMobileDevice Then
                    Response.Redirect("MobilePages/MobileLoginPage.aspx")
                Else
                    'Select Case Request.Browser.Browser
                    '    Case "IE"
                    '        Response.Redirect("MainTemplete.aspx")
                    '    Case "Firefox"
                    '        Response.Redirect("MainTemplete.aspx")
                    '    Case "AppleMAC-Safari"
                    '        Response.Redirect("MainTemplete.aspx")
                    '    Case "Opera"
                    '        Response.Redirect("MainTemplete.aspx")
                    '    Case "Unknown"
                    '        Response.Redirect("MobilePages/MobileLoginPage.aspx")
                    '    Case Else
                    '        Hbk.LogBrowsers(Request.Browser.Browser)
                    '        TfLog.Log.MyLog.Write(Request.Browser.Browser, "DEVICETYPETEST", Diagnostics.EventLogEntryType.Information)
                    '        Response.Redirect("MainTemplete.aspx")
                    'End Select
                    Hbk.LogBrowsers(Request.Browser.Browser)
                    Response.Redirect("MainTemplete.aspx")
                End If
            End If
        Catch ex As system.Exception
        End Try
    End Sub

End Class
