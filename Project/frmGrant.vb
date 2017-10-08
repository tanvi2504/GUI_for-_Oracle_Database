Imports System.Data.OracleClient
Imports SpeechLib
Public Class frmGrant
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim rs As OracleDataReader

    Private Sub frmGrant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select * from tabs"
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
        a = MsgBox("Do you wish to grant rights on selected table ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm")
        If (a = vbYes) Then
            Try
                cn.Open()
                cmd.Connection = cn
                cmd.CommandText = "Grant " + ComboBox1.Text + " On " + ComboBox2.Text + " to " + TextBox3.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()
                TextBox2.Text = "Grant " + ComboBox1.Text + " On " + ComboBox2.Text + " to " + TextBox3.Text
                MsgBox("Grant successfully", MsgBoxStyle.Information, "Done")
            Catch ex As Exception
                MsgBox("Error in granting rights from Database", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox2.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox2.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub
End Class