Imports System.Collections.Generic

Partial Class MobilePasswordP
    Inherits System.Web.UI.MobileControls.MobilePage
    Public loQuestion As Integer = 0
    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Dim Username As String = Request.QueryString("username")
            Dim ResponseA As String = Hbk.isCorrectAnswer(Username, txtAnswer.Text, lblQuestion.Text.Split(":")(0))
            If ResponseA <> "correct" Then
                lblerr.Text = "Your Security Question Was incorrect."
                Exit Sub
            End If

            Dim ErrMsg As String = Hbk.MobileLogin(Username, txtPass.Text)
            If (Not ErrMsg Is Nothing) Then
                If (ErrMsg = "ACTIVATE") Then
                    lblerr.Text = "Please finish activating your account and try again."
                Else
                    lblerr.Text = ErrMsg
                End If
            End If
        Catch ex As System.Exception
            lblerr.Text = "Your Login Failed"
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.ExpiresAbsolute = #1/1/1980#
        'Response.AddHeader("cache-control", "no-store, must-revalidate, private")
        'Response.AddHeader("Pragma", "no-cache")
        'Response.CacheControl = "no-cache"
        'Response.Expires = -1

        Try
            If Not IsPostBack Then
                Dim Username As String = Request.QueryString("username")
                Dim Key As Hbk.UserInfo = Hbk.GetHBKey(Username)
                If (Key.UserName = "##ERROR##") Then
                    lblerr.Text = Key.Password
                Else
                    If (Key.HBKey = "") Then

                    Else
                        Me.imgHBK.ImageUrl = Key.HBKey
                        Me.lblHBK.Text = Key.HBKeyName
                        If (Not Key.LastLogin Is Nothing AndAlso Key.LastLogin <> "") Then
                            Me.lblLastLoggin.Text = Key.LastLogin
                        End If
                        Dim Q As String = ""
                        Dim rndQ As Integer = 0
                        Dim random As New Random(Now.Second)
                        Dim rndN As Integer = random.Next(1, 9)
                        Select Case rndN
                            Case 1, 2, 3
                                Q = "1:" & Hbk.Questions(0)
                                'Me.Session.Add("AssignedQuestion", "1")
                                'Me.Session.Add("QAnswer", Hbk.Questions(0))
                            Case 4, 5, 6
                                Q = "2:" & Hbk.Questions(1)
                                'Me.Session.Add("AssignedQuestion", "2")
                                'Me.Session.Add("QAnswer", Hbk.Questions(1))
                            Case 7, 8, 9
                                Q = "3:" & Hbk.Questions(2)
                                'Me.Session.Add("AssignedQuestion", "3")
                                'Me.Session.Add("QAnswer", Hbk.Questions(2))
                            Case Else
                                Q = "1:" & Hbk.Questions(0)
                                'Me.Session.Add("AssignedQuestion", "1")
                                'Me.Session.Add("QAnswer", Hbk.Questions(0))
                        End Select
                        'lblQuestion.Text = Session.Item("QAnswer")
                        lblQuestion.Text = Q
                    End If
                End If
            End If
        Catch ex As System.Exception
            lblerr.Text = "An Issue occurred while logging in."
        End Try
    End Sub
End Class
