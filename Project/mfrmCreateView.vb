Imports MySql.Data.MySqlClient
Imports SpeechLib
Public Class mfrmCreateView

    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim rs As MySqlDataReader
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox1.Text + ""
        rs = cmd.ExecuteReader()
        While (rs.Read())
            CheckedListBox1.Items.Add(rs.getValue(0).ToString())
        End While
        rs.Close()
        cmd.Dispose()
        cn.Close()
    End Sub

    Private Sub frmCreateView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "SHOW TABLES"
        rs = cmd.ExecuteReader()
        While (rs.Read())
            ComboBox1.Items.Add(rs.GetValue(0).ToString())
        End While
        rs.Close()
        cmd.Dispose()
        cn.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter table name", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        If CheckedListBox1.CheckedItems.Count <= 0 Then
            MsgBox("Please enter fields", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        Dim str As String
        Dim i As Integer
        i = 0
        str = ""
        str = "create view " + TextBox1.Text + " as select "
        For i = 0 To CheckedListBox1.CheckedItems.Count - 1 Step 1
            str = str + CheckedListBox1.CheckedItems.Item(i).ToString() + ","
        Next
        Dim plc As Integer
        plc = str.LastIndexOf(",")
        str = str.Remove(plc, 1)
        str = str + " from " + ComboBox1.Text
        If (CheckBox1.Checked = True And TextBox3.Text <> "") Then
            str = str + " where " + TextBox3.Text
        End If
        Try
            'MsgBox(str)
            cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = str
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            TextBox2.Text = str
            MsgBox("View Created Successfully", MsgBoxStyle.Information, "Done")
            TextBox1.Text = ""
            ComboBox1.Items.Clear()
            CheckedListBox1.Items.Clear()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
            MsgBox("Error in Creating view", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox2.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox2.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If (CheckBox1.Checked = True) Then
            TextBox3.ReadOnly = False
            TextBox3.Focus()
        Else
            TextBox3.ReadOnly = True
            TextBox3.Text = ""
        End If
    End Sub
End Class