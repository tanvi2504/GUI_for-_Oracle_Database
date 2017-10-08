Imports MySql.Data.MySqlClient
Imports SpeechLib
Public Class mfrmAlterTable
    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim rs As MySqlDataReader
    Private Sub frmAlterTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "SHOW TABLES"
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ComboBox2.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting Tables from Database", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a
        a = MsgBox("Do you wish to alter table ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm")
        If (a = vbYes) Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "alter table " + ComboBox2.Text + " add " + TextBox2.Text + " " + ComboBox1.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox1.Text = "alter table " + ComboBox2.Text + " add " + TextBox2.Text + " " + ComboBox1.Text
                MsgBox("Table Altered successfully", MsgBoxStyle.Information, "Done")
            Catch ex As Exception
                MsgBox("Error in altering table", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a
        a = MsgBox("Do you wish to alter table ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm")
        If (a = vbYes) Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "alter table " + ComboBox2.Text + " drop " + TextBox2.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox1.Text = "alter table " + ComboBox2.Text + " drop " + TextBox2.Text
                MsgBox("Table Altered successfully", MsgBoxStyle.Information, "Done")
            Catch ex As Exception
                MsgBox("Error in altering table", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox1.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox1.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub
End Class