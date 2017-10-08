Imports System.Data.OracleClient
Public Class frmCreateView

    Dim cn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim rs As OracleDataReader
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select column_name from user_tab_columns where table_name='" + ComboBox1.Text + "'"
        rs = cmd.ExecuteReader()
        While (rs.Read())
            CheckedListBox1.Items.Add(rs.getValue(0).ToString())
        End While
        rs.Close()
        cmd.Dispose()
        cn.Close()
    End Sub

    Private Sub frmCreateView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = "Data Source=" + Module1.ServerName + ";User Id=" + Module1.Username + ";Password=" + Module1.Password
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from tabs"
        rs = cmd.ExecuteReader()
        While (rs.Read())
            ComboBox1.Items.Add(rs.GetValue(0).ToString())
        End While
        rs.Close()
        cmd.Dispose()
        cn.Close()
    End Sub
End Class