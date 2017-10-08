Imports MySql.Data.MySqlClient
Imports SpeechLib
Public Class mfrmJoins
    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim rs As MySqlDataReader
    Private Sub frmJoins_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cn.ConnectionString = "Server=" + mModule1.ServerName + ";Database=" + mModule1.DatabaseName + ";Uid=" + mModule1.Username + ";Pwd=" + mModule1.Password + ";"
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "SHOW TABLES"
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ComboBox1.Items.Add(rs.GetValue(0).ToString())
                ComboBox3.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox("Error in getting Tables from Database", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox1.Text + ""
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ComboBox2.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
            MsgBox("Error in getting Fields from Table", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        ComboBox4.Items.Clear()
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox3.Text + ""
            rs = cmd.ExecuteReader()
            While (rs.Read())
                ComboBox4.Items.Add(rs.GetValue(0).ToString())
            End While
            rs.Close()
            cmd.Dispose()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
            MsgBox("Error in getting Fields from Table", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox1.Text <> "") Then
            Dim Voice As New SpeechLib.SpVoice

            Voice.Speak(TextBox1.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            DataGridView1.Visible = True
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            cn.Open()
            Dim qry As String
            qry = ""
            If (RadioButton1.Checked = True) Then
                qry = "select * from " + ComboBox1.SelectedItem.ToString() + "," + ComboBox3.SelectedItem.ToString() + " where " + ComboBox1.SelectedItem.ToString() + "." + ComboBox2.SelectedItem.ToString() + "=" + ComboBox3.SelectedItem.ToString() + "." + ComboBox4.SelectedItem.ToString()
            ElseIf (RadioButton2.Checked = True) Then
                qry = "select * from " + ComboBox1.SelectedItem.ToString() + "," + ComboBox3.SelectedItem.ToString() + " where " + ComboBox1.SelectedItem.ToString() + "." + ComboBox2.SelectedItem.ToString() + "=" + ComboBox3.SelectedItem.ToString() + "." + ComboBox4.SelectedItem.ToString() + "(+)"
            ElseIf (RadioButton3.Checked = True) Then
                qry = "select * from " + ComboBox1.SelectedItem.ToString() + "," + ComboBox3.SelectedItem.ToString() + " where " + ComboBox1.SelectedItem.ToString() + "." + ComboBox2.SelectedItem.ToString() + "(+)=" + ComboBox3.SelectedItem.ToString() + "." + ComboBox4.SelectedItem.ToString()
            End If
            cmd = New MySqlCommand(qry, cn)
            Dim adp As New MySqlDataAdapter(cmd)
            'ds1.Tables.Remove(mModule1.TableName)
            Dim ds1 As New DataSet
            'ds1.Clear()
            DataGridView1.DataSource = Nothing
            ds1.Tables.Add(ComboBox1.SelectedItem.ToString())
            adp.Fill(ds1, ComboBox1.SelectedItem.ToString())
            Me.DataGridView1.DataSource = ds1
            Me.DataGridView1.DataMember = ComboBox1.SelectedItem.ToString()
            TextBox1.Text = qry
        Catch ex As Exception
            MsgBox("Error in getting Data from Table", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class