Public Partial Class DownloadingPage
    Inherits System.Web.UI.Page

    Private Sub DownLink_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles DownLink.OnRender
        Try
            Dim loA As String = Request.QueryString("a")
            Dim loS As String = Request.QueryString("s")
            Dim loF As String = Request.QueryString("f")
            loS = loS.Replace("/", "|").Replace(" ", "%20")
            loF = loF.Replace(" ", "%20")
            Dim loc As String = "ViewStatement.aspx?dt=open&a=" + loA + "&s=" + loS + "&f=" + loF
            Writer.Write("<a href='" & loc & "' >Open - Statement.pdf</a>")
            Writer.Write("<br />")
            loc = "ViewStatement.aspx?dt=save&a=" + loA + "&s=" + loS + "&f=" + loF
            Writer.Write("<a href='" & loc & "' >Download - Statement.pdf</a>")
        Catch ex As System.Exception
        End Try
    End Sub

End Class