Imports Tf.All
Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals
Imports Tf.AllV2.TCP

Partial Class MobileBalancesP
    Inherits System.Web.UI.MobileControls.MobilePage

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.ExpiresAbsolute = #1/1/1980#
        'Response.AddHeader("cache-control", "no-store, must-revalidate, private")
        'Response.AddHeader("Pragma", "no-cache")
        'Response.CacheControl = "no-cache"
        'Response.Expires = -1
        Try
            'tflog.log.Mylog.Write("", "Hbk.RenderBalances")
            Dim token As String = Request.QueryString("token")
            Dim username As String = Request.QueryString("username")
            Dim Client As TCP.NetTalk.NetStream = Hbk.ValidateStream(token, username)
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

            Dim imgList As New ArrayList
            Dim userList As New ArrayList
            Dim balList As New ArrayList
            Dim keyList As New ArrayList
            imgList.Add(Me.Img1)
            imgList.Add(Me.Img2)
            imgList.Add(Me.Img3)
            imgList.Add(Me.Img4)
            userList.Add(Me.user1)
            userList.Add(Me.user2)
            userList.Add(Me.user3)
            userList.Add(Me.user4)
            balList.Add(Me.balances1)
            balList.Add(Me.balances2)
            balList.Add(Me.balances3)
            balList.Add(Me.balances4)

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
                        lblerr.Text = ErrMsg
                    Else
                        Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                        Dim Mbr As New CuObjects.Member
                        Mbr.Load(XRead)
                        AddSubAcct = (Acct.SubAccts Is Nothing)
                        If (AddSubAcct) Then Acct.SubAccts = New ArrayList

                        Alt = False
                        For Each MbrAcct In Mbr.Accts
                            If (MbrAcct.Type = TfEnumerators.EAcctType.Loan AndAlso IsNumeric(MbrAcct.m_Items("Avail"))) Then
                                SubAcct = New Hbk.Acct("LOC", MbrAcct.Suffix)
                            Else
                                SubAcct = New Hbk.Acct(MbrAcct.Type.ToString(), MbrAcct.Suffix)
                            End If
                            Dim mText As String = ""
                            Dim mValue As String = ""
                            Dim uText As String = ""
                            If (AddSubAcct) Then Acct.SubAccts.Add(SubAcct)

                            If (Mbr.FirstName.Length > 1) Then
                                uText += Char.ToUpper(Mbr.FirstName.Chars(0))
                                uText += Mbr.FirstName.Substring(1).ToLower()
                                uText += " "
                            End If
                            If (Mbr.LastName.Length > 1) Then
                                uText += Char.ToUpper(Mbr.LastName.Chars(0))
                                uText += Mbr.LastName.Substring(1).ToLower()
                                uText += " "
                            End If
                            uText += Mbr.AcctNu.ToString
                            '
                            DirectCast(userList(i), System.Web.UI.MobileControls.Label).Text = uText
                            DirectCast(imgList(i), System.Web.UI.MobileControls.Image).Visible = True
                            Dim outAcct As String = SubAcct.Type & " " & SubAcct.Suffix
                            outAcct = outAcct.PadRight(11, " ").Substring(0, 11)
                            mText = outAcct

                            Dim bal As String = IIf(MbrAcct.m_Items("Bal") Is Nothing, "0.00", MbrAcct.m_Items("Bal"))
                            Dim Avail As String
                            If (MbrAcct.Type = TfEnumerators.EAcctType.Loan) Then
                                Avail = MbrAcct.Item("NextDue")
                            Else
                                Avail = IIf(MbrAcct.m_Items("Avail") Is Nothing, "0.00", MbrAcct.m_Items("Avail"))
                            End If
                            bal = CDec(bal).ToString("C").Replace("$", "")
                            bal = bal.PadRight(11, " ").Substring(0, 11)
                            If (IsNumeric(Avail)) Then
                                Avail = CDec(Avail).ToString("C").Replace("$", "")
                                Avail = Avail.PadRight(11, " ").Substring(0, 11)
                                Avail = "Avail: " & Avail
                            ElseIf IsDate(Avail) Then
                                Avail = Avail.PadRight(11, " ").Substring(0, 11)
                                Avail = "Due: " & Avail
                            Else
                                Avail = " "
                            End If
                            mText += " Bal: " & bal & Avail

                            mValue = "MobileHistoryP.aspx?Acct="
                            mValue += MbrAcct.AcctNu.ToString
                            mValue += " "
                            mValue += SubAcct.Type
                            mValue += " "
                            mValue += MbrAcct.Suffix.ToString
                            mValue += "&Reset=T&username=" & username & "&token=" & token
                            DirectCast(balList(i), System.Web.UI.MobileControls.List).Items.Add(New System.Web.UI.MobileControls.MobileListItem(mText, mValue))

                            Alt = Not Alt
                        Next
                    End If
                End If
            Next
        Catch ex As System.Exception
            lblerr.Text = "An error has occurred."
        End Try
    End Sub

    Private Sub balances_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.MobileControls.ListCommandEventArgs) Handles balances1.ItemCommand, balances2.ItemCommand, balances3.ItemCommand, balances4.ItemCommand
        Response.Redirect(e.ListItem.Value)
    End Sub

End Class
