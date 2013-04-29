Imports Tf.AllV2.TCP
Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals

Partial Class Transfer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Bals As Render
    Protected WithEvents XfrInfo As Render
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)

            Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts
            Dim Acct As Hbk.Member
            Dim SubAcct As Hbk.Acct
            Dim Itm As String
            Dim XfrFrom As String() = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("XfrFrom").Split("|")
            Dim XfrTo As String() = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("XfrTo").Split("|")

            ' Populate History Acct List
            For Each Acct In Accts
                If (Acct.Permission = Hbk.EHbkPermission.High) Then
                    For Each SubAcct In Acct.SubAccts
                        Itm = Acct.Number & " " & SubAcct.Type & " " & SubAcct.Suffix
                        If (Hbk.CanXfer(SubAcct, XfrFrom)) Then lstFromAcct.Items.Add(Itm)
                        If (Hbk.CanXferTo(SubAcct, XfrTo)) Then lstToAcct.Items.Add(Itm)
                    Next
                    lstToAcct.Items.Add("")
                    lstFromAcct.Items.Add("")
                Else
                    For Each SubAcct In Acct.SubAccts
                        Itm = Acct.Number & " " & SubAcct.Type & " " & SubAcct.Suffix
                        If (Hbk.CanXferTo(SubAcct, XfrTo)) Then lstToAcct.Items.Add(Itm)
                    Next
                    lstToAcct.Items.Add("")
                End If
            Next

            If (IsPostBack) Then
                lstToAcct.Items.FindByText(Request.Form.Item("lstToAcct")).Selected = True
                lstFromAcct.Items.FindByText(Request.Form.Item("lstFromAcct")).Selected = True
                lstHours.Items.FindByText(Request.Form.Item("lstHours")).Selected = True
            Else
                txtFDate.Text = Now.ToShortDateString
                lstHours.SelectedIndex = 0
            End If
        Catch ex As System.Exception

        End Try

    End Sub
    Private Sub Bals_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Bals.OnRender
        Try
            Hbk.RenderBalances(Writer)
        Catch ex As System.Exception

        End Try

    End Sub
    Private Sub XfrInfo_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles XfrInfo.OnRender
        Try
            If (IsPostBack()) Then
                ' Attempt a transfer...
                Dim FromItm As Web.UI.WebControls.ListItem = lstFromAcct.SelectedItem
                Dim isFutureTran As Boolean = False
                If (FromItm Is Nothing OrElse FromItm.Text = "") Then
                    Hbk.RenderError("You must select an account from the 'From Acct:' list.", Writer)
                    Exit Sub
                End If
                Dim ToItm As Web.UI.WebControls.ListItem = lstToAcct.SelectedItem
                If (ToItm Is Nothing OrElse ToItm.Text = "") Then
                    Hbk.RenderError("You must select an account from the 'To Acct:' list.", Writer)
                    Exit Sub
                End If
                If (ToItm.Text = FromItm.Text) Then
                    Hbk.RenderError("The from account and to account cannot be the same.", Writer)
                    Exit Sub
                End If
                If (Not IsNumeric(txtAmount.Text)) Then
                    Hbk.RenderError("You must enter a valid amount in the 'Amount:' field.", Writer)
                    Exit Sub
                End If
                If txtAmount.Text <= 0 Then
                    Hbk.RenderError("You must enter a amount greater than 0.", Writer)
                    Exit Sub
                End If
                Dim FuturePostCntLimit As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("FutureTransCntLimit")
                Dim FuturePostDateLimit As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("FutureTransDateLimit")
                Dim FuturePostAmtLimit As Decimal = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("FutureTransAmtLimit")

                If txtFDate.Text.Trim.Length = 0 Then
                    Hbk.RenderError("You must enter a valid date to post the transfer(must be within " & FuturePostDateLimit & " days of the current day).", Writer)
                    Exit Sub
                End If
                If Not IsDate(txtFDate.Text.Trim) Then
                    Hbk.RenderError("You must enter a valid date to post the transfer(must be within " & FuturePostDateLimit & " days of the current day).", Writer)
                    Exit Sub
                End If
                Dim FullDate As Date = Now
                If Not (txtFDate.Text.Trim = Now.ToShortDateString.Split(" ")(0) AndAlso lstHours.SelectedValue = "AnyTime") Then
                    FullDate = CDate(txtFDate.Text.Trim & " " & IIf(lstHours.SelectedValue = "AnyTime", "1:00:00 AM", lstHours.SelectedValue))
                    If FullDate < Now Then
                        Hbk.RenderError("The date to post the transfer must be today or a future date within " & FuturePostDateLimit & " days of the today.", Writer)
                        Exit Sub
                    End If
                    If FullDate > Now.AddDays(FuturePostDateLimit) Then
                        Hbk.RenderError("The date to post the transfer must be a must be a date within " & FuturePostDateLimit & " days of the current day.", Writer)
                        Exit Sub
                    End If
                    If FullDate > Now AndAlso FullDate <= Now.AddDays(FuturePostDateLimit) Then
                        If CDec(txtAmount.Text) > FuturePostAmtLimit Then
                            Hbk.RenderError("When setting a transfer to post for a future date you may not exceed $" & FuturePostDateLimit.ToString("C") & ".", Writer)
                            Exit Sub
                        Else
                            isFutureTran = True
                        End If
                    End If
                Else
                    isFutureTran = False
                End If

                Dim FromAcct() As String = FromItm.Text.Split(" ")
                Dim ToAcct() As String = ToItm.Text.Split(" ")

                Dim Xfr As New CuObjectsShr.Params.Transfer
                Dim FAcct As New CuObjectsShr.Params.Transfer.Account
                Dim TAcct As New CuObjectsShr.Params.Transfer.Account
                FAcct.AcctNu = FromAcct(0)
                If (FromAcct(1) = "LOC") Then FromAcct(1) = "Loan"
                FAcct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), FromAcct(1))
                FAcct.Suffix = FromAcct(2)
                FAcct.Amt = txtAmount.Text
                Xfr.FromAccts.Add(FAcct)

                TAcct.AcctNu = ToAcct(0)
                If (ToAcct(1) = "LOC") Then ToAcct(1) = "Loan"
                TAcct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), ToAcct(1))
                TAcct.Suffix = ToAcct(2)
                TAcct.Amt = txtAmount.Text
                Xfr.ToAccts.Add(TAcct)

                If (txtMemo.Text <> "") Then
                    Xfr.Options.Message = txtMemo.Text
                Else
                    'Xfr.Options.Message = FromItm.Text & " to " & ToItm.Text  '"Home banking transfer."
                    Xfr.Options.Message = ""
                End If

                Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()

                Dim Acct As Hbk.Member = Hbk.MyAccts(0)
                Xfr.Options.Username = Acct.Number
                'Xfr.TotalAmt = txtAmount.Text

                If isFutureTran Then
                    Xfr.Options.Password = FullDate

                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "SetFutureTransfer"))
                    XWrite.WriteStartElement("Xfr")
                    Xfr.Save(XWrite)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Response) Then
                        Hbk.BeginInfo(Writer)
                        Writer.Write("The transfer of <b>")
                        Writer.Write(CDec(txtAmount.Text).ToString("C"))
                        Writer.Write("</b> from <b>[")
                        Writer.Write(lstFromAcct.SelectedItem.Text)
                        Writer.Write("]</b> to <b>[")
                        Writer.Write(lstToAcct.SelectedItem.Text)
                        Writer.Write("]</b> was set to post on <b>" & FullDate & "</b>.")
                        Hbk.EndInfo(Writer)
                        PRead.Close()
                    Else
                        Dim SR As New IO.StreamReader(PRead)
                        Hbk.RenderError(SR.ReadToEnd(), Writer)
                        SR.Close()
                    End If
                Else
                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Transfer"))
                    XWrite.WriteStartElement("Xfr")
                    Xfr.Save(XWrite)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Response) Then
                        ' Good?
                        Hbk.BeginInfo(Writer)
                        Writer.Write("The transfer of <b>")
                        Writer.Write(CDec(txtAmount.Text).ToString("C"))
                        Writer.Write("</b> from [")
                        Writer.Write(lstFromAcct.SelectedItem.Text)
                        Writer.Write("] to [")
                        Writer.Write(lstToAcct.SelectedItem.Text)
                        Writer.Write("] was <b>successful</b>.")
                        Hbk.EndInfo(Writer)
                        PRead.Close()
                    Else
                        Dim SR As New IO.StreamReader(PRead)
                        Hbk.RenderError(SR.ReadToEnd(), Writer)
                        SR.Close()
                    End If
                End If
            End If
            'End If
        Catch ex As System.Exception
            Hbk.RenderError("Please log back in.", Writer)

        End Try

    End Sub
End Class
