Partial Class ErrorPage
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
            Dim S As String = Request.QueryString.Item("Msg")
            If S.Contains("Object reference not set to an instance of an object.") Then
                S = "Your Homebanking site is currently down for maintenance, please wait try again later. Sorry for the inconvenience."
            End If

            If (Not S Is Nothing AndAlso S <> "") Then wPnlErr.Text = S
        Catch ex As System.Exception
        End Try
    End Sub

End Class
