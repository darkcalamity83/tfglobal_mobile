Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2.TCP

Partial Class MoneyDesktop
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents Hist As Render

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
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

            Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts
            Dim Acct As Hbk.Member
            Dim SubAcct As Hbk.Acct

            ' Populate History Acct List
            For Each Acct In Accts
                If (Acct.Permission <> Hbk.EHbkPermission.Low) Then
                    For Each SubAcct In Acct.SubAccts
                        lstAcct.Items.Add(Acct.Number & " " & SubAcct.Type & " " & SubAcct.Suffix)
                    Next
                    lstAcct.Items.Add("")
                End If
            Next

            Dim Selected As String
            Dim ViewAmount As String
            Dim PrintPage As String = ""

            If (Not IsPostBack) Then
                Selected = Request.QueryString.Item("Acct")
                txtDate.Text = Request.QueryString.Item("Dt")
                ViewAmount = Request.QueryString.Item("View")
                If (ViewAmount Is Nothing) Then ViewAmount = "10"
            Else
                Selected = Request.Form.Item("lstAcct")
                ViewAmount = Request.Form.Item("lstViewAmount")
            End If

            Dim Itm As Web.UI.WebControls.ListItem = lstAcct.Items.FindByText(Selected)
            If (Not Itm Is Nothing) Then Itm.Selected = True
            ViewAmount = ViewAmount.Replace("_", " ")
            Itm = lstViewAmount.Items.FindByText(ViewAmount)
            If (Not Itm Is Nothing) Then Itm.Selected = True
        Catch ex As System.Exception

        End Try

    End Sub

    Private Sub Hist_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Hist.OnRender
        Try
            ' Query String-Based History
            Dim Itm As Web.UI.WebControls.ListItem = lstAcct.SelectedItem
            If (Itm Is Nothing) Then Exit Sub
            Dim Acct() As String = Itm.Text.Split(" ")
            If (Acct.Length <> 3) Then Exit Sub

            Dim HReq As New CuObjectsShr.Params.History

            HReq.Acct.AcctNu = CInt(Acct(0))
            If (Acct(1) <> "LOC") Then
                HReq.Acct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), Acct(1))
            Else
                HReq.Acct.Type = TfEnumerators.EAcctType.Loan
            End If
            HReq.Acct.Suffix = CInt(Acct(2))
            HReq.Reset = (Me.IsPostBack OrElse Request.QueryString.Item("Reset") = "T")
            If (txtDate.Text <> "") Then
                txtDate.Text = Hbk.FormatDate(txtDate.Text, Date.Today().ToShortDateString())
                HReq.EndDate = txtDate.Text
            Else
                HReq.EndDate = Date.Today()
            End If

            Select Case lstViewAmount.SelectedValue
                Case "10"
                    HReq.NumRecs = 10
                Case "20"
                    HReq.NumRecs = 20
                Case "30"
                    HReq.NumRecs = 30
                Case "1mo"
                    HReq.BegDate = HReq.EndDate.AddMonths(-1)
                    HReq.NumRecs = 100
                Case "2mo"
                    HReq.BegDate = HReq.EndDate.AddMonths(-2)
                    HReq.NumRecs = 100
                Case "3mo"
                    HReq.BegDate = HReq.EndDate.AddMonths(-3)
                    HReq.NumRecs = 100
                Case Else
                    HReq.NumRecs = 10
            End Select

            Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()
            Dim Alt As Boolean

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "History"))
            XWrite.WriteStartElement("Hist")
            HReq.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                Hbk.RenderError(SR.ReadToEnd(), Writer)
                SR.Close()
            Else
                Dim MbrHist As New CuObjectsShr.Account.History
                Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                MbrHist.Load(XRead)
                XRead.Close()

                Writer.Write("<div class='PnlList'>")
                Writer.Write("<table cellpadding='0' cellspacing='0'>")
                Writer.Write("<tr><td class='Tab'><img src='TBar/Hist.gif' > <b>")
                Writer.Write(MbrHist.Acct.AcctNu)
                Writer.Write(" ")
                Writer.Write(Acct(1))
                Writer.Write(" ")
                Writer.Write(MbrHist.Acct.Suffix)
                Writer.Write("</b></td><td align='right'><a title='Go to beginning of history.' href='History.aspx?Acct=")
                Writer.Write(HReq.Acct.AcctNu)
                Writer.Write(" ")
                Writer.Write(HReq.Acct.Type.ToString())
                Writer.Write(" ")
                Writer.Write(HReq.Acct.Suffix)
                Writer.Write("&Reset=T'>Reset <img src='imgs/reset.gif' /></a>")
                Writer.Write("<a title='Print Your History.' href='PrintHistoryFreindly.aspx?Acct=")
                Writer.Write(HReq.Acct.AcctNu)
                Writer.Write(" ")
                Writer.Write(HReq.Acct.Type.ToString())
                Writer.Write(" ")
                Writer.Write(HReq.Acct.Suffix)
                Writer.Write("&View=" & lstViewAmount.SelectedItem.Text.Replace(" ", "_") & "&Dt=" & txtDate.Text & "'>Print <img src='imgs/reset.gif' /></a>")
                If (Not MbrHist.EOF) Then
                    Writer.Write("<a title='Click for more history.' href='History.aspx?Acct=")
                    Writer.Write(HReq.Acct.AcctNu)
                    Writer.Write(" ")
                    Writer.Write(HReq.Acct.Type.ToString())
                    Writer.Write(" ")
                    Writer.Write(HReq.Acct.Suffix)
                    If (txtDate.Text <> "") Then
                        Writer.Write("&Dt=")
                        Writer.Write(txtDate.Text)
                    End If
                    Writer.Write("&Reset=F")
                    If (lstViewAmount.SelectedValue <> "1mo" AndAlso lstViewAmount.SelectedValue <> "2mo" AndAlso lstViewAmount.SelectedValue <> "3mo") Then
                        Writer.Write("&View=")
                        Writer.Write(lstViewAmount.SelectedItem.Text.Replace(" ", "_"))
                    End If
                    Writer.Write("'>Next <img src='Imgs/Next.gif'></a>")
                End If
                Writer.Write("</td></tr><tr><td colspan='2'>")
                Writer.Write("<table cellpadding='0' cellspacing='0'>")
                Writer.Write("<tr><th width='10%'>Date</th><th width='15%'>Transaction</th><th width='15%'>Amt</th><th width='15%'>Balance</th><th width='10%'>Check</th><th width='25%'>Description</th>")
                If (HReq.Acct.Type = TfEnumerators.EAcctType.Checking) Then
                    Writer.Write("<th>&nbsp;</th>")
                End If
                Writer.Write("</tr>")

                Dim HItm As CuObjectsShr.Account.History.Item
                For Each HItm In MbrHist.Hists
                    Writer.Write("<tr class='")
                    If (Alt) Then
                        Writer.Write("Row1")
                    Else
                        Writer.Write("Row2")
                    End If
                    Writer.Write("'><td align='center'>")
                    Writer.Write(HItm.Date.ToShortDateString())
                    Writer.Write("</td><td>")
                    Writer.Write(HItm.Transaction)
                    Writer.Write("</td><td align='right'>")
                    Writer.Write(HItm.Amt.ToString("C"))
                    Writer.Write("</td><td align='right'>")
                    Writer.Write(HItm.Bal.ToString("C"))
                    Writer.Write("</td><td align='right'>")
                    If (HReq.Acct.Type = TfEnumerators.EAcctType.Checking AndAlso Not HItm.Check = 0) Then
                        Writer.Write(HItm.Check)
                    Else
                        Writer.Write("&nbsp;")
                    End If
                    Writer.Write("</td><td align='left'>")
                    If (Not HItm.TransferAcct Is Nothing AndAlso HItm.TransferAcct.AcctNu <> 0 AndAlso HItm.TransactionCode.StartsWith("WT")) Then
                        Writer.Write("to " & HItm.TransferAcct.AcctNu & " " & HItm.TransferAcct.Type.ToString().ToLower() & " " & IIf(HItm.TransferAcct.Suffix <> 0, HItm.TransferAcct.Suffix, "") & IIf(HItm.Desc <> "", " (" & HItm.Desc.ToLower() & ")", ""))
                    ElseIf (Not HItm.TransferAcct Is Nothing AndAlso HItm.TransferAcct.AcctNu <> 0 AndAlso HItm.TransactionCode.StartsWith("DT")) Then
                        Writer.Write("from " & HItm.TransferAcct.AcctNu & " " & HItm.TransferAcct.Type.ToString().ToLower() & " " & IIf(HItm.TransferAcct.Suffix <> 0, HItm.TransferAcct.Suffix, "") & IIf(HItm.Desc <> "", " (" & HItm.Desc.ToLower() & ")", ""))
                    ElseIf (HItm.Desc <> "") Then
                        Writer.Write(HItm.Desc.ToLower())
                    Else
                        Writer.Write("&nbsp;")
                    End If
                    Writer.Write("</td>")
                    If (MbrHist.Acct.Type = TfEnumerators.EAcctType.Checking) Then
                        Writer.Write("<td class=""viewCheck"">")
                        If (Not HItm.Check = 0) Then
                            Writer.Write("<a href=""ViewCheck.aspx?acct=" & Hbk.FullCheckingAccountNumber(MbrHist.Acct.AcctNu.ToString()) & "&num=" & HItm.Check.ToString() & "&date=" & HItm.Date.ToShortDateString() & "&amt=" & System.Math.Abs(HItm.Amt).ToString("C").Replace("$", "").Replace(".", "").Replace(",", "") & """ target=""_blank"">view check</a>")
                        Else
                            Writer.Write("&nbsp;")
                        End If
                        Writer.Write("</td>")
                    End If
                    Writer.Write("</tr>")

                    Alt = Not Alt
                Next
                Writer.Write("</table></td></tr></table></div>")
            End If
        Catch ex As System.Exception

        End Try

    End Sub

End Class
