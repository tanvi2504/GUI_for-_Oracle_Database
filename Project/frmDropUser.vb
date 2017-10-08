Imports System.Data.OracleClient
Imports SpeechLib
Public Class frmDropUser
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text <> "") Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "Drop User " + TextBox1.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox2.Text = "Drop User " + TextBox1.Text
                MsgBox("User Dropped successfully", MsgBoxStyle.Information, "Done")
                TextBox1.Text = ""
                TextBox1.Focus()
            Catch ex As Exception
                MsgBox("Error in Dropping User from Database", MsgBoxStyle.Critical, "Error")
            End Try
        Else
            MsgBox("Please Enter Username", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub frmDropUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox2.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox2.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub
End Class