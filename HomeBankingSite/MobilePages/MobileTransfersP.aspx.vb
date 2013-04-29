'Imports Tf.All
'Imports FixedRecordV1
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2
Imports Tf.AllV2.TCP

Partial Class MobileTransfersP
    Inherits System.Web.UI.MobileControls.MobilePage
    Public Client As TCP.NetTalk.NetStream
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.ExpiresAbsolute = #1/1/1980#
        'Response.AddHeader("cache-control", "no-store, must-revalidate, private")
        'Response.AddHeader("Pragma", "no-cache")
        'Response.CacheControl = "no-cache"
        'Response.Expires = -1

        Try

            'TfLog.Log.MyLog.Write("", "Hbk.RenderBalances")
            Dim token As String = Request.QueryString("token")
            Dim username As String = Request.QueryString("username")
            Client = Hbk.ValidateStream(token, username)
            If Client Is Nothing Then
                Response.Redirect("MobileLoginPage.aspx")
                Exit Sub
            End If

            Dim Accts As Sys.data.XMLArray = Hbk.GetAccts(Client, username)
            Dim Acct As Hbk.Member
            Dim Alt As Boolean
            Dim MbrAcct As CuObjectsShr.Account.ShrGeneric
            Dim ErrMsg As String = Nothing
            Dim SubAcct As Hbk.Acct
            Dim AddSubAcct As Boolean

            lnkBal.NavigateUrl = "MobileBalancesP.aspx?username=" & username & "&token=" & token
            lnkTran.NavigateUrl = "MobileTransfersP.aspx?username=" & username & "&token=" & token
            lnkChange.NavigateUrl = "MobileChangeLoginP.aspx?username=" & username & "&token=" & token
            lnkAnnounce.NavigateUrl = "MobileAnnouncementsP.aspx?username=" & username & "&token=" & token

            For i As Integer = 0 To 3
                If i > Accts.Count - 1 Then
                    Exit For
                End If
                Acct = Accts(i)

                If (Acct.Permission <> Hbk.EHbkPermission.Low OrElse Acct.SubAccts Is Nothing) Then
                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                    XWrite.WriteStartElement("Accts")
                    XWrite.WriteTag("AcctNu", Acct.Number)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                        Dim SR As New IO.StreamReader(PRead)
                        ErrMsg = SR.ReadToEnd()
                        SR.Close()
                    Else
                        Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                        Dim Mbr As New CuObjects.Member
                        Mbr.Load(XRead)
                        AddSubAcct = (Acct.SubAccts Is Nothing)
                        If (AddSubAcct) Then Acct.SubAccts = New ArrayList

                        Alt = False
                        For Each MbrAcct In Mbr.Accts
                            If (MbrAcct.Type = TfEnumerators.EAcctType.Loan AndAlso IsNumeric(MbrAcct.m_Items("Avail"))) Then
                            Else
                                SubAcct = New Hbk.Acct(MbrAcct.Type.ToString(), MbrAcct.Suffix)
                                Dim mValue As String = ""
                                mValue += MbrAcct.AcctNu.ToString
                                mValue += " "
                                mValue += SubAcct.Type
                                mValue += " "
                                mValue += MbrAcct.Suffix.ToString
                                tranto.Items.Add(New System.Web.UI.MobileControls.MobileListItem(mValue, mValue))
                                tranfrom.Items.Add(New System.Web.UI.MobileControls.MobileListItem(mValue, mValue))
                            End If
                        Next
                        tranto.Items.Add(New System.Web.UI.MobileControls.MobileListItem(" ", ""))
                        tranfrom.Items.Add(New System.Web.UI.MobileControls.MobileListItem(" ", ""))
                    End If
                End If
            Next
        Catch ex As System.Exception
            response1.Text = "An error has occurred."
        End Try
    End Sub

    Private Sub btntransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        Try
            Dim strTo As String = ""
            Dim strFrom As String = ""
            Dim strAmt As String = ""
            Dim strDesc As String = ""
            If tranto.Selection.Value.Trim <> "" Then
                strTo = tranto.Selection.Value.Trim
            Else
                Exit Sub
            End If
            If tranfrom.Selection.Value.Trim <> "" Then
                strFrom = tranfrom.Selection.Value
            Else
                Exit Sub
            End If
            If IsNumeric(txtamt.Text.Trim.Replace("$", "").Replace(",", "")) AndAlso CLng(txtamt.Text.Trim.Replace("$", "").Replace(",", "")) > 0 Then
                strAmt = txtamt.Text.Trim.Replace("$", "").Replace(",", "")
            Else
                Exit Sub
            End If
            If strTo = strFrom Then
                Exit Sub
            End If
            strDesc = txtdesc.Text
            Dim FromAcct() As String = strFrom.Split(" ")
            Dim ToAcct() As String = strTo.Split(" ")

            Dim Xfr As New CuObjectsShr.Params.Transfer
            Dim FAcct As New CuObjectsShr.Params.Transfer.Account
            Dim TAcct As New CuObjectsShr.Params.Transfer.Account
            FAcct.AcctNu = FromAcct(0)
            FAcct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), FromAcct(1))
            FAcct.Suffix = FromAcct(2)
            FAcct.Amt = strAmt
            Xfr.FromAccts.Add(FAcct)

            TAcct.AcctNu = ToAcct(0)
            TAcct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), ToAcct(1))
            TAcct.Suffix = ToAcct(2)
            TAcct.Amt = strAmt
            Xfr.ToAccts.Add(TAcct)

            If (strDesc <> "") Then
                Xfr.Options.Message = strDesc
            Else
                Xfr.Options.Message = strFrom & " to " & strTo  '"Home banking transfer."
            End If

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Transfer"))
            XWrite.WriteStartElement("Xfr")
            Xfr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Response) Then
                ' Good?
                Dim resp As String = ""
                resp += "The transfer of "
                resp += CDec(strAmt).ToString("C")
                resp += " from ["
                resp += strFrom
                resp += "] to ["
                resp += strTo
                resp += "] was successful."
                PRead.Close()
                response1.Text = resp
            Else
                Dim ErrMsg As String
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                response1.Text = ErrMsg
            End If
        Catch ex As System.Exception
            response1.Text = "An error has occurred."
        End Try
    End Sub
End Class
