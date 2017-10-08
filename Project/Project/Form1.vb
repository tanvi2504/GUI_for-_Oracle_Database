Imports System.Data.OracleClient
Public Class Form1
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim rs As OracleDataReader

    Private Sub CreateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateToolStripMenuItem.Click
        frmCreateTable.Show()
    End Sub

    Private Sub AlterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlterToolStripMenuItem.Click
        frmAlterTable.Show()
    End Sub

    Private Sub DropToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropToolStripMenuItem.Click
        frmDropTable.Show()
    End Sub

    Private Sub CreateToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateToolStripMenuItem1.Click
        frmCreateView.Show()
    End Sub

    Private Sub AlterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlterToolStripMenuItem1.Click
        frmAlterView.Show()
    End Sub

    Private Sub DropToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropToolStripMenuItem1.Click
        frmDropView.Show()
    End Sub

    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        DataGridView1.DataSource = Nothing
        DataGridView1.Visible = False
        frmConnect.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub Connect_Database()
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Visible = False
            cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select * from tabs"
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ListBox1.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting Tables from Database", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            DataGridView1.Visible = True
            Module1.TableName = ListBox1.SelectedItem
            cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn.Open()
            Dim adp As New OracleDataAdapter("SELECT * FROM " + Module1.TableName, cn)
            Dim ds As New DataSet
            ds.Tables.Add(Module1.TableName)
            adp.Fill(ds.Tables(Module1.TableName))
            DataGridView1.DataSource = ds.Tables(Module1.TableName)
            adp.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting Data from Table", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub


End Class
