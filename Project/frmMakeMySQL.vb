Imports MySql.Data.MySqlClient
Public Class frmMakeMySQL
    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim rs As MySqlDataReader
    Private Sub frmMakeMySQL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If (TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "") Then
                cn.ConnectionString = "Server=localhost;Database=" + TextBox1.Text + ";Uid=" + TextBox2.Text + ";Pwd=" + TextBox3.Text + ";"
                ComboBox1.Items.Clear()
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "show tables"
                rs = cmd.ExecuteReader()
                While (rs.Read())
                    ComboBox1.Items.Add(rs.GetValue(0).ToString())
                End While
                rs.Close()
                cmd.Dispose()
                cn.Close()
                GroupBox1.Enabled = True
            Else
                MsgBox("Please enter Complete details", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Error in Connecting to Database", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (ComboBox1.SelectedIndex <> -1) Then

            RichTextBox1.Text = ""
            cn.ConnectionString = "Server=127.0.0.1;Database=" + TextBox1.Text + ";Uid=" + TextBox2.Text + ";Pwd=" + TextBox3.Text + ";"
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