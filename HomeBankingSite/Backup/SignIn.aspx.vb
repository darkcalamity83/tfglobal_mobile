Imports Tf.AllV2
Partial Public Class SignIn
    Inherits System.Web.UI.Page
    Public loQuestion As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now)
            tflog.log.Mylog.Write("Page_Load PostBack=" & Me.IsPostBack.ToString, "SignIn.Load")
            If (Hbk.DownForNightlies) Then
                wPnlErr.Visible = True
                lblErr.Text = "The system is down for nightly maintenance and will be online at " & System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StartUp") & " " & System.Web.Configuration.WebConfigurationManager.AppSettings.Item("TimeZone") & "."
            End If
            If (Me.IsPostBack) Then
                If txtUserId.Text.Trim.Length = 0 Then
                    Exit Sub
                End If
                Me.Session.Add("Username", txtUserId.Text)
                Dim Key As Hbk.UserInfo = Hbk.GetHBKey(Session.Item("Username"))
                If (Key.UserName = "##ERROR##") Then
                    wPnlErr.Visible = True
                    wPnlErr.BackColor = Color.Red
                    wPnlErr.BorderStyle = BorderStyle.Double
                    wPnlErr.BorderWidth = 3
                    lblErr.Text = Key.Password
                ElseIf Key.ActivationCode <> "" Then
                    Try
                        Session.Item("QAnswer") = ""
                    Catch ex1 As system.Exception
                    End Try
                    Response.Redirect("Activation.aspx")
                Else
                    Dim UserName As String = ""
                    Try
                        UserName = Session.Item("Username")
                    Catch ex1 As system.Exception
                        lblErr.Text = "Please enable cookies and try again."
                        Exit Sub
                    End Try
                    Dim AQ As String = ""
                    Try
                        AQ = Session.Item("AssignedQuestion")
                    Catch ex1 As system.Exception
                        lblErr.Text = "Please enable cookies and try again."
                        Exit Sub
                    End Try
                    Dim ResponseA As String = Hbk.isCorrectAnswer(UserName, Me.txtA.Text, AQ)
                    If ResponseA = "correct" Then
                        Try
                            Session.Item("QAnswer") = ""
                        Catch ex1 As system.Exception
                        End Try
                        Hbk.SetUserIP(UserName, Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
                        Response.Redirect("PasswordWebSafe.aspx")
                    Else
                        wPnlErr.Visible = True
                        wPnlErr.BackColor = Color.Red
                        wPnlErr.BorderStyle = BorderStyle.Double
                        wPnlErr.BorderWidth = 3
                        lblErr.Text = "Your Security Question Was incorrect."
                    End If
                End If
            Else
                Dim Ques As String = ""
                Dim rndQ As Integer = 0
                Dim rnd As Random
                Dim rndN As Integer
                Try
                    rnd = New Random(Now.Second)
                    rndN = rnd.Next(1, 9)
                Catch ex1 As system.Exception
                    rndN = 1
                End Try
                Select Case rndN
                    Case 1, 2, 3
                        rndQ = 1
                        Ques = Hbk.Questions(0)
                    Case 4, 5, 6
                        rndQ = 2
                        Ques = Hbk.Questions(1)
                    Case 7, 8, 9
                        rndQ = 3
                        Ques = Hbk.Questions(2)
                    Case Else
                        rndQ = 1
                        Ques = Hbk.Questions(0)
                End Select
                Me.Session.Add("AssignedQuestion", rndQ)
                Me.Session.Add("QAnswer", Ques)
                Me.lblQ.Text = Ques
            End If
        Catch ex As system.Exception

        End Try
        
    End Sub

End Class