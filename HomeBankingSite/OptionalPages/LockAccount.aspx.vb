Imports Tf.All

Partial Class LockAccount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Bals As Render
    Protected WithEvents LockAcctInfo As Render

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
        Response.ExpiresAbsolute = #1/1/1980#
        Response.AddHeader("cache-control", "no-store, must-revalidate, private")
        Response.AddHeader("Pragma", "no-cache")
        Response.CacheControl = "no-cache"
        Response.Expires = -1

        Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts
        Dim Acct As Hbk.Member

        ' Populate History Acct List
        For Each Acct In Accts
            If (Acct.Permission = Hbk.EHbkPermission.High) Then
                lstLockAcct.Items.Add(Acct.Number)
            End If
        Next

        If (IsPostBack) Then
            lstLockAcct.Items.FindByText(Request.Form.Item("lstLockAcct")).Selected = True
        End If
    End Sub

    Private Sub Bals_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Bals.OnRender
        Hbk.RenderBalances(Writer)
    End Sub

    Private Sub XfrInfo_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles LockAcctInfo.OnRender
        If (IsPostBack()) Then
            ' Attempt a transfer...
            Dim LockAcct As Web.UI.WebControls.ListItem = lstLockAcct.SelectedItem
            If (LockAcct Is Nothing OrElse LockAcct.Text = "") Then
                Hbk.RenderError("You must select an account from the 'Account To Lock:' list.", Writer)
                Exit Sub
            End If

            Dim Acct As New CuObjectsShr.Account.MbrBase
            Dim Client As TCP.NetTalk.NetStream
            Dim XWriter As Sys.Data.Xml.TextWriter
            Dim PRead As TCP.NetTalk.Plain.Reader
            Dim SR As IO.StreamReader
            Try
                Client = Hbk.MyClient
                XWriter = New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "LockAccount"))
                Acct.Number = LockAcct.Text
                XWriter.WriteStartElement("Acct")
                Acct.Save(XWriter)
                XWriter.WriteEndElement()
                XWriter.Close()
                PRead = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    SR = New IO.StreamReader(PRead)
                    Hbk.RenderError(SR.ReadToEnd(), Writer)
                    SR.Close()
                    Exit Sub
                End If
            Catch ex As Exception
                Hbk.RenderError(ex.Message, Writer)
                Exit Sub
            End Try

            Hbk.BeginInfo(Writer)
            Writer.Write("Account ")
            Writer.Write(LockAcct.Text)
            Writer.Write(" has been locked so no more transactions can be made to the account.  You will need to contact the Credit Union to have account ")
            Writer.Write(LockAcct.Text)
            Writer.Write(" unlocked.")
            Hbk.EndInfo(Writer)
            PRead.Close()
        End If
    End Sub
End Class
