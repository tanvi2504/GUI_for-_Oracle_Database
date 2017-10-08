Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmSwitchSQL

    Dim cn As New SqlConnection
    Dim cmd As New SqlCommand

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            cn.ConnectionString = "Data Source=" + TextBox1.Text + ";Initial Catalog=" + TextBox2.Text + ";Integrated Security=True"
            cn.Open()
            cn.Close()
            GroupBox1.Enabled = True
        Catch ex As Exception
            MsgBox("Error in Connecting to Database", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            OpenFileDialog1.ShowDialog()
            RichTextBox1.Text = ""
            If (OpenFileDialog1.FileName <> "") Then
                TextBox3.Text = OpenFileDialog1.FileName
                RichTextBox1.LoadFile(TextBox3.Text, RichTextBoxStreamType.PlainText)
                Dim s As String
                s = ""
                Dim s1 As String
                Dim dind As Integer
                dind = 0
                Dim tot As String
                Dim rema As String
                rema = ""
                tot = ""
                s1 = ""
                tot = RichTextBox1.Text
                dind = tot.IndexOf("Data:")
                If (dind > 0) Then
                    s = tot.Substring(0, dind)
                    rema = tot.Substring(dind + 5)
                    s = s.Trim
                    rema = rema.Trim
                Else
                    s = tot
                End If
                ListBox1.Items.Clear()
                Dim reg As New Regex(",")
                For Each s1 In reg.Split(s)
                    s1 = s1.Trim()
                    If (s1 <> "") Then
                        ListBox1.Items.Add(s1)
                    End If
                Next
                If (ListBox1.Items.Count > 0) Then
                    Dim i As Integer
                    Dim reg2 As New Regex("-")
                    Dim str As String
                    Dim tabname As String
                    ListBox1.SelectedIndex = 0
                    str = ListBox1.SelectedItem.ToString()
                    s1 = ""
                    tabname = ""
                    For Each s1 In reg2.Split(str)
                        tabname = s1
                    Next
                    Dim fn As String
                    Dim dt As String
                    fn = ""
                    dt = ""
                    Dim fg As Integer
                    fg = 0
                    Dim qry As String
                    qry = "create table " + tabname + "("
                    For i = 1 To ListBox1.Items.Count - 1 Step 1
                        fn = ""
                        dt = ""
                        str = ""
                        fg = 0
                        ListBox1.SelectedIndex = i
                        str = ListBox1.SelectedItem.ToString()
                        For Each s1 In reg2.Split(str)
                            If (fg = 0) Then
                                fn = s1
                                fg = 1
                            Else
                                dt = s1
                                fg = 0
                            End If
                        Next
                        dt = dt.ToUpper
                        If (dt = "VARCHAR2") Or (dt = "VARCHAR") Or (dt = "DATE") Or (dt = "DATETIME") Then
                            dt = "VARCHAR(50)"
                        ElseIf (dt = "NUMBER") Or (dt = "NUMERIC") Or (dt = "DECIMAL") Then
                            dt = "NUMERIC"
                        End If
                        qry = qry + fn + " " + dt + ","
                    Next
                    qry = qry.Substring(0, qry.Length - 1)
                    qry = qry + ")"
                    'MsgBox(qry)
                    cn.ConnectionString = "Data Source=" + TextBox1.Text + ";Initial Catalog=" + TextBox2.Text + ";Integrated Security=True"
                    cn.Open()
                    cmd.Connection = cn
                    cmd.CommandText = qry
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    cn.Close()
                    If (dind > 0) Then
                        ListBox1.Items.Clear()
                        Dim dr As New Regex("-")
                        For Each s1 In dr.Split(rema)
                            s1 = Trim(s1)
                            If (s1 <> "") Then
                                s1 = s1.Replace("''", "NULL")
                                ListBox1.Items.Add(s1)
                            End If
                        Next
                        Dim tl As Integer
                        tl = ListBox1.Items.Count
                        Dim lp As Integer
                        lp = 0

                        For lp = 0 To tl - 1 Step 1
                            ListBox1.SelectedIndex = lp
                            qry = ListBox1.SelectedItem.ToString
                            qry = qry.Substring(0, qry.Length - 1)
                            cn.Open()
                            cmd.Connection = cn
                            cmd.CommandText = "insert into " + tabname + " values(" + qry + ")"
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            cn.Close()
                        Next
                    End If
                    MsgBox("Table Switched Successfully", MsgBoxStyle.Information, "Done")
                Else
                    MsgBox("Cannot Switch as File is Corrupted", MsgBoxStyle.Critical, "Error")
                End If
            End If
        Catch ex As Exception
            MsgBox("Error Raised : " + ex.Message.ToString(), MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub frmSwitchSQL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class