Imports Tf.AllV2.TCP
Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals

Partial Class StopChecks
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents InfErr As Render

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected ErrorMsg As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (IsPostBack()) Then
            Try
                Dim Request As New CuObjectsShr.Params.StopPayment
                Dim Check As New CuObjectsShr.Params.StopPayment.Check
                Check.Date = Date.Today().AddDays(30)
                If (txtAmount.Text = "" AndAlso txtBegNum.Text = "" AndAlso txtEndNum.Text = "") Then
                    wInfo.Visible = False
                    wPnlErr.Visible = True
                    wPnlErr.Text = "You must enter valid information to proceed."
                    Exit Sub
                End If
                If (txtAmount.Text <> "") Then Check.Amount = txtAmount.Text
                If (txtBegNum.Text <> "") Then
                    Check.BegNum = txtBegNum.Text
                    If (txtEndNum.Text <> "") Then
                        Check.EndNum = txtEndNum.Text
                    Else
                        Check.EndNum = Check.BegNum
                    End If
                    If (Check.EndNum < Check.BegNum) Then
                        Check.BegNum = Check.EndNum
                        Check.EndNum = txtBegNum.Text
                    End If
                End If

                Dim Acct() As String = lstAccts.SelectedItem.Text.Split(" ")
                Request.Acct.AcctNu = Acct(0)
                Request.Acct.Type = TfEnumerators.EAcctType.Checking
                Request.Acct.Suffix = Acct(2)
                Request.Checks.Add(Check)

                Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()

                Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "StopChecks"))
                XWrite.WriteStartElement("Req")
                Request.Save(XWrite)
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Response) Then
                    wPnlErr.Visible = False
                    wInfo.Visible = True
                    txtAmount.Text = ""
                    txtBegNum.Text = ""
                    txtEndNum.Text = ""
                Else
                    wInfo.Visible = False
                    wPnlErr.Visible = True
                    Dim SR As New IO.StreamReader(PRead)
                    wPnlErr.Text = SR.ReadToEnd()
                    SR.Close()
                End If
                PRead.Close()
            Catch ex As System.Exception
                wPnlErr.Visible = True
                wPnlErr.Text = ex.Message
            End Try
        Else
            Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts
            Dim Acct As Hbk.Member
            Dim SubAcct As Hbk.Acct

            Try
                ' Populate History Acct List
                For Each Acct In Accts
                    If (Acct.Permission = Hbk.EHbkPermission.High) Then
                        For Each SubAcct In Acct.SubAccts
                            If (SubAcct.Type = "Checking") Then
                                lstAccts.Items.Add(Acct.Number & " " & SubAcct.Type & " " & SubAcct.Suffix)
                            End If
                        Next
                    End If
                Next
            Catch ex As System.Exception

            End Try

        End If
    End Sub

End Class
