Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Tls

Public Class Form1

    Dim conn As New MySqlConnection("server=localhost;port=3306;username=root;password=;database=vb")
    Dim cmd As New MySqlCommand
    Dim dt As New DataTable
    Dim da As New MySqlDataAdapter
    Dim dr As MySqlDataReader
    Dim form2 As New Form2()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conn.Open()
            Dim query As String
            query = "INSERT INTO `clinicmanagementsystem`(`Name`, `Age` , `IC` , `Gender` , `Date`) VALUES (@Name,@Age,@IC,@Gender,@Date)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Name", txtName.Text)
            cmd.Parameters.AddWithValue("@Age", txtAge.Text)
            cmd.Parameters.AddWithValue("@IC", txtIC.Text)
            cmd.Parameters.AddWithValue("@Gender", ComboBox1.Text)
            cmd.Parameters.AddWithValue("@Date", CDate(DateTimePicker1.Text))
            cmd.ExecuteNonQuery()
            MsgBox("Successful Saved to Database", MsgBoxStyle.Information, "Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT * FROM ClinicManagementSystem WHERE ID =@ID"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", txtSelect.Text)
            dr = cmd.ExecuteReader
            If Not dr Is Nothing Then
                dr.Read()

                txtName.Text = dr("Name").ToString
                txtAge.Text = dr("Age").ToString
                txtIC.Text = dr("IC").ToString
                ComboBox1.Text = dr("Gender").ToString
                DateTimePicker1.Text = dr("Date").ToString

                dr.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
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

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        loadData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Delete From clinicmanagementsystem WHERE ID=@ID"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", txtSelect.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Record Deleted")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        loadData()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        form2.Show()
    End Sub

    Private Sub DoctorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DoctorToolStripMenuItem.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
