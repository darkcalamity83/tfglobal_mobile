Partial Class Balances
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Protected WithEvents Bals As Render
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Bals_OnRender(ByVal Writer As System.Web.UI.HtmlTextWriter) Handles Bals.OnRender
        Try
            Hbk.RenderBalances(Writer)
        Catch ex As System.Exception
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1)
            Response.Expires = 0
            Response.CacheControl = "no-cache"
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1))
        Catch ex As System.Exception
        End Try
    End Sub
End Class
