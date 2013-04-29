Imports Tf.AllV2.TCP
Imports Tf.AllV2.TfGlobals

Partial Public Class PrintHistoryFreindly
    Inherits System.Web.UI.Page
    Private HistCls As New CuObjectsShr.Account.History
    Private AcctNu As String = ""
    Private AcctType As TfEnumerators.EAcctType
    Private AcctSuffix As String = ""
    Private Dt As String = ""
    Private ViewAmount As String = ""
    Private Acct() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            Dim Selected As String = ""
            If (Not IsPostBack) Then

                Selected = Request.QueryString.Item("Acct")
                Dt = Request.QueryString.Item("Dt")
                ViewAmount = Request.QueryString.Item("View")
                If (ViewAmount Is Nothing) Then ViewAmount = "10"
                HistCls = Hbk.GetHistCls(Selected, IIf(Dt <> "", Dt, ""), ViewAmount)
                Acct = Selected.Split(" ")

                AcctNu = CInt(Acct(0))
                If (Acct(1) <> "LOC") Then
                    AcctType = [Enum].Parse(GetType(TfEnumerators.EAcctType), Acct(1))
                Else
                    AcctType = TfEnumerators.EAcctType.Loan
                End If

                AcctSuffix = CInt(Acct(2))

            End If
        Catch ex As System.Exception

        End Try

    End Sub
    Private Sub Hist_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Hist.OnRender
        Try
            Dim Alt As Boolean
            Dim MbrHist As New CuObjectsShr.Account.History

            MbrHist = HistCls

            Writer.Write("<div class='PnlList'>")
            Writer.Write("<table class='WhiteWin2' cellpadding='0' cellspacing='0' width='96%' style='color:Black;'>")
            Writer.Write("<tr><td class='Tab'><img src='TBar/Hist.gif' > <b>")
            Writer.Write(MbrHist.Acct.AcctNu)
            Writer.Write(" ")
            Writer.Write(Acct(1))
            Writer.Write(" ")
            Writer.Write(MbrHist.Acct.Suffix)
            Writer.Write("</b>&nbsp;&nbsp;&nbsp;<a style='color:Black;' href='Balances.aspx' onclick='window.print()' >PRINT</a>&nbsp;&nbsp;&nbsp;<a style='color:Black;' href='Balances.aspx'>BACK</a></td><td align='right'>")
            Writer.Write("</td></tr><tr><td colspan='2'>")
            Writer.Write("<table cellpadding='0' cellspacing='0'>")
            Writer.Write("<tr><th width='15%'>Date</th><th width='15%'>Transaction</th><th width='15%'>Amt</th><th width='15%'>Balance</th><th width='15%'>Chk Nu/Trace</th><th width='25%'>Description</th>")
            Writer.Write("</tr>")

            Dim HItm As CuObjectsShr.Account.History.Item
            For Each HItm In MbrHist.Hists
                Writer.Write("<tr  style=""font-size:small"" class='")
                If (Alt) Then
                    Writer.Write("Row1'>")
                Else
                    Writer.Write("Row2'>")
                End If
                Writer.Write("<td align='center' width='15%'>")
                Writer.Write(HItm.Date.ToShortDateString.Split(" ")(0))
                Writer.Write("</td><td align='center' width='15%'>")
                Writer.Write(HItm.Transaction)
                Writer.Write("</td><td align='center' width='15%'>")
                Writer.Write(HItm.Amt.ToString("C"))
                Writer.Write("</td><td align='center' width='15%'>")
                Writer.Write(HItm.Bal.ToString("C"))
                Writer.Write("</td><td align='center' width='15%'>")
                If HItm.Check <> 0 Then
                    Writer.Write(HItm.Check)
                Else
                    Writer.Write(HItm.Trace)
                End If
                Writer.Write("</td><td align='right' width='25%'>")
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
                Writer.Write("</tr>")

                Alt = Not Alt
            Next
            Writer.Write("</table></td></tr></table></div>")
        Catch ex As System.Exception

        End Try

    End Sub
End Class