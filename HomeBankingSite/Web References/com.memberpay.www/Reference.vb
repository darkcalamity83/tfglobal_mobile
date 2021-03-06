﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.1433.
'
Namespace com.memberpay.www
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="SingleSignOnSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SingleSignOn
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private AuthenticateMemberOperationCompleted As System.Threading.SendOrPostCallback
        
        Private LoginOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.HomeBankingSite.My.MySettings.Default.HomeBankingSite_com_memberpay_www_SingleSignOn
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event AuthenticateMemberCompleted As AuthenticateMemberCompletedEventHandler
        
        '''<remarks/>
        Public Event LoginCompleted As LoginCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AuthenticateMember", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function AuthenticateMember(ByVal request As String) As String
            Dim results() As Object = Me.Invoke("AuthenticateMember", New Object() {request})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub AuthenticateMemberAsync(ByVal request As String)
            Me.AuthenticateMemberAsync(request, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub AuthenticateMemberAsync(ByVal request As String, ByVal userState As Object)
            If (Me.AuthenticateMemberOperationCompleted Is Nothing) Then
                Me.AuthenticateMemberOperationCompleted = AddressOf Me.OnAuthenticateMemberOperationCompleted
            End If
            Me.InvokeAsync("AuthenticateMember", New Object() {request}, Me.AuthenticateMemberOperationCompleted, userState)
        End Sub
        
        Private Sub OnAuthenticateMemberOperationCompleted(ByVal arg As Object)
            If (Not (Me.AuthenticateMemberCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent AuthenticateMemberCompleted(Me, New AuthenticateMemberCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Login(ByVal CU_ID As Long, ByVal USER_ID As String, ByVal PASSWORD As String, ByVal ON_BEHALF_OF As String, ByVal ON_BEHALF_OF_CU As String) As String
            Dim results() As Object = Me.Invoke("Login", New Object() {CU_ID, USER_ID, PASSWORD, ON_BEHALF_OF, ON_BEHALF_OF_CU})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub LoginAsync(ByVal CU_ID As Long, ByVal USER_ID As String, ByVal PASSWORD As String, ByVal ON_BEHALF_OF As String, ByVal ON_BEHALF_OF_CU As String)
            Me.LoginAsync(CU_ID, USER_ID, PASSWORD, ON_BEHALF_OF, ON_BEHALF_OF_CU, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub LoginAsync(ByVal CU_ID As Long, ByVal USER_ID As String, ByVal PASSWORD As String, ByVal ON_BEHALF_OF As String, ByVal ON_BEHALF_OF_CU As String, ByVal userState As Object)
            If (Me.LoginOperationCompleted Is Nothing) Then
                Me.LoginOperationCompleted = AddressOf Me.OnLoginOperationCompleted
            End If
            Me.InvokeAsync("Login", New Object() {CU_ID, USER_ID, PASSWORD, ON_BEHALF_OF, ON_BEHALF_OF_CU}, Me.LoginOperationCompleted, userState)
        End Sub
        
        Private Sub OnLoginOperationCompleted(ByVal arg As Object)
            If (Not (Me.LoginCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoginCompleted(Me, New LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")>  _
    Public Delegate Sub AuthenticateMemberCompletedEventHandler(ByVal sender As Object, ByVal e As AuthenticateMemberCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class AuthenticateMemberCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")>  _
    Public Delegate Sub LoginCompletedEventHandler(ByVal sender As Object, ByVal e As LoginCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class LoginCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
End Namespace
