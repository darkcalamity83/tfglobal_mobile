Imports Tf.All
Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2.TCP

Partial Class LoanApp
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
            'Put user code to initialize the page here
            If (Not IsPostBack()) Then
                Dim LnTypes() As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("LnTypes").Split("|")
                Dim LnType As String
                Dim Arr() As String

                For Each LnType In LnTypes
                    Arr = LnType.Split(",")
                    lstType.Items.Add(New ListItem(Arr(0), Arr(1)))
                Next
            Else
                Dim LnApp As New CuObjectsLn.Loan.Application
                Try
                    Dim Acct As Hbk.Member = Hbk.MyAccts(0)
                    LnApp.Acct = Acct.Number
                    LnApp.AmtReq = txtAmount.Text
                    LnApp.TypeCode = lstType.SelectedValue
                    LnApp.Term = txtTerm.Text

                    Dim Applicant As New CuObjectsLn.Loan.Applicant
                    Applicant.Acct = Acct.Number
                    Applicant.CellPhone = txtPhone.Text
                    Applicant.Email = txtEmail.Text

                    If (txtRent.Text <> "") Then
                        Dim Debt As New CuObjectsLn.Loan.Debt
                        Debt.MonthlyPmt = txtRent.Text
                        Debt.Desc = "Monthly rent or mortgage"
                        Applicant.Debts.Add(Debt)
                    End If
                    Applicant.License = txtLicense.Text

                    If (txtIncome.Text <> "") Then
                        Applicant.CurrEmployment.Pay = txtIncome.Text
                        Applicant.CurrEmployment.PayPeriod = TfEnumerators.ELnFrequency.Annually
                        Applicant.CurrEmployment.Address = txtEmpAddr.Text
                        Applicant.CurrEmployment.City = txtEmpCity.Text
                        Applicant.CurrEmployment.State = txtEmpState.Text
                        If (txtEmpZIP.Text <> "") Then Applicant.CurrEmployment.Zip = txtEmpZIP.Text
                    End If

                    LnApp.Applicants.Add(Applicant)

                    If (txtFirstName.Text <> "") Then
                        Applicant = New CuObjectsLn.Loan.Applicant
                        Applicant.FirstName = txtFirstName.Text
                        Applicant.LastName = txtLastName.Text
                        If (txtSSN.Text <> "") Then Applicant.SSN = txtSSN.Text
                        LnApp.Applicants.Add(Applicant)
                    End If
                Catch ex1 As System.Exception
                    Me.lblErr.Text = ex1.Message
                    Me.wPnlErr.Visible = True
                    Exit Sub
                End Try

                Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()
                Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "HbkLn"))
                XWrite.WriteStartElement("App")
                LnApp.Save(XWrite)
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim ErrMsg As String = ""
                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    Dim SR As New IO.StreamReader(PRead)
                    ErrMsg = SR.ReadToEnd()
                    SR.Close()
                Else
                End If
                'LnApp.AmtReq = txtAmount.Text
                Me.pnlApp.Visible = False
                Dim Msg As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("LnAppMsg")
                If (Msg Is Nothing OrElse Msg = "") Then Msg = "Thank you for applying.  Your loan application has been submitted and will be reviewed by a loan officer."
                If ErrMsg IsNot Nothing AndAlso ErrMsg.Trim <> "" Then
                    Msg = ErrMsg
                End If
                Me.wInfo.Text = Msg
                Me.wInfo.Visible = True
            End If
        Catch ex As System.Exception
        End Try
    End Sub

End Class
