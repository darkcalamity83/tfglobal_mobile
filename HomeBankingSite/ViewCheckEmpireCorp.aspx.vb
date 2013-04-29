Partial Public Class ViewCheckEmpireCorp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            Dim acct As String = Request.QueryString("acct")
            Dim num As String = Request.QueryString("num")
            Dim dt As String = Request.QueryString("date")
            Dim amt As String = Request.QueryString("amt")

            If (acct Is Nothing OrElse num Is Nothing OrElse dt Is Nothing OrElse amt Is Nothing) Then
                Response.Write("Invalid Request.")
            Else
                Dim rand As New System.Random(34)
                Dim id As String = rand.Next().ToString()
                Dim pdate As Date = Date.Parse(dt)
                If (id.Length > 8) Then
                    id = id.Substring(0, 8)
                Else
                    For i As Integer = id.Length - 1 To 7
                        id = "0" & id
                    Next
                End If
                Dim postData As String = id & "~nyteam~<RQTYPE>1<RTN>226076122<ACCT>" & acct & "<CKNUM>" & num & "<PDATE>" & pdate.ToString("yyyyMMdd") & "<AMT>" & amt
                Dim url As String = "https://checkimage.empirecorp.org/imagerequest.asp?" & System.Web.HttpUtility.UrlEncode(postData)
                Dim req As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)

                req.KeepAlive = False
                'req.Accept = "text/html, *image/gif, image/jpeg, */*"
                req.ContentType = "*application/x-www-form-urlencoded"
                req.Method = "POST"
                req.ContentLength = postData.Length
                Dim newStream As System.IO.Stream
                Try
                    newStream = req.GetRequestStream()
                Catch ex As System.Exception
                    Exit Sub
                End Try
                newStream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(postData), 0, postData.Length)
                newStream.Close()
                Response.ClearContent()
                Dim res As System.Net.HttpWebResponse = req.GetResponse()

                Dim reader As System.IO.Stream = res.GetResponseStream()
                Dim b(1000) As Byte
                Dim len As Integer = 1
                Dim stream As New IO.MemoryStream()
                Dim writer As New System.IO.BinaryWriter(stream)
                While (len > 0)
                    len = reader.Read(b, 0, b.Length)
                    If (len > 0) Then
                        writer.Write(b, 0, len)
                    End If
                End While
                writer.Close()
                reader.Close()

                Response.ContentType = "image/jpeg"
                'Response.ContentType = "text/plain"
                Response.BinaryWrite(stream.ToArray())
                'Response.Write(url)
                Response.Flush()
                Response.Close()
            End If
        Catch ex As System.Exception

        End Try


    End Sub

End Class