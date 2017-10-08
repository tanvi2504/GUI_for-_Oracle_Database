Public Class frmSwitchMain

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub MakeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeToolStripMenuItem.Click

    End Sub

    Private Sub OracleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OracleToolStripMenuItem.Click
        frmMakeOracle.Show()
    End Sub

    Private Sub SwitchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchToolStripMenuItem.Click

    End Sub

    Private Sub SQLServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SQLServerToolStripMenuItem.Click
        frmSwitchSQL.Show()
    End Sub

    Private Sub AccessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccessToolStripMenuItem.Click
        frmSwitchAccess.Show()
    End Sub

    Private Sub OracleToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OracleToolStripMenuItem1.Click
        frmSwitchOracle.Show()
    End Sub

    Private Sub SQLServerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SQLServerToolStripMenuItem1.Click
        frmMakeSQL.Show()
    End Sub

    Private Sub MySQLToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MySQLToolStripMenuItem1.Click
        frmMakeMySQL.Show()
    End Sub

    Private Sub MySQLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MySQLToolStripMenuItem.Click
        frmSwitchMySQL.Show()
    End Sub

    Private Sub AccessToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccessToolStripMenuItem1.Click
        frmMakeAccess.Show()
    End Sub

    Private Sub GUIOracleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GUIOracleToolStripMenuItem.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub GUIMySQLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GUIMySQLToolStripMenuItem.Click
        Me.Close()
        mForm1.Show()
    End Sub
End Class