Imports MySql.Data.MySqlClient
Imports SpeechLib
Public Class mfrmCreateTable
    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim f As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If f = 1 Then
            ListBox1.Items.Add(TextBox2.Text + " " + ComboBox1.Text + " Primary Key")
            f = 0
        Else
            ListBox1.Items.Add(TextBox2.Text + " " + ComboBox1.Text)
        End If
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
        Dim plc As Integer
        plc = str.LastIndexOf(",")
        str = str.Remove(plc, 1)
        str = str + ")"
        Try
            'MsgBox(str)
            cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = str
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            TextBox3.Text = str
            MsgBox("Table Created Successfully", MsgBoxStyle.Information, "Done")
            TextBox1.Text = ""
            ListBox1.Items.Clear()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
            MsgBox("Error in Creating table", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'TextBox2.Text = TextBox2.Text + " Primary Key"
        f = 1
        Button5.Enabled = False
    End Sub

    Private Sub frmCreateTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        f = 0
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If (TextBox3.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox3.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub
End Class