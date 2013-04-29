Public Partial Class MainTemplete
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
        Catch ex As system.Exception
        End Try
    End Sub

    Private Sub CU_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles CU.OnRender
        Try
            If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Mobile") = "1" Then
                Writer.Write("<a href='MobilePages/MobileLoginPage.aspx' style='float:left; color:White;'>Try our new Mobile Banking - </a>")
            End If
            Writer.Write("<p>" & System.Web.Configuration.WebConfigurationManager.AppSettings.Item("CUName") & " Mobile Banking</p>")
        Catch ex As system.Exception
        End Try
    End Sub
End Class