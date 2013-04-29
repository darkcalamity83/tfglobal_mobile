Imports tf.AllV2.TfGlobals
Imports tf.AllV2.TCP
Imports tf.AllV2

Partial Class PendingTransactions
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Protected WithEvents Bals As Render
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1)
            Response.Expires = 0
            Response.CacheControl = "no-cache"
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1))
        Catch ex As System.Exception
        End Try

    End Sub

    Private Sub PendingTransfers_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles PendingTransfers.OnRender
        Try
            Hbk.RenderPendingTransfers(Writer)
        Catch ex As System.Exception
        End Try
    End Sub

    Private Sub PendingACH_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles PendingACH.OnRender
        Try
            Hbk.RenderPendingACH(Writer)
        Catch ex As System.Exception
        End Try
    End Sub

    Private Sub PendingInfo_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles PendingInfo.OnRender
        Try
            Dim CheckRemove As String = Request.QueryString.Item("remove")
            If CheckRemove IsNot Nothing AndAlso CheckRemove.Length > 0 Then
                If IsNumeric(CheckRemove) Then
                    Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()
                    Dim Xfr As New CuObjectsShr.Params.Transfer
                    Xfr.Options = New CuObjectsShr.Params.Options
                    Xfr.Options.Reference = CheckRemove
                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "DeleteFutureTransfer"))
                    XWrite.WriteStartElement("Transfer")
                    Xfr.Save(XWrite)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                        Dim SR As New IO.StreamReader(PRead)
                        Dim errmsg As String
                        errmsg = SR.ReadToEnd()
                        SR.Close()
                        Hbk.BeginInfo(Writer)
                        Writer.Write(errmsg)
                        Hbk.EndInfo(Writer)
                    Else
                        Dim SR As New IO.StreamReader(PRead)
                        Dim msg As String
                        msg = SR.ReadToEnd()
                        SR.Close()
                        Hbk.BeginInfo(Writer)
                        Writer.Write("You Successfully removed the pending transfer.")
                        Hbk.EndInfo(Writer)
                    End If
                End If
            End If
        Catch ex As System.Exception
            Hbk.RenderError("Please log back in.", Writer)
            Exit Sub
        End Try
        Try
            Dim CheckRetry As String = Request.QueryString.Item("retry")
            If CheckRetry IsNot Nothing AndAlso CheckRetry.Length > 0 Then
                If IsNumeric(CheckRetry) Then
                    Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()
                    Dim Xfr As New CuObjectsShr.Params.Transfer
                    Xfr.Options = New CuObjectsShr.Params.Options
                    Xfr.Options.Reference = CheckRetry
                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "RetryFutureTransfer"))
                    XWrite.WriteStartElement("Transfer")
                    Xfr.Save(XWrite)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                        Dim SR As New IO.StreamReader(PRead)
                        Dim errmsg As String
                        errmsg = SR.ReadToEnd()
                        SR.Close()
                        Hbk.BeginInfo(Writer)
                        Writer.Write(errmsg)
                        Hbk.EndInfo(Writer)
                    Else
                        Dim SR As New IO.StreamReader(PRead)
                        Dim msg As String
                        msg = SR.ReadToEnd()
                        SR.Close()
                        Hbk.BeginInfo(Writer)
                        Writer.Write("The transfer has been reset to attempt to transfer in 1 hour.")
                        Hbk.EndInfo(Writer)
                    End If
                End If
            End If
        Catch ex As System.Exception
            Hbk.RenderError("Please log back in.", Writer)
        End Try
    End Sub
End Class
