Partial Class Requests
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
        'Put user code to initialize the page here
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            Me.lstRequestType.SelectedIndex = 0
            Me.lblFrom.Text = Hbk.MemberName & " - Account#: " & DirectCast(Hbk.MyAccts().Item(0), Hbk.Member).Number
            Me.lblDirections.Text = "Please specify the name and account number of all accounts along with your contact information. For issues Please include a detailed message not surrpassing 500 characters."
            Me.wPnlErr.Visible = False
        Catch ex As system.Exception
            Me.lblErr.Text = ex.Message
            Me.wPnlErr.Visible = True
        End Try
    End Sub
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim Subject As String = ""
        Dim Message As String = ""
        Dim aFrom As String = ""
        Try
            aFrom = Me.lblFrom.Text.Replace("Account#: ", "")
            aFrom = aFrom.PadRight(40 & " ")
            aFrom = aFrom.Substring(0, 40)
            Message = aFrom & " - " & Me.lstRequestType.Text & vbCrLf & Me.txtMessage.Text
            Subject = Me.lblFrom.Text
            Hbk.SendMessage(Subject, Message)
            Me.txtMessage.Text = ""
            lblErr.Text = "Your request has been recieved and will be processed shortly."
            Me.wPnlErr.Visible = True
        Catch ex As system.Exception
            Me.lblErr.Text = ex.Message
            Me.wPnlErr.Visible = True
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.txtMessage.Text = ""
            Me.wPnlErr.Visible = False
        Catch ex As system.Exception
        End Try
    End Sub
End Class
