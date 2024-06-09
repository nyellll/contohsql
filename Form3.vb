Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class Form3
    Dim conn As New MySqlConnection("server=localhost;port=3306;username=root;password=;database=vb")
    Dim cmd As New MySqlCommand
    Dim dt As New DataTable
    Dim da As New MySqlDataAdapter
    Dim dr As MySqlDataReader
    Dim form2 As New Form2()

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadData()
        txtID.ReadOnly = True
        txtName.ReadOnly = True
    End Sub

    Sub loadData()
        Try
            conn.Open()
            With cmd
                .Connection = conn
                .CommandText = "SELECT * FROM ClinicManagementSystem"
            End With
            da.SelectCommand = cmd
            dt.Clear()
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Sub loadData2()
        Try
            With cmd
                .Connection = conn
                .CommandText = "SELECT * FROM ClinicManagementSystem"
            End With
            da.SelectCommand = cmd
            dt.Clear()
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub DoctorModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DoctorModuleToolStripMenuItem.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT * FROM ClinicManagementSystem WHERE ID=@ID"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", txtSelectID.Text)
            dr = cmd.ExecuteReader
            If Not dr Is Nothing Then
                dr.Read()

                txtID.Text = dr("ID").ToString
                txtName.Text = dr("Name").ToString
                txtDisease.Text = dr("Disease Details").ToString
                ComboBox1.Text = dr("Status").ToString

                dr.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            loadData2()

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conn.Open()
            Dim query As String
            query = "UPDATE ClinicManagementSystem set Disease_Details='" & txtDisease.Text & "',Status='" & ComboBox1.Text & "'where ID ='" & txtSelectID.Text & "'"
            Dim cmd As New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@txtDisease", txtName.Text)
            cmd.Parameters.AddWithValue("@Status", ComboBox1.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Successfully Updated", MsgBoxStyle.Information, "Success")

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            loadData2()
            conn.Close()

        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        loadData()
    End Sub
End Class