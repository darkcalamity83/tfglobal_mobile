Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals

Partial Public Class ViewStatement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now)
        Dim Acct As String = Request.QueryString("a")
        Dim Statement As String = Request.QueryString("s")
        Dim FileName As String = Request.QueryString("f")
        Dim DownType As String = Request.QueryString("dt")
        Dim CanView As Boolean = False

        If (Not Acct Is Nothing AndAlso Not Statement Is Nothing) Then
            Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts()
            For Each A As Hbk.Member In Accts
                If (A.Permission = Hbk.EHbkPermission.High) Then
                    If (A.Number = Acct) Then
                        CanView = True
                        Exit For
                    End If
                End If
            Next

            If (DirectCast(Accts.Item(0), Hbk.Member).OnlineStatements <> Hbk.EOnlineStatements.SignedUp) Then CanView = False

            If (System.IO.Directory.Exists(System.IO.Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StatementDirectory"), Statement.Replace("|", "."))) AndAlso CanView = True) Then
                Dim File As String = System.IO.Path.Combine(System.IO.Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StatementDirectory"), Statement.Replace("|", ".")), Acct & ".pdf")
                If (System.IO.File.Exists(File)) Then
                    Try
                        If (FileName Is Nothing OrElse FileName = "") Then FileName = "statement.pdf"
                        Response.ClearHeaders()
                        Response.ContentType = "application/unknown"
                        If DownType = "open" Then
                            Response.AddHeader("content-disposition", "inline;filename=" & FileName & ".pdf")
                        ElseIf DownType = "save" Then
                            Response.AddHeader("content-disposition", "attachment;filename=" & FileName & ".pdf")
                        End If
                        Response.WriteFile(File)
                        'Response.TransmitFile(File)
                        Response.End()
                    Catch ex As System.Exception
                        Response.Write(ex.ToString())
                    End Try
                Else
                    Try
                        File = HttpContext.Current.Server.MapPath("~/Help/NoActivity.pdf")
                        Response.ClearHeaders()
                        If DownType = "open" Then
                            Response.AddHeader("content-disposition", "inline;filename=noactivity.pdf")
                        ElseIf DownType = "save" Then
                            Response.AddHeader("content-disposition", "attachment;filename=noactivity.pdf")
                        End If
                        Response.ContentType = "application/unknown"
                        Response.WriteFile(File)
                        Response.End()
                    Catch ex As System.Exception
                        Response.Write(ex.ToString())
                    End Try
                End If
            Else
                Response.Write("You cannot view statements on this account.")
            End If
        End If
    End Sub

End Class