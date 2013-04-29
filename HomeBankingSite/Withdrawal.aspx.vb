Imports Tf.All
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2
Imports Tf.AllV2.TCP

Partial Class Withdrawal
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
            Response.ExpiresAbsolute = #1/1/1980#
            Response.AddHeader("cache-control", "no-store, must-revalidate, private")
            Response.AddHeader("Pragma", "no-cache")
            Response.CacheControl = "no-cache"
            Response.Expires = -1

            Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts
            Dim Acct As Hbk.Member
            Dim SubAcct As Hbk.Acct
            Dim Itm As String
            Dim XfrFrom As String() = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("XfrFrom").Split("|")

            ' Populate History Acct List
            For Each Acct In Accts
                If (Acct.Permission = Hbk.EHbkPermission.High) Then
                    For Each SubAcct In Acct.SubAccts
                        Itm = Acct.Number & " " & SubAcct.Type & " " & SubAcct.Suffix
                        If (Hbk.CanXfer(SubAcct, XfrFrom)) Then lstFromAcct.Items.Add(Itm)
                    Next
                    lstFromAcct.Items.Add("")
                End If
            Next

            If (IsPostBack) Then
                lstFromAcct.Items.FindByText(Request.Form.Item("lstFromAcct")).Selected = True
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
                If (FromItm Is Nothing OrElse FromItm.Text = "") Then
                    Hbk.RenderError("You must select an account from the 'From Acct:' list.", Writer)
                    Exit Sub
                End If
                If (Not IsNumeric(txtAmount.Text)) Then
                    Hbk.RenderError("You must enter a valid amount in the 'Amount:' field.", Writer)
                    Exit Sub
                End If
                Dim FromAcct() As String = FromItm.Text.Split(" ")

                Dim Wtd As New CuObjectsShr.Params.Withdrawal
                Dim Check As New CuObjectsShr.Params.Withdrawal.Check
                Dim Acct As New CuObjectsShr.Params.Withdrawal.Account
                'Dim Acct As New CuObjectsShr.Params.Deposit.Account

                Acct.AcctNu = FromAcct(0)
                If (FromAcct(1) = "LOC") Then FromAcct(1) = "Loan"
                Acct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), FromAcct(1))
                Acct.Suffix = FromAcct(2)
                Acct.Amt = txtAmount.Text
                'Acct.FndType = ETrxFundTypes.C
                Check.Amt = txtAmount.Text
                Wtd.Options.Message = txtMemo.Text

                ' Group status the account to get the address information...
                Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()
                Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                XWrite.WriteStartElement("Accts")
                XWrite.WriteTag("AcctNu", Acct.AcctNu.ToString())
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim Mbr As CuObjects.Member
                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    Dim SR As New IO.StreamReader(PRead)
                    Hbk.RenderError(SR.ReadToEnd(), Writer)
                    SR.Close()
                    Exit Sub
                Else
                    Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                    Mbr = New CuObjects.Member
                    Mbr.Load(XRead)
                    XRead.Close()
                End If

                Check.Line2 = Mbr.Address
                Check.Line1 = Mbr.Title & " " & Mbr.FirstName & "  " & Mbr.MiddleName & " " & Mbr.LastName & " " & Mbr.Suffix
                Check.Line3 = Mbr.City & " " & Mbr.State & " " & Mbr.Zip

                If (txtMemo.Text = "") Then
                    Check.Message = "HomeBanking Withdrawal."
                    Wtd.Options.Message = "HomeBanking Withdrawal."
                Else
                    Check.Message = txtMemo.Text
                    Wtd.Options.Message = txtMemo.Text
                End If
                Wtd.Accts.Add(Acct)

                Check.AcctBrch = Mbr.Brch
                Check.AcctNum = Mbr.AcctNu
                Check.AcctSfx = Acct.Suffix
                Check.AcctType = Acct.Type

                Wtd.Checks.Add(Check)
                Wtd.Member = Mbr
                'Wtd.CheckTotal = Check.Amt

                XWrite = New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Withdrawal"))
                XWrite.WriteStartElement("Wtd")
                Wtd.Save(XWrite)
                XWrite.WriteEndElement()
                XWrite.Close()

                PRead = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Response) Then
                    ' Good?
                    Hbk.BeginInfo(Writer)
                    Writer.Write("The withdrawal of <b>")
                    Writer.Write(CSng(txtAmount.Text).ToString("C"))
                    Writer.Write("</b> from [")
                    Writer.Write(lstFromAcct.SelectedItem.Text)
                    Writer.Write("] was <b>successful</b>.")
                    Hbk.EndInfo(Writer)
                    PRead.Close()
                Else
                    Dim SR As New IO.StreamReader(PRead)
                    Hbk.RenderError(SR.ReadToEnd(), Writer)
                    SR.Close()
                End If
            End If
        Catch ex As System.Exception
            Hbk.RenderError("Please log back in.", Writer)
        End Try

    End Sub

End Class
