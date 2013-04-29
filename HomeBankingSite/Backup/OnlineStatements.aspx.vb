Imports Tf.AllV2
Imports Tf.AllV2.TfGlobals

Partial Class OnlineStatements
    Inherits System.Web.UI.Page
    '
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnOK As System.Web.UI.WebControls.Button

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
            Response.Cache.SetExpires(DateTime.Now) '
            Response.ClearHeaders()
            If (Not IsPostBack()) Then
                Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts()
                '
                If (DirectCast(Accts.Item(0), Hbk.Member).OnlineStatements <> Hbk.EOnlineStatements.SignedUp) Then
                    Me.txtEmail.Text = DirectCast(Accts.Item(0), Hbk.Member).Email
                    Me.pnlSetup.Visible = True
                    Me.pnlStatements.Visible = False
                    If (DirectCast(Accts.Item(0), Hbk.Member).OnlineStatements = Hbk.EOnlineStatements.SignUp) Then
                        Me.btnConsent.Text = "Resend"
                        Me.lblStatus.Text = "(An email has been sent to you to validate the email address entered.  If you do not receive the email within 10 minutes, double check the email address entered, correct it, and click ""Resend"" to send the email validation to the correct email address.)"
                    End If
                Else
                    Me.pnlSetup.Visible = False
                    Me.pnlStatements.Visible = True
                    If (DirectCast(Accts.Item(0), Hbk.Member).Email Is Nothing OrElse DirectCast(Accts.Item(0), Hbk.Member).Email = "") Then
                        Response.Redirect("~/ChangeLogin.aspx?OS=True")
                    End If
                End If

            End If
        Catch ex As System.Exception

        End Try

    End Sub

    Protected Sub btnConsent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsent.Click
        Try
            Hbk.SetOnlineStatements(Me.txtEmail.Text, Hbk.EOnlineStatements.SignUp, "")
            Me.btnConsent.Text = "Resend"
            Me.lblStatus.Text = "(An email has been sent to you to validate the email address entered.  If you do not receive the email within 10 minutes, double check the email address entered, correct it, and click ""Resend"" to send the email validation to the correct email address.)"
        Catch ex As System.Exception
        End Try
    End Sub

    Public Function GetUrl(ByVal Statement As String, ByVal FileName As String) As String
        Try
            Statement = Statement.Replace("/", "|").Replace(" ", "%20")
            FileName = FileName.Replace(" ", "%20") & ".pdf"
            Dim ret As String = "javascript:ViewStatement('" & Statement & "', '" & FileName & "', '" & cboAccount.Text & "');return false;"
            'Dim ret As String = "javascript:window.open('ViewStatement.aspx?a=" + cboAccount.Text + "&s=" + Statement + "&f=" + FileName + "');"
            'Response.Redirect("ViewStatement.aspx?a=" + cboAccount.Text + "&s=" + Statement + "&f=" + FileName)
            '"ViewStatement.aspx?a=" + document.getElementById("<%=cboAccount.ClientID %>").value + "&s=" + Statment + "&f=" + FileName;
        Catch ex As System.Exception
        End Try
        Return ""
    End Function

    Public Sub popStatements(ByVal year As String)
        Try
            Dim lstItm As WebControls.ListItem
            Dim Dirs() As String = System.IO.Directory.GetDirectories(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StatementDirectory"))
            Dim SDt As String = ""
            Dim DDt As Date = Nothing
            Dim outText As String
            Dim statementPath As String = ""
            lbStatements.Items.Clear()
            lstItm = New WebControls.ListItem
            lstItm.Text = "--Select a Statement--"
            lstItm.Value = ""
            lbStatements.Items.Add(lstItm)
            For Each Dir As String In Dirs
                lstItm = New WebControls.ListItem

                Dir = System.IO.Path.GetFileName(Dir)
                Dir = Dir.Replace(".", "/")
                outText = ""
                SDt = Dir.Replace(" ", "").Split("-")(1)
                DDt = Date.Parse(SDt)
                '
                Dim DateOne As Date = Dir.Replace(" ", "").Split("-")(0)
                Dim DateTwo As Date = Dir.Replace(" ", "").Split("-")(1)
                If DateOne.Year.ToString = year Then
                    If DateOne.AddMonths(2) < DateTwo Then
                        outText &= "Q " & DDt.ToString("MMMM yyyy")
                    Else
                        outText &= "M " & DDt.ToString("MMMM yyyy")
                    End If
                    statementPath = Dir
                    'outText &= DDt.Year
                    'outText &= DDt.Month
                    'If (DDt.Year > GYear) Then GYear = DDt.Year
                    lstItm.Text = outText
                    lstItm.Value = statementPath
                    lbStatements.Items.Add(lstItm)
                End If
            Next
            lbStatements.SelectedIndex = 0
        Catch ex As System.Exception
        End Try
    End Sub

    Private Sub cboYear_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.DataBound
        Try
            popStatements(cboYear.Items(0).Text)
        Catch ex As System.Exception
        End Try

    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        'Me.GridView1.DataBind()
        Try
            popStatements(cboYear.Items(cboYear.SelectedIndex).Text)
        Catch ex As System.Exception

        End Try

    End Sub

    'Private Sub btnGetStatement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetStatement.Click
    '    Dim acct As String = cboAccount.Text
    '    Dim statement As String = ""
    '    Dim FileName As String = ""
    '    Response.Redirect("ViewStatement.aspx?a=" + acct + "&s=" + statement + "&f=" + FileName)
    'End Sub

    Private Sub lbStatements_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatements.SelectedIndexChanged
        Try
            If lbStatements.SelectedItem.Value <> "" Then
                Dim acct As String = cboAccount.Text
                Dim statement As String = lbStatements.SelectedItem.Value
                Dim FileName As String = lbStatements.SelectedItem.Text
                statement = statement.Replace("/", "|").Replace(" ", "%20")
                FileName = FileName.Replace(" ", "%20")
                Dim script1 As String = ""
                script1 = "<script language=""javascript"" type=""text/javascript"">"
                script1 &= "var win = window.open(""DownloadingPage.aspx?a=" + acct + "&s=" + statement + "&f=" + FileName + """, ""Downloading"", ""width=400,height=350"");"
                'script1 &= "if (!win)"
                'script1 &= "document.write(""<div id='blockedpoput' >"");"
                'script1 &= "document.write(""<asp:Label ID='Label1' runat='server' Style='color: Black;' Text='Label'>"");"
                'script1 &= "document.write(""Your browser has blocked your statement from opening in a new window."");"
                'script1 &= "document.write(""</asp:Label>"");"
                'script1 &= "document.write(""<asp:Image ID='ImgBlocker' runat='server' ImageUrl='~/Imgs/popupblocker.bmp' />"");"
                'script1 &= "document.write(""<br />"");"
                'script1 &= "document.write(""<br />"");"
                'script1 &= "document.write(""<asp:Label ID='LblBlocker' runat='server' Style='color: Black;' Text='Label'>Some browsers may require users to allow Pop-ups. If using Internet Explorer, click on the yellow bar at the top of the page and then select 'Always Allow Pop-ups From This Site...' "");"
                'script1 &= "document.write(""If using another browser with Pop-ups disabled, you will need to permit them from this site. "");"
                'script1 &= "document.write(""</asp:Label>"");"
                'script1 &= "document.write(""</div>"");"
                script1 &= "</script>"
                Response.Write(script1)
            End If
        Catch ex As System.Exception
        End Try
    End Sub
End Class

<System.ComponentModel.DataObject()> _
Public Class StatementAccounts
    <System.ComponentModel.DataObjectMethodAttribute(ComponentModel.DataObjectMethodType.Select, True)> _
    Public Function GetAccounts() As DataTable
        Try
            Dim DT As New DataTable("StatementAccounts")
            DT.Columns.Add(New System.Data.DataColumn("Number"))

            Dim Accts As Sys.Data.XMLArray = Hbk.MyAccts()
            Dim Row As System.Data.DataRow = Nothing
            For Each Acct As Hbk.Member In Accts
                If (Acct.Permission = Hbk.EHbkPermission.High) Then
                    Row = DT.NewRow()
                    Row.Item(0) = Acct.Number
                    DT.Rows.Add(Row)
                End If
            Next
            Return DT
        Catch ex As System.Exception
            Return Nothing
        End Try
    End Function
End Class

<System.ComponentModel.DataObject()> _
Public Class Statements
    <System.ComponentModel.DataObjectMethodAttribute(ComponentModel.DataObjectMethodType.Select, True)> _
    Public Function GetStatements(ByVal Year As Integer) As DataTable
        Try
            Dim DT As New DataTable("Statements")
            DT.Columns.Add(New System.Data.DataColumn("Statement"))
            DT.Columns.Add(New System.Data.DataColumn("Desc"))
            DT.Columns.Add(New System.Data.DataColumn("Year", GetType(Integer)))
            DT.Columns.Add(New System.Data.DataColumn("Month", GetType(Integer)))
            Dim Dirs() As String = System.IO.Directory.GetDirectories(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StatementDirectory"))
            Dim Row As DataRow = Nothing
            Dim SDt As String = ""
            Dim DDt As Date = Nothing
            Dim GYear As Integer
            For Each Dir As String In Dirs
                Row = DT.NewRow()
                Dir = System.IO.Path.GetFileName(Dir)
                Dir = Dir.Replace(".", "/")
                Row.Item(0) = Dir
                SDt = Dir.Replace(" ", "").Split("-")(1)
                DDt = Date.Parse(SDt)
                'Row.Item(1) = DDt.ToString("MMMM yyyy")

                Dim DateOne As Date = Dir.Replace(" ", "").Split("-")(0)
                Dim DateTwo As Date = Dir.Replace(" ", "").Split("-")(1)
                If DateOne.AddMonths(2) < DateTwo Then
                    Row.Item(1) = "Q " & DDt.ToString("MMMM yyyy")
                Else
                    Row.Item(1) = "M " & DDt.ToString("MMMM yyyy")
                End If

                Row.Item(2) = DDt.Year
                Row.Item(3) = DDt.Month
                If (DDt.Year > GYear) Then GYear = DDt.Year
                DT.Rows.Add(Row)
            Next

            If (Year = 0) Then Year = GYear
            Dim Ret As New DataTable("Statements")
            Ret.Columns.Add(New System.Data.DataColumn("Statement"))
            Ret.Columns.Add(New System.Data.DataColumn("Desc"))
            Ret.Columns.Add(New System.Data.DataColumn("Year", GetType(Integer)))
            Ret.Columns.Add(New System.Data.DataColumn("Month", GetType(Integer)))

            Dim Rows() As DataRow = DT.Select("Year=" & Year, "Month DESC")
            Dim RRow As DataRow = Nothing
            For Each Row In Rows
                RRow = Ret.NewRow()
                RRow.Item(0) = Row.Item(0)
                RRow.Item(1) = Row.Item(1)
                RRow.Item(2) = Row.Item(2)
                RRow.Item(3) = Row.Item(3)
                Ret.Rows.Add(RRow)
            Next

            Return Ret
        Catch ex As System.Exception
            Return Nothing
        End Try

    End Function

    <System.ComponentModel.DataObjectMethodAttribute(ComponentModel.DataObjectMethodType.Select, False)> _
    Public Function GetStatementYears() As DataTable
        Dim DT As New DataTable("Statements")
        DT.Columns.Add(New System.Data.DataColumn("Year", GetType(Integer)))
        Dim Dirs() As String = System.IO.Directory.GetDirectories(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("StatementDirectory"))
        Dim Row As DataRow = Nothing
        Dim SDt As String = ""
        Dim DDt As Date = Nothing
        For Each Dir As String In Dirs
            Row = DT.NewRow()
            Dir = System.IO.Path.GetFileName(Dir)
            Dir = Dir.Replace(".", "/")
            SDt = Dir.Replace(" ", "").Split("-")(1)
            DDt = Date.Parse(SDt)
            Row.Item(0) = DDt.Year
            DT.Rows.Add(Row)
        Next

        Dim Ret As DataTable = SelectDistinct("Statements", DT, "Year")

        Return Ret
    End Function

    Private Shared Function ColumnEqual(ByRef A As Object, ByRef B As Object) As Boolean
        If (A Is DBNull.Value AndAlso B Is DBNull.Value) Then
            Return True
        End If
        If (A Is DBNull.Value OrElse B Is DBNull.Value) Then
            Return False
        End If
        Return (A.Equals(B))
    End Function

    Private Shared Function SelectDistinct(ByVal TableName As String, ByRef SourceTable As DataTable, ByVal FieldName As String) As DataTable
        Dim dt As DataTable = New DataTable(TableName)
        dt.Columns.Add(FieldName, SourceTable.Columns(FieldName).DataType)

        Dim LastValue As Object = Nothing
        For Each dr As DataRow In SourceTable.Select("", FieldName & " DESC")
            If (LastValue = Nothing OrElse Not (ColumnEqual(LastValue, dr(FieldName)))) Then
                LastValue = dr(FieldName)
                dt.Rows.Add(New Object() {LastValue})
            End If
        Next
        Return dt
    End Function

End Class