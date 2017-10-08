Imports MySql.Data.MySqlClient
Public Class mForm1
    Dim cn As New MySqlConnection ' Connect MySQL and .Net
    Dim cmd As New MySqlCommand ' Execute Query
    Dim rs As MySqlDataReader ' Read Data (Row by Row)

    '----------------------------------------
    Dim cn1 As New MySqlConnection
    Dim cmd1 As MySqlCommand
    Dim ds1 As New DataSet 'Struct of Database Table
    Dim adp1 As MySqlDataAdapter ' Fetch Data (Bulk)
    Dim rs1 As MySqlDataReader

    Private Sub CreateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateToolStripMenuItem.Click
        mfrmCreateTable.Show()
    End Sub

    Private Sub AlterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlterToolStripMenuItem.Click
        mfrmAlterTable.Show()
    End Sub

    Private Sub DropToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropToolStripMenuItem.Click
        mfrmDropTable.Show()
    End Sub

    Public Sub Connect_Database()
        Try
            ListBox1.Items.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            TextBox1.Text = ""
            Button1.Visible = False
            GroupBox2.Visible = False
            DataGridView1.DataSource = Nothing
            DataGridView1.Visible = False
            cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "show tables"
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
            mModule1.TableName = ListBox1.SelectedItem
            If cn1.State = ConnectionState.Open Then
                cn1.Close()
            End If
            cn1.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
            cn1.Open()
            cmd1 = New MySqlCommand("select * from " + mModule1.TableName, cn1)
            adp1 = New MySqlDataAdapter(cmd1)
            'ds1.Tables.Remove(mModule1.TableName)
            ds1 = New DataSet
            'ds1.Clear()
            DataGridView1.DataSource = Nothing
            ds1.Tables.Add(mModule1.TableName)
            adp1.Fill(ds1, mModule1.TableName)
            Me.DataGridView1.DataSource = ds1
            Me.DataGridView1.DataMember = mModule1.TableName
            Button1.Visible = True
            GroupBox2.Visible = True
            ComboBox1.Items.Clear()
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "SHOW COLUMNS FROM " + mModule1.TableName + ""
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ComboBox1.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting Data from Table", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ConnectToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        DataGridView1.DataSource = Nothing
        DataGridView1.Visible = False
        mfrmConnect.Show()
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        DataGridView1.DataSource = Nothing
        DataGridView1.Visible = False
        ListBox1.Items.Clear()
        Button1.Visible = False
        GroupBox2.Visible = False
        mModule1.Password = ""
        mModule1.DatabaseName = ""
        mModule1.ServerName = ""
        mModule1.TableName = ""
        mModule1.Username = ""
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Connect_Database()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub CreateToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateToolStripMenuItem1.Click
        mfrmCreateView.Show()
    End Sub

    Private Sub DropToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropToolStripMenuItem1.Click
        mfrmDropView.Show()
    End Sub

    Private Sub AggregateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AggregateToolStripMenuItem.Click
        mfrmAgg.Show()
    End Sub

    Private Sub JoinsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JoinsToolStripMenuItem.Click
        mfrmJoins.Show()
    End Sub

    Private Sub SubQueryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubQueryToolStripMenuItem.Click
        mfrmSub.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cmdbui As New MySqlCommandBuilder(adp1)
        Try
            Dim i As Integer
            i = adp1.Update(ds1, mModule1.TableName)
            MsgBox("Record Updated - " + i.ToString())
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub mForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub SwitchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchToolStripMenuItem.Click
        frmSwitchMain.Show()
    End Sub

    
    Private Sub DataGridView1_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        MsgBox("Invalid Data. Please enter Correct Data.", MsgBoxStyle.Critical, "Error")
    End Sub
End Class
