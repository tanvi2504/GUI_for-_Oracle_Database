Public Class frmSplash

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Value = ProgressBar1.Value + 1
        If ProgressBar1.Value = 100 Then
            Timer1.Enabled = False
            Me.Hide()
            frmSelection.Show()
        End If
    End Sub
End Class