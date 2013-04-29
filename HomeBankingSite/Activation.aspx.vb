Imports Tf.AllV2

Partial Class Activation
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
            tflog.log.Mylog.Write("Page_Load PostBack=" & Me.IsPostBack.ToString, "Activation.Load")
            If (Hbk.DownForNightlies) Then
                wPnlErr.Visible = True
                pnlLogin.Visible = False
                lblErr.Text = "The system is down for nightly maintenance and will be online at " & System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StartUp") & "."
            End If
            If (Me.Request.QueryString.Item("hbkey") = "true") Then
                Me.pnlAllInfo.Visible = False
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Sub imgClicked(ByVal imgname As String)
        Try
            Dim ErrMsg As String = Nothing
            Dim HBKey As String = ""

            HBKey = "~/HBKey/" & imgname
            Dim Key As Hbk.UserInfo = Hbk.GetHBKey(Session.Item("Username"))

            If (Me.pnlAllInfo.Visible = True) Then
                Try
                    If validateUserInfo() = False Then Exit Sub
                    If ValidateHBSeqInfo() = False Then Exit Sub
                    Key = Hbk.GetHBKey(txtAcct.Text)
                    If Key.ErrNum = Hbk.EErrors.TimeToActivate Then
                        ErrMsg = Hbk.Activate(txtAcct.Text, txtActivation.Text, txtUser.Text, txtPassword.Text, txtPassword2.Text, HBKey, Me.txtHBKeyName.Text, Me.txtEmail.Text, Me.Q1.Text, Me.Q2.Text, Me.Q3.Text)
                    End If

                Catch ex1 As System.Exception
                    ErrMsg = ex1.Message
                    wPnlErr.Visible = True
                    lblErr.Text = ErrMsg
                    Exit Sub
                End Try
            Else
                Try
                    If ValidateHBSeqInfo() = False Then Exit Sub
                    If Key.ErrNum = Hbk.EErrors.PartialActivate Then
                        Hbk.SetHBKey(Session.Item("Username"), HBKey, Me.txtHBKeyName.Text, Me.Q1.Text, Me.Q2.Text, Me.Q3.Text, Me.txtEmail.Text)
                    End If
                Catch ex1 As System.Exception
                    ErrMsg = ex1.Message
                    wPnlErr.Visible = True
                    lblErr.Text = ErrMsg
                    Exit Sub
                End Try
            End If

            Response.Redirect("PasswordWebSafe.aspx")
        Catch ex As System.Exception
        End Try
    End Sub
    Private Sub lnk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Apples.Click, Arch.Click, Boat.Click, Bridge.Click, Bridge1.Click, Bus.Click, Butterfly.Click, Cone.Click, Eggs.Click, Hammer.Click, LightHouse.Click, Lightning.Click, Monitor.Click, Peach.Click, Phone.Click, Printer.Click, Tiger.Click, Tornado.Click, Truck.Click, Watermelon.Click
        Try
            imgClicked(DirectCast(sender, LinkButton).ID & ".jpg")
        Catch ex As System.Exception
        End Try
    End Sub
    Public Function validateUserInfo() As Boolean
        Try
            If txtAcct.Text.Trim = "" OrElse Not IsNumeric(txtAcct.Text.Trim) Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter your account number."
                txtAcct.Focus()
                Return False
            End If
            If txtActivation.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter your Activation code."
                txtActivation.Focus()
                Return False
            End If
            If txtUser.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter a username of your choice."
                txtUser.Focus()
                Return False
            End If
            If txtUser.Text.Trim.Length < 6 Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter a username with 6 or more characters."
                txtUser.Focus()
                Return False
            End If
            If Not Hbk.isUniqueUsername(txtUser.Text.Trim) Then
                wPnlErr.Visible = True
                lblErr.Text = "Please try a different username."
                txtUser.Focus()
                Return False
            End If
            If txtPassword.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter a Password of your choice."
                txtPassword.Focus()
                Return False
            End If
            If txtPassword2.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please confirm your password."
                txtPassword2.Focus()
                Return False
            End If
            If txtPassword.Text.Trim <> txtPassword2.Text.Trim Then
                wPnlErr.Visible = True
                lblErr.Text = "Please confirm that your passwords match."
                txtPassword.Focus()
                Return False
            End If
            If txtPassword.Text.Trim.Length < 6 Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter a password that is 6 characters long."
                txtPassword.Focus()
                Return False
            End If
            If Not isLegalPass(txtPassword.Text.Trim) Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter a password that has at least one letter and one number."
                txtPassword.Focus()
                Return False
            End If
            Return True
        Catch ex As System.Exception
            Return False
        End Try
    End Function
    Public Function ValidateHBSeqInfo() As Boolean
        Try
            If txtHBKeyName.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter a HBKey name of your choice."
                txtHBKeyName.Focus()
                Return False
            End If
            If Q1.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter first pets name?"
                Q1.Focus()
                Return False
            End If
            If Q2.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter your favorite sport?"
                Q2.Focus()
                Return False
            End If
            If Q3.Text.Trim = "" Then
                wPnlErr.Visible = True
                lblErr.Text = "Please enter your favorite color?"
                Q3.Focus()
                Return False
            End If
            Return True
        Catch ex As System.Exception
            Return False
        End Try
    End Function
    Public Function isLegalPass(ByVal pass As String) As Boolean
        Try
            Dim hasNumber As Boolean = False
            Dim hasLetter As Boolean = False

            For Each C As Char In pass
                If IsNumeric(C) Then
                    hasNumber = True
                Else
                    hasLetter = True
                End If
            Next

            If hasLetter AndAlso hasNumber Then
                Return True
            Else
                Return False
            End If
        Catch ex As System.Exception
            Return False
        End Try
    End Function
End Class
