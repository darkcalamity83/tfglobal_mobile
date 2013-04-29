Imports System.Web
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Security.Principal
Imports tf.AllV2

Public Class [Global]
    Inherits System.Web.HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        tflog.log.Mylog.gLogName = "HomeBankingSite.log"
        tflog.log.Mylog.Write("------>", "Global.New")
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Try
            tflog.log.Mylog.Write("Sender=" & sender.ToString, "Global.Application_Error", Diagnostics.EventLogEntryType.Error)
            Response.Redirect(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ErrorPage") & "?Msg=" & Server.GetLastError().InnerException.Message)
        Catch ex As System.Exception
        End Try
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        tflog.log.Mylog.Write("Sender=" & sender.ToString & " Cookie=" & FormsAuthentication.FormsCookieName, "Global.Application_AuthenticateRequest")
        Dim AuthCookie As HttpCookie = Context.Request.Cookies(FormsAuthentication.FormsCookieName)

        If (AuthCookie Is Nothing) Then Exit Sub

        Dim AuthTicket As FormsAuthenticationTicket = Nothing

        Try
            AuthTicket = FormsAuthentication.Decrypt(AuthCookie.Value)
        Catch ex As System.Exception
            ' Log Exception
        End Try

        If (AuthTicket Is Nothing) Then Exit Sub

        Context.User = New GenericPrincipal(New FormsIdentity(AuthTicket), AuthTicket.UserData.Split("|"))
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        'GC.Collect()
    End Sub
End Class
