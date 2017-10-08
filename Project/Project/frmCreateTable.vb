Imports System.Data.OracleClient
Public Class frmCreateTable
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Add(TextBox2.Text + " " + ComboBox1.Text)
        TextBox2.Text = ""
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter table name", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        If ListBox1.Items.Count <= 0 Then
            MsgBox("Please enter fields", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        Dim str As String
        Dim i As Integer
        i = 0
        str = ""
        str = "create table " + TextBox1.Text + "("
        For i = 0 To ListBox1.Items.Count - 1 Step 1
            ListBox1.SelectedIndex = i
            str = str + ListBox1.Text + ","
        Next
        str = str(str.Length - 1) = ")"
        Try
            cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = str
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MsgBox("Table Created Successfully", MsgBoxStyle.Information, "Done")
            TextBox1.Text = ""
            ListBox1.Items.Clear()
        Catch ex As Exception
            MsgBox("Error in Creating table", MsgBoxStyle.Critical, "Error")
        End Try
        
    End Sub
End Class