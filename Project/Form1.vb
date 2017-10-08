Imports System.Data.OracleClient
Public Class Form1
    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim rs As OracleDataReader

    '----------------------------------------
    Dim cn1 As New OracleConnection
    Dim cmd1 As OracleCommand
    Dim ds1 As New DataSet
    Dim adp1 As OracleDataAdapter
    Dim rs1 As OracleDataReader

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

    Private Sub AlterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DropToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropToolStripMenuItem1.Click
        frmDropView.Show()
    End Sub

    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
            ListBox2.Items.Clear()
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select * from user_views"
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ListBox2.Items.Add(rs.GetValue(0).ToString())
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
            If cn1.State = ConnectionState.Open Then
                cn1.Close()
            End If
            cn1.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn1.Open()
            cmd1 = New OracleCommand("select * from " + Module1.TableName, cn1)
            adp1 = New OracleDataAdapter(cmd1)
            'ds1.Tables.Remove(Module1.TableName)
            ds1 = New DataSet
            'ds1.Clear()
            DataGridView1.DataSource = Nothing
            ds1.Tables.Add(Module1.TableName)
            adp1.Fill(ds1, Module1.TableName)
            Me.DataGridView1.DataSource = ds1
            Me.DataGridView1.DataMember = Module1.TableName
            Button1.Visible = True
            GroupBox2.Visible = True
            ComboBox1.Items.Clear()
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select column_name from user_tab_columns where table_name='" + Module1.TableName + "'"
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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        Dim cmdbui As New OracleCommandBuilder(adp1)
        Try
            Dim i As Integer
            i = adp1.Update(ds1, Module1.TableName)
            MsgBox("Record Updated - " + i.ToString())
        Catch ex As Exception
            MsgBox("Invalid Data. Please check and Re-Try", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            TextBox1.Text = ""
            DataGridView1.Visible = True
            If cn1.State = ConnectionState.Open Then
                cn1.Close()
            End If
            cn1.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn1.Open()
            cmd1 = New OracleCommand("select * from " + Module1.TableName, cn1)
            adp1 = New OracleDataAdapter(cmd1)
            'ds1.Tables.Remove(Module1.TableName)
            ds1 = New DataSet
            'ds1.Clear()
            DataGridView1.DataSource = Nothing
            ds1.Tables.Add(Module1.TableName)
            adp1.Fill(ds1, Module1.TableName)
            Me.DataGridView1.DataSource = ds1
            Me.DataGridView1.DataMember = Module1.TableName
            rs = cmd.ExecuteReader()
            Dim fc As Integer
            fc = rs.FieldCount
            Dim i As Integer
            For i = 0 To fc - 1 Step 1

                ComboBox1.Items.Add(rs.GetName(i).ToString())
            Next
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting Data from Table", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Not TextBox1.Text = "" Then
            Try
                If cn1.State = ConnectionState.Open Then
                    cn1.Close()
                End If
                cn1.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
                cn1.Open()
                cmd1 = New OracleCommand("select * from " + Module1.TableName + " where " + ComboBox1.Text + ComboBox2.Text + "'" + TextBox1.Text + "'", cn1)
                adp1 = New OracleDataAdapter(cmd1)
                'ds1.Tables.Remove(Module1.TableName)
                ds1 = New DataSet
                'ds1.Clear()
                DataGridView1.DataSource = Nothing
                ds1.Tables.Add(Module1.TableName)
                adp1.Fill(ds1, Module1.TableName)
                Me.DataGridView1.DataSource = ds1
                Me.DataGridView1.DataMember = Module1.TableName
            Catch ex As Exception
                MsgBox("Error in getting Data from Table", MsgBoxStyle.Critical, "Error")
            End Try
        Else
            MsgBox("Enter Data to Search", MsgBoxStyle.Critical, "Error")
        End If

    End Sub

    Private Sub GrantToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrantToolStripMenuItem.Click
        frmGrant.Show()
    End Sub

    Private Sub RevokeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RevokeToolStripMenuItem.Click
        frmRevoke.Show()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            DataGridView1.Visible = True
            Module1.TableName = ListBox2.SelectedItem
            If cn1.State = ConnectionState.Open Then
                cn1.Close()
            End If
            cn1.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
            cn1.Open()
            cmd1 = New OracleCommand("select * from " + Module1.TableName, cn1)
            adp1 = New OracleDataAdapter(cmd1)
            'ds1.Tables.Remove(Module1.TableName)
            ds1 = New DataSet
            'ds1.Clear()
            DataGridView1.DataSource = Nothing
            ds1.Tables.Add(Module1.TableName)
            adp1.Fill(ds1, Module1.TableName)
            Me.DataGridView1.DataSource = ds1
            Me.DataGridView1.DataMember = Module1.TableName
            Button1.Visible = True
            GroupBox2.Visible = True
            ComboBox1.Items.Clear()
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select column_name from user_tab_columns where table_name='" + Module1.TableName + "'"
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

    Private Sub CreateUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateUserToolStripMenuItem.Click
        frmCreateUser.Show()
    End Sub

    Private Sub DropUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropUserToolStripMenuItem.Click
        frmDropUser.Show()
    End Sub

    Private Sub GrantSystemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrantSystemToolStripMenuItem.Click
        frmGrantS.Show()
    End Sub

    Private Sub RevokeSystemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RevokeSystemToolStripMenuItem.Click
        frmRevokeS.Show()
    End Sub

    Private Sub ConnectToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        DataGridView1.DataSource = Nothing
        DataGridView1.Visible = False
        frmConnect.Show()
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        DataGridView1.DataSource = Nothing
        DataGridView1.Visible = False
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Button1.Visible = False
        GroupBox2.Visible = False
        Module1.Password = ""
        Module1.ServerMode = 0
        Module1.ServerName = ""
        Module1.TableName = ""
        Module1.Username = ""
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Connect_Database()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AggregateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AggregateToolStripMenuItem.Click
        frmAgg.Show()
    End Sub

    Private Sub JoinsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JoinsToolStripMenuItem.Click
        frmJoins.Show()
    End Sub

    Private Sub SubqueryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubqueryToolStripMenuItem.Click
        frmSub.Show()
    End Sub

    Private Sub SwitchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchToolStripMenuItem.Click
        Me.Close()
        frmSwitchMain.Show()
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        MsgBox("Invalid Data. Please enter Correct Data.", MsgBoxStyle.Critical, "Erro")
    End Sub
End Class
