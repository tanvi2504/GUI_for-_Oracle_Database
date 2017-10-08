Imports System.Data.OracleClient
Imports SpeechLib
Public Class frmCreateUser
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Private Sub frmCreateUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text <> "" And TextBox2.Text <> "") Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "create user " + TextBox1.Text + " identified by " + TextBox2.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox3.Text = "create user " + TextBox1.Text + " identified by " + TextBox2.Text
                MsgBox("User Created successfully", MsgBoxStyle.Information, "Done")

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox1.Focus()
            Catch ex As Exception
                MsgBox("Error in Creating User", MsgBoxStyle.Critical, "Error")
            End Try
        Else
            MsgBox("Enter Username / Password", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox3.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox3.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub
End Class