Imports Tf.All
Partial Public Class BillPay
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.ExpiresAbsolute = #1/1/1980#
        'Response.AddHeader("cache-control", "no-store, must-revalidate, private")
        'Response.AddHeader("Pragma", "no-cache")
        'Response.CacheControl = "no-cache"
        'Response.Expires = -1
        'Dim BPS As New com.memberpay.www.SingleSignOn
        ''Dim acctno As String = "7786"
        'Dim acctno As String
        'Dim Accts As Sys.data.XMLArray = Hbk.MyAccts
        'Dim Acct As Hbk.Member = Accts(0)
        'acctno = Acct.Number
        'Dim RTNo As String = ConfigurationManager.AppSettings.Item("RT")
        'Dim CorpID As String = ConfigurationManager.AppSettings.Item("BillCompID")
        'Dim pass As String = ConfigurationManager.AppSettings.Item("BillPass")
        'lblerr2.Text = acctno
        ''Dim RTNo As String = "271192310"
        ''Dim CorpID As String = "2005"
        ''Dim pass As String = "wicu2008%"
        'Dim xmlRequest As String = ""
        'xmlRequest = "<SSORequest version='1.0'>"
        'xmlRequest += "<MemberID>" & acctno & "</MemberID>"
        'xmlRequest += "<CorpID>" & CorpID & "</CorpID>"
        'xmlRequest += "<RoutingNumber>" & RTNo & "</RoutingNumber>"
        'xmlRequest += "<Password>" & pass & "</Password>"
        'xmlRequest += "</SSORequest>"
        'Dim xmlResponse As String = ""
        'xmlResponse = BPS.AuthenticateMember(xmlRequest)
        'Dim readxml As New System.Xml.XmlDocument
        'readxml.LoadXml(xmlResponse)
        ''lblerr2.Text = xmlResponse

        'If readxml.ChildNodes(0).ChildNodes(0).InnerText = "0" Then
        '    Dim token As String = readxml.ChildNodes(0).ChildNodes(2).InnerText
        '    'Response.Write("<script>" & vbCrLf)
        '    'Response.Write("window.open('https://www.memberpay.com/MemberBillPay/singlesignonrequest.aspx?token=" & token & "&Exit_URL=https://homebanking.wicu.org');")
        '    'Response.Write(vbCrLf & "</script>")
        '    Response.Write("<script>" & vbCrLf)
        '    Response.Write("parent.location.replace('https://www.memberpay.com/MemberBillPay/singlesignonrequest.aspx?token=" & token & "&Exit_URL=https://homebanking.wicu.org');")
        '    Response.Write(vbCrLf & "</script>")
        '    'lblerr.Text = "https://www.memberpay.com/MemberBillPay/singlesignonrequest.aspx?token=" & token & "&Exit_URL=Balances.aspx"
        'Else
        'Try
        '    Dim teststr As String = readxml.ChildNodes(0).ChildNodes(1).InnerText
        '    lblerr.Text = teststr
        '    'Response.Redirect("ErrorPage.aspx?Msg=" & teststr.Substring(15, teststr.Length - 15))
        'Catch ex1 As system.Exception
        Response.Redirect("ErrorPage.aspx?Msg = Please try using bill pay later")
        'End Try
        'End If
    End Sub

End Class