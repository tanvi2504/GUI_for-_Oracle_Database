Imports System.Data.OleDb
Public Class frmMakeAccess
    Dim cn As New OleDbConnection
    Dim cmd As New OleDbCommand
    Dim rs As OleDbDataReader
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OpenFileDialog2.ShowDialog()
        If (OpenFileDialog2.FileName <> "") Then
            TextBox1.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If (TextBox1.Text <> "") Then
                cn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + TextBox1.Text
                ComboBox1.Items.Clear()
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "select * from MSysObjects where TYPE=1"
                rs = cmd.ExecuteReader()
                While (rs.Read())
                    ComboBox1.Items.Add(rs.GetValue(0).ToString())
                End While
                rs.Close()
                cmd.Dispose()
                cn.Close()
            Else
                MsgBox("Please enter Complete details", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("" + ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (ComboBox1.SelectedIndex <> -1) Then

            RichTextBox1.Text = ""
            cn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + TextBox1.Text
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select * from " + ComboBox1.SelectedItem.ToString()
            rs = cmd.ExecuteReader()
            Dim fc As Integer
            fc = rs.FieldCount
            Dim i As Integer
            RichTextBox1.Text = "Table-" + ComboBox1.SelectedItem.ToString() + ","
            For i = 0 To fc - 1 Step 1
                RichTextBox1.Text = RichTextBox1.Text + rs.GetName(i).ToString() + "-" + rs.GetDataTypeName(i).ToString() + ","
            Next
            rs.Close()
            cmd.Dispose()
            cn.Close()
            If (RadioButton2.Checked = True) Then
                RichTextBox1.Text = RichTextBox1.Text + vbCrLf + "Data:"
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "select * from " + ComboBox1.SelectedItem.ToString()
                rs = cmd.ExecuteReader()
                Dim tf As Integer
                tf = rs.FieldCount
                Dim j As Integer
                j = 0
                While (rs.Read())
                    Dim str As String
                    str = ""
                    For j = 0 To tf - 1 Step 1
                        str = str + "'" + rs.GetValue(j).ToString() + "',"
                    Next
                    RichTextBox1.Text = RichTextBox1.Text + str + "-"
                End While
                rs.Close()
                cmd.Dispose()
                cn.Close()
            End If
            SaveFileDialog1.ShowDialog()
            If (SaveFileDialog1.FileName <> "") Then
                RichTextBox1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
                MsgBox("File saved successfully", MsgBoxStyle.Information, "Done")
            End If
        End If
    End Sub
End Class