Imports System.Web.Security
Imports tf.AllV2
Imports tf.AllV2.TfGlobals
Imports tf.AllV2.TCP
Imports System.Collections.Generic

Public Class Hbk
    'Public Shared AcctNumber As String = "0"
    'Public Shared YearChanged As Boolean = False

    Public Shared Function FullCheckingAccountNumber(ByVal acct As String) As String
        Try
            Dim table(,) As Integer = {{0, 1, 2, 3, 4, 5, 6, 7, 8, 9}, {0, 2, 4, 6, 8, 1, 3, 5, 7, 9}}
            Dim sum As Integer

            For idx As Integer = 0 To (9 - acct.Length) - 1
                acct = "0" & acct
            Next

            acct = "1071" & acct

            Dim i As Integer = acct.Length - 1
            Dim odd As Integer = 0
            For i = acct.Length - 1 To 0 Step -1
                odd = 1 - odd
                sum += table(odd, Char.GetNumericValue(acct.ToCharArray()(i)) - Char.GetNumericValue("0".ToCharArray()(0)))
            Next
            sum = sum Mod 10
            sum = IIf(sum > 0, 10 - sum, 0)
            acct = acct & sum.ToString()
        Catch ex As System.Exception
        End Try
        Return acct
    End Function
    Public Shared Questions() As String = {"What is your first pets name?", "What is your favorite sport?", "What is your favorite color?"}
    Public Enum EErrors
        None = 0
        InvalidUser = 1
        NoActivationCode = 2
        TimeToActivate = 3
        InvalidPassword = 4
        PartialActivate = 5
    End Enum
    Public Enum EHbkPermission
        High = 3
        Med = 2
        Low = 1
    End Enum
    Public Enum EOnlineStatements
        NotSignedUp = 0
        SignUp = 1
        SignedUp = 2
    End Enum
    Public Class Member
        Inherits Sys.Data.clsObject

        Public Number As String
        Public Permission As EHbkPermission
        Public Email As String
        Public OnlineStatements As Integer
        Public SubAccts As ArrayList
    End Class
    Public Class Acct
        Public Type As String
        Public Suffix As Integer

        Public Sub New(ByVal Type As String, ByVal Suffix As Integer)
            Me.Type = Type
            Me.Suffix = Suffix
        End Sub
    End Class
    Public Class UserInfo
        Inherits Sys.Data.clsObject

        Public IP As String
        Public Acct As String
        Public SubAccts As HashSet(Of Member)
        Public ActivationCode As String
        Public UserName As String
        Public OldUserName As String
        Public OldPassword As String
        Public Password As String
        Public ConfirmPassword As String
        Public HBKey As String
        Public HBKeyName As String
        Public Email As String
        Public LastLogin As String
        Public ErrNum As EErrors
        Public Q1 As String
        Public Q2 As String
        Public Q3 As String
    End Class
    Public Class HbkMsgs
        Inherits Sys.Data.clsObject

        Public Page As String
        Public NuOfMsgs As Integer
        Public HtmlMsgs As String
        Public [Error] As String

    End Class
    Public Class Credential
        Inherits TCP.NetTalk.Client.BlockingCredential

        Public Sub New(ByVal Username As String, ByVal Password As String)
            MyBase.New(Username, Password)
        End Sub
        Public Overrides Sub Authenticate(ByVal Stream As TCP.NetTalk.NetStream)
            Try
                TfLog.Log.MyLog.Write("", "Hbk.Credential.Authenticate")
                Dim ErrMsg As String = Nothing
                MyBase.SendHandshake(Stream)
                If (MyBase.IsError(Stream.newReader(Me.m_UnAuthMaxBufferSize), ErrMsg)) Then
                    TfLog.Log.MyLog.Write(ErrMsg, "Hbk.Credential.Authenticate", Diagnostics.EventLogEntryType.Error)
                    Throw New System.Exception(ErrMsg)
                    Exit Sub
                End If
                MyBase.SendCredential(Stream)
                Dim Reader As TCP.NetTalk.Plain.Reader = Stream.newReader(Me.m_UnAuthMaxBufferSize)
                If (MyBase.IsError(Reader, ErrMsg)) Then
                    TfLog.Log.MyLog.Write(ErrMsg, "Hbk.Credential.Authenticate", Diagnostics.EventLogEntryType.Error)
                    Throw New System.Exception(ErrMsg)
                    Exit Sub
                End If

                ' Load the accounts list.
                Dim Accts As New Sys.Data.XMLArray(GetType(Member), "Acct")
                Dim Xml As New Sys.Data.Xml.TextReader(Reader)
                Try
                    Accts.Load(Xml)
                Catch ex As System.Exception
                    TfLog.Log.MyLog.Write(ex.Message, "Hbk.Credential.Authenticate", Diagnostics.EventLogEntryType.Error)
                    Throw New System.Exception(ex.Message)
                    Exit Sub
                End Try
                Xml.Close()

                Hbk.MyAccts = Accts
            Catch ex As System.Exception
            End Try
        End Sub
    End Class
    Public Class OnlineCredential
        Inherits TCP.NetTalk.Client.BlockingCredential

        Public Sub New(ByVal Username As String, ByVal Password As String)
            MyBase.New(Username, Password)
        End Sub
        Public Overrides Sub Authenticate(ByVal Stream As TCP.NetTalk.NetStream)
            Try
                TfLog.Log.MyLog.Write("", "Hbk.OnlineCredential.Authenticate")
                Dim ErrMsg As String = Nothing
                MyBase.SendHandshake(Stream)
                If (MyBase.IsError(Stream.newReader(Me.m_UnAuthMaxBufferSize), ErrMsg)) Then
                    TfLog.Log.MyLog.Write(ErrMsg, "Hbk.OnlineCredential.Authenticate", Diagnostics.EventLogEntryType.Error)
                    Throw New System.Exception(ErrMsg)
                    Exit Sub
                End If
                MyBase.SendCredential(Stream)
                Dim Reader As TCP.NetTalk.Plain.Reader = Stream.newReader(Me.m_UnAuthMaxBufferSize)
                If (MyBase.IsError(Reader, ErrMsg)) Then
                    TfLog.Log.MyLog.Write(ErrMsg, "Hbk.OnlineCredential.Authenticate", Diagnostics.EventLogEntryType.Error)
                    Throw New System.Exception(ErrMsg)
                    Exit Sub
                End If

                ' Load the accounts list.
                Reader.Close()
            Catch ex As System.Exception
            End Try
        End Sub
    End Class
    Public Shared Property MyClient() As TCP.NetTalk.NetStream
        Get
            Try
                Return HttpContext.Current.Session.Item("Client")
            Catch ex As System.Exception
                Return Nothing
            End Try
        End Get
        Set(ByVal Value As TCP.NetTalk.NetStream)
            Try
                HttpContext.Current.Session.Item("Client") = Value
            Catch ex As System.Exception
            End Try
        End Set
    End Property
    Public Shared Property MyOnlineClient() As TCP.NetTalk.NetStream
        Get
            Try
                Return HttpContext.Current.Session.Item("OnlineClient")
            Catch ex As System.Exception
                Return Nothing
            End Try
        End Get
        Set(ByVal Value As TCP.NetTalk.NetStream)
            Try
                HttpContext.Current.Session.Item("OnlineClient") = Value
            Catch ex As System.Exception
            End Try
        End Set
    End Property
    Public Shared Property MyAccts() As Sys.Data.XMLArray
        Get
            Try
                Return HttpContext.Current.Session.Item("Accts")
            Catch ex As System.Exception
                Return Nothing
            End Try
        End Get
        Set(ByVal Value As Sys.Data.XMLArray)
            Try
                HttpContext.Current.Session.Item("Accts") = Value
            Catch ex As System.Exception
            End Try
        End Set
    End Property
    Public Shared ReadOnly Property MyMobileAccts(ByVal UserName As String) As HashSet(Of Member)
        Get
            Dim hshMembers As New HashSet(Of Member)
            Try
                Dim Usr As Hbk.UserInfo
                Usr = Hbk.getUserInfo(UserName, "UserName")
                If Usr IsNot Nothing Then
                    Dim loMem As New Member
                    loMem.Number = Usr.Acct
                    loMem.Email = Usr.Email
                    loMem.Permission = EHbkPermission.High
                    hshMembers.Add(loMem)
                    If Usr.SubAccts IsNot Nothing Then
                        For Each Mem As Member In Usr.SubAccts
                            hshMembers.Add(loMem)
                        Next
                    End If
                End If
            Catch ex As System.Exception
            End Try
            Return hshMembers
        End Get
    End Property
    Protected Shared Sub funMobileSave(ByRef pClient As TCP.NetTalk.NetStream, _
                               ByRef pUserName As String)
        Try
            Hbk.LogLastLogin(pUserName, Now.ToLongDateString & " at " & Now.ToShortTimeString)

            Dim user As UserInfo = AuthenticateStream(pUserName)
            Dim token As String = user.Password

            HttpContext.Current.Response.Redirect("MobileBalancesP.aspx?username=" & pUserName & "&token=" & token, True)
        Catch ex As System.Exception
        End Try
    End Sub
    Protected Shared Function funSave(ByRef pClient As TCP.NetTalk.NetStream, _
                                   ByRef pUserName As String, _
                          Optional ByVal pRedirect As Boolean = True, Optional ByVal isMobileDevice As Boolean = False) As String
        Try
            TfLog.Log.MyLog.Write("pUserName=" & pUserName & " pRedirect=" & pRedirect.ToString, "Hbk.funSave")
            Dim AuthTicket As New FormsAuthenticationTicket(1, pUserName, DateTime.Now, DateTime.Now.AddMinutes(20), False, "")
            HttpContext.Current.Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(AuthTicket)))
            HttpContext.Current.Session.Item("Username") = pUserName
            MyClient = pClient

            If (pRedirect) Then
                Hbk.LogLastLogin(pUserName, Now.ToLongDateString & " at " & Now.ToShortTimeString)
                HttpContext.Current.Response.Redirect("Balances.aspx", False)
            End If
        Catch ex As System.Exception
        End Try
        Return ""
    End Function
    Protected Shared Function funServerErr(ByVal pReader As TCP.NetTalk.Plain.Reader) As UserInfo
        Try
            funServerErr = New UserInfo
            Dim SR As New IO.StreamReader(pReader)
            funServerErr.Password = SR.ReadToEnd
            SR.Close()
            Try
                funServerErr.ErrNum = pReader.m_HeaderReader.Attribute("ErrNum", -1)
            Catch ex As System.Exception
            End Try
            funServerErr.UserName = "##ERROR##"
        Catch ex As System.Exception
            Return Nothing
        End Try
    End Function
    Public Shared ReadOnly Property DownForNightlies() As Boolean
        Get
            Try
                ' Check Time To Make Sure Home Banking Is Not Down For Nightlies
                Dim ShutDown As DateTime = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ShutDown")
                Dim StartUp As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StartUp")
                Dim Current As DateTime = Now.ToShortTimeString

                If Current >= ShutDown AndAlso Current <= ShutDown.AddMinutes(CInt(StartUp)) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As System.Exception
                Return True
            End Try
        End Get
    End Property
    Public Shared Function ValidateStream(ByVal token As String, ByVal username As String) As TCP.NetTalk.NetStream
        Try
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim Credential As New OnlineCredential("validateHBK", "validateREQUEST")
            Dim IPAddr As String = ""
            Try
                IPAddr = HttpContext.Current.Request.UserHostAddress
                IPAddr = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Catch ex As System.Exception
            End Try
            Credential.AddAttribute("IP", IPAddr)
            Credential.AddAttribute("Type", "validate")
            Credential.AddAttribute("token", token)
            Credential.AddAttribute("username2", username)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            If Client Is Nothing OrElse Client.IsOpen = False Then
                Return Nothing
            Else
                Return Client
            End If
        Catch ex As System.Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function AuthenticateStream(ByVal username As String) As UserInfo
        Try
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim Credential As New OnlineCredential("authenticateHBK", "authenticateREQUEST")

            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Authenticate")
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "AuthenticateUser"))
            XWrite.WriteStartElement("UserInfo")
            XWrite.WriteTag("UserName", username)
            XWrite.WriteTag("Password", "")
            Dim IPAddr As String = ""
            Try
                IPAddr = HttpContext.Current.Request.UserHostAddress
                IPAddr = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Catch ex As System.Exception
            End Try
            XWrite.WriteTag("IP", IPAddr)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim loRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (loRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return Nothing
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(loRead)
                Mbr.Load(XRead)
                XRead.Close()
            End If

            Return Mbr
        Catch ex As System.Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function GetAccts(ByRef Client As TCP.NetTalk.NetStream, ByRef username As String) As Sys.Data.XMLArray

        Try
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetAccts"))
            XWrite.WriteStartElement("UserInfo")
            XWrite.WriteTag("UserName", username)
            XWrite.WriteTag("Password", "")
            Dim IPAddr As String = ""
            Try
                IPAddr = HttpContext.Current.Request.UserHostAddress
                IPAddr = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Catch ex As System.Exception
            End Try
            XWrite.WriteTag("IP", IPAddr)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim Accts As New Sys.Data.XMLArray(GetType(Member), "Acct")

            Dim loRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (loRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return Nothing
            Else
                Dim Xml As New Sys.Data.Xml.TextReader(loRead)
                Try
                    Accts.Load(Xml)
                Catch ex As System.Exception
                    TfLog.Log.MyLog.Write(ex.Message, "Hbk.Credential.Authenticate", Diagnostics.EventLogEntryType.Error)
                    Throw New System.Exception(ex.Message)
                    Exit Function
                End Try
                Xml.Close()
            End If

            Return Accts
        Catch ex As System.Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function Login(ByRef pUserName As String, ByRef pPassword As String) As String
        ' Grab UserName And Password
        TfLog.Log.MyLog.Write("pUserName=" & pUserName & " pPassword=" & pPassword.ToString, "Hbk.Save")
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New Credential(pUserName, pPassword)
            Dim IPAddr As String = ""
            Try
                IPAddr = HttpContext.Current.Request.UserHostAddress
                IPAddr = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Catch ex As System.Exception
            End Try
            Credential.AddAttribute("IP", IPAddr)
            Credential.AddAttribute("UserName2", pUserName)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)

            Dim ErrMsg As String = Nothing
            Dim XWrite As Sys.Data.Xml.TextWriter
            Dim Accts As Sys.Data.XMLArray = Nothing

            Accts = MyAccts()
            Dim Acct As Member

            For Each Acct In Accts
                XWrite = New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                XWrite.WriteStartElement("Accts")
                XWrite.WriteTag("AcctNu", Acct.Number)
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    Dim SR As New IO.StreamReader(PRead)
                    ErrMsg = SR.ReadToEnd()
                    SR.Close()
                    Throw New System.Exception(ErrMsg)
                Else
                    Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                    Dim Mbr2 As New CuObjects.Member
                    Mbr2.Load(XRead)
                    XRead.Close()
                End If
            Next

            Return funSave(Client, pUserName)
        Catch ex As System.Exception
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then
                Client.Close(True)
                Client = Nothing
            End If
            If ex.ToString.Contains("tfRawStream.Read = 0") Then
                Return "Homebanking is experiencing some issues at the moment, please try back in a few minutes. Thank you"
            ElseIf ex.ToString.Contains("An established connection was aborted") Then
                Return "There was an issue accessing your account or your account may be lock for a small amount of time due to excessive failed login attempts. please try again later, Thank you"
            ElseIf ex.ToString.Contains("Object reference not set to an instance of an object") Then
                Return "Your login attempt failed, please re-enter your password and try again."
            ElseIf ex.ToString.Contains("Host=") OrElse ex.ToString.Contains("Port=") Then
                Return "Your login attempt failed."
            ElseIf ex.Message.Trim.Length = 0 Then
                Return "An error has occurred with your account, please try again later or contact the credit union for further instructions."
            Else
                Return ex.Message
            End If
        End Try
    End Function
    Public Shared Function MobileLogin(ByRef pUserName As String, ByRef pPassword As String) As String
        ' Grab UserName And Password
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New Credential(pUserName, pPassword)
            Dim IPAddr As String = ""
            Try
                IPAddr = HttpContext.Current.Request.UserHostAddress
                IPAddr = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Catch ex As System.Exception
            End Try
            Credential.AddAttribute("IP", IPAddr)
            Credential.AddAttribute("UserName2", pUserName)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)

            Dim ErrMsg As String = Nothing
            Dim XWrite As Sys.Data.Xml.TextWriter
            Dim MobileMyAccts As HashSet(Of Member) = MyMobileAccts(pUserName)
            For Each loAcct As Member In MobileMyAccts
                XWrite = New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                XWrite.WriteStartElement("Accts")
                XWrite.WriteTag("AcctNu", loAcct.Number)
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    Dim SR As New IO.StreamReader(PRead)
                    ErrMsg = SR.ReadToEnd()
                    SR.Close()
                    Throw New System.Exception(ErrMsg)
                Else
                    Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                    Dim Mbr2 As New CuObjects.Member
                    Mbr2.Load(XRead)
                    XRead.Close()
                End If
            Next

            funMobileSave(Client, pUserName)

            Return ""

        Catch ex As System.Exception
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then
                Client.Close(True)
                Client = Nothing
            End If
            If ex.ToString.Contains("tfRawStream.Read = 0") Then
                Return "Homebanking is experiencing some issues at the moment, please try back in a few minutes. Thank you"
            ElseIf ex.ToString.Contains("An established connection was aborted") Then
                Return "There was an issue accessing your account or your account may be lock for a small amount of time due to excessive failed login attempts. please try again later, Thank you"
            ElseIf ex.ToString.Contains("Object reference not set to an instance of an object") Then
                Return "Your login attempt failed, please re-enter your password and try again."
            ElseIf ex.ToString.Contains("Host=") OrElse ex.ToString.Contains("Port=") Then
                Return "Your login attempt failed."
            ElseIf ex.Message.Trim.Length = 0 Then
                Return "An error has occurred with your account, please try again later or contact the credit union for further instructions."
            Else
                Return ex.Message
            End If
        End Try
    End Function
    Public Shared Function OnlineLogin(ByVal ProcessAcct As String) As String
        TfLog.Log.MyLog.Write("ProcessAcct=" & ProcessAcct, "Hbk.OnlineLogin")
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New OnlineCredential("Online", "Manage")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("ProcessAcct", ProcessAcct)
            Credential.AddAttribute("Type", "OnlineVerify")
            Credential.AddAttribute("UserName2", ProcessAcct)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)

            MyOnlineClient = Client
            Return ""
        Catch ex As System.Exception
            TfLog.Log.MyLog.Write(ex.Message, "Hbk.OnlineLogin", Diagnostics.EventLogEntryType.Error)
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then Client.Close(True)
            Return ex.Message
        End Try
    End Function
    Public Shared Function SetOnlineStatements(ByVal Email As String, ByVal OS As EOnlineStatements, ByVal Process As String) As String
        Try
            TfLog.Log.MyLog.Write("Email=" & Email, "Hbk.SetOnlineStatements")
            Dim Client As TCP.NetTalk.NetStream = MyClient()
            Dim UpdateAccts As Boolean = True
            If (Client Is Nothing) Then
                Client = MyOnlineClient()
                UpdateAccts = False
            End If
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "SetOnlineStatements"))
            Dim ErrMsg As String = ""

            XWrite.WriteStartElement("OnlineStatements")
            XWrite.WriteTag("Email", Email)
            XWrite.WriteTag("OnlineStatements", CInt(OS).ToString())
            XWrite.WriteTag("Process", Process)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                Throw New System.Exception(ErrMsg)
            Else
                PRead.Close()
                If (UpdateAccts) Then
                    Dim Accts As New Sys.Data.XMLArray(GetType(Member), "Acct")
                    For Each Acct As Member In MyAccts
                        If (Email <> "") Then Acct.Email = Email
                        Acct.OnlineStatements = OS
                        Accts.Add(Acct)
                    Next
                    MyAccts = Accts
                End If
            End If
            Return ErrMsg
        Catch ex As System.Exception
            Return ex.ToString
        End Try
    End Function
    Public Shared Function ChangeLogin(ByRef UserName As String, _
                                       ByRef Password As String, _
                                       ByRef NewUsername As String, _
                                       ByRef NewPassword1 As String, _
                                       ByRef NewPassword2 As String, _
                                       ByRef Email As String, _
                                       ByRef HBKeyName As String, _
                                       Optional ByVal Redirect As Boolean = True) As String
        ' Grab UserName And Password
        TfLog.Log.MyLog.Write("UserName=" & UserName & " Password=" & Password, "Hbk.ChangeLogin")
        Try
            Dim Client As TCP.NetTalk.NetStream = MyClient()
            Dim Accts As New Sys.Data.XMLArray(GetType(Member), "Acct")
            Dim mbr As New UserInfo

            mbr.UserName = NewUsername
            mbr.Password = NewPassword1
            mbr.OldPassword = Password
            mbr.OldUserName = UserName
            mbr.ConfirmPassword = NewPassword2
            mbr.Email = Email
            mbr.IP = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            mbr.HBKeyName = HBKeyName

            Dim Writer As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "ChangeLogin"))
            Writer.WriteStartElement("UserInfo")
            Writer.WriteTag("UserName", mbr.UserName)
            Writer.WriteTag("Password", mbr.Password)
            Writer.WriteTag("OldUserName", mbr.OldUserName)
            Writer.WriteTag("OldPassword", mbr.OldPassword)
            Writer.WriteTag("ConfirmPassword", mbr.ConfirmPassword)
            Writer.WriteTag("Email", mbr.Email)
            Writer.WriteTag("IP", mbr.IP)
            Writer.WriteTag("HBKeyName", mbr.HBKeyName)
            Writer.WriteEndElement()
            Writer.Close()

            Dim Reader As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (Reader.m_HeaderReader.TrxType <> TCP.NetTalk.ETrxType.Response) Then
                Dim SR As New IO.StreamReader(Reader)
                ChangeLogin = SR.ReadToEnd()
                SR.Close()
            Else
                If (Not Email Is Nothing AndAlso Email <> "") Then
                    For Each Acct As Member In MyAccts()
                        Acct.Email = Email
                        Accts.Add(Acct)
                    Next
                    MyAccts = Accts
                End If

                If (Not NewUsername Is Nothing AndAlso NewUsername <> "") Then
                    Return funSave(Client, NewUsername, Redirect)
                Else
                    Return funSave(Client, UserName, Redirect)
                End If
            End If
        Catch ex As System.Exception
            TfLog.Log.MyLog.Write(ex.Message, "Hbk.ChangeLogin", Diagnostics.EventLogEntryType.Error)
            Return ex.Message
        End Try
    End Function
    Public Shared Function Activate(ByRef Acct As String, _
                                    ByRef ActivationCode As String, _
                                    ByRef UserName As String, _
                                    ByRef Password As String, _
                                    ByRef ConfirmPassword As String, _
                                    ByRef HBKey As String, _
                                    ByRef HBKeyName As String, _
                                    ByRef Email As String, _
                                    ByRef Q1 As String, _
                                    ByRef Q2 As String, _
                                    ByRef Q3 As String) As String
        TfLog.Log.MyLog.Write("Acct=" & Acct & " ActivationCode=" & ActivationCode, "Hbk.Activate")
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New Credential(UserName, Password)
            Credential.AddAttribute("Type", "Activate")
            Credential.AddAttribute("Acct", Acct)
            Credential.AddAttribute("ActivationCode", ActivationCode)
            Credential.AddAttribute("ConfirmPassword", ConfirmPassword)
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("HBKey", HBKey)
            Credential.AddAttribute("HBKeyName", HBKeyName)
            Credential.AddAttribute("Email", Email)
            Credential.AddAttribute("Q1", Q1)
            Credential.AddAttribute("Q2", Q2)
            Credential.AddAttribute("Q3", Q3)
            Credential.AddAttribute("UserName2", UserName)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)

            Return funSave(Client, UserName)
        Catch ex As System.Exception
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then Client.Close(True)
            Return ex.Message
        End Try
    End Function
    Public Shared Function GetHBKey(ByRef UserName As String) As UserInfo
        'tflog.log.Mylog.Write("UserName=" & UserName, "Hbk.GetHBKey")
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Dim ErrMsg As String = Nothing
        Dim Mbr As New UserInfo
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New OnlineCredential("HBK", "Manage")
            Dim IPAddr As String = ""
            Try
                IPAddr = HttpContext.Current.Request.UserHostAddress
                IPAddr = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Catch ex As System.Exception
            End Try
            Credential.AddAttribute("IP", IPAddr)
            Credential.AddAttribute("Type", "HBVerify")
            Credential.AddAttribute("UserName2", UserName)

            Try
                Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Catch ex As System.Exception
                Mbr.UserName = "##ERROR##"
                Mbr.Password = ex.Message
                Return Mbr
            End Try

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetHBKey"))
            XWrite.WriteStartElement("UserInfo")
            XWrite.WriteTag("UserName", UserName)
            XWrite.WriteTag("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim loRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (loRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return funServerErr(loRead)
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(loRead)
                Mbr.Load(XRead)
                XRead.Close()
            End If
        Catch ex As System.Exception
            TfLog.Log.MyLog.Write(ex.Message, "Hbk.GetHBKey", Diagnostics.EventLogEntryType.Error)
            ErrMsg = ex.Message
        Finally
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then Client.Close(True)
        End Try
        If (Not ErrMsg Is Nothing AndAlso ErrMsg <> "") Then
            Mbr.UserName = "##ERROR##"
            Mbr.Password = ErrMsg
            TfLog.Log.MyLog.Write(ErrMsg, "Hbk.GetHBKey", Diagnostics.EventLogEntryType.Error)
        End If
        Return Mbr
    End Function
    Public Shared Function isUniqueUsername(ByRef UserName As String) As Boolean
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Dim ErrMsg As String = Nothing
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim Mbr As New UserInfo
            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", UserName)
            Try
                Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Catch ex As System.Exception
                Return False
            End Try
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "IsUniqueUserName"))
            XWrite.WriteStartElement("UserInfo")
            XWrite.WriteTag("UserName", UserName)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim loRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (loRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return False
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(loRead)
                Mbr.Load(XRead)
                XRead.Close()
            End If

            If Mbr.UserName = "isUnique" Then
                Return True
            Else
                Return False
            End If

        Catch ex As System.Exception
            TfLog.Log.MyLog.Write(ex.Message, "Hbk.isUniqueUsername", Diagnostics.EventLogEntryType.Error)
            ErrMsg = ex.Message
        Finally
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then Client.Close(True)
        End Try
        If (Not ErrMsg Is Nothing AndAlso ErrMsg <> "") Then
            Return False
        End If
        Return True
    End Function
    Public Shared Function isCorrectAnswer(ByRef UserName As String, ByRef Answer As String, ByVal AQ As String) As String
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Dim ErrMsg As String = Nothing
        Dim Q As String = AQ
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim Mbr As New UserInfo

            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", UserName)
            Try
                Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Catch ex As System.Exception
                Return "invalid"
            End Try
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "QandA"))
            XWrite.WriteStartElement("UserInfo")
            XWrite.WriteTag("UserName", UserName)
            XWrite.WriteTag("Password", Q)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim loRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (loRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return "invalid"
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(loRead)
                Mbr.Load(XRead)
                XRead.Close()
            End If

            If Mbr.UserName = Answer Then
                Return "correct"
            Else
                Return "invalid"
            End If

        Catch ex As System.Exception
            TfLog.Log.MyLog.Write(ex.Message, "Hbk.isUniqueUsername", Diagnostics.EventLogEntryType.Error)
            ErrMsg = ex.Message
        Finally
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then Client.Close(True)
        End Try
        Return "invalid"
    End Function
    Public Shared Function CanXfer(ByRef SubAcct As Hbk.Acct, ByRef XfrFrom() As String) As Boolean
        Try
            Dim Item As String
            Dim Type As String
            Dim Suffix As String()
            Dim Index As Integer

            TfLog.Log.MyLog.Write("SubAcct=" & SubAcct.Type, "Hbk.CanXfer")
            For Each Item In XfrFrom
                Suffix = Item.Split(",")
                Type = Suffix(0)
                If (Suffix.Length = 1) Then
                    ReDim Preserve Suffix(1)
                    Suffix(1) = "*"
                End If
                If (Type = SubAcct.Type) Then
                    For Index = 1 To Suffix.Length - 1
                        If (IsNumeric(Suffix(Index)) AndAlso CInt(Suffix(Index)) < 0) Then
                            If (Math.Abs(CInt(Suffix(Index))) = SubAcct.Suffix) Then Return False
                        Else
                            If (Suffix(Index) = "*" OrElse Suffix(Index) = SubAcct.Suffix) Then Return True
                        End If
                    Next
                End If
            Next
        Catch ex As System.Exception
            Return False
        End Try
    End Function
    Public Shared Function CanXferTo(ByRef SubAcct As Hbk.Acct, ByRef XfrTo() As String) As Boolean
        Try
            Dim Item As String
            Dim Type As String
            Dim Suffix As String()
            Dim Index As Integer

            TfLog.Log.MyLog.Write("SubAcct=" & SubAcct.Type, "Hbk.CanXferTo")
            For Each Item In XfrTo
                Suffix = Item.Split(",")
                Type = Suffix(0)
                If (Suffix.Length = 1) Then
                    ReDim Preserve Suffix(1)
                    Suffix(1) = "*"
                End If
                If (Type = SubAcct.Type) Then
                    For Index = 1 To Suffix.Length - 1
                        If (IsNumeric(Suffix(Index)) AndAlso CInt(Suffix(Index)) < 0) Then
                            If (Math.Abs(CInt(Suffix(Index))) = SubAcct.Suffix) Then Return False
                        Else
                            If (Suffix(Index) = "*" OrElse Suffix(Index) = SubAcct.Suffix) Then Return True
                        End If
                    Next
                End If
            Next
        Catch ex As System.Exception
            Return False
        End Try

    End Function
    Public Shared Function FormatDate(ByVal Value As String, Optional ByVal DefaultValue As String = Nothing) As String
        Try
            If (Value Is Nothing OrElse Value = "") Then Return DefaultValue

            Dim dtValue As New Date
            Dim lYear As Long

            If (DefaultValue Is Nothing) Then
                DefaultValue = Date.MinValue
            End If

            If (Value <> "") Then
                Try
                    If (IsDate(Value) AndAlso Date.Compare(Value, Date.MinValue) = 0) Then
                        If (IsDate(DefaultValue) AndAlso Date.Compare(DefaultValue, Date.MinValue) = 0) Then
                            dtValue = Date.MinValue
                        ElseIf (IsDate(DefaultValue)) Then
                            dtValue = Date.Parse(DefaultValue)
                            Value = DefaultValue
                        Else
                            dtValue = Date.MinValue
                        End If
                    Else
                        dtValue = Date.Parse(Value)
                    End If
                Catch oe As SystemException
                    If (IsNumeric(Value)) Then
                        If (Value.Length = 6) Then
                            If (Value.Substring(4, 2) < 40) Then
                                lYear = 2000
                            Else
                                lYear = 1900
                            End If
                            lYear += CLng(Value.Substring(4, 2))
                            Value = Value.Substring(0, 2) & "/" & Value.Substring(2, 2) & "/" & CStr(lYear)
                        ElseIf (Value.Length = 8) Then
                            Value = Value.Substring(0, 2) & "/" & Value.Substring(2, 2) & "/" & Value.Substring(4)
                        Else
                            Value = DefaultValue
                        End If
                    Else
                        Value = DefaultValue
                    End If
                    Try
                        dtValue = Date.Parse(Value)
                    Catch oex As SystemException
                        Return DefaultValue
                    End Try
                End Try
                Value = dtValue.ToShortDateString()
                If (Date.Compare(Value, Date.MinValue) = 0) Then Return DefaultValue
                Return Value
            End If
        Catch ex As System.Exception
        End Try
        Return DefaultValue
    End Function
    Public Shared Sub Logout(ByRef Response As Web.HttpResponse)
        Try
            TfLog.Log.MyLog.Write("", "Hbk.Logout")
            Dim Cookie As HttpCookie = HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)

            If (Not Cookie Is Nothing) Then
                Cookie.Expires = DateAdd(DateInterval.Month, -1, Date.Now)
                HttpContext.Current.Response.Cookies.Add(Cookie)
            End If

            Try
                MyClient.Close(True)
            Catch ex1 As System.Exception
            End Try

            If (Not HttpContext.Current.Session Is Nothing) Then HttpContext.Current.Session.Abandon()
            'HttpContext.Current.Response.Redirect(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Logout"), True)
            Response.Write("<script>" & vbCrLf)
            Response.Write("parent.location.replace('" & System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Logout") & "');")
            Response.Write(vbCrLf & "</script>")
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared ReadOnly Property MemberName() As String
        Get
            Try
                Dim Client As TCP.NetTalk.NetStream = MyClient()
                Dim HMbr As Member = MyAccts().Item(0)
                Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                Dim ErrMsg As String
                Dim Name As String = ""
                XWrite.WriteStartElement("Accts")
                XWrite.WriteTag("AcctNu", HMbr.Number)
                XWrite.WriteEndElement()
                XWrite.Close()
                '
                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()

                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    '
                    Dim SR As New IO.StreamReader(PRead)
                    ErrMsg = SR.ReadToEnd()
                    SR.Close()
                    Throw New System.Exception(ErrMsg)

                Else

                    Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                    Dim Mbr As New CuObjects.Member
                    Mbr.Load(XRead)

                    If (Mbr.FirstName.Length > 1) Then
                        Name &= Char.ToUpper(Mbr.FirstName.Chars(0))
                        Name &= Mbr.FirstName.Substring(1).ToLower()
                        Name &= " "
                    End If

                    If (Mbr.LastName.Length > 1) Then
                        Name &= Char.ToUpper(Mbr.LastName.Chars(0))
                        Name &= Mbr.LastName.Substring(1).ToLower()
                    End If

                End If
                Return Name
            Catch ex As System.Exception
                Return ""
            End Try
        End Get
    End Property
    Public Shared Sub RenderPendingTransfers(ByVal Writer As HtmlTextWriter)
        Try
            TfLog.Log.MyLog.Write("", "Hbk.RenderPendingTransfers")
            Dim Client As TCP.NetTalk.NetStream = MyClient()
            Dim Accts As Sys.Data.XMLArray = MyAccts()
            Dim Acct As Member
            Dim Xfr As New CuObjectsShr.Params.Transfer
            Dim Alt As Boolean
            Dim ErrMsg As String = Nothing
            Acct = Accts(0)

            If (Acct.Permission <> EHbkPermission.Low OrElse Acct.SubAccts Is Nothing) Then
                Xfr.Acnt = Acct.Number
                Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetFutureTransfers"))
                XWrite.WriteStartElement("Transfer")
                Xfr.Save(XWrite)
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    Dim SR As New IO.StreamReader(PRead)
                    ErrMsg = SR.ReadToEnd()
                    SR.Close()
                Else
                    Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                    Dim FutureXfrs As New Sys.Data.XMLArray(GetType(CuObjectsShr.Params.FutureTransfer), "FutureXfr")
                    Dim FutureXfr As CuObjectsShr.Params.FutureTransfer
                    FutureXfrs.Load(XRead)
                    XRead.Close()

                    If (Acct.Permission <> EHbkPermission.Low) Then
                        Writer.Write("<div class='PnlList'>")
                        Writer.Write("<table cellpadding='0' cellspacing='0'>")
                        Writer.Write("<tr><td class='Tab'><img src='TBar/Xfr.gif' > ")
                        Writer.Write("<b>" & Acct.Number.ToString & " - Transfers</b>")
                        Writer.Write("</td></tr>")
                        Writer.Write("<tr><td><table cellpadding='0' cellspacing='0'>")
                        Writer.Write("<tr><th width='20%'>From Account</th><th width='15%'>To Account</th><th width='15%'>Amount</th><th width='20%'>Posting Date</th><th width='15%'>Description</th><th width='15%'>Change</th></tr>")

                        Alt = False

                        'Pending Transfers
                        Writer.Write("<tr><td><img src='TBar/Xfr.gif' > ")
                        Writer.Write("<b>Pending Tranfers</b>")
                        Writer.Write("</td></tr>")
                        For Each FutureXfr In FutureXfrs
                            If FutureXfr.Posted = 0 Then
                                Writer.Write("<tr class='")
                                If (Alt) Then
                                    Writer.Write("Row1")
                                Else
                                    Writer.Write("Row2")
                                End If
                                Writer.Write("'><td>")

                                Writer.Write(FutureXfr.FromAcct)
                                Writer.Write(" ")
                                Writer.Write([Enum].GetName(GetType(EAcctType), FutureXfr.FromAcctType))
                                Writer.Write(" ")
                                Writer.Write(FutureXfr.FromAcctSfx)
                                Writer.Write("</td><td align='right'>")

                                Writer.Write(FutureXfr.ToAcct)
                                Writer.Write(" ")
                                Writer.Write([Enum].GetName(GetType(EAcctType), FutureXfr.ToAcctType))
                                Writer.Write(" ")
                                Writer.Write(FutureXfr.ToAcctSfx)
                                Writer.Write("</td><td align='right'>")

                                If (IsNumeric(FutureXfr.Amt)) Then
                                    Writer.Write(CDec(FutureXfr.Amt).ToString("C"))
                                Else
                                    Writer.Write("&nbsp;")
                                End If
                                Writer.Write("</td>")

                                Writer.Write("<td align='center'>")
                                Writer.Write(FutureXfr.ProcessDate)
                                Writer.Write("</td><td align='right'>")
                                Writer.Write(FutureXfr.Desc.Replace("'", "").Replace("/", "").Replace("<", "").Replace(">", "").Replace("""", ""))
                                Writer.Write("</td><td align='right'>")
                                Dim tempstr As String = "<a href='PendingTransactions.aspx?remove=" & FutureXfr.FTidx & "'><b>Remove</b></a>"
                                Writer.Write(tempstr)
                                Writer.Write("</td></tr>")

                                Alt = Not Alt
                            End If
                        Next

                        'Posted Transfers
                        Writer.Write("<tr><td><img src='TBar/Xfr.gif' > ")
                        Writer.Write("<b>Posted Tranfers</b>")
                        Writer.Write("</td></tr>")
                        For Each FutureXfr In FutureXfrs
                            If FutureXfr.Posted = 1 Then
                                Writer.Write("<tr class='")
                                If (Alt) Then
                                    Writer.Write("Row1")
                                Else
                                    Writer.Write("Row2")
                                End If
                                Writer.Write("'><td>")

                                Writer.Write(FutureXfr.FromAcct)
                                Writer.Write(" ")
                                Writer.Write([Enum].GetName(GetType(EAcctType), FutureXfr.FromAcctType))
                                Writer.Write(" ")
                                Writer.Write(FutureXfr.FromAcctSfx)
                                Writer.Write("</td><td align='right'>")

                                Writer.Write(FutureXfr.ToAcct)
                                Writer.Write(" ")
                                Writer.Write([Enum].GetName(GetType(EAcctType), FutureXfr.ToAcctType))
                                Writer.Write(" ")
                                Writer.Write(FutureXfr.ToAcctSfx)
                                Writer.Write("</td><td align='right'>")
                                If (IsNumeric(FutureXfr.Amt)) Then
                                    Writer.Write(CDec(FutureXfr.Amt).ToString("C"))
                                Else
                                    Writer.Write("&nbsp;")
                                End If
                                Writer.Write("</td>")

                                Writer.Write("<td align='center'>")
                                Writer.Write(FutureXfr.ProcessDate)
                                Writer.Write("</td><td align='right'>")
                                Writer.Write(FutureXfr.Desc.Replace("'", "").Replace("/", "").Replace("<", "").Replace(">", "").Replace("""", ""))
                                Writer.Write("</td><td align='right'>")
                                Writer.Write("Successfull")
                                Writer.Write("</td></tr>")

                                Alt = Not Alt
                            End If
                        Next

                        'Failed Transfers
                        Writer.Write("<tr><td><img src='TBar/Xfr.gif' > ")
                        Writer.Write("<b>Rejected Tranfers</b>")
                        Writer.Write("</td></tr>")
                        For Each FutureXfr In FutureXfrs
                            If FutureXfr.Posted = 2 Then
                                Writer.Write("<tr class='")
                                If (Alt) Then
                                    Writer.Write("Row1")
                                Else
                                    Writer.Write("Row2")
                                End If
                                Writer.Write("'><td>")

                                Writer.Write(FutureXfr.FromAcct)
                                Writer.Write(" ")
                                Writer.Write([Enum].GetName(GetType(EAcctType), FutureXfr.FromAcctType))
                                Writer.Write(" ")
                                Writer.Write(FutureXfr.FromAcctSfx)
                                Writer.Write("</td><td align='right'>")

                                Writer.Write(FutureXfr.ToAcct)
                                Writer.Write(" ")
                                Writer.Write([Enum].GetName(GetType(EAcctType), FutureXfr.ToAcctType))
                                Writer.Write(" ")
                                Writer.Write(FutureXfr.ToAcctSfx)
                                Writer.Write("</td><td align='right'>")

                                If (IsNumeric(FutureXfr.Amt)) Then
                                    Writer.Write(CDec(FutureXfr.Amt).ToString("C"))
                                Else
                                    Writer.Write("&nbsp;")
                                End If
                                Writer.Write("</td>")

                                Writer.Write("<td align='center'>")
                                Writer.Write(FutureXfr.ProcessDate)
                                Writer.Write("</td><td align='right'>")
                                Writer.Write(FutureXfr.Desc.Replace("'", "").Replace("/", "").Replace("<", "").Replace(">", "").Replace("""", ""))
                                Writer.Write("</td><td align='right'>")
                                Dim tempstr As String = "<a href='PendingTransactions.aspx?retry=" & FutureXfr.FTidx & "'><b>Retry</b> - " & FutureXfr.ErrMsg.Replace("<", "").Replace(">", "").Replace("/", "") & "</a>"
                                Writer.Write(tempstr)
                                Writer.Write("</td></tr>")

                                Alt = Not Alt
                            End If
                        Next

                        Writer.Write("</table></td></tr></table></div>")
                    End If
                End If
            End If

            If (Not ErrMsg Is Nothing) Then
                RenderError(ErrMsg, Writer)
            End If
        Catch ex As System.Exception
            Hbk.RenderError("Please log back in.", Writer)
        End Try
    End Sub
    Public Shared Sub RenderPendingACH(ByVal Writer As HtmlTextWriter)
        Try
            TfLog.Log.MyLog.Write("", "Hbk.RenderPendingTransfers")
            Dim Client As TCP.NetTalk.NetStream = MyClient()
            Dim Accts As Sys.Data.XMLArray = MyAccts()
            Dim Acct As Member
            Dim Xfr As New CuObjectsShr.Params.Transfer
            Dim Alt As Boolean
            Dim ErrMsg As String = Nothing
            Acct = Accts(0)

            If (Acct.Permission <> EHbkPermission.Low OrElse Acct.SubAccts Is Nothing) Then
                Xfr.Acnt = Acct.Number
                Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetFutureACHTransactions"))
                XWrite.WriteStartElement("Transfer")
                Xfr.Save(XWrite)
                XWrite.WriteEndElement()
                XWrite.Close()

                Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                    Dim SR As New IO.StreamReader(PRead)
                    ErrMsg = SR.ReadToEnd()
                    SR.Close()
                Else
                    Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                    Dim FutureACHs As New Sys.Data.XMLArray(GetType(CuObjectsShr.Params.FutureACHTransactions), "FutureACH")
                    Dim FutureACH As CuObjectsShr.Params.FutureACHTransactions
                    FutureACHs.Load(XRead)
                    XRead.Close()

                    If (Acct.Permission <> EHbkPermission.Low) Then
                        Writer.Write("<div class='PnlList'>")
                        Writer.Write("<table cellpadding='0' cellspacing='0'>")
                        Writer.Write("<tr><td class='Tab'><img src='TBar/BillPay.gif' > ")
                        Writer.Write("<b>" & Acct.Number.ToString & " - Clearing House</b>")
                        Writer.Write("</td></tr>")
                        Writer.Write("<tr><td><table cellpadding='0' cellspacing='0'>")
                        Writer.Write("<tr><th width='20%'>Posting Date</th><th width='20%'>Company Name</th><th width='20%'>Type</th><th width='20%'>Amount</th><th width='20%'>Status</th></tr>")

                        Alt = False

                        'Pending Transfers
                        Writer.Write("<tr><td><img src='TBar/BillPay.gif' > ")
                        Writer.Write("<b>Pending Clearing House</b>")
                        Writer.Write("</td></tr>")
                        For Each FutureACH In FutureACHs
                            Writer.Write("<tr class='")
                            If (Alt) Then
                                Writer.Write("Row1")
                            Else
                                Writer.Write("Row2")
                            End If
                            Writer.Write("'><td>")

                            Writer.Write(FutureACH.ProcDate.ToShortDateString.Split(" ")(0))

                            Writer.Write("</td><td align='right'>")

                            Writer.Write(FutureACH.CuName)

                            Writer.Write("</td><td align='right'>")

                            Dim strType As String = FutureACH.TranCode
                            Dim tempType As String = ""

                            If strType.Chars(1) = "2" OrElse strType.Chars(1) = "3" Then
                                tempType = "Deposit"
                            ElseIf strType.Chars(1) = "6" OrElse strType.Chars(1) = "7" OrElse strType.Chars(1) = "8" Then
                                tempType = "Withdrawal"
                            Else
                                tempType = "Misc"
                            End If

                            If strType.Chars(0) = "2" Then
                                tempType &= " To Checking"
                            ElseIf strType.Chars(0) = "3" Then
                                tempType &= " To Share"
                            ElseIf strType.Chars(0) = "4" OrElse strType.Chars(0) = "5" Then
                                tempType &= " To Loan"
                            Else
                                tempType &= " To Share"
                            End If

                            strType = tempType

                            Writer.Write(strType)

                            Writer.Write("</td>")

                            Writer.Write("<td align='center'>")
                            Writer.Write(FutureACH.Amount.ToString.Substring(0, FutureACH.Amount.ToString.Length - 2) & "." & FutureACH.Amount.ToString.Substring(FutureACH.Amount.ToString.Length - 2, 2))
                            Writer.Write("</td><td align='right'>")

                            Dim status As String = ""
                            If FutureACH.Posted = 0 Then
                                status = "Pending"
                            Else
                                status = "Posted"
                            End If
                            Writer.Write(status)
                            Writer.Write("</td></tr>")

                            Alt = Not Alt
                        Next

                        Writer.Write("</table></td></tr></table></div>")
                    End If
                End If
            End If

            If (Not ErrMsg Is Nothing) Then
                RenderError(ErrMsg, Writer)
            End If
        Catch ex As System.Exception
            Hbk.RenderError("Please log back in.", Writer)
        End Try
    End Sub
    Public Shared Sub RenderBalances(ByVal Writer As HtmlTextWriter)
        Try
            TfLog.Log.MyLog.Write("", "Hbk.RenderBalances")
            Dim Client As TCP.NetTalk.NetStream = MyClient()
            Dim Accts As Sys.Data.XMLArray = MyAccts()
            Dim Acct As Member
            Dim Alt As Boolean
            Dim MbrAcct As CuObjectsShr.Account.ShrGeneric
            Dim ErrMsg As String = Nothing
            Dim SubAcct As Acct
            Dim AddSubAcct As Boolean

            For Each Acct In Accts
                If (Acct.Permission <> EHbkPermission.Low OrElse Acct.SubAccts Is Nothing) Then
                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                    XWrite.WriteStartElement("Accts")
                    XWrite.WriteTag("AcctNu", Acct.Number)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                        Dim SR As New IO.StreamReader(PRead)
                        ErrMsg = SR.ReadToEnd()
                        SR.Close()
                    Else
                        Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                        Dim Mbr As New CuObjects.Member
                        Mbr.Load(XRead)
                        XRead.Close()

                        If (Acct.Permission <> EHbkPermission.Low) Then
                            Writer.Write("<div class='PnlList'>")
                            Writer.Write("<table cellpadding='0' cellspacing='0'>")
                            Writer.Write("<tr><td class='Tab'><img src='TBar/Mbr.gif' > ")
                            Writer.Write(Mbr.AcctNu.ToString)
                            Writer.Write(" ")
                            If (Mbr.FirstName.Length > 1) Then
                                Writer.Write(Char.ToUpper(Mbr.FirstName.Chars(0)))
                                Writer.Write(Mbr.FirstName.Substring(1).ToLower())
                                Writer.Write(" ")
                            End If
                            If (Mbr.LastName.Length > 1) Then
                                Writer.Write(Char.ToUpper(Mbr.LastName.Chars(0)))
                                Writer.Write(Mbr.LastName.Substring(1).ToLower())
                            End If

                            Writer.Write("</td></tr><tr><td>")
                            Writer.Write("<table class='mobile' cellpadding='0' cellspacing='0'>")
                           

                            ' Do We Need To Add Sub Accounts?
                            AddSubAcct = (Acct.SubAccts Is Nothing)
                            If (AddSubAcct) Then Acct.SubAccts = New ArrayList

                            Alt = False
                            For Each MbrAcct In Mbr.Accts
                                If (MbrAcct.Type = TfEnumerators.EAcctType.Loan AndAlso IsNumeric(MbrAcct.m_Items("Avail"))) Then
                                    SubAcct = New Acct("LOC", MbrAcct.Suffix)
                                Else
                                    SubAcct = New Acct(MbrAcct.Type.ToString(), MbrAcct.Suffix)
                                End If
                                'new mobile app table sizing and postitioning code
                                Writer.Write("<tr><th sytle='width:3%'>Account: </th><th>")
                                Writer.Write("<a title='Click to view history.' href='History.aspx?Acct=")
                                Writer.Write(MbrAcct.AcctNu)
                                Writer.Write(" ")
                                Writer.Write(SubAcct.Type)
                                Writer.Write(" ")
                                Writer.Write(MbrAcct.Suffix)
                                Writer.Write("&Reset=T'>")

                                ' Writer.Write(MbrAcct.Number)
                                Writer.Write(" ")
                                Writer.Write(" " & SubAcct.Type)
                                Writer.Write(" ")
                                Writer.Write(SubAcct.Suffix & " ")
                                Writer.Write("</a></th></tr>")

                                Writer.Write("<tr><th width='15%'>Balance</th><th width='15%'>Available</th><th width='20%'>Next Due Date</th></tr>")
                                If (AddSubAcct) Then
                                    Acct.SubAccts.Add(SubAcct)

                                End If
                               


                                Writer.Write("<tr class='")
                                If (Alt) Then
                                    Writer.Write("Row1")
                                Else
                                    Writer.Write("Row2")
                                End If
                                Writer.Write("'><td align='center'>")
                                'Writer.Write("'><td><a title='Click to view history.' href='History.aspx?Acct=")
                                'Writer.Write(MbrAcct.AcctNu)
                                'Writer.Write(" ")
                                'Writer.Write(SubAcct.Type)
                                'Writer.Write(" ")
                                'Writer.Write(MbrAcct.Suffix)
                                'Writer.Write("&Reset=T'>")

                                'Writer.Write(MbrAcct.Number)
                                'Writer.Write(" ")
                                'Writer.Write("<b>" & SubAcct.Type)
                                'Writer.Write(" ")
                                'Writer.Write(SubAcct.Suffix & "</b")
                                'Writer.Write("</a></td><td align='right'>")

                                Dim bal As String = IIf(MbrAcct.m_Items("Bal") Is Nothing, "0.00", MbrAcct.m_Items("Bal"))
                                Dim Avail As String = IIf(MbrAcct.m_Items("Avail") Is Nothing, "0.00", MbrAcct.m_Items("Avail"))
                                Writer.Write(CDec(bal).ToString("C"))
                                Writer.Write("</td><td align='center'>")
                                If (IsNumeric(Avail)) Then
                                    Writer.Write(CDec(Avail).ToString("C"))
                                Else
                                    Writer.Write("&nbsp;")
                                End If
                                Writer.Write("</td>")
                                If (MbrAcct.Type = TfEnumerators.EAcctType.Loan) Then
                                    Writer.Write("<td align='center'>")
                                    Writer.Write(MbrAcct.Item("NextDue"))
                                    Writer.Write("</td></tr><tr><th width='15%'>Payment Amt</th><th width='15%'>Pay Off</th></tr>")
                                    Writer.Write("</tr><tr class='Row1'><td align='center'>$")
                                    Writer.Write(MbrAcct.Item("PmtAmt"))
                                    Writer.Write("</td><td align='center'>$")
                                    Writer.Write(MbrAcct.Item("Payoff"))
                                    Writer.Write("</td>")
                                Else
                                    ' Writer.Write("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>")
                                    Writer.Write("<td>&nbsp;</td>")
                                End If
                                Writer.Write("</tr>")
                                Writer.Write("<tr class='mobile'><td class='mobile' style='border: none'>&nbsp;</td></tr>")
                                Alt = Not Alt
                            Next
                            Writer.Write("</table></td></tr></table></div>")
                        Else ' Low, Initialize Sub Accts
                            Acct.SubAccts = New ArrayList
                            For Each MbrAcct In Mbr.Accts
                                Acct.SubAccts.Add(New Acct(MbrAcct.Type.ToString(), MbrAcct.Suffix))
                            Next
                        End If
                    End If
                End If
            Next

            If (Not ErrMsg Is Nothing) Then
                RenderError(ErrMsg, Writer)
            End If
        Catch ex As System.Exception
            RenderError("Please log back in.", Writer)
        End Try
    End Sub
    Public Shared Sub RenderMobileBalances(ByVal Writer As HtmlTextWriter, ByVal token As String, ByVal username As String, ByRef resp As Web.HttpResponse)
        Try
            TfLog.Log.MyLog.Write("", "Hbk.RenderBalances")
            Dim Client As TCP.NetTalk.NetStream = ValidateStream(token, username)
            If Client Is Nothing Then
                resp.Redirect("MobileLoginPage.aspx")
                Exit Sub
            End If
            Dim Accts As Sys.Data.XMLArray = GetAccts(Client, username)
            Dim Acct As Member
            Dim Alt As Boolean
            Dim MbrAcct As CuObjectsShr.Account.ShrGeneric
            Dim ErrMsg As String = Nothing
            Dim SubAcct As Acct
            Dim AddSubAcct As Boolean

            For Each Acct In Accts
                If (Acct.Permission <> EHbkPermission.Low OrElse Acct.SubAccts Is Nothing) Then
                    Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "Balance"))
                    XWrite.WriteStartElement("Accts")
                    XWrite.WriteTag("AcctNu", Acct.Number)
                    XWrite.WriteEndElement()
                    XWrite.Close()

                    Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
                    If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                        Dim SR As New IO.StreamReader(PRead)
                        ErrMsg = SR.ReadToEnd()
                        SR.Close()
                    Else
                        Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                        Dim Mbr As New CuObjects.Member
                        Mbr.Load(XRead)

                        If (Acct.Permission <> EHbkPermission.Low) Then
                            'Writer.Write("<mobile:image runat='server' id='../TBar/Mbr.gif' />")
                            Writer.Write("<mobile:label runat='server' Font-Bold='True'>")
                            If (Mbr.FirstName.Length > 1) Then
                                Writer.Write(Char.ToUpper(Mbr.FirstName.Chars(0)))
                                Writer.Write(Mbr.FirstName.Substring(1).ToLower())
                                Writer.Write(" ")
                            End If
                            If (Mbr.LastName.Length > 1) Then
                                Writer.Write(Char.ToUpper(Mbr.LastName.Chars(0)))
                                Writer.Write(Mbr.LastName.Substring(1).ToLower())
                                Writer.Write(" ")
                            End If
                            Writer.Write(Mbr.AcctNu)
                            Writer.Write("</mobile:label>")
                            Writer.Write("<br />")
                            Writer.Write("<mobile:label runat='server' Font-Bold='True'>")
                            Writer.Write("Account    |Balance    |Avail/Due  ")
                            Writer.Write("</mobile:label>")
                            Writer.Write("<br />")
                            Writer.Write("<mobile:List Runat='server' >")
                            ' Do We Need To Add Sub Accounts?
                            AddSubAcct = (Acct.SubAccts Is Nothing)
                            If (AddSubAcct) Then Acct.SubAccts = New ArrayList

                            Alt = False
                            For Each MbrAcct In Mbr.Accts
                                If (MbrAcct.Type = TfEnumerators.EAcctType.Loan AndAlso IsNumeric(MbrAcct.m_Items("Avail"))) Then
                                    SubAcct = New Acct("LOC", MbrAcct.Suffix)
                                Else
                                    SubAcct = New Acct(MbrAcct.Type.ToString(), MbrAcct.Suffix)
                                End If
                                Dim mText As String = ""
                                Dim mValue As String = ""
                                If (AddSubAcct) Then Acct.SubAccts.Add(SubAcct)
                                'Writer.Write("<Item Text='")
                                'Writer.Write("<a title='Click to view history.' href='MobileHistoryP.aspx?Acct=")
                                'Writer.Write(MbrAcct.Number)
                                'Writer.Write(" ")
                                'Writer.Write(SubAcct.Type)
                                'Writer.Write(" ")
                                'Writer.Write(MbrAcct.Suffix)
                                'Writer.Write("&Reset=T&username=&" & username & "&token=" & token & "'>")
                                Dim outAcct As String = SubAcct.Type & " " & SubAcct.Suffix
                                outAcct = outAcct.PadRight(11, " ").Substring(0, 11)
                                Writer.Write(outAcct)
                                Writer.Write("|")
                                Dim bal As String = IIf(MbrAcct.m_Items("Bal") Is Nothing, "0.00", MbrAcct.m_Items("Bal"))
                                Dim Avail As String
                                If (MbrAcct.Type = TfEnumerators.EAcctType.Loan) Then
                                    Avail = MbrAcct.Item("NextDue")
                                Else
                                    Avail = IIf(MbrAcct.m_Items("Avail") Is Nothing, "0.00", MbrAcct.m_Items("Avail"))
                                End If
                                bal = CDec(bal).ToString("C").Replace("$", "")
                                bal = bal.PadRight(11, " ").Substring(0, 11)
                                Writer.Write(bal)
                                Writer.Write("|")
                                If (IsNumeric(Avail)) Then
                                    Avail = CDec(Avail).ToString("C").Replace("$", "")
                                    Avail = Avail.PadRight(11, " ").Substring(0, 11)
                                    Writer.Write(Avail)
                                ElseIf IsDate(Avail) Then
                                    Avail = Avail.PadRight(11, " ").Substring(0, 11)
                                    Writer.Write(Avail)
                                Else
                                    Writer.Write("&nbsp;")
                                End If
                                Writer.Write("' Value='' />")
                                'Writer.Write("<br />")
                                Alt = Not Alt
                            Next
                            Writer.Write("</mobile:List>")
                        Else ' Low, Initialize Sub Accts
                            Acct.SubAccts = New ArrayList
                            For Each MbrAcct In Mbr.Accts
                                Acct.SubAccts.Add(New Acct(MbrAcct.Type.ToString(), MbrAcct.Suffix))
                            Next
                        End If
                        XRead.Close()
                    End If
                End If
            Next

            If (Not ErrMsg Is Nothing) Then
                RenderError(ErrMsg, Writer)
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub RenderMsgs(ByVal Writer As HtmlTextWriter)
        Try
            TfLog.Log.MyLog.Write("", "Hbk.RenderMsgs")
            Dim ErrMsg As String = Nothing
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim XWrite As Sys.Data.Xml.TextWriter
            Dim aMsgs As New HbkMsgs
            Dim PRead As TCP.NetTalk.Plain.Reader

            Try
                Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
                Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

                Dim Credential As New OnlineCredential("Login", "Msgs")
                Credential.AddAttribute("Type", "Quick")
                Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Catch ex1 As System.Exception
                Dim err As String = ex1.ToString
            End Try

            aMsgs.Page = "login"

            Try
                XWrite = New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetMsgs"))
            Catch ex1 As System.Exception
                Writer.Write("<H1>Site is currently down to process the credit union's nightlies. Please try back shortly.</H1>")
                Exit Sub
            End Try

            XWrite.WriteStartElement("HbkMsgs")
            aMsgs.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            PRead = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                Hbk.RenderError(SR.ReadToEnd(), Writer)
                SR.Close()
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                aMsgs.Load(XRead)
                XRead.Close()

                Try
                    Writer.Write(aMsgs.HtmlMsgs)
                Catch ex1 As System.Exception
                    ErrMsg = ex1.ToString
                End Try
            End If

            If (Not ErrMsg Is Nothing) Then
                RenderError(ErrMsg, Writer)
            End If
        Catch ex As System.Exception

        End Try

    End Sub
    Public Shared Sub RenderError(ByRef ErrMsg As String, ByVal Writer As HtmlTextWriter)
        Try
            TfLog.Log.MyLog.Write("ErrMsg=" & ErrMsg, "Hbk.RenderError")
            Writer.Write("<div id='wPnlErr'>")
            Writer.Write(ErrMsg)
            Writer.Write("</div>")
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub RenderInfo(ByRef Info As String, ByVal Writer As HtmlTextWriter)
        Try
            TfLog.Log.MyLog.Write("Info=" & Info, "Hbk.RenderInfo")
            Writer.Write("<div class='Info'><table cellpadding='0' cellspacing='0' height='100%'><tr><td valign='middle'>")
            Writer.Write(Info)
            Writer.Write("</td></tr></table></div>")
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub BeginInfo(ByRef Writer As HtmlTextWriter)
        Try
            Writer.Write("<div class='Info'><table cellpadding='0' cellspacing='0' height='100%'><tr><td valign='middle'>")
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub EndInfo(ByRef Writer As HtmlTextWriter)
        Try
            Writer.Write("</td></tr></table></div>")
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub SendMessage(ByVal Subject As String, ByVal Message As String)
        Try
            TfLog.Log.MyLog.Write("Subject=" & Subject & " Msg=" & Message, "Hbk.SendMessage")
            Dim Client As TCP.NetTalk.NetStream = MyClient()
            Dim Mbr As New CuObjects.Member
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "SendMessage"))
            Dim ErrMsg As String

            Mbr.FirstName = Subject
            Mbr.LastName = Message

            XWrite.WriteStartElement("Member")
            Mbr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                ErrMsg = CInt(PRead.m_HeaderReader.Attribute("ErrNum", "0")).ToString.PadLeft(3, "0")
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg &= " - " & SR.ReadToEnd()
                SR.Close()
                Throw New System.Exception(ErrMsg)
            Else
                PRead.Close()
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub LogBrowsers(ByVal Browser As String)
        Try
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim ErrMsg As String
            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", Browser)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "LogBrowser"))

            Mbr.UserName = Browser

            XWrite.WriteStartElement("Member")
            Mbr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                TfLog.Log.MyLog.Write("ErrMsg=" & ErrMsg, "Hbk.SetHBKey", Diagnostics.EventLogEntryType.Error)
            Else
                PRead.Close()
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub SetHBKey(ByVal Username As String, ByVal HBKey As String, ByVal HBKeyName As String, ByVal Q1 As String, ByVal Q2 As String, ByVal Q3 As String, ByVal Email As String)
        Try
            TfLog.Log.MyLog.Write("Username=" & Username)
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim ErrMsg As String
            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", Username)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "SetHBKey"))

            Mbr.UserName = Username
            Mbr.HBKey = HBKey
            Mbr.HBKeyName = HBKeyName
            Mbr.Q1 = Q1
            Mbr.Q2 = Q2
            Mbr.Q3 = Q3
            Mbr.Email = Email
            Mbr.IP = Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress)

            XWrite.WriteStartElement("Member")
            Mbr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                TfLog.Log.MyLog.Write("ErrMsg=" & ErrMsg, "Hbk.SetHBKey", Diagnostics.EventLogEntryType.Error)
                Throw New System.Exception(ErrMsg)
            Else
                PRead.Close()
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub SetUserIP(ByVal Username As String, ByRef IpAddress As String)
        Try
            TfLog.Log.MyLog.Write("Username=" & Username, "Hbk.SetUserIP")
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim ErrMsg As String
            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", Username)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Mbr.UserName = Username
            Mbr.IP = IpAddress

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "SetUserIP"))
            XWrite.WriteStartElement("Member")
            Mbr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                TfLog.Log.MyLog.Write("ErrMsg=" & ErrMsg, "Hbk.SetUserIP", Diagnostics.EventLogEntryType.Error)
                Throw New System.Exception(ErrMsg)
            Else
                PRead.Close()
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Function funGetMyIp(Optional ByVal pHost As String = "127.0.0.1") As String
        Try
            'Dim hostentry As Net.IPHostEntry = Dns.GetHostEntry(Net.Dns.GetHostName)

            Dim hostentry As Net.IPHostEntry
            If pHost = "127.0.0.1" OrElse pHost = "::1" OrElse pHost.ToLower = "loopback" OrElse pHost.ToLower = "localhost" Then
                hostentry = Net.Dns.GetHostEntry(Net.Dns.GetHostName)
                TfLog.Log.MyLog.Write("127.0.0.1 came in so used: " & Net.Dns.GetHostName)
            Else

                Dim TestIp As Net.IPAddress

                If Net.IPAddress.TryParse(pHost, TestIp) Then
                    If TestIp.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                        TfLog.Log.MyLog.Write("Was IPv4 address so return same: " & pHost)
                        Return pHost
                    End If
                    TfLog.Log.MyLog.Write("was IPv6 address so getting IPv4 address")
                Else
                    TfLog.Log.MyLog.Write("was a HostName not a IPAddress: " & pHost)
                End If

                hostentry = Net.Dns.GetHostEntry(pHost)
            End If

            For Each ip As Net.IPAddress In hostentry.AddressList
                If ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    If ip.ToString = "127.0.0.1" OrElse ip.ToString = "::1" OrElse pHost.ToLower = "loopback" OrElse pHost.ToLower = "localhost" Then
                        Continue For
                    End If
                    TfLog.Log.MyLog.Write("Found IPv4 address as: " & ip.ToString)
                    Return ip.ToString
                End If
            Next
            Return pHost
        Catch ex As System.Exception
            Return pHost
        End Try
    End Function
    Public Shared Sub LogLastLogin(ByRef UserName As String, ByVal last As String)
        Try
            TfLog.Log.MyLog.Write("Username=" & UserName, "Hbk.SetLastLogin")
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim ErrMsg As String
            Dim loIpAddress As String = funGetMyIp(HttpContext.Current.Request.UserHostAddress)
            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", loIpAddress)
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", UserName)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Mbr.UserName = UserName
            Mbr.LastLogin = last
            Mbr.IP = loIpAddress

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "SetLastLogin"))
            XWrite.WriteStartElement("Member")
            Mbr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                TfLog.Log.MyLog.Write("ErrMsg=" & ErrMsg, "Hbk.SetLastLogin", Diagnostics.EventLogEntryType.Error)
                Throw New System.Exception(ErrMsg)
            Else
                PRead.Close()
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Shared Sub LogFailedAttempt(ByRef UserName As String)
        Try
            TfLog.Log.MyLog.Write("Username=" & UserName, "Hbk. LogFailedAttempt")
            Dim Client As TCP.NetTalk.NetStream = Nothing
            Dim Mbr As New UserInfo
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")
            Dim ErrMsg As String
            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", UserName)
            Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Mbr.UserName = UserName

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "LogFailedAttempt"))
            XWrite.WriteStartElement("Member")
            Mbr.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Dim SR As New IO.StreamReader(PRead)
                ErrMsg = SR.ReadToEnd()
                SR.Close()
                TfLog.Log.MyLog.Write("ErrMsg=" & ErrMsg, "Hbk.LogFailedAttempt", Diagnostics.EventLogEntryType.Error)
                Throw New System.Exception(ErrMsg)
            Else
                PRead.Close()
            End If
        Catch ex As System.Exception
        End Try
    End Sub
    Public Overloads Shared Function getUserInfo(ByVal Data As String, ByVal field As String) As UserInfo
        TfLog.Log.MyLog.Write(field & "=" & Data, "Hbk.Getuserinfo(with acctnumber)")
        Dim Client As TCP.NetTalk.NetStream = Nothing
        Dim ErrMsg As String = Nothing
        Dim Mbr As New UserInfo
        Try
            Dim Host As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Host")
            Dim Port As Integer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Port")

            Dim Credential As New OnlineCredential("Login", "Msgs")
            Credential.AddAttribute("IP", Hbk.funGetMyIp(HttpContext.Current.Request.UserHostAddress))
            Credential.AddAttribute("Type", "Quick")
            Credential.AddAttribute("UserName2", Data)
            Try
                Client = New TCP.NetTalk.NetStream(Host, Port, Credential)
            Catch ex As System.Exception
                Mbr.UserName = "##ERROR##"
                Mbr.Password = ex.Message
                Return Mbr
            End Try
            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "GetUserInfo"))
            XWrite.WriteStartElement("UserInfo")
            XWrite.WriteTag("UserName", Data)
            XWrite.WriteTag("Password", field)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim loRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (loRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return funServerErr(loRead)
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(loRead)
                Mbr.Load(XRead)
                XRead.Close()
            End If
        Catch ex As System.Exception
            TfLog.Log.MyLog.Write(ex.Message, "Hbk.GetUserInfo", Diagnostics.EventLogEntryType.Error)
            ErrMsg = ex.Message
        Finally
            If (Not Client Is Nothing AndAlso Client.IsOpen) Then Client.Close(True)
        End Try
        Return Mbr
    End Function
    Public Shared Function GetHistCls(ByVal AcctFull As String, ByVal DT As String, ByVal NoTrans As String) As CuObjectsShr.Account.History

        Dim HReq As New CuObjectsShr.Params.History
        Dim MbrHist As New CuObjectsShr.Account.History

        Try
            Dim Acct() As String = AcctFull.Split(" ")
            If (Acct.Length <> 3) Then Return Nothing

            HReq.Acct.AcctNu = CInt(Acct(0))
            If (Acct(1) <> "LOC") Then
                HReq.Acct.Type = [Enum].Parse(GetType(TfEnumerators.EAcctType), Acct(1))
            Else
                HReq.Acct.Type = TfEnumerators.EAcctType.Loan
            End If
            HReq.Acct.Suffix = CInt(Acct(2))
            HReq.Reset = True
            If (DT <> "") Then
                DT = Hbk.FormatDate(DT, Date.Today().ToShortDateString())
                HReq.EndDate = DT
            Else
                HReq.EndDate = Date.Today()
            End If

            Select Case NoTrans
                Case "10"
                    HReq.NumRecs = 10
                Case "20"
                    HReq.NumRecs = 20
                Case "30"
                    HReq.NumRecs = 30
                Case "1_Month"
                    HReq.BegDate = HReq.EndDate.AddMonths(-1)
                    HReq.NumRecs = 100
                Case "2_Months"
                    HReq.BegDate = HReq.EndDate.AddMonths(-2)
                    HReq.NumRecs = 100
                Case "3_Months"
                    HReq.BegDate = HReq.EndDate.AddMonths(-3)
                    HReq.NumRecs = 100
                Case Else
                    HReq.NumRecs = 10
            End Select

            Dim Client As TCP.NetTalk.NetStream = Hbk.MyClient()
            Dim Alt As Boolean

            Dim XWrite As New Sys.Data.Xml.TextWriter(Client.newWriter(TCP.NetTalk.ETrxType.Request, "Hbk", "History"))
            XWrite.WriteStartElement("Hist")
            HReq.Save(XWrite)
            XWrite.WriteEndElement()
            XWrite.Close()

            Dim PRead As TCP.NetTalk.Plain.Reader = Client.newReader()
            If (PRead.m_HeaderReader.TrxType = TCP.NetTalk.ETrxType.Error) Then
                Return Nothing
            Else
                Dim XRead As New Sys.Data.Xml.TextReader(PRead)
                MbrHist.Load(XRead)
                XRead.Close()
            End If

        Catch ex As System.Exception
            MbrHist = Nothing
        End Try

        Return MbrHist

    End Function
End Class