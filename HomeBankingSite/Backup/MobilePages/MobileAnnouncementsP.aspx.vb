Imports Tf.All
Imports FixedRecordV1
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2

Partial Class MobileAnnouncementsP
    Inherits System.Web.UI.MobileControls.MobilePage

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim token As String = Request.QueryString("token")
        Dim username As String = Request.QueryString("username")
        lnkBal.NavigateUrl = "MobileBalancesP.aspx?username=" & username & "&token=" & token
        lnkTran.NavigateUrl = "MobileTransfersP.aspx?username=" & username & "&token=" & token
        lnkAnnounce.NavigateUrl = "MobileAnnouncementsP.aspx?username=" & username & "&token=" & token
        lnkChange.NavigateUrl = "MobileChangeLoginP.aspx?username=" & username & "&token=" & token
        TfLog.Log.MyLog.Write("", "Hbk.RenderAnnouncements")
        Dim ErrMsg As String = Nothing
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Dim XWrite As Sys.Data.Xml.TextWriter
        Dim aMsgs As New Hbk.HbkMsgs
        Dim PRead As TCP.NetTalk.Plain.Reader

        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New Hbk.OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("Type", "Quick")
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
        Catch ex As System.Exception
            Dim err As String = "An error has occurred."
        End Try

        aMsgs.Page = "login"

        XWrite = New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetMsgs"))
        XWrite.WriteStartElement("HbkMsgs")
        aMsgs.HtmlMsgs = "text"
        aMsgs.Save(XWrite)
        XWrite.WriteEndElement()
        XWrite.Close()

        PRead = Client.newReader()
        If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
            Dim SR As New IO.StreamReader(PRead)
            lblerr.Text = SR.ReadToEnd()
            SR.Close()
        Else
            Dim XRead As New Sys.Data.Xml.TextReader(PRead)
            aMsgs.Load(XRead)
            XRead.Close()

            For Each Msg As String In aMsgs.HtmlMsgs.Split("|")
                Msg = Msg.Replace("<b>", "").Replace("</b>", "")
                Me.announce.Items.Add(New System.Web.UI.MobileControls.MobileListItem(Msg, ""))
                Me.announce.Items.Add(New System.Web.UI.MobileControls.MobileListItem("", ""))
            Next

        End If
    End Sub
End Class
