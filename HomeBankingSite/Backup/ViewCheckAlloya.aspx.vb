Partial Public Class ViewCheckAlloya
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            Dim acct As String = Request.QueryString("acct")
            acct = Right(acct.PadLeft(10, "0"), 10).TrimStart("0")
            Dim num As String = Request.QueryString("num")
            Dim dt As String = Request.QueryString("date")
            Dim amt As String = Request.QueryString("amt")
            Dim ImageFB As String = Request.QueryString("FB")
            Dim CUID As String = Request.QueryString("RT")

            If (acct Is Nothing OrElse num Is Nothing OrElse dt Is Nothing OrElse amt Is Nothing) Then
                Response.Write("Invalid Request.")
                Exit Sub
            Else
                Dim Accts As Tf.AllV2.TfGlobals.Sys.Data.XMLArray = Hbk.MyAccts
                If Accts Is Nothing Then
                    Response.Write("Invalid Request.")
                    Exit Sub
                End If

                Dim postData As String = "CUID=" & CUID & "&Account=" & acct & "&Serial=" & num & "&ImageFormat=GIF&ImageFB=" & ImageFB
                Dim url As String = "https://broker2.images.membersunited.org/Scripts/AFS/AfsWebAPI/AfsWebAPI.dll?StreamImage?" & postData

                Response.Redirect(url)

            End If
        Catch ex As System.Exception
            Response.Write("<h2>Im sorry your check image was not found at the moment. Please try again later.</h2>")
        End Try


    End Sub

End Class