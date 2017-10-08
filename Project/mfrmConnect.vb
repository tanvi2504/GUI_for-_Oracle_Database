Public Class mfrmConnect

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter Server Name", MsgBoxStyle.Critical, "Error")
            TextBox1.Focus()
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox("Please enter Database Name", MsgBoxStyle.Critical, "Error")
            TextBox2.Focus()
            Exit Sub
        End If
        If TextBox3.Text = "" Then
            MsgBox("Please enter Username", MsgBoxStyle.Critical, "Error")
            TextBox3.Focus()
            Exit Sub
        End If
        If TextBox4.Text = "" Then
            MsgBox("Please enter Password", MsgBoxStyle.Critical, "Error")
            TextBox4.Focus()
            Exit Sub
        End If
        mModule1.ServerName = TextBox1.Text
        mModule1.DatabaseName = TextBox2.Text
        mModule1.Username = TextBox3.Text
        mModule1.Password = TextBox4.Text
        Me.Close()
        mForm1.Connect_Database()
    End Sub

    Private Sub mfrmConnect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class