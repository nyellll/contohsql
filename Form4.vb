Imports MySql.Data.MySqlClient

Public Class Form4
    Dim conn As New MySqlConnection("server=localhost;port3306;username=root;password=;database=vb")
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Dim form2 As New Form2()

    Public Property SelectedID As String
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True

        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT * FROM clinicmanagementsystem WHERE IC=@IC"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@IC", SelectedID)
            dr = cmd.ExecuteReader

            If dr.Read() Then
                TextBox1.Text = dr("Name").ToString
                TextBox2.Text = dr("Age").ToString
                TextBox3.Text = dr("IC").ToString
                ComboBox1.Text = dr("Gender").ToString
                DateTimePicker1.Text = dr("Date").ToString
                TextBox4.Text = dr("Disease_Details").ToString
            Else
                MessageBox.Show("Sorry, No Record found")
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            Form1.Show()
        End Try
    End Sub
End Class