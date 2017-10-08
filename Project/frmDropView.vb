Imports System.Data.OracleClient
Imports SpeechLib
Public Class frmDropView
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim rs As OracleDataReader


    Private Sub frmDropView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select * from user_views"
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ComboBox1.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting View from Database", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim a
        a = MsgBox("Do you wish to continue dropping the table ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm")
        If (a = vbYes) Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "drop view " + ComboBox1.SelectedItem
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox1.Text = "drop view " + ComboBox1.SelectedItem
                MsgBox("View dropped successfully", MsgBoxStyle.Information, "Done")
            Catch ex As Exception
                MsgBox("Error in dropping View from Database", MsgBoxStyle.Critical, "Error")
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