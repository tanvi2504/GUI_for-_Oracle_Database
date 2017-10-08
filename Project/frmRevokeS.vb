Imports System.Data.OracleClient
Imports SpeechLib
Public Class frmRevokeS
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim rs As OracleDataReader
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a
        a = MsgBox("Do you wish to revoke rights from user ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm")
        If (a = vbYes) Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "revoke " + ComboBox1.SelectedItem.ToString + " from " + TextBox1.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox2.Text = "revoke " + ComboBox1.SelectedItem.ToString + " from " + TextBox1.Text
                MsgBox("Revoke successful", MsgBoxStyle.Information, "Done")
            Catch ex As Exception
                MsgBox("Error in granting rights from Database", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub frmRevokeS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox2.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox2.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub
End Class