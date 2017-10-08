Imports MySql.Data.MySqlClient
Imports SpeechLib
Public Class mfrmDropTable
    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim rs As MySqlDataReader
    Private Sub frmDropTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        ListBox1.Items.Clear()
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox2.Text + ""
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ListBox1.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
            MsgBox("Error in getting Fields from Table", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim a
        a = MsgBox("Do you wish to continue dropping the table ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm")
        If (a = vbYes) Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "drop table " + ComboBox2.SelectedItem
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox1.Text = "drop table " + ComboBox2.SelectedItem
                MsgBox("Table dropped successfully", MsgBoxStyle.Information, "Done")
            Catch ex As Exception
                MsgBox("Error in dropping Tables from Database", MsgBoxStyle.Critical, "Error")
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