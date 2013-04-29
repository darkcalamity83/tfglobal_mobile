Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2.TCP

Partial Class MobileHistoryP
    Inherits System.Web.UI.MobileControls.MobilePage
    Dim token As String
    Dim username As String
    Dim Selected As String
    Dim DT As String

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            token = Request.QueryString("token")
            username = Request.QueryString("username")
            Selected = Request.QueryString.Item("Acct")
            'DT = Request.QueryString.Item("Dt")
            Dim ViewAmount As String = "10"
            Dim Acct() As String = Selected.Split(" ")
            Dim HReq As New CuObjectsShr.Params.History
            lnkBal.NavigateUrl = "MobileBalancesP.aspx?username=" & username & "&token=" & token
            lnkTran.NavigateUrl = "MobileTransfersP.aspx?username=" & username & "&token=" & token
            lnkAnnounce.NavigateUrl = "MobileAnnouncementsP.aspx?username=" & username & "&token=" & token
            lnkChange.NavigateUrl = "MobileChangeLoginP.aspx?username=" & username & "&token=" & token


            HReq.Acct.AcctNu = CInt(Acct(0))
            If (Acct(1) <> "LOC") Then
                HReq.Acct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), Acct(1))
            Else
                HReq.Acct.Type = TfEnumerators.EAcctType.Loan
            End If
            HReq.Acct.Suffix = CInt(Acct(2))
            HReq.Reset = (Me.IsPostBack OrElse Request.QueryString.Item("Reset") = "T")

            HReq.EndDate = Date.Today()

            HReq.NumRecs = 10

            Dim Client As TCP.NetTalk.NetStream = Hbk.ValidateStream(token, username)
            Dim Alt As Boolean

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "History"))
            XWrite.WriteStartElement("Hist")
            HReq.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim ErrMsg As String = ""

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                lblerr.Text = ErrMsg
            Else
                Dim MbrHist As New CuObjectsShr.Account.History
                Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                MbrHist.Load(XRead)
                XRead.Close()

                If MbrHist.EOF Then
                    Me.next1.Visible = False
                Else
                    Me.next1.Visible = True
                End If
                user1.Text = Selected

                For Each item As CuObjectsShr.Account.History.Item In MbrHist.Hists
                    Dim hValue As String = ""
                    Dim hText As String = ""
                    hText = "A " & item.Transaction & " of $" & item.Amt.ToString("C").Replace("$", "") & _
                    " was done on " & item.Date.ToShortDateString & ". "
                    If item.Check <> 0 Then
                        hText += "CheckNu is " & item.Check & ". "
                    End If
                    If item.Desc.Trim.Length > 0 Then
                        hText += "(" & item.Desc.PadRight(20, " ").Substring(0, 20) & ")"
                    End If
                    history1.Items.Add(New System.Web.UI.MobileControls.MobileListItem(hText, hValue))
                    history1.Items.Add(New System.Web.UI.MobileControls.MobileListItem("", ""))
                Next

            End If
        Catch ex As System.Exception
            lblerr.Text = "An error has occurred."
        End Try
    End Sub

    Private Sub reset1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles reset1.Click
        Dim mValue As String = ""
        mValue = "MobileHistoryP.aspx?Acct="
        mValue += Selected
        mValue += "&Reset=T&username=" & username & "&token=" & token
        Response.Redirect(mValue)
    End Sub

    Private Sub next1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles next1.Click
        Dim mValue As String = ""
        mValue = "MobileHistoryP.aspx?Acct="
        mValue += Selected
        mValue += "&Reset=F&username=" & username & "&token=" & token
        Response.Redirect(mValue)
    End Sub
End Class
