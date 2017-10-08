Public Class frmConnect

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter server name", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        Module1.ServerName = TextBox1.Text
        Module1.Username = TextBox2.Text
        Module1.Password = TextBox3.Text
        Me.Hide()
        Form1.Connect_Database()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBox1.Enabled = True
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBox1.Enabled = False
    End Sub
End Class